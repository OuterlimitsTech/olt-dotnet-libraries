using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace OLT.Core
{
    // https://github.com/dotnet/aspnetcore/issues/4008
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
