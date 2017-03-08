using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAndroid.Models.Request;
namespace MeterAPPServer.Models.Request
{
    public class InvoiceReq: BaseRequest
    {
        public string invoiceNo { get; set; }
        public string readMeterId { get; set; }
    }
}