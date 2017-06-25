using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Models
{
    public class EventViewModel
    {
        public string title { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string type { get; set; }
        public string logoPath { get; set; }
        public string description { get; set; }
        public System.DateTime startDateTime { get; set; }
        public System.DateTime endDateTime { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public string address { get; set; }
    }
}