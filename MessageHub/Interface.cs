using System;
using System.Collections.Generic;

namespace MessageHub
{
    public partial class Interface
    {
        public int Id { get; set; }
        public string MessageName { get; set; }
        public string Description { get; set; }
        public bool? FromBiz { get; set; }
        public bool AckStatusCapable { get; set; }

    }
}
