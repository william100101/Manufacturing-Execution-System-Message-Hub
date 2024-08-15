using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;

namespace MESWebApplication
{
    public static class ADSecurity
    {
        public static bool IsInGroup(this ClaimsPrincipal User, string GroupName)
        {
            var groups = new List<string>();

            var wi = (WindowsIdentity)User.Identity;
            if (wi.Groups != null)
            {
                foreach (var group in wi.Groups)
                {
                    try
                    {
                        groups.Add(group.Translate(typeof(NTAccount)).ToString().ToUpper());
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
                bool returnValue = false;
                returnValue = groups.Contains(GroupName.ToUpper());
                return returnValue;
            }
            return false;
        }
    }

}

