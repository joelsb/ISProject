using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Somiod.Models
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Somiod.Models")]
    public class Application
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime CreationDt { get; set; }
    }
}

