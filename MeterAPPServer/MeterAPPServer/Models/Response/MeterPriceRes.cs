using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAndroid.Models.Response;
namespace MeterAPPServer.Models.Response
{
    [Serializable]
    public class MeterPriceRes:BaseRes
    {
        public decimal TotleFee { get; set; }
        public decimal avgPrice { get; set; }
    }
}