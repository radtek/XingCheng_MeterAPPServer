using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterAPPServer.Models.Entity
{
     [Serializable]
    public class ChargeInfoItem
    {
         public int ChargeCount{get;set;}
         public double ChargeFee { get; set; }
         public int ChargeWater { get; set; }
    }
}