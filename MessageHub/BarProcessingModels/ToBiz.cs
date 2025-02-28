﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MessageHub.BarProcessingModels
{
    public partial class ToBiz
    {
        public int ToBizId { get; set; }
        public string MessageName { get; set; }
        public string Message { get; set; }
        public int MessageStateId { get; set; }
        public string TransactionId { get; set; }
        public DateTime QueuedTime { get; set; }
        public DateTime? ProcessedTime { get; set; }
        public int? FromBizId { get; set; }
        public string ErrorDescription { get; set; }
        public string OrderNumber { get; set; }
        public bool AlertSent { get; set; }
        public bool? Response { get; set; }
        public bool Ackreceived { get; set; }
        public bool? StatusReceived { get; set; }
        public bool? MessageFailedInMf { get; set; }
        public string ResubmitUseridId { get; set; }
        public int? BundleId { get; set; }
        public string ItemNumber { get; set; }
        public string HeatNumber { get; set; }
        public string IngotNumber { get; set; }
        public string LotNumber { get; set; }
        public string LineId { get; set; }
        public string ResubmittedByUserId { get; set; }

        public virtual MessageState MessageState { get; set; }
    }
}