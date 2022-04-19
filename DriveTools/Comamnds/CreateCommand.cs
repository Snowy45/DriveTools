using System;
using System.IO;


namespace DriveTools.Comamnds
{
    public class CreateCommand : CommandListener
    {
        public void OnDriveCommand(object source, string[] args, string rawInput, string directory)
        {
            switch (args[0].ToLower())
            {
                case "crt":
                case "create":
                    if (args.Length < 3) break;

                    string option = args[1];
                    string name = utils.removeArgsFromRaw(args, 2, rawInput);

                    try
                    {
                        if (option.ToLower().Equals("-d"))
                        {
                            Directory.CreateDirectory(directory + "/" + name);
                        }
                        else if (option.ToLower().Equals("-f"))
                        {
                            File.CreateText(directory + "/" + name);
                        } else
                        {
                            break;
                        }
                        
                        utils.tColor(ConsoleColor.Green);
                        Console.WriteLine("Successfully created: {0} in directory: {1}", name, directory);
                        Console.ResetColor();
                        utils.CreateEmptySpace(1);

                    } catch (UnauthorizedAccessException _ex)
                    {
                        utils.tColor(ConsoleColor.Red);
                        Console.WriteLine("Failed to create the file: {0} in the directory: {1} because of an Unauthorized-Access", name, directory);
                        Console.ResetColor();
                        utils.CreateEmptySpace(1);
                    }

                    break;
            }
        }
    }
}
