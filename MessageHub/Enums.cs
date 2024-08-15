using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHub
{
    public enum MessageStateEnum
    {
        Queued = 0,
        Locked = 1,
        Pending = 2,
        Success = 3,
        Failed = 4,
        Cancelled = 5
    }
}
