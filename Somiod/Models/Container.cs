﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Somiod.Models
{
    public class Container 
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDt { get; set; }
        public int Parent { get; set; }
    }
}