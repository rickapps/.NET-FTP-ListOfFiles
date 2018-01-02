
# .NET-FTP-ListOfFiles
FTP a list of files from a remote machine to local machine
Visual Studio 2017 Console Application in C# using .NET Framework 4.52

Obtain only the files and images you need from any ftp source such as an old website you are porting.
Inputs: credentials.txt - a three line file containing the ftp server, username, password

        transfers.txt - list one file per line to transfer from remote server to local machine
        
Command line usage: ftpGetFiles credentials.txt transfers.txt targetFolder
The command will retrieve the files listed in transfers.txt and copy them to targetFolder on your local machine.
Files in transfers.txt can be in multiple directories on the remote server. Just include the path in the filename.
