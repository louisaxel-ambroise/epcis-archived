using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Xml;

namespace FasTnT.Web.EpcisServices
{

    public partial class EpcisSerializeContractBehaviorAttributeAttribute
    {
        public class EpcisSerializerOperationBehavior : DataContractSerializerOperationBehavior
        {
            public EpcisSerializerOperationBehavior(OperationDescription operation) : base(operation) { }
            public override XmlObjectSerializer CreateSerializer(Type type, string name, string ns, IList<Type> knownTypes)
            {
                return new EventListSerializer(type, base.CreateSerializer(type, name, ns, knownTypes));
            }

            public override XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes)
            {
                return new EventListSerializer(type, base.CreateSerializer(type, name, ns, knownTypes));
            }
        }
    }
}
