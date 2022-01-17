using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Dtos
{
    public class GenericResp
    {
        public bool Status { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}