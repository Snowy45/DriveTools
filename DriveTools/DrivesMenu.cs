using System;
using System.Collections.Generic;
using System.IO;

namespace DriveTools
{
    public class DrivesMenu
    {
        //Menu Variables
        public static string[] options = { };
        public static Dictionary<string, DriveInfo> driversList = new Dictionary<string, DriveInfo>();
        static int optionsIndex = 0;
        static int reDrawFrom;
        public static int currentTopPos;
        public static int currentLeftPos;


        public static void LoadDrivesMenu()
        {


            resetDriversData();
            Console.Clear();
            driveUtils.DriversTitle();
            driveUtils.loadAllDrivers();

            //Menu Setup
            reDrawFrom = Console.CursorTop;
            utils.DrawMenu(options, optionsIndex, reDrawFrom, true, Console.CursorLeft, Console.CursorTop);
            currentTopPos = Console.CursorTop;
            currentLeftPos = Console.CursorLeft;

            //Menu Logic
            while (true)
            {

                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key.Equals(ConsoleKey.UpArrow) || key.Key.Equals(ConsoleKey.DownArrow))
                {


                    if (key.Key.Equals(ConsoleKey.DownArrow))
                    {
                        optionsIndex++;
                        if (optionsIndex > options.Length - 1)
                        {
                            optionsIndex = 0;
                        }
                        utils.DrawMenu(options, optionsIndex, reDrawFrom, false, currentLeftPos, currentTopPos);
                    }
                    else
                    {
                        if (key.Key.Equals(ConsoleKey.UpArrow))
                        {
                            optionsIndex--;
                            if (optionsIndex < 0)
                            {
                                optionsIndex = options.Length - 1;
                            }
                            utils.DrawMenu(options, optionsIndex, reDrawFrom, false, currentLeftPos, currentTopPos);
                        }
                    }
                }
                else if (key.Key.Equals(ConsoleKey.Enter))
                {

                    if (options[optionsIndex] != null)
                    {
                        if (driversList.ContainsKey(options[optionsIndex]))
                        {
                            new Drive(driversList.GetValueOrDefault(options[optionsIndex]));
                            break;
                        }
                    }

                }
                else if (key.Key.Equals(ConsoleKey.Escape))
                {
                    MainMenu.LoadMainMenu();
                    break;
                }
                else
                {
                    utils.ClearCurrentLine();
                }


            }
        }

        static void resetDriversData()
        {
            options = null;
            driversList.Clear();
            optionsIndex = 0;
            reDrawFrom = 0;
            currentLeftPos = 0;
            currentTopPos = 0;
        }
    }

    class driveUtils
    {
        public static void DriversTitle()
        {
            utils.tColor(ConsoleColor.Magenta);
            Console.WriteLine("██████╗░██████╗░██╗██╗░░░██╗███████╗██████╗░░██████╗");
            Console.WriteLine("██╔══██╗██╔══██╗██║██║░░░██║██╔════╝██╔══██╗██╔════╝");
            Console.WriteLine("██║░░██║██████╔╝██║╚██╗░██╔╝█████╗░░██████╔╝╚█████╗░");
            Console.WriteLine("██║░░██║██╔══██╗██║░╚████╔╝░██╔══╝░░██╔══██╗░╚═══██╗");
            Console.WriteLine("██████╔╝██║░░██║██║░░╚██╔╝░░███████╗██║░░██║██████╔╝");
            Console.WriteLine("╚═════╝░╚═╝░░╚═╝╚═╝░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═════╝░");
        }

        public static void loadAllDrivers()
        {

            List<string> drivers = new List<string>();
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                drivers.Add(drive.Name);
                DrivesMenu.driversList.Add(drive.Name, drive);

            }
            DrivesMenu.options = drivers.ToArray();
        }
    }

    public class Drive
    {


        public Drive(DriveInfo drive)
        {
            Console.Clear();
            driveUtils.DriversTitle();
            Console.WriteLine(drive.Name);
            utils.CreateEmptySpace(2);

            utils.tColor(ConsoleColor.Red);
            Console.WriteLine("In case you encounter any problems while navigating \ninside the drive you can refer to this help section;");
            Console.WriteLine("");
            Console.WriteLine("Help/Navigation Commands:\n--------------------------\n");
            Console.WriteLine("back - Go back to the drivers list");
            Console.WriteLine("files - Get a list of all the files and folders in the current directory");
            Console.WriteLine("expand(ex,exp) <directory> - Expands to the specified folder and lets you retrive data from there");
            Console.WriteLine("predi(pd) - Go back to the previous directory");
            Console.WriteLine("delete(del) <file/directory) - Delete specified file/folder");
            Console.WriteLine("create(crt) <extension> - Creates a file with the specified extension");
            Console.WriteLine("el - List sEncryption Formats");
            Console.WriteLine("Encrypt(enc) <Encryption Type> <file> - Encrypts a file | NOTE: On mac only text inside the files could be encrypted");
            Console.WriteLine("Decrypt(enc) Optional<Decryption Type> <file> - Tries to decrypt a file with avalible decryption methods a file | Optional: You could specify a decryption format");
            Console.WriteLine("Mail(m) <file> <To> - Sends a mail to specified addresess containing the file");


            utils.tColor(ConsoleColor.Magenta);
            utils.CreateEmptySpace(2);
            if (drive.IsReady)
            {



                Console.WriteLine("Drive Data:");
                Console.WriteLine("------------");
                Console.WriteLine("Ready: {0}", drive.IsReady);

                Console.WriteLine("Name: {0}", drive.Name);
                Console.WriteLine("Type: {0}", drive.DriveType);
                Console.WriteLine("Format: {0}", drive.DriveFormat);
                Console.WriteLine("Directory: {0}", drive.RootDirectory);
                Console.WriteLine("Total-Space: {0} Bytes | {1} Gigabytes", drive.TotalSize, (float)drive.TotalSize / 1073741824);
                Console.WriteLine("Free-Space: {0} Bytes | {1} Gigabytes", drive.TotalFreeSpace, (float)drive.TotalFreeSpace / 1073741824);
                Console.WriteLine("Volume: {0}", drive.VolumeLabel);
            }
            else
            {
                Console.WriteLine("Drive Data:");
                Console.WriteLine("------------");
                Console.WriteLine("Ready: {0}", drive.IsReady);

                Console.WriteLine("Name: {0}", drive.Name);
                Console.WriteLine("Type: ?");
                Console.WriteLine("Format: ?");
                Console.WriteLine("Directory: ?");
                Console.WriteLine("Total-Space: ?");
                Console.WriteLine("Free-Space: ?");
                Console.WriteLine("Volume: ?");
            }
            Console.ResetColor();

            Dictionary<string, int> directoryIndex = new Dictionary<string, int>();
            Dictionary<int, string> directoryName = new Dictionary<int, string>();

            string currentDirectory = drive.Name;
            directoryIndex.Add(currentDirectory, 0);
            directoryName.Add(0, currentDirectory);

            while (true)
            {


                utils.tColor(ConsoleColor.Green);
                Console.Write("{0}>", currentDirectory.Substring(1));
                Console.ResetColor();

                string rawInput = Console.ReadLine();
                string[] input = rawInput.Split(' ');

                switch (input[0].ToLower())
                {
                    case "back":
                        DrivesMenu.LoadDrivesMenu();
                        break;
                    case "files":
                        string[] directorys = Directory.GetDirectories(currentDirectory);
                        string[] files = Directory.GetFiles(currentDirectory);

                        utils.CreateEmptySpace(1);
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
                        Console.WriteLine("List of all Files:");

                        //Print Files
                        foreach (string file in files)
                        {

                            utils.tColor(ConsoleColor.Cyan);
                            Console.Write(file.Substring(drive.Name.Length + 1));
                            Console.ResetColor();
                            Console.WriteLine(" | Last Modified: " + Directory.GetLastWriteTime(file));
                        }
                        break;
                    case "ex":
                    case "exp":
                    case "expand":
                        if (input.Length < 2)
                        {
                            utils.tColor(ConsoleColor.Red);
                            Console.WriteLine("Enter directory name");
                            Console.ResetColor();
                            utils.CreateEmptySpace(1);
                            break;
                        }


                        if (Directory.Exists(currentDirectory + "/" + rawInput.Substring(input[0].Length + 1).TrimEnd()))
                        {
                            //Move a folder up
                            string dirName = rawInput.Substring(input[0].Length + 2).TrimEnd().ToLower();

                            directoryIndex.Add(currentDirectory + "/" + input[1].Substring(0, 1).ToUpper() + dirName, directoryIndex[currentDirectory] + 1);
                            currentDirectory += "/" + input[1].Substring(0, 1).ToUpper() + dirName;
                            directoryName.Add(directoryIndex[currentDirectory], currentDirectory);

                        }
                        else
                        {
                            utils.tColor(ConsoleColor.Red);
                            Console.WriteLine("Specified directory couldn't be found, use command: files | to get a list of avalible folders");
                            Console.ResetColor();
                            utils.CreateEmptySpace(1);
                            break;
                        }

                        break;
                    case "pd":
                    case "predi":


                        if (directoryIndex[currentDirectory] == 0)
                        {
                            utils.tColor(ConsoleColor.Red);
                            Console.WriteLine("Can't go to previous directory");
                            Console.ResetColor();
                            utils.CreateEmptySpace(1);
                            break;
                        }

                        string directoryToRemove = currentDirectory;

                        currentDirectory = directoryName[directoryIndex[currentDirectory] - 1];
                        directoryName.Remove(directoryIndex[directoryToRemove]);
                        directoryIndex.Remove(directoryToRemove);


                        break;
                    case "del":
                    case "delete":
                        if (input.Length < 2)
                        {
                            utils.tColor(ConsoleColor.Red);
                            Console.WriteLine("Enter file/directory name");
                            Console.ResetColor();
                            utils.CreateEmptySpace(1);
                            break;
                        }
                        Console.WriteLine(rawInput.Substring(input[0].Length + 1));
                        if (Directory.Exists(currentDirectory + "/" + rawInput.Substring(input[0].Length + 1)))
                        {
                            string dirName = rawInput.Substring(input[0].Length + 1);
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
                                        Directory.Delete(currentDirectory + "/" + dirName);

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
                        else if (File.Exists(currentDirectory + "/" + rawInput.Substring(input[0].Length + 1)))
                        {
                            string fileName = rawInput.Substring(input[0].Length + 1);
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
                                        File.Delete(currentDirectory + "/" + fileName);

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
                        else
                        {
                            utils.tColor(ConsoleColor.Red);
                            Console.WriteLine("Specified directory or file couldn't be found, use command: files | to get a list of avalible folders");
                            Console.ResetColor();
                            utils.CreateEmptySpace(1);
                            break;
                        }


                        break;
                    case "crt":
                    case "create":

                        break;
                    default:
                        utils.ClearPreviosLine();
                        break;

                }




            }




        }




    }
}
