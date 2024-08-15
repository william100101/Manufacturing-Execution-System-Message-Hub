﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MessageHub._4500TonPressModels
{
    public partial class _4500TonPressContext : DbContext
    {
        public _4500TonPressContext()
        {
        }

        public _4500TonPressContext(DbContextOptions<_4500TonPressContext> options)
            : base(options)
        {
            this.Database.SetCommandTimeout(TimeSpan.FromSeconds(60 * 6));
        }

        public virtual DbSet<FromBiz> FromBizs { get; set; }
        public virtual DbSet<MessageState> MessageStates { get; set; }
        public virtual DbSet<ToBiz> ToBizs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FromBiz>(entity =>
            {
                entity.ToTable("FromBiz", tb => tb.HasTrigger("trg_dboFromBiz_LastUpdate"));

                entity.HasIndex(e => e.MessageStateId, "FkIdx_DboFromBiz_MessageStateID");

                entity.Property(e => e.FromBizId)
                    .HasColumnName("FromBizID")
                    .HasComment("The Identity/Primary Key column for table dbo.FromBiz");

                entity.Property(e => e.AckReceived).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("_CreatedBy")
                    .HasDefaultValueSql("((((suser_name()+' ')+host_name())+'//')+app_name())");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("_CreatedOn")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.ErrorDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Optional Description value for this record");

                entity.Property(e => e.LastUpdateBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("_LastUpdateBy")
                    .HasDefaultValueSql("((((suser_name()+' ')+host_name())+'//')+app_name())");

                entity.Property(e => e.LastUpdateOn)
                    .HasColumnName("_LastUpdateOn")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("xml");

                entity.Property(e => e.MessageName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MessageStateId)
                    .HasColumnName("MessageStateID")
                    .HasComment("Referential Foreign Key pointer to Schema.Table.Column: dbo.MessageState.MessageStateID");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessedTime).HasColumnType("datetime");

                entity.Property(e => e.QueuedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Response).HasDefaultValueSql("((0))");

                entity.Property(e => e.ToBizId)
                    .HasColumnName("ToBizID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TransactionID");

                entity.HasOne(d => d.MessageState)
                    .WithMany(p => p.FromBizs)
                    .HasForeignKey(d => d.MessageStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DboMessageState_DboFromBiz_MessageStateID");
            });

            modelBuilder.Entity<MessageState>(entity =>
            {
                entity.ToTable("MessageState");

                entity.Property(e => e.MessageStateId)
                    .ValueGeneratedNever()
                    .HasColumnName("MessageStateID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("_CreatedBy")
                    .HasDefaultValueSql("((((suser_name()+' ')+host_name())+'//')+app_name())");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("_CreatedOn")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Description value for this record");

                entity.Property(e => e.LastUpdateBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("_LastUpdateBy")
                    .HasDefaultValueSql("((((suser_name()+' ')+host_name())+'//')+app_name())");

                entity.Property(e => e.LastUpdateOn)
                    .HasColumnName("_LastUpdateOn")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ToBiz>(entity =>
            {
                entity.ToTable("ToBiz", tb => tb.HasTrigger("trg_dboToBiz_LastUpdate"));

                entity.HasIndex(e => e.MessageStateId, "FkIdx_DboToBiz_MessageStateID");

                entity.Property(e => e.ToBizId)
                    .HasColumnName("ToBizID")
                    .HasComment("The Identity/Primary Key column for table dbo.ToBiz");

                entity.Property(e => e.Ackreceived).HasColumnName("ACKReceived");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("_CreatedBy")
                    .HasDefaultValueSql("((((suser_name()+' ')+host_name())+'//')+app_name())");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("_CreatedOn")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.ErrorDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("Optional Description value for this record");

                entity.Property(e => e.FromBizId).HasColumnName("FromBizID");

                entity.Property(e => e.LastUpdateBy)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("_LastUpdateBy")
                    .HasDefaultValueSql("((((suser_name()+' ')+host_name())+'//')+app_name())");

                entity.Property(e => e.LastUpdateOn)
                    .HasColumnName("_LastUpdateOn")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("xml");

                entity.Property(e => e.MessageFailedInMf).HasColumnName("MessageFailedInMF");

                entity.Property(e => e.MessageName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MessageStateId)
                    .HasColumnName("MessageStateID")
                    .HasComment("Referential Foreign Key pointer to Schema.Table.Column: dbo.MessageState.MessageStateID");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessedTime).HasColumnType("datetime");

                entity.Property(e => e.QueuedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Response).HasDefaultValueSql("((0))");

                entity.Property(e => e.ResubmittedByUserId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StatusReceived).HasDefaultValueSql("((0))");

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TransactionID");

                entity.HasOne(d => d.MessageState)
                    .WithMany(p => p.ToBizs)
                    .HasForeignKey(d => d.MessageStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DboMessageState_DboToBiz_MessageStateID");
            });

            modelBuilder.HasSequence<int>("TO_BIZ_TRANSACTIONID")
                .HasMin(0)
                .HasMax(999999);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}