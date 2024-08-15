using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESWebApplication.Models
{
    public class FTPDir
    {
        public string DirName { set; get; }

        public List<FTPDir> FtpDirs { set; get; }
        public List<string> Files { set; get; }

        public FTPDir()
        {
            DirName = "";
            FtpDirs = new List<FTPDir>();
            Files = new List<string>();
        }

    }
}
