using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;

namespace MESWebApplication.Authorization
{
	public static class Security
	{
		public static bool IsInGroup(ClaimsPrincipal User, string GroupName)
		{
			var groups = new List<string>();
			bool check = false;
			var user = (WindowsIdentity)User.Identity;
			if (user.Groups != null)
			{
				foreach (var group in user.Groups)
				{
					string gname = group.Translate(typeof(NTAccount)).ToString();
					gname = gname.Replace("CTNTMASTER\\", "");
					check = gname.ToUpper().Contains(GroupName.ToUpper());
					if (check)
						break;
				}

				foreach (string groupName in groups)
				{
					string s = groupName;
					Debug.WriteLine(s);
				}
			}
			return check;
		}
	}
}
