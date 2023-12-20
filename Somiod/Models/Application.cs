using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace Somiod.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDt { get; set; }
    }
}