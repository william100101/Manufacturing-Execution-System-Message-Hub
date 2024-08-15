using System;
using System.Collections.Generic;

namespace MessageHub
{
    public partial class InterfaceTarget
    {
        public string MessageName { get; set; }
        public int? TargetSystemID { get; set; }
        public bool AckStatusCapable { get; set; }
        public string Distribution { get; set; }
        public bool ErrorMessageCapable { get; set; }
    }
}
