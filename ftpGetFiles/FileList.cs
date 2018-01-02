using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickApps.ftpGetFiles
{
    public class FileList
    {
        public FileList()
        {
            Transfers = new List<string>();
        }

        public bool ObtainTransferList(string file)
        {
            bool isSuccess = false;

            if (File.Exists(file))
            {
                StreamReader stream = File.OpenText(file);
                string transFile = stream.ReadLine();
                while (transFile != null)
                {
                    if (!string.IsNullOrWhiteSpace(transFile))
                    {
                        Transfers.Add(transFile);
                    }
                    transFile = stream.ReadLine();
                    isSuccess = true;
                }
            }

            return isSuccess;
        }

        public bool MakePath(string FullFileName)
        {
            // Get the file path
            string path = Path.GetDirectoryName(FullFileName);
            // Create the folder if needed
            Directory.CreateDirectory(path);
            return true;
        }

        public List<string> Transfers { get; private set; }

        public string TargetFolder { get; set; }
    }

}
