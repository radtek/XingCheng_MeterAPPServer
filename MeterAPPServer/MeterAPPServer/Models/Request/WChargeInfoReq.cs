using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAndroid.Models.Request;

namespace MeterAPPServer.Models.Request
{
    public class WChargeInfoReq : BaseRequest
    {
        public string loginid { get; set; }
        public bool TJType { get; set; }
    }
}