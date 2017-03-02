using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Epcis.WebApi
{
    public class CustomXmlFormatter : MediaTypeFormatter
    {
        public override bool CanReadType(Type type)
        {
            return type == typeof(XDocument);
        }

        public override bool CanWriteType(Type type)
        {
            return type == typeof (XDocument);
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            using (var reader = new StreamReader(readStream))
            {
                var body = await reader.ReadToEndAsync();

                return XDocument.Parse(body);
            }
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            return Task.Run(() =>
            {
                using (var writer = new XmlTextWriter(writeStream, Encoding.UTF8))
                {
                    ((XDocument) value).WriteTo(writer);
                }
            });
        }
    }
}