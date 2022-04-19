using System;
using System.IO;

namespace DriveTools.Comamnds
{
    public class DeleteCommand : CommandListener
    { 
        public void OnDriveCommand(object source, string[] args, string rawInput, string directory)
        {
            switch (args[0].ToLower())
            {
                case "del":
                case "delete":
                    if (args.Length < 2) break;

                    string option = args[1];
                    if (option.ToLower().Equals("-d"))
                    {
                        if (Directory.Exists(directory + "/" + utils.removeArgsFromRaw(args, 2, rawInput)))
                        {
                            string dirName = utils.removeArgsFromRaw(args, 2, rawInput);
                            utils.tColor(ConsoleColor.Red);
                            Console.WriteLine("!!!-NOTICE-!!!\nYou are going to delete the next Directory: {0}.\n Are you sure you want to do it?(y/n)", dirName);
                            Console.ResetColor();
                            bool gotAnswer = false;

                            while (gotAnswer == false)
                            {
                                string answer = Console.ReadLine();
                                switch (answer)
                                {
                                    case "n":
                                    case "no":
                                        utils.tColor(ConsoleColor.Red);
                                        Console.WriteLine("Cancelled - Directory: {0} will not be deleted", dirName);
                                        Console.ResetColor();
                                        gotAnswer = true;

                                        break;

                                    case "y":
                                    case "yes":
                                        Directory.Delete(directory + "/" + dirName);

                                        utils.tColor(ConsoleColor.Red);
                                        Console.WriteLine("Deleted - Directory: {0} has been deleted", dirName);
                                        Console.ResetColor();
                                        gotAnswer = true;
                                        break;

                                    default:
                                        utils.ClearPreviosLine();
                                        break;

                                }
                            }
                        }


                    }
                    else if (option.ToLower().Equals("-f"))
                    {
                        if (File.Exists(directory + "/" + utils.removeArgsFromRaw(args, 2, rawInput)))
                        {
                            string fileName = utils.removeArgsFromRaw(args, 2, rawInput);
                            utils.tColor(ConsoleColor.Red);
                            Console.WriteLine("!!!-NOTICE-!!!\nYou are going to delete the next File: {0}.\n Are you sure you want to do it?(y/n)", fileName);
                            Console.ResetColor();
                            bool gotAnswer = false;

                            while (gotAnswer == false)
                            {
                                string answer = Console.ReadLine();
                                switch (answer)
                                {
                                    case "n":
                                    case "no":
                                        utils.tColor(ConsoleColor.Red);
                                        Console.WriteLine("Cancelled - File: {0} will not be deleted", fileName);
                                        Console.ResetColor();
                                        gotAnswer = true;

                                        break;

                                    case "y":
                                    case "yes":
                                        File.Delete(directory + "/" + fileName);

                                        utils.tColor(ConsoleColor.Red);
                                        Console.WriteLine("Deleted - File: {0} has been deleted", fileName);
                                        Console.ResetColor();
                                        gotAnswer = true;
                                        break;

                                    default:
                                        utils.ClearPreviosLine();
                                        break;

                                }
                            }
                        }

                    }

                    break;
            }
        }
    }
}
