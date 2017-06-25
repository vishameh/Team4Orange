using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManager.Models
{
    public class UserViewModel
    {
        public int user_ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string phoneHome { get; set; }
        public string phoneCell { get; set; }
        public string phoneOffice { get; set; }
        public string foodPref { get; set; }
        public string companyName { get; set; }
        public string branch { get; set; }
        public string additionalInfo { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public string address { get; set; }
    }
}