using System;
using System.Collections.Generic;
using System.IO;
using DriveTools.Comamnds;

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
            utils.DriversTitle();
            utils.loadAllDrivers();

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
                        if (driversList.ContainsKey(options[optionsIndex]) && driversList[options[optionsIndex]].IsReady)
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
        
    }

    public class Drive
    {
        public delegate void DriveCommandEventHandler(object source, string[] args, string rawInput, string directory);

        public static event DriveCommandEventHandler DriveCommand;

        public Dictionary<string, int> directoryIndex = new Dictionary<string, int>();
        public Dictionary<int, string> directoryName = new Dictionary<int, string>();

        public string currentDirectory;
        public bool commandLoop = true;

        public Drive(DriveInfo drive)
        {
            Console.Clear();

            currentDirectory = drive.Name; // Set starting directory

            utils.DriversTitle();
            Console.WriteLine(drive.Name);
            utils.CreateEmptySpace(2);

            //Writes to console driver information
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
            Console.ResetColor();
            
            directoryIndex.Add(currentDirectory, 0);
            directoryName.Add(0, currentDirectory);

            while (commandLoop)
            {
                utils.tColor(ConsoleColor.Green);
                Console.Write("{0}>", currentDirectory.Substring(1));
                Console.ResetColor();

                string rawInput = Console.ReadLine();
                string[] input = rawInput.Split(' ');

                OnDriveCommand(input, rawInput, currentDirectory);
            }
        }

        protected virtual void OnDriveCommand(string[] args, string rawInput, string directory)
        {
            

            if (DriveCommand != null)
                DriveCommand(this, args, rawInput, directory);

        }
    }
}

