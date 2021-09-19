using System.Collections.Generic;
using System.Linq;

namespace OLT.Libraries.UnitTest.OLT.Shared
{
    public class HelperToDelimitedString
    {
        public HelperToDelimitedString(string delimiter, bool insertSpaces, string qualifier, params string[] values)
        {
            Values = values.ToList();
            Delimiter = delimiter;
            InsertSpaces = insertSpaces;
            Qualifier = qualifier;
        }

        public List<string> Values { get; }
        public string Delimiter { get; }
        public bool InsertSpaces { get; }
        public string Qualifier { get; }

        public string Expected
        {
            get
            {
                var formattedList = new List<string>();
                Values.ForEach(val =>
                {
                    var formatted = Qualifier == string.Empty ? val : string.Format("{1}{0}{1}", val, Qualifier);
                    formattedList.Add(formatted);
                });


                var delimiter = InsertSpaces ? $"{Delimiter} " : Delimiter;

                return string.Join(delimiter, formattedList);
            }
        }
    }
}