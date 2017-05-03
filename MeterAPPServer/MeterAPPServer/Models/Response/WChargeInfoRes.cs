using MeterAPPServer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAndroid.Models.Response;

namespace MeterAPPServer.Models.Response
{
    public class WChargeInfoRes : BaseRes
    {
       // public List<ChargeInfoItem> chargeItem { get; set; }
        public int ChargeCount { get; set; }
        public double ChargeFee { get; set; }
        public int ChargeWater { get; set; }
       
    }
}