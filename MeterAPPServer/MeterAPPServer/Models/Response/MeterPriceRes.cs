using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAndroid.Models.Response;
using MeterAPPServer.Models.Entity;
namespace MeterAPPServer.Models.Response
{
    [Serializable]
    public class MeterPriceRes:BaseRes
    {
        public decimal TotleFee { get; set; }
        public decimal avgPrice { get; set; }
        public string calcProc { get; set; }

        public FeeInfo step1 { get; set; }
        public FeeInfo step2 { get; set; }
        public FeeInfo step3 { get; set; }
    }
}