using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageHub;

namespace MESWebApplication.Models
{
    public class MessageTypeModel
    {
        public string Name { get; set; }
        public int State { get; set; }
        public MessageSource Source { get; set; }
    }
}
