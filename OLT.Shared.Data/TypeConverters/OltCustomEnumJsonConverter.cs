using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace OLT.Core
{
    // https://github.com/dotnet/aspnetcore/issues/4008
    [Obsolete("Moved to OltEnumRouteConverter in OLT.Common.Json.Newtonsoft")]
    [ExcludeFromCodeCoverage]
    public class OltCustomEnumJsonConverter<T> : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var s = value as string;
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<T>(@"""" + value.ToString() + @"""");
        }
    }
}
