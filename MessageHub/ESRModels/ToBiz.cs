﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MessageHub.ESRModels
{
    public partial class ToBiz
    {
        public int ToBizId { get; set; }
        public int? DestinationId { get; set; }
        public string MessageName { get; set; }
        public string Message { get; set; }
        public int MessageStateId { get; set; }
        public DateTime QueuedTime { get; set; }
        public DateTime? ProcessedTime { get; set; }
        public int? TransactionId { get; set; }

        public virtual BizCorrespondent Destination { get; set; }
        public virtual MessageState MessageState { get; set; }
    }
}