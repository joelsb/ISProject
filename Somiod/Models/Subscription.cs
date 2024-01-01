using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;
using System.Xml;

namespace Somiod.Models
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Somiod.Models")]
    public class Subscription
    {
        

        [DataMember]
        public string Event { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime CreationDt { get; set; }

        [DataMember]
        public int Parent { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Endpoint { get; set; }
    }
}