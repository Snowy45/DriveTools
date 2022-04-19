using System;
using System.IO;

namespace DriveTools.Comamnds
{
    public class ExpandCommand : CommandListener
    {
        

        public void OnDriveCommand(object source, string[] args, string rawInput, string directory)
        {
            switch (args[0].ToLower())
            {
                case "exp":
                case "ex":
                    //Checking that the user has provided arguments for this command
                    if (args.Length < 2 && utils.removeArgsFromRaw(args, 1, rawInput).Length > 1) break;


                    Drive drive = (Drive)source;

                    if (Directory.Exists(directory + "/" + rawInput.Substring(args[0].Length + 1).TrimEnd()))
                    {
                        //Move a folder up
                        string dirName = rawInput.Substring(args[0].Length + 2).TrimEnd().ToLower();

                        drive.directoryIndex.Add(directory + "/" + args[1].Substring(0, 1).ToUpper() + dirName, drive.directoryIndex[directory] + 1);
                        drive.currentDirectory += "/" + args[1].Substring(0, 1).ToUpper() + dirName;
                        drive.directoryName.Add(drive.directoryIndex[drive.currentDirectory], drive.currentDirectory);

                    }
                    else
                    {
                        utils.tColor(ConsoleColor.Red);
                        Console.WriteLine("Specified directory couldn't be found, use command: files | To get a list of avalible folders");
                        Console.ResetColor();
                        utils.CreateEmptySpace(1);
                        break;
                    }
                    break;
            }
        }
    }
}
