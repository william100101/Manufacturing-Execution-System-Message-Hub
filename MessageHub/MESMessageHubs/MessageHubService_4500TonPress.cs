using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using System.Net;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using MessageHub._4500TonPressModels;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;


namespace MessageHub
{
    public class MessageHubService_4500TonPress
    {
        private IDbContextFactory<_4500TonPressContext> _contextFactory_4500TonPress { get; set; }

        public MasterDataModel MasterData { get; set; }

        public MessageHubService_4500TonPress(IDbContextFactory<_4500TonPressContext> contextFactory)
        {
            _contextFactory_4500TonPress = contextFactory;
            MasterData = GetMasterData_4500TonPress();
        }

        private MasterDataModel GetMasterData_4500TonPress()
        {
            var masterData = new MasterDataModel();
            masterData.MessageStates = GetMessageStates_4500TonPress();

            masterData.Interfaces = GetInterfaces_4500TonPress();

            return masterData;
        }


        private List<TargetSystem> GetTargetSystems_4500TonPress()
        {

            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {

                var distinctRowsToBiz = context.ToBizs
                    .GroupBy(row => row.MessageName)
                    .Select(group => group.First())
                    .ToList();
                var distinctRowsFromBiz = context.FromBizs
                    .GroupBy(row => row.MessageName)
                    .Select(group => group.First())
                    .ToList();
                var targetSystemsToBiz = distinctRowsToBiz
                .Select(x => new TargetSystem
                {
                    TargetSystemID = 0,
                    Name = x.MessageName,
                    Description = x.MessageName
                });
                var targetSystemsFromBiz = distinctRowsFromBiz
                .Select(x => new TargetSystem
                {
                    TargetSystemID = 0,
                    Name = x.MessageName,
                    Description = x.MessageName
                });
                return targetSystemsFromBiz.Concat(targetSystemsToBiz).ToList();
            }
        }

        private List<MessageState> GetMessageStates_4500TonPress() 
        {
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
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

        //running a like check instead of equality to cover different lock messages
        public List<_4500TonPressModels.ToBiz> GetLatestToBiz(string messageName, DateTime StartDate, DateTime EndDate, string searchCondition = "")
        {
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {
                string sql = "";

                if (!string.IsNullOrEmpty(searchCondition)) 
                {
                    sql = $"SELECT * FROM ToBiz WHERE MessageName LIKE '%{messageName}%' AND CONVERT(date, QueuedTime) >= '{StartDate.ToString("yyyy-MM-dd")}' AND CONVERT(date, QueuedTime) <= '{EndDate.ToString("yyyy-MM-dd")}' AND CONVERT(NVARCHAR(MAX), Message) LIKE '%{searchCondition}%' ORDER BY ToBizId DESC";
                }
                else
                {
                    sql = $"SELECT * FROM ToBiz WHERE MessageName LIKE '%{messageName}%' AND CONVERT(date, QueuedTime) >= '{StartDate.ToString("yyyy-MM-dd")}' AND CONVERT(date, QueuedTime) <= '{EndDate.ToString("yyyy-MM-dd")}' ORDER BY ToBizId DESC";
                }

                return context.ToBizs.FromSqlRaw(sql).ToList();
            }
        }


        //running a like check instead of equality to cover different lock messages
        public List<_4500TonPressModels.FromBiz> GetLatestFromBiz(string messageName, DateTime StartDate, DateTime EndDate, string searchCondition = "")
        {
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {
                string sql = "";

                if (!string.IsNullOrEmpty(searchCondition)) 
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

            using (var context = _contextFactory_4500TonPress.CreateDbContext())
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

        private List<InterfaceTarget> GetInterfaceTargets_4500TonPress()
        {
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {
                var distinctRowsToBiz = context.ToBizs
                    .GroupBy(row => row.MessageName)
                    .Select(group => group.First())
                    .ToList();
                var distinctRowsFromBiz = context.FromBizs
                    .GroupBy(row => row.MessageName)
                    .Select(group => group.First())
                    .ToList();
                var interfaceTargetListFromBizs = distinctRowsFromBiz
                    .Where(x => x.TransactionId != null)
                    .Select(x => new InterfaceTarget
                    {
                        MessageName = x.MessageName,
                        Distribution = "WChoi@cartech.com",
                        ErrorMessageCapable = true,
                        TargetSystemID = x.FromBizId,
                        AckStatusCapable = false
                    });

                var interfaceTargetListToBizs = distinctRowsToBiz
                    .Where(x => x.TransactionId != null)
                    .Select(x => new InterfaceTarget
                    {
                        MessageName = x.MessageName,
                        Distribution = "WChoi@cartech.com",
                        ErrorMessageCapable = true,
                        TargetSystemID = x.FromBizId,
                        AckStatusCapable = true
                    });
                return interfaceTargetListToBizs.Concat(interfaceTargetListFromBizs).ToList();
            }
        }


        public List<_4500TonPressModels.FromBiz> GetAcksReading(List<string> transactionIds)
        {
            for (int i = 0; i < transactionIds.Count - 1; i++)
            {
                transactionIds[i] = transactionIds[i].TrimStart('0');
            }

            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {
                var query = from x in context.FromBizs
                            where x.MessageName == "MessageAcknowledgement"
                            select x;
                return query.ToList();
            }
        }

        public List<string> GetDistinctMessageNamesFromBizOnly()
        {
            List<string> distinctMessageNames = new List<string>();
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {
                distinctMessageNames = context.ToBizs.FromSqlRaw("Select Distinct MessageName from Frombiz (nolock)").Select(x => x.MessageName).ToList();
            }
            return distinctMessageNames;
        }

        public List<_4500TonPressModels.FromBiz> GetAcknowledgementsFromBizOnly()
        {
            List<_4500TonPressModels.FromBiz> acknowledgements = new List<_4500TonPressModels.FromBiz>();
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {
                var query = from x in context.FromBizs
                            where x.MessageName == "MessageAcknowledgement"
                            select x;
                return query.ToList();
            }
        }

        public List<_4500TonPressModels.FromBiz> GetStatusesFromBizOnly()
        {
            List<_4500TonPressModels.FromBiz> acknowledgements = new List<_4500TonPressModels.FromBiz>();
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {
                var query = from x in context.FromBizs
                            where x.MessageName == "MessageStatus"
                            select x;
                return query.ToList();
            }
        }

        public List<_4500TonPressModels.FromBiz> GetStatusesReading(List<string> transactionIds)
        {
            for (int i = 0; i < transactionIds.Count - 1; i++)
            {
                transactionIds[i] = transactionIds[i].TrimStart('0');
            }

            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {
                var query = from x in context.FromBizs
                            where x.MessageName == "MessageStatus"
                            select x;
                return query.ToList();
            }
        }
        public bool ResetFromBizStatus(int FromBizId, string ResubmitUserId, string PrevErrorDescription)
        {
            bool rc = false;
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
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
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {
                var record = context.ToBizs.Where(x => x.ToBizId == ToBizId).FirstOrDefault();
                record.MessageStateId = 0;
                record.ResubmittedByUserId = ResubmitUserId;
                record.ErrorDescription = PrevErrorDescription;
                int modifyCount = context.SaveChanges();
                rc = modifyCount > 0;
            }
            return rc;
        }

        public _4500TonPressModels.FromBiz GetFromBizById(int Id)
        {
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {
                var query = from x in context.FromBizs
                            where x.FromBizId == Id
                            select x;
                return query.FirstOrDefault();
            }
        }

        public _4500TonPressModels.ToBiz GetToBizById(int Id)
        {
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
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
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {

                var query = from x in context.FromBizs
                            where x.FromBizId == message.Id
                            select x;
                var existing = query.First();

                existing.Message = newXML;
                existing.MessageStateId = (int)MessageStateEnum.Queued;
                existing.ErrorDescription = "";

                int rc = context.SaveChanges();
                saved = rc > 0;
            }
            return saved;
        }

        private bool EditOriginalToBiz(InterfaceMessageModel message, string newXML)
        {
            bool saved = false;
            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {

                var query = from x in context.ToBizs
                            where x.ToBizId == message.Id
                            select x;
                var existing = query.First();

                existing.Message = newXML;
                existing.MessageStateId = (int)MessageStateEnum.Queued;
                existing.ErrorDescription = "";
                existing.ResubmittedByUserId = message.ResubmitUserId;


                int rc = context.SaveChanges();
                saved = rc > 0;
            }
            return saved;
        }

        public List<Interface> GetInterfaces_4500TonPress() // different getter than other apps due to inconsistent naming for orderlock in ToBiz
        {
            List<Interface> interfaceLists = new List<Interface>();

            List<string> FromBizNames = new List<string>();
            List<string> ToBizNames = new List<string>();


            using (var context = _contextFactory_4500TonPress.CreateDbContext())
            {
                FromBizNames = context.FromBizs
                                         .Select(t => t.MessageName)
                                         .Distinct()
                                         .ToList();            }

            for (int i = 0; i < FromBizNames.Count; i++)
            {
                Interface newInterface = new Interface();
                newInterface.Description = FromBizNames[i];
                newInterface.FromBiz = true;
                newInterface.MessageName = FromBizNames[i];
                interfaceLists.Add(newInterface);
            }


            Interface OrderLockCmd = new Interface();
            OrderLockCmd.Description = "OrderLockCmd";
            OrderLockCmd.FromBiz = false;
            OrderLockCmd.MessageName = "OrderLockCmd";
            interfaceLists.Add(OrderLockCmd);

            return interfaceLists;
        }
        public List<AckStatusReciver> GetStatusAndAck(int id)
        {
            string sql = @"SELECT t.ToBizID, convert(xml, t.message) as ToBizMessage,
                            (SELECT top 1 FromBizID from frombiz where TransactionID=t.TransactionID and messagename='MessageStatus' order by processedtime desc) as StatusMessageFromBizID,
                            (SELECT top 1 convert(xml, message) from frombiz where TransactionID=t.TransactionID and messagename='MessageStatus' order by processedtime desc) as StatusMessage,
                            (SELECT top 1 FromBizID from frombiz where TransactionID=t.TransactionID and messagename='MessageStatus' order by processedtime desc) as AckMessageFromBizID,
                            (SELECT top 1 convert(xml, message) from frombiz where TransactionID=t.TransactionID and messagename='MessageAcknowledgement' order by processedtime desc) as AckMessage
 
                            FROM ToBiz t
 
                            where tobizid=@id";

            using (var connection = _contextFactory_4500TonPress.CreateDbContext().Database.GetDbConnection())
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
