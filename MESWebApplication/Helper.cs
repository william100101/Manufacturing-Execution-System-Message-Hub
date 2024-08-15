using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageHub;
using MESWebApplication.Models;
//   test commit using u12567
namespace MESWebApplication
{
    public static class Helper
    {
        public static string DecodeMessageName(List<Interface> interfaces, string MessageName)
        {
            string rc = MessageName;
            var x = interfaces.Where(x => x.MessageName == MessageName).FirstOrDefault();
            if (x != null)
            {
                rc = x.Description;
            }

            return rc;
        }

        public static string GetTargetSystem(List<TargetSystem> TargetSystems, int TargetSystemId)
        {
            string rc = "";

            var target = TargetSystems.Where(x => x.TargetSystemID == TargetSystemId).FirstOrDefault();
            if (target != null)
            {
                rc = target.Description;
            }            
            return rc;
        }

        public static bool ReSendReading(MessageHubServiceMelt messageHubService, InterfaceMessageModel message)
        {
            bool rc = false;
            switch (message.MessageSource)
            {
                case MessageSource.FromBiz:
                    rc = messageHubService.ResetFromBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                case MessageSource.ToBiz:
                    rc = messageHubService.ResetToBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                default:
                    break;
            }

            return rc;
        }
        public static bool ReSendReading(MessageHubServiceVerticalAnnealing messageHubService, InterfaceMessageModel message)
        {
            bool rc = false;
            switch (message.MessageSource)
            {
                case MessageSource.FromBiz:
                    rc = messageHubService.ResetFromBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                case MessageSource.ToBiz:
                    rc = messageHubService.ResetToBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                default:
                    break;
            }

            return rc;
        }
        public static bool ReSendReading(MessageHubServiceESR messageHubService, InterfaceMessageModel message)
        {
            bool rc = false;
            switch (message.MessageSource)
            {
                case MessageSource.FromBiz:
                    rc = messageHubService.ResetFromBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                case MessageSource.ToBiz:
                    rc = messageHubService.ResetToBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                default:
                    break;
            }

            return rc;
        }
        public static bool ReSendReading(MessageHubServiceWetGrinder messageHubService, InterfaceMessageModel message)
        {
            bool rc = false;
            switch (message.MessageSource)
            {
                case MessageSource.FromBiz:
                    rc = messageHubService.ResetFromBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                case MessageSource.ToBiz:
                    rc = messageHubService.ResetToBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                default:
                    break;
            }

            return rc;
        }
        public static bool ReSendReading(MessageHubServiceBarProcessing messageHubService, InterfaceMessageModel message)
        {
            bool rc = false;
            switch (message.MessageSource)
            {
                case MessageSource.FromBiz:
                    rc = messageHubService.ResetFromBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                case MessageSource.ToBiz:
                    rc = messageHubService.ResetToBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                default:
                    break;
            }

            return rc;
        }
        public static bool ReSendReading(MessageHubServiceNDT messageHubService, InterfaceMessageModel message)
        {
            bool rc = false;
            switch (message.MessageSource)
            {
                case MessageSource.FromBiz: 
                    rc = messageHubService.ResetFromBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                case MessageSource.ToBiz:
                    rc = messageHubService.ResetToBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                default:
                    break;
            }

            return rc;
        }

        public static bool ReSendReading(MessageHubService_4500TonPress messageHubService, InterfaceMessageModel message)
        {
            bool rc = false;
            switch (message.MessageSource)
            {
                case MessageSource.FromBiz: 
                    rc = messageHubService.ResetFromBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                case MessageSource.ToBiz:
                    rc = messageHubService.ResetToBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                default:
                    break;
            }

            return rc;
        }
        public static bool ReSendReading(MessageHubServiceSX32 messageHubService, InterfaceMessageModel message)
        {
            bool rc = false;
            switch (message.MessageSource)
            {
                case MessageSource.FromBiz:
                    rc = messageHubService.ResetFromBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                case MessageSource.ToBiz:
                    rc = messageHubService.ResetToBizStatus(message.Id, message.ResubmitUserId, message.ErrorDescription);
                    break;
                default:
                    break;
            }

            return rc;
        }
    }
}
