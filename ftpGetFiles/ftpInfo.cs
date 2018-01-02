using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickApps.ftpGetFiles
{
    public class FtpInfo
    {
        public string UserID { get; private set; }

        public string Password { get; private set; }

        public Uri RemoteSite { get; private set; }

        public Uri SourcePath { get; private set; }

        public bool ObtainCredentials(string credFile)
        {
            bool isSuccess = false;

            if (File.Exists(credFile))
            {
                StreamReader source = File.OpenText(credFile);
                string ftpSite = source.ReadLine();
                UserID = source.ReadLine();
                Password = source.ReadLine();
                string sourceFolder = source.ReadLine();
                RemoteSite = new UriBuilder("ftp", ftpSite).Uri;
                if (string.IsNullOrWhiteSpace(sourceFolder))
                {
                    SourcePath = RemoteSite;
                }
                else
                {
                    SourcePath = new Uri(RemoteSite, sourceFolder);
                }
                isSuccess = true;
            }

            return isSuccess;
        }
    }
}
