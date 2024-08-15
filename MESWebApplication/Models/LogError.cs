using System;
using System.Collections.Generic;

namespace MESWebApplication.Models
{
    public class LogError
    {
        public string FileName { get; set; }
        public string Thread { get; set; }
        public List<string> Lines { get; set; }
        public DateTime TimeStamp { get; set; }
        public String MessageNames { get; set; }
        public bool Complete { get; set; }
        public LogError()
        {
            Complete = false;
            Thread = "";
            MessageNames = "Unknown";
            Lines = new List<string>();
        }
    }
}
