using MessageHub.MeltModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHub
{
    public static class ExtensionMethods
    {
        //Dictionary of tags used for getting heat number
        public static List<string> HeatTags = new List<string> {"<Heat>", "<BT_010_Heat>", "<HeatNumber>", "<Heat xmlns=\"\">" };
        public static List<string> HeatTagsEnd = new List<string> { "</Heat>", "</BT_010_Heat>", "</HeatNumber>"};

        
        //MELT
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.MeltModels.ToBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.ToBiz;

            generic.Id = message.ToBizId;
            generic.TargetSystemID = (int) message.DestinationId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            generic.TransactionID = message.TransactionId.ToString();
            generic.L4transactionId = message.L4transactionId;
            generic.ErrorDescription = null;
            generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.HeatNumber = message.HeatNumber;
            generic.OrderNumber = message.OrderNumber;  

            return generic;
        }

        //MELT
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.MeltModels.FromBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.FromBiz;

            generic.Id = message.FromBizId;
            generic.TargetSystemID = (int)message.SourceId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            generic.TransactionID = message.TransactionId.ToString(); 
            generic.L4transactionId = message.L4transactionId; 
            generic.ErrorDescription = null;
            //generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.HeatNumber = message.HeatNumber;
            generic.OrderNumber = message.OrderNumber;

            return generic;
        }

        //MELT
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.MeltModels.FromBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.MeltModels.FromBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }

        //MELT
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.MeltModels.ToBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.MeltModels.ToBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }


        //ESR
        public static TargetSystem GetTargetSystemESR(this ESRModels.BizCorrespondent message)
        {
            var generic = new TargetSystem();

            generic.TargetSystemID = message.BizCorrespondentId;
            generic.Name = message.Name;
            generic.Description = message.Description;

            return generic;
        }
        
        //ESR
        public static List<TargetSystem> GetInterfaceTargetESR(this List<ESRModels.BizCorrespondent> messages)
        {
            var list = new List<TargetSystem>();

            foreach (ESRModels.BizCorrespondent message in messages)
            {
                list.Add(message.GetTargetSystemESR());
            }

            return list;
        }

        //ESR
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.ESRModels.ToBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.ToBiz;

            generic.Id = message.ToBizId;
            generic.TargetSystemID = (int)message.DestinationId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            generic.TransactionID = message.TransactionId.ToString();
            generic.L4transactionId = "";
            generic.ErrorDescription = null;
            //generic.ResubmitUserId = message.ResubmitUseridId;
            generic.PrevErrorDescription = null;

            return generic;
        }

        //ESR
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.ESRModels.FromBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.FromBiz;

            generic.Id = message.FromBizId;
            generic.TargetSystemID = (int)message.SourceId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            generic.TransactionID = message.TransactionId.ToString();
            generic.L4transactionId = "";
            generic.ErrorDescription = message.ErrorDescription;
            //generic.ResubmitUserId = message.ResubmitUseridId;
            generic.PrevErrorDescription = null;


            return generic;
        }

        //ESR
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.ESRModels.FromBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.ESRModels.FromBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }

        //ESR
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.ESRModels.ToBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.ESRModels.ToBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }

        //NDT
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.NDTModels.ToBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.ToBiz;

            generic.Id = message.ToBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            generic.TransactionID = message.TransactionId.ToString(); 
            generic.L4transactionId = message.TransactionId.ToString();
            generic.ErrorDescription = message.ErrorDescription;
            generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s => message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }

            return generic;
        }

        //NDT
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.NDTModels.FromBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.FromBiz;

            generic.Id = message.FromBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            if (message.TransactionId == null)
            {
                generic.TransactionID = null;
                generic.L4transactionId = null;
            }
            else
            {
                generic.TransactionID = message.TransactionId.ToString();
                generic.L4transactionId = message.TransactionId.ToString();
            }
            generic.ErrorDescription = message.ErrorDescription;
            //generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s => message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }


            return generic;
        }

        //NDT
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.NDTModels.FromBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.NDTModels.FromBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }

        //NDT
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.NDTModels.ToBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.NDTModels.ToBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }

        //Bar Processing
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.BarProcessingModels.FromBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.FromBiz;

            generic.Id = message.FromBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            if (message.TransactionId == null)
            {
                generic.TransactionID = null;
                generic.L4transactionId = null;
            }
            else
            {
                generic.TransactionID = message.TransactionId.ToString();
                generic.L4transactionId = message.TransactionId.ToString();
            };
            generic.ErrorDescription = message.ErrorDescription;
            //generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s => message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }

            return generic;
        }

        //Bar Processing
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.BarProcessingModels.ToBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.ToBiz;

            generic.Id = message.ToBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            generic.TransactionID = message.TransactionId.ToString(); 
            generic.L4transactionId = message.TransactionId.ToString();
            generic.ErrorDescription = message.ErrorDescription;
            generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s => message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }

            return generic;
        }

        //Bar Processing
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.BarProcessingModels.FromBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.BarProcessingModels.FromBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }
        //Bar Processing
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.BarProcessingModels.ToBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.BarProcessingModels.ToBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }

        //4500 Ton Press
        public static MessageHub.MessageState GetMessageState_4500TonPress(this MessageHub._4500TonPressModels.MessageState message)
        {
            var generic = new MessageHub.MessageState();

            generic.MessageStateID = message.MessageStateId;
            generic.Name = message.Name;
            generic.Description = message.Description;

            return generic;
        }

        //4500 Ton Press
        public static List<MessageHub.MessageState> GetMessageState(this List<MessageHub._4500TonPressModels.MessageState> messages)
        {
            var list = new List<MessageHub.MessageState>();

            foreach (MessageHub._4500TonPressModels.MessageState message in messages)
            {
                list.Add(message.GetMessageState_4500TonPress());
            }

            return list;
        }
        //4500 Ton Press
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub._4500TonPressModels.FromBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.FromBiz;

            generic.Id = message.FromBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            if (message.TransactionId == null)
            {
                generic.TransactionID = null;
                generic.L4transactionId = null;
            }
            else
            {
                generic.TransactionID = message.TransactionId.ToString(); 
                generic.L4transactionId = message.TransactionId.ToString();
            }
            generic.ErrorDescription = message.ErrorDescription;
            //generic.ResubmitUserId = message.ResubmitUseridId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s => message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }

            return generic;
        }

        //4500 Ton Press
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub._4500TonPressModels.ToBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.ToBiz;

            generic.Id = message.ToBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            generic.TransactionID = message.TransactionId.ToString(); 
            generic.L4transactionId = message.TransactionId.ToString();
            generic.ErrorDescription = message.ErrorDescription;
            generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s => message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }

            return generic;
        }

        //4500 Ton Press
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub._4500TonPressModels.FromBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub._4500TonPressModels.FromBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }
        //4500 Ton Press
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub._4500TonPressModels.ToBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub._4500TonPressModels.ToBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }

        //Wet Grinder
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.WetGrinderModels.FromBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.FromBiz;

            generic.Id = message.FromBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            if (message.TransactionId == null)
            {
                generic.TransactionID = null;
                generic.L4transactionId = null;
            }
            else
            {
                generic.TransactionID = message.TransactionId.ToString();
                generic.L4transactionId = message.TransactionId.ToString();
            }
            generic.ErrorDescription = message.ErrorDescription;
            //generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s => message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }

            return generic;
        }

        //Wet Grinder
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.WetGrinderModels.ToBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.ToBiz;

            generic.Id = message.ToBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            generic.TransactionID = message.TransactionId.ToString();
            generic.L4transactionId = message.TransactionId.ToString();
            generic.ErrorDescription = message.ErrorDescription;
            generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s => message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }

            return generic;
        }

        //Wet Grinder
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.WetGrinderModels.FromBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.WetGrinderModels.FromBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }
        //Wet Grinder
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.WetGrinderModels.ToBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.WetGrinderModels.ToBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }

        //SX32
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.SX32Models.ToBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.ToBiz;

            generic.Id = message.ToBizId;
            generic.TargetSystemID = (int)message.FromBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            generic.TransactionID = message.TransactionId.ToString(); 
            generic.L4transactionId = "";
            generic.ErrorDescription = message.ErrorDescription;
            //generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s => message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }

            return generic;
        }

        //SX32
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.SX32Models.FromBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.FromBiz;

            generic.Id = message.FromBizId;
            generic.TargetSystemID = (int)message.ToBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            generic.TransactionID = message.FromBizId.ToString();
            generic.L4transactionId = "";
            generic.ErrorDescription = message.ErrorDescription;
            //generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s => message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }

            return generic;
        }

        //SX32
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.SX32Models.FromBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.SX32Models.FromBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }

        //SX32
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.SX32Models.ToBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.SX32Models.ToBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }


        //Vertical Annealing
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.VerticalAnnealingModels.ToBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.ToBiz;

            generic.Id = message.ToBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            generic.TransactionID = message.TransactionId.ToString(); 
            generic.L4transactionId = message.TransactionId.ToString();
            generic.ErrorDescription = message.ErrorDescription;
            generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s => message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }

            return generic;
        }

        //Vertical Annealing
        public static InterfaceMessageModel GetInterfaceMessageModel(this MessageHub.VerticalAnnealingModels.FromBiz message)
        {
            InterfaceMessageModel generic = new InterfaceMessageModel();

            generic.MessageSource = MessageSource.FromBiz;

            generic.Id = message.FromBizId;

            generic.MessageName = message.MessageName;
            generic.Message = message.Message;
            generic.MessageStateID = message.MessageStateId;
            generic.QueuedTime = message.QueuedTime;
            generic.ProcessedTime = message.ProcessedTime;
            if (message.TransactionId == null)
            {
                generic.TransactionID = null;
                generic.L4transactionId = null;
            }
            else
            {
                generic.TransactionID = message.TransactionId.ToString();
                generic.L4transactionId = message.TransactionId.ToString();
            } 
            generic.ErrorDescription = message.ErrorDescription;
            //generic.ResubmitUserId = message.ResubmittedByUserId;
            generic.PrevErrorDescription = null;

            generic.OrderNumber = message.OrderNumber;

            if (HeatTags.Any(s=>message.Message.Contains(s)))
            {
                string detectedStartTag = HeatTags.FirstOrDefault(s => message.Message.Contains(s));
                string detectedEndTag = HeatTagsEnd.FirstOrDefault(s => message.Message.Contains(s));

                int startIndex = message.Message.IndexOf(detectedStartTag) + detectedStartTag.Length;
                int endIndex = message.Message.IndexOf(detectedEndTag);

                if (startIndex < endIndex)
                {
                    generic.HeatNumber = message.Message.Substring(startIndex, endIndex - startIndex);
                }
            }
            else
            {
                generic.HeatNumber = null;
            }

            return generic;

        }

        //Vertical Annealing
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.VerticalAnnealingModels.FromBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.VerticalAnnealingModels.FromBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }

        //Vertical Annealing
        public static List<InterfaceMessageModel> GetInterfaceMessageModel(this List<MessageHub.VerticalAnnealingModels.ToBiz> messages)
        {
            var list = new List<InterfaceMessageModel>();

            foreach (MessageHub.VerticalAnnealingModels.ToBiz message in messages)
            {
                list.Add(message.GetInterfaceMessageModel());
            }

            return list;
        }

    }
}
