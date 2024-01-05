using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Somiod.Models
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Somiod.Models")]
    public class Data
    {
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime CreationDt { get; set; }
        [DataMember]
        public int Parent { get; set; }
    }
}