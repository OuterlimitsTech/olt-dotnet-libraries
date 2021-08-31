using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class GenericExtensions
    {
        public static async Task ForEachAsync<T>(this List<T> list, Func<T, Task> func)
        {
            foreach (var value in list)
            {
                await func(value);
            }
        }


        /// <summary>
        /// Copies matching properies from one object to another
        /// </summary>
        public static T CopyFrom<T>(this T toObject, object fromObject) where T : class
        {
            var fromObjectType = fromObject.GetType();

            foreach (PropertyInfo toProperty in toObject.GetType().GetProperties())
            {
                PropertyInfo fromProperty = fromObjectType.GetProperty(toProperty.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (fromProperty != null) // match found
                {
                    // check types
                    var fromType = Nullable.GetUnderlyingType(fromProperty.PropertyType) ?? fromProperty.PropertyType;
                    var toType = Nullable.GetUnderlyingType(toProperty.PropertyType) ?? toProperty.PropertyType;

                    if (toType.IsAssignableFrom(fromType) && toProperty.CanWrite)
                    {
                        toProperty.SetValue(toObject, fromProperty.GetValue(fromObject, null), null);
                    }
                }
            }

            return toObject;
        }



        public static void Cleanup(this System.IO.DirectoryInfo obj, DateTime olderThan, bool deleteSubDirectories)
        {
            foreach (var fileInfo in obj.GetFiles().Where(p => p.CreationTime <= olderThan).ToList())
            {
                if (fileInfo.CreationTime <= olderThan)
                {
                    fileInfo.Delete();
                }
            }

            foreach (var dir in obj.GetDirectories())
            {
                if (deleteSubDirectories)
                {
                    if (dir.CreationTime <= olderThan)
                    {
                        dir.Delete(true);
                    }
                }
                else
                {
                    dir.Cleanup(olderThan, false);
                }
            }
        }

        public static void Remove<T>(this ICollection<T> @this, Func<T, bool> func)
        {
            @this.ToList().ForEach(x =>
            {
                if (func(x))
                {
                    @this.Remove(x);
                }
            });
        }

        /// <summary>
        /// Performs a ForEach on the collection and executes the passed <see cref="Action"/> on each item.
        /// </summary>
        /// <typeparam name="TItem">The <see cref="Type"/> of the items in the collection.</typeparam>
        /// <param name="values">The current collection of values.</param>
        /// <param name="eachAction">The <see cref="Action"/> that will executed against each item in the collection.</param>
        /// <returns>The original collection.</returns>
        [DebuggerStepThrough]
        public static IEnumerable<TItem> Each<TItem>(this IEnumerable<TItem> values, Action<TItem> eachAction)
        {

            if (values == null) throw new ArgumentNullException(nameof(values));

            var items = values.ToList();
            foreach (var item in items)
            {
                eachAction(item);
            }

            return items;
        }


        ///<summary>
        /// Checks to see if the collection contains a specific item.  
        ///</summary>
        ///<remarks>
        /// The test is performed by the <see cref="Predicate{T}"/> passed as a parameter.
        /// </remarks>
        ///<param name="values">Extends <see cref="IEnumerable{T}"/>.</param>
        ///<param name="predicate">The <see cref="Predicate{T}"/> that should return <c>true</c> if the item is a match; otherwise <c>false</c>.</param>
        ///<typeparam name="TItem">The <c>type</c> of the items in the colllection.</typeparam>
        ///<returns><c>true</c> if the collection contains an item that satisfies the <see cref="Predicate{T}"/>; otherwise <c>false</c>.</returns>
        public static bool Contains<TItem>(this IEnumerable<TItem> values, Predicate<TItem> predicate)
        {
            return values.Any(item => predicate(item));
        }

        /// <summary>
        /// Creates a DataTable with a column of Type T, and populates it with the items in the Enumeration.
        /// </summary>
        ///<typeparam name="T">The <c>type</c> of the items in the colllection.</typeparam>
        /// <param name="list">Extends <see cref="IEnumerable{T}"/>.</param>
        /// <returns>A datatable pupulated from this.</returns>
        public static DataTable AsDataTable<T>(this IEnumerable<T> list)
        {
            var dataTable = new DataTable();
            try
            {
                dataTable.Columns.Add(null, typeof(T));

                foreach (var item in list)
                    dataTable.Rows.Add(new object[] { item });


                dataTable.AcceptChanges();
            }
            catch (Exception)
            {
                dataTable.Dispose();
                throw;
            }

            return dataTable;
        }

        /// <summary>
        /// Creates a DataTable with a column of Type T, and populates it with the items in the Enumeration.
        /// </summary>
        ///<typeparam name="T">The <c>type</c> of the items in the colllection.</typeparam>
        /// <param name="list">Extends <see cref="IEnumerable{T}"/>.</param>
        /// <returns>A datatable pupulated from this.</returns>
        public static DataTable AsDataTableForPublicProperties<T>(this IEnumerable<T> list)
        {
            var listEnumerator = list.GetEnumerator();
            listEnumerator.MoveNext();
            var firstItem = listEnumerator.Current;
            var propertiesDictionary = firstItem.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(firstItem, null));
            var columnList = new string[propertiesDictionary.Keys.Count];
            propertiesDictionary.Keys.CopyTo(columnList, 0);

            return list.AsDataTableForPublicProperties(columnList);

        }

        // ReSharper disable MethodOverloadWithOptionalParameter
        /// <summary>
        /// Creates a DataTable with a column of Type T, and populates it with the items in the Enumeration.
        /// </summary>
        ///<typeparam name="T">The <c>type</c> of the items in the colllection.</typeparam>
        /// <param name="list">Extends <see cref="IEnumerable{T}"/>.</param>
        /// <param name="columnList">The list of columns and properties to Use.</param>
        /// <returns>A datatable pupulated from this.</returns>
        public static DataTable AsDataTableForPublicProperties<T>(this IEnumerable<T> list, params string[] columnList)
        {
            var listEnumerator = list.GetEnumerator();
            listEnumerator.MoveNext();
            var firstItem = listEnumerator.Current;
            var propertiesDictionary = firstItem.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(firstItem, null));

            var dataTable = new DataTable();
            try
            {
                if (columnList != null)
                {
                    foreach (var key in columnList)
                        dataTable.Columns.Add(key, propertiesDictionary[key].GetType());

                    foreach (var item in list)
                    {
                        propertiesDictionary = item.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(item, null));
                        var dataRowToAdd = new object[propertiesDictionary.Keys.Count];
                        for (var i = 0; i < propertiesDictionary.Keys.Count; i++)
                            dataRowToAdd[i] = propertiesDictionary[columnList[i]];
                        dataTable.Rows.Add(dataRowToAdd);
                    }

                    dataTable.AcceptChanges();
                }
            }
            catch (Exception)
            {
                dataTable.Dispose();
                throw;
            }

            return dataTable;
        }

        public static string ToHex(this System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public static string ToRGB(this System.Drawing.Color c)
        {
            return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }

    }




}

