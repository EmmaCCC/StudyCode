using System;
using System.Linq;
using Newtonsoft.Json.Converters;

namespace WebApi.File.Extensions
{
    public class DateTimeFormatConverter : IsoDateTimeConverter
    {
        private string[] _readDateTimeFormats = new string[] { "yyyy-MM-dd" };

        public string[] ReadDateTimeFormats
        {
            get
            {
                return this._readDateTimeFormats;
            }
            set
            {
                this._readDateTimeFormats = value;
            }
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            string str = reader.Value.ToString();
            if (objectType == typeof(DateTimeOffset))
            {
                return (object)DateTimeOffset.ParseExact(str, this._readDateTimeFormats, (IFormatProvider)this.Culture, this.DateTimeStyles);
            }
            if (objectType == typeof(DateTime))
            {
                return (object)DateTime.ParseExact(str, this._readDateTimeFormats, (IFormatProvider)this.Culture, this.DateTimeStyles);
            }
            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}