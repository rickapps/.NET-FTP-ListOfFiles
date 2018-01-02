using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RickApps.ftpGetFiles
{
    public class Message
    {
        private int progressTotal;
        private int progressIncrement;
        private int progressCounter;

        public void ShowUsage(string header = null)
        {
            string appName = Path.GetFileName(Assembly.GetExecutingAssembly().CodeBase);
            if (!string.IsNullOrWhiteSpace(header))
            {
                Console.WriteLine(header);
            }
            Console.WriteLine($"Usage -- {appName} credential.txt files.txt [target directory] [-o]");
            Console.WriteLine("The app uses FTP to copy files from a remote server to a target directory on your local server.");
            Console.WriteLine("If no target directory is specified, downloaded files will be copied to the current folder.");
            Console.WriteLine("The file 'credential.txt' should contain four lines: ftp.remoteSite.com/username/password/source folder");
            Console.WriteLine("If the fourth line is missing, the ftp login folder will be the source folder for the remote site.");
            Console.WriteLine("The file names listed in 'files.txt' can contain relative paths.");
            Console.WriteLine("The paths are relative to the source folder on the remote site.");
            Console.WriteLine("-o Overwrites existing files in the target directory. Otherwise, existing files are skipped.");
            WaitForResponse();
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void InitProgressBar(int total, int increment)
        {
            progressCounter = 0;
            progressIncrement = increment;
            progressTotal = total;
        }

        public void ShowProgress()
        {
            progressCounter++;
            if (progressCounter == 1)
            {
                Console.WriteLine("Processing Files...");
            }
            else if (progressCounter % progressIncrement == 0 || progressCounter >= progressTotal)
            {
                Console.WriteLine($"{progressCounter} of {progressTotal} files processed...");
            }

        }

        public void WaitForResponse()
        {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
