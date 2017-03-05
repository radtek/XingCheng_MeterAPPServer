using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterAPPServer.Models.Entity
{
    [Serializable]
    public class FeeInfo
    {
        public decimal avgPrice { get; set; }
        public decimal fee { get; set; }
        public decimal waterNum { get; set; } 
    }
}