﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MessageHub._4500TonPressModels
{
    public partial class FromBiz
    {
        /// <summary>
        /// The Identity/Primary Key column for table dbo.FromBiz
        /// </summary>
        public int FromBizId { get; set; }
        public string MessageName { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// Referential Foreign Key pointer to Schema.Table.Column: dbo.MessageState.MessageStateID
        /// </summary>
        public int MessageStateId { get; set; }
        public DateTime QueuedTime { get; set; }
        public DateTime? ProcessedTime { get; set; }
        /// <summary>
        /// Optional Description value for this record
        /// </summary>
        public string ErrorDescription { get; set; }
        public string OrderNumber { get; set; }
        public bool AlertSent { get; set; }
        public string TransactionId { get; set; }
        public int? ToBizId { get; set; }
        public bool? AckReceived { get; set; }
        public bool? Response { get; set; }
        public bool? StatusReceived { get; set; }
        public DateTimeOffset LastUpdateOn { get; set; }
        public string LastUpdateBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public virtual MessageState MessageState { get; set; }
    }
}