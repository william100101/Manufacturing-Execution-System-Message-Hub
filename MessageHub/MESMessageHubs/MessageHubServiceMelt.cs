using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using System.Net;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

using MessageHub.MeltModels;
using MessageHub._4500TonPressModels;
using MessageHub.ESRModels;
using MessageHub.NDTModels;
using MessageHub.SX32Models;
using MessageHub.VerticalAnnealingModels;
using MessageHub.WetGrinderModels;

using System.Linq.Expressions;
using System.Net.NetworkInformation;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;


namespace MessageHub
{
    public class MessageHubServiceMelt
    {            
        private IDbContextFactory<MeltContext> _contextFactoryMelt { get; set; }
        public MasterDataModel MasterData { get; set; }

        public MessageHubServiceMelt(IDbContextFactory<MeltContext> contextFactory, IDbContextFactory<_4500TonPressContext> contextFactory_4500TonPress)
        {
            _contextFactoryMelt = contextFactory;
            MasterData = GetMasterDataMelt();
        }
        public MasterDataModel GetMasterDataMelt()
        {
            var masterData = new MasterDataModel();
            masterData.MessageStates = GetMessageStatesMelt();

            masterData.Interfaces = GetInterfacesMelt(); 

            return masterData;
        }


        private List<MessageState> GetMessageStatesMelt() 
        {
            using (var context = _contextFactoryMelt.CreateDbContext())
            {
                var MessageStates = context.MessageStates.
                    Select(x => new MessageState
                    {
                        MessageStateID = x.MessageStateId,
                        Name = x.Name,
                        Description = x.Description
                    });
                return MessageStates.ToList();
            }
        }

        public List<Interface> GetInterfacesMelt()
        {


            List<Interface> interfaceLists = new List<Interface>();

            List<string> FromBizNames = new List<string>();
            List<string> ToBizNames = new List<string>();


            
            using (var context = _contextFactoryMelt.CreateDbContext())
            {
                FromBizNames = context.VMsgHubMessageTypesFromBizs
                                         .Select(t => t.MessageName)
                                         .Distinct()
                                         .ToList();
                ToBizNames = context.VMsgHubMessageTypesToBizs
                                         .Select(t => t.MessageName)
                                         .Distinct()
                                         .ToList();
            }

            for (int i = 0; i < FromBizNames.Count; i++)
            {
                Interface newInterface = new Interface();
                newInterface.Description = FromBizNames[i];
                newInterface.FromBiz = true;
                newInterface.MessageName = FromBizNames[i];
                newInterface.AckStatusCapable = false;
                interfaceLists.Add(newInterface);
            }

            for (int i = 0; i < ToBizNames.Count; i++)
            {
                Interface newInterface = new Interface();
                newInterface.Description = ToBizNames[i];
                newInterface.FromBiz = false;
                newInterface.MessageName = ToBizNames[i];
                newInterface.AckStatusCapable = true;
                interfaceLists.Add(newInterface);
            }

            return interfaceLists;

        }

        public List<string> GetInterfaceStrings()
        {
            List<string> FromBizNames = new List<string>();
            List<string> ToBizNames = new List<string>();

            using (var context = _contextFactoryMelt.CreateDbContext())
            {
                FromBizNames = context.VMsgHubMessageTypesFromBizs
                                         .Select(t => t.MessageName)
                                         .Distinct()
                                         .ToList();
                ToBizNames = context.VMsgHubMessageTypesToBizs
                                         .Select(t => t.MessageName)
                                         .Distinct()
                                         .ToList();
            }

            return FromBizNames.Concat(ToBizNames).ToList();
        }

        public List<MeltModels.ToBiz> GetLatestToBiz(string messageName, DateTime StartDate, DateTime EndDate, string searchCondition = "")
        {
            using (var context = _contextFactoryMelt.CreateDbContext())
            {
                string sql = "";

                if (!string.IsNullOrEmpty(searchCondition)) //when search condition is not empty
                {
                    sql = $"SELECT * FROM ToBiz WHERE MessageName = '{messageName}' AND CONVERT(date, QueuedTime) >= '{StartDate.ToString("yyyy-MM-dd")}' AND CONVERT(date, QueuedTime) <= '{EndDate.ToString("yyyy-MM-dd")}' AND CONVERT(NVARCHAR(MAX), Message) LIKE '%{searchCondition}%' ORDER BY ToBizId DESC";
                }
                else
                {
                    sql = $"SELECT * FROM ToBiz WHERE MessageName = '{messageName}' AND CONVERT(date, QueuedTime) >= '{StartDate.ToString("yyyy-MM-dd")}' AND CONVERT(date, QueuedTime) <= '{EndDate.ToString("yyyy-MM-dd")}' ORDER BY ToBizId DESC";
                }

                return context.ToBizs.FromSqlRaw(sql).ToList();
            }
        }



        public List<MeltModels.FromBiz> GetLatestFromBiz(string messageName, DateTime StartDate, DateTime EndDate, string searchCondition = "")
        {
            using (var context = _contextFactoryMelt.CreateDbContext())
            {
                string sql = "";
                if (!string.IsNullOrEmpty(searchCondition)) //when search condition is not empty
                {
                    sql = $"SELECT * FROM FromBiz WHERE MessageName = '{messageName}' AND CONVERT(date, QueuedTime) >= '{StartDate.ToString("yyyy-MM-dd")}' AND CONVERT(date, QueuedTime) <= '{EndDate.ToString("yyyy-MM-dd")}' AND CONVERT(NVARCHAR(MAX), Message) LIKE '%{searchCondition}%' ORDER BY FromBizId DESC";
                }
                else
                {
                    sql = $"SELECT * FROM FromBiz WHERE MessageName = '{messageName}' AND CONVERT(date, QueuedTime) >= '{StartDate.ToString("yyyy-MM-dd")}' AND CONVERT(date, QueuedTime) <= '{EndDate.ToString("yyyy-MM-dd")}' ORDER BY FromBizId DESC";
                }

                return context.FromBizs.FromSqlRaw(sql).ToList();
            }
        }
        public List<InterfaceMessageModel> GetLatestBoth(DateTime startTime, DateTime endTime)
        {
            string sql = "";
            List<InterfaceMessageModel> FromBizMessages;
            List<InterfaceMessageModel> ToBizMessages;

            using (var context = _contextFactoryMelt.CreateDbContext())
            {

                //FromBiz query
                sql = $"SELECT * FROM FromBiz WHERE CONVERT(date, QueuedTime) >= '{startTime.ToString("yyyy-MM-dd")}' AND CONVERT(date, QueuedTime) <= '{endTime.ToString("yyyy-MM-dd")}' ORDER BY FromBizId DESC";
                FromBizMessages = context.FromBizs.FromSqlRaw(sql).ToList().GetInterfaceMessageModel();

                //ToBiz query
                sql = $"SELECT * FROM ToBiz WHERE CONVERT(date, QueuedTime) >= '{startTime.ToString("yyyy-MM-dd")}' AND CONVERT(date, QueuedTime) <= '{endTime.ToString("yyyy-MM-dd")}' ORDER BY ToBizId DESC";
                ToBizMessages = context.ToBizs.FromSqlRaw(sql).ToList().GetInterfaceMessageModel();

                return FromBizMessages.Concat(ToBizMessages).ToList();
            }
        }

        public List<MeltModels.FromBiz> GetAcksReading(List<string> transactionIds)
        {
            for (int i = 0; i < transactionIds.Count - 1; i++)
            {
                transactionIds[i] = transactionIds[i].TrimStart('0');
            }

            using (var context = _contextFactoryMelt.CreateDbContext())
            {
                var query = from x in context.FromBizs
                            where x.MessageName == "MessageAcknowledgement" && transactionIds.Contains(x.L4transactionId)
                            select x;
                return query.ToList();
            }
        }

        public List<MeltModels.FromBiz> GetStatusesReading(List<string> transactionIds)
        {
            for (int i = 0; i < transactionIds.Count - 1; i++)
            {
                transactionIds[i] = transactionIds[i].TrimStart('0');
            }

            using (var context = _contextFactoryMelt.CreateDbContext())
            {
                var query = from x in context.FromBizs
                            where x.MessageName == "MessageStatus" && transactionIds.Contains(x.L4transactionId)
                            select x;
                return query.ToList();
            }
        }
        public bool ResetFromBizStatus(int FromBizId, string ResubmitUserId, string PrevErrorDescription)
        {
            bool rc = false;
            using (var context = _contextFactoryMelt.CreateDbContext())
            {
                var record = context.FromBizs.Where(x => x.FromBizId == FromBizId).FirstOrDefault();
                record.MessageStateId = 0;
                //record.ResubmittedByUserId = ResubmitUserId;
                record.ErrorDescription = PrevErrorDescription;
                int modifyCount = context.SaveChanges();
                rc = modifyCount > 0;
            }
            return rc;
        }
        public bool ResetToBizStatus(int ToBizId, string ResubmitUserId, string PrevErrorDescription)
        {
            bool rc = false;
            using (var context = _contextFactoryMelt.CreateDbContext())
            {
                var record = context.ToBizs.Where(x => x.ToBizId == ToBizId).FirstOrDefault();
                record.MessageStateId = 0; 
                record.ResubmittedByUserId = ResubmitUserId;
                int modifyCount = context.SaveChanges();
                rc = modifyCount > 0;
            }
            return rc;
        }
        public MeltModels.FromBiz GetFromBizById(int Id)
        {
            using (var context = _contextFactoryMelt.CreateDbContext())
            {
                var query = from x in context.FromBizs
                            where x.FromBizId == Id
                            select x;
                return query.FirstOrDefault();
            }
        }
        public MeltModels.ToBiz GetToBizById(int Id)
        {
            using (var context = _contextFactoryMelt.CreateDbContext())
            {
                var query = from x in context.ToBizs
                            where x.ToBizId == Id
                            select x;
                return query.FirstOrDefault();
            }
        }
        public InterfaceMessageModel SubmitModifiedReading(InterfaceMessageModel message, string newXML)
        {
            InterfaceMessageModel msg = null;
            if (message != null)
            {
                bool rc = false;
                if (message.MessageSource == MessageSource.FromBiz)
                {
                    rc = EditOriginalFromBiz(message, newXML);
                    msg = GetFromBizById(message.Id).GetInterfaceMessageModel();
                }

                if (message.MessageSource == MessageSource.ToBiz)
                {
                    rc = EditOriginalToBiz(message, newXML);
                    msg = GetToBizById(message.Id).GetInterfaceMessageModel();
                }
            }

            return msg;
        }
        private bool EditOriginalFromBiz(InterfaceMessageModel message, string newXML)
        {
            bool saved = false;
            using (var context = _contextFactoryMelt.CreateDbContext())
            {

                var query = from x in context.FromBizs
                            where x.FromBizId == message.Id
                            select x;
                var existing = query.First();

                existing.Message = newXML;
                existing.MessageStateId = (int)MessageStateEnum.Queued;
                existing.ErrorDescription = "";
                //existing.ResubmittedByUserId = message.ResubmitUserId;

                int rc = context.SaveChanges();
                saved = rc > 0;
            }
            return saved;
        }
        private bool EditOriginalToBiz(InterfaceMessageModel message, string newXML)
        {
            bool saved = false;
            using (var context = _contextFactoryMelt.CreateDbContext())
            {

                var query = from x in context.ToBizs
                            where x.ToBizId == message.Id
                            select x;
                var existing = query.First();

                existing.Message = newXML;
                existing.MessageStateId = (int)MessageStateEnum.Queued;
                //existing.ErrorDescription = "";
                existing.ResubmittedByUserId = message.ResubmitUserId;

                int rc = context.SaveChanges();
                saved = rc > 0;
            }
            return saved;
        }

        public List<AckStatusReciver> GetStatusAndAck(int id)
        {
            string sql = @"SELECT t.ToBizID, convert(xml, t.message) as ToBizMessage,
                            (SELECT top 1 FromBizID from frombiz where l4transactionid=t.l4transactionid and messagename='MessageStatus' order by processedtime desc) as StatusMessageFromBizID,
                            (SELECT top 1 convert(xml, message) from frombiz where l4transactionid=t.l4transactionid and messagename='MessageStatus' order by processedtime desc) as StatusMessage,
                            (SELECT top 1 FromBizID from frombiz where l4transactionid=t.l4transactionid and messagename='MessageStatus' order by processedtime desc) as AckMessageFromBizID,
                            (SELECT top 1 convert(xml, message) from frombiz where l4transactionid=t.l4transactionid and messagename='MessageAcknowledgement' order by processedtime desc) as AckMessage
 
                            FROM ToBiz t
 
                            where tobizid=@id";

            

            using (var connection = _contextFactoryMelt.CreateDbContext().Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Parameters.Add(new SqlParameter("@id", id));

                    var ackStatusRecivers = new List<AckStatusReciver>();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ackStatusReciver = new AckStatusReciver
                            {
                                ToBizID = reader.GetInt32(reader.GetOrdinal("ToBizID")),
                                ToBizMessage = reader.GetString(reader.GetOrdinal("ToBizMessage")),
                                StatusMessageFromBizID = reader.IsDBNull(reader.GetOrdinal("StatusMessageFromBizID")) ? 0 : reader.GetInt32(reader.GetOrdinal("StatusMessageFromBizID")),
                                StatusMessage = reader.IsDBNull(reader.GetOrdinal("StatusMessage")) ? null : reader.GetString(reader.GetOrdinal("StatusMessage")),
                                AckMessageFromBizID = reader.IsDBNull(reader.GetOrdinal("AckMessageFromBizID")) ? 0 : reader.GetInt32(reader.GetOrdinal("AckMessageFromBizID")),
                                AckMessage = reader.IsDBNull(reader.GetOrdinal("AckMessage")) ? null : reader.GetString(reader.GetOrdinal("AckMessage"))
                            };

                            ackStatusRecivers.Add(ackStatusReciver);
                        }
                    }

                    return ackStatusRecivers;
                }
            }
        }
    }
}
