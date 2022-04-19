using System;
using System.IO;
using DriveTools;

namespace DriveTools.Comamnds
{
    public class HelpCommand : CommandListener
    {
        public void OnDriveCommand(object source, string[] args, string rawInput, string directory)
        {
            switch (args[0].ToLower())
            {
                case "help":
                    utils.tColor(ConsoleColor.Red);

                    

                    Console.WriteLine("In case you encounter any problems while navigating \ninside the drive you can refer to this help section;");
                    Console.WriteLine("");
                    Console.WriteLine("Help/Navigation Commands:\n--------------------------\n");
                    Console.WriteLine("back - Go back to the drivers list");
                    Console.WriteLine("files(fl) - Get a list of all the files and folders in the current directory");
                    Console.WriteLine("expand(ex,exp) <directory> - Expands to the specified folder and lets you retrive data from there");
                    Console.WriteLine("predi(pd) - Go back to the previous directory");
                    Console.WriteLine("delete(del) <file/directory name> - Delete specified file/folder");
                    Console.WriteLine("create(crt) <F/D> <Name> - Creates a file or directory with the specified name and extension");
                    Console.WriteLine("el - List Encryption Formats");
                    Console.WriteLine("encrypt(enc) <Encryption Type> <file> - Encrypts a file | NOTE: On mac only text inside the files could be encrypted");
                    Console.WriteLine("decrypt(enc) Optional<Decryption Type> <file> - Tries to decrypt a file with avalible decryption methods a file | Optional: You could specify a decryption format");
                    Console.WriteLine("mail(m) <file> <To> - Sends a mail to specified addresess containing the file");


                    utils.tColor(ConsoleColor.Magenta);
                    utils.CreateEmptySpace(2);

                    break;
            }
        }
    }
}
