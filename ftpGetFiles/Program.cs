using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RickApps.ftpGetFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            FtpInfo remote = new FtpInfo();
            FileList downloads = new FileList();
            Message errors = new Message();

            string targetFolder = string.Empty;
            bool overwrite = false;

            // Is there an overwrite option?
            if (args.Length > 2)
            {
                if (args.Length == 4)
                {
                    if (args[3].Equals("-o", StringComparison.CurrentCultureIgnoreCase))
                    {
                        overwrite = true;
                        targetFolder = args[2];
                    }
                    else
                    {
                        errors.ShowUsage($"ERROR -- Invalid option. Valid options are: -o (Overwrite existing files)");
                    }
                }
                else if (args[2]. Equals("-o", StringComparison.CurrentCultureIgnoreCase))
                {
                    overwrite = true;
                    targetFolder = Directory.GetCurrentDirectory();
                }
                else
                {
                    targetFolder = args[2];
                }
            }
            else if (args.Length == 2)
            {
                targetFolder = Directory.GetCurrentDirectory();
            }
            else
            {
                errors.ShowUsage();
                return;
            }

            // Check that our credentials are good.
            if (!remote.ObtainCredentials(args[0]))
            {
                errors.ShowUsage($"ERROR -- Cannot open the credentials file: {Path.GetFullPath(args[0])}");
                return;
            }

            // Get our list of files to download
            if (!downloads.ObtainTransferList(args[1]))
            {
                errors.ShowUsage($"ERROR -- Cannot open the download file: {Path.GetFullPath(args[1])}");
                return;
            }

            // Check if our target directory is any good
            try
            {
                Directory.CreateDirectory(targetFolder);
            }
            catch (Exception e)
            {
                errors.ShowUsage($"ERROR -- Invalid target directory: {targetFolder}");
                Environment.Exit(0);
            }

            // Validity checks passed.
            using (WebClient ftpClient = new WebClient())
            {
                ftpClient.Credentials = new System.Net.NetworkCredential(remote.UserID, remote.Password);

                errors.InitProgressBar(downloads.Transfers.Count, 5);
                foreach (string file in downloads.Transfers)
                {
                    string targetPath = Path.Combine(targetFolder, file);
                    string ftpPath = new Uri(remote.SourcePath, file).ToString();
                    try
                    {
                        // Make sure our target folder exists.
                        if (downloads.MakePath(targetPath))
                        {
                            if (overwrite || !File.Exists(targetPath))
                            {
                                ftpClient.DownloadFile(ftpPath, targetPath);
                            }
                            else
                            {
                                errors.ShowMessage($"Skipping {targetPath} -- already exists");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        errors.ShowMessage($"Could not download {file} : Msg is {e.Message}");
                    }
                    errors.ShowProgress();
                }
            }
            errors.WaitForResponse();
        }
    }
}
