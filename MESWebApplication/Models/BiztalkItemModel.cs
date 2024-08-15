using System;
using System.Management;

namespace MESWebApplication.Models
{
    public class BiztalkItemModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public BiztalkItemType ItemType { get; set; }
        public ManagementObject ManagementObject { get; set; }

    }

    public enum BiztalkItemType
    {
        MSBTS_SendPort,
        MSBTS_ReceiveLocation
    }

}
