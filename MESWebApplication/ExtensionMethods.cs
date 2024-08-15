using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Text;
using MessageHub;
using MESWebApplication.Models;

namespace MESWebApplication
{
    public static class ExtensionMethods
    {
        public static string FormatXML(this string xml)
        {
            // I want indented xml so it looks nice for reading on a webpage.
            string rc = xml;
            if (xml.Length > 0)
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
                settings.NewLineChars = "\r\n";

                settings.NewLineHandling = NewLineHandling.Replace;
                StringBuilder sb = new StringBuilder();
                using (XmlWriter writer = XmlWriter.Create(sb, settings))
                {
                    doc.Save(writer);
                }
                rc = sb.ToString();
            }
            return rc;
        }

        public static bool TargetResponseExpected(this List<InterfaceTarget> InterfaceTargets, string MessageName)
        {
            bool rc = false;

            var interfaceTarget = InterfaceTargets.Where(x => x.MessageName == MessageName).FirstOrDefault();


            if (interfaceTarget != null) { 
            rc = interfaceTarget.AckStatusCapable;
            }
            
            return rc;
        }

        public static bool TargetResponseExpected(this List<Interface> Interfaces, string MessageName)
        {
            bool rc = false;

            var interfaceMatch = Interfaces.Where(x => x.MessageName == MessageName).FirstOrDefault();


            if (interfaceMatch != null)
            {
                rc = interfaceMatch.AckStatusCapable;
            }

            return rc;
        }

        public static int GetMessageNameCount(this List<InterfaceMessageModel> messages, string messageName)
        {
            int rc = 0;

            rc = messages.Where(x => x.MessageName == messageName).Count();

            return rc;
        }
        public static int GetMessageNameStatusCount(this List<InterfaceMessageModel> messages, string messageName, int messageStateID)
        {
            int rc = 0;

            rc = messages.Where(x => x.MessageName == messageName && x.MessageStateID == messageStateID).Count();

            return rc;
        }
        public static List<int> GetTargets(this List<InterfaceMessageModel> messages, string messageName)
        {
            var list = messages.Where(x => x.MessageName == messageName).Select(x => x.TargetSystemID).ToList();
            var distinctList = list.Distinct().ToList();
            return distinctList;
        }
        public static int GetTargetCount(this List<InterfaceMessageModel> messages, string messageName, int TargetSystemId)
        {
            return messages.Where(x => x.MessageName == messageName && x.TargetSystemID == TargetSystemId).Count();
        }
        public static bool HasValue(this string value)
        {
            return (value != null && value.Trim() != "");
        }
        public static string JustDate(this DateTime value)
        {
            return value.ToString("M/dd/yyyy");
        }
        public static string IndentXML(this string xml)
        {
            // I want indented xml so it looks nice for reading on a webpage.
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.NewLineChars = "\r\n";

            settings.NewLineHandling = NewLineHandling.Replace;
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }
        public static string FormatDate(this DateTime? value)
        {            
            string rc = "";
            if (value != null)
            {
                rc = FormatDate((DateTime) value);
            }
            return rc;
        }
        public static string FormatDate(this DateTime value)
        {
            string rc = "";
            if (value != null)
            {
                rc = ((DateTime)value).ToString("M/dd/yy   hh:mm:ss tt");
            }
            return rc;
        }        
    }
}
