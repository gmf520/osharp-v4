using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace OSharp.Web.Http.Formattings
{
    public class CsvMediaTypeFormatter : MediaTypeFormatter
    {
        public CsvMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public CsvMediaTypeFormatter(MediaTypeMapping mediaTypeMapping)
            : this()
        {
            MediaTypeMappings.Add(mediaTypeMapping);
        }

        public CsvMediaTypeFormatter(IEnumerable<MediaTypeMapping> mediaTypeMappings)
            : this()
        {
            foreach (var mediaTypeMapping in mediaTypeMappings)
                MediaTypeMappings.Add(mediaTypeMapping);
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return IsTypeOfIEnumerable(type);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
        {
            return Task.Factory.StartNew(() => WriteStream(type, value, stream));
        }

        //private utils

        private static void WriteStream(Type type, object value, Stream stream)
        {
            //NOTE: We have check the type inside CanWriteType method
            //If request comes this far, the type is IEnumerable. We are safe.

            Type itemType = type.GetGenericArguments()[0];

            using (StringWriter stringWriter = new StringWriter())
            {
                stringWriter.WriteLine(
                    string.Join<string>(
                        ",", itemType.GetProperties().Select(x => x.Name)
                    )
                );

                foreach (var obj in (IEnumerable<object>)value)
                {
                    object obj1 = obj;
                    var vals = obj.GetType().GetProperties().Select(
                        pi => new
                        {
                            Value = pi.GetValue(obj1, null)
                        }
                    );

                    string valueLine = string.Empty;

                    foreach (var item in vals)
                    {
                        string val = item.Value.ToString();
                        if (item.Value != null)
                        {
                            //Check if the value contans a comma and place it in quotes if so
                            if (val.Contains(","))
                                val = string.Concat("\"", val, "\"");

                            //Replace any \r or \n special characters from a new line with a space
                            if (val.Contains("\r"))
                                val = val.Replace("\r", " ");
                            if (val.Contains("\n"))
                                val = val.Replace("\n", " ");

                            valueLine = string.Concat(valueLine, val, ",");

                        }
                        else
                        {
                            valueLine = string.Concat(string.Empty, ",");
                        }
                    }

                    stringWriter.WriteLine(valueLine.TrimEnd(','));
                }

                using (var streamWriter = new StreamWriter(stream))
                    streamWriter.Write(stringWriter.ToString());
            }
        }

        private static bool IsTypeOfIEnumerable(Type type)
        {
            return type.GetInterfaces().Any(interfaceType => interfaceType == typeof(IEnumerable));
        }
    }
}
