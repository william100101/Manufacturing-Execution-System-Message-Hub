using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageHub;

namespace MessageHub
{
    public class MasterDataModel
    {
        public List<TargetSystem> TargetSystems { get; set; }
        public List<MessageState> MessageStates { get; set; }
        public List<InterfaceTarget> InterfaceTargets { get; set; }
        public List<Interface> Interfaces { get; set; }
    }
}
