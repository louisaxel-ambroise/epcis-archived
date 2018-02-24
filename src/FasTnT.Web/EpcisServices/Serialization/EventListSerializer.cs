using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace FasTnT.Web.EpcisServices
{

    public partial class EpcisSerializeContractBehaviorAttributeAttribute
    {
        public class EventListSerializer : XmlObjectSerializer
        {
            const string EventList = "eventList";
            Type type;
            bool isCustomSerialization;
            XmlObjectSerializer fallbackSerializer;
            public EventListSerializer(Type type, XmlObjectSerializer fallbackSerializer)
            {
                this.type = type;
                isCustomSerialization = typeof(ICustomSerializable).IsAssignableFrom(type);
                this.fallbackSerializer = fallbackSerializer;
            }

            public override bool IsStartObject(XmlDictionaryReader reader)
            {
                if (isCustomSerialization)
                {
                    return reader.LocalName == EventList;
                }
                else
                {
                    return fallbackSerializer.IsStartObject(reader);
                }
            }

            public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
            {
                if (isCustomSerialization)
                {
                    object result = Activator.CreateInstance(type);
                    MemoryStream ms = new MemoryStream(reader.ReadElementContentAsBase64());
                    ((ICustomSerializable)result).InitializeFrom(ms);
                    return result;
                }
                else
                {
                    return fallbackSerializer.ReadObject(reader, verifyObjectName);
                }
            }

            public override void WriteEndObject(XmlDictionaryWriter writer)
            {
                if (isCustomSerialization)
                {
                    writer.WriteEndElement();
                }
                else
                {
                    fallbackSerializer.WriteEndObject(writer);
                }
            }

            public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
            {
                if (isCustomSerialization)
                {
                    MemoryStream ms = new MemoryStream();
                    ((ICustomSerializable)graph).WriteTo(ms);
                    char[] bytes = Encoding.UTF8.GetString(ms.ToArray()).ToCharArray();
                    writer.WriteRaw(bytes, 0, bytes.Length);
                }
                else
                {
                    fallbackSerializer.WriteObjectContent(writer, graph);
                }
            }

            public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
            {
                if (isCustomSerialization)
                {
                    writer.WriteStartElement(EventList);
                }
                else
                {
                    fallbackSerializer.WriteStartObject(writer, graph);
                }
            }
        }
    }
}
