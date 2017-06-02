using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paperwall.Api.Models
{
    public class ReceivedMessageModel
    {
        
        public string messageText { get; set; } 
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double precision { get; set; }
        public string writerIdentification { get; set; }

    }
}