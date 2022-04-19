using System;
namespace DriveTools.Comamnds
{
    public class PreviousCommand : CommandListener
    {
        

        public void OnDriveCommand(object source, string[] args, string rawInput, string directory)
        {
            switch (args[0].ToLower())
            {
                case "pd":
                case "predi":
                    Drive drive = (Drive)source;

                    if (drive.directoryIndex[directory] == 0)
                    {
                        utils.tColor(ConsoleColor.Red);
                        Console.WriteLine("Can't go to previous directory");
                        Console.ResetColor();
                        utils.CreateEmptySpace(1);
                        break;
                    }

                    string directoryToRemove = directory;

                    drive.currentDirectory = drive.directoryName[drive.directoryIndex[directory] - 1];
                    drive.directoryName.Remove(drive.directoryIndex[directoryToRemove]);
                    drive.directoryIndex.Remove(directoryToRemove);
                    break;
            }
        }
    }
}
