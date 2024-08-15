using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#nullable enable

namespace MessageHub
{
    public class InterfaceMessageModel
    {
        public MessageSource MessageSource { get; set; }
        public int Id { get; set; }
        public string MessageName { get; set; }
        public string Message { get; set; }
        public int MessageStateID { get; set; }
        public DateTime QueuedTime { get; set; }
        public DateTime? ProcessedTime { get; set; }
        public string ErrorDescription { get; set; }
        public string PrevErrorDescription { get; set; }
        public string? TransactionID { get; set; }
        public string? L4transactionId { get; set; }
        public int TargetSystemID { get; set; }
        public string ResubmitUserId { get; set; }
        public string? HeatNumber { get; set; }
        public string? OrderNumber { get; set; }

    }

    public enum MessageSource
    { 
        ToBiz,
        FromBiz
    }




}
