using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAndroid.Models.Request;
namespace MeterAPPServer.Models.Request
{
    public class MeterPriceReq:BaseRequest
    {
        public string readMeterRecordId { get; set; }
        public string waterUserId { get; set; }
        public decimal currData { get; set; }
    }
}