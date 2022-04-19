using System;
using System.IO;
using DriveTools.Comamnds;

namespace DriveTools
{
    public class FilesCommand : CommandListener
    {
        public void OnDriveCommand(object source, string[] args, string rawInput, string directory)
        {
            if (args[0].ToLower().Equals("files") || args[0].ToLower().Equals("fl") || args[0].ToLower().Equals("fls"))
            {
                DriveInfo drive  = new DriveInfo(directory);
                string[] directorys;
                string[] files;

                try
                {
                    directorys = Directory.GetDirectories(directory);
                    files = Directory.GetFiles(directory);
                }
                catch
                {
                    Console.WriteLine("Files couldn't be accessed");
                    return;
                }
                

                utils.CreateEmptySpace(1);
                utils.tColor(ConsoleColor.Yellow);
                Console.WriteLine("List of all Folders:");
                utils.CreateEmptySpace(1);

                //Print Folders
                foreach (string folder in directorys)
                {
                    utils.tColor(ConsoleColor.Cyan);
                    Console.Write(folder.Substring(drive.Name.Length));
                    Console.ResetColor();
                    Console.WriteLine(" | Last Modified: " + Directory.GetLastWriteTime(folder));
                }

                utils.CreateEmptySpace(1);
                utils.tColor(ConsoleColor.Yellow);
                Console.WriteLine("List of all Files:");

                //Print Files
                foreach (string file in files)
                {
                    
                    utils.tColor(ConsoleColor.Cyan);
                    Console.Write(file.Substring(drive.Name.Length + 1));
                    Console.ResetColor();
                    Console.WriteLine(" | Last Modified: " + Directory.GetLastWriteTime(file));
                }
            }
        }
    }
}
