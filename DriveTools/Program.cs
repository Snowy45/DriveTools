using System;
using DriveTools.Comamnds;

namespace DriveTools
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadCommands();


            Console.Title = "DriveTools";
            MainMenu.LoadMainMenu();
            
        }

        public static void LoadCommands()
        {
            // Commands
            FilesCommand filesCMD = new FilesCommand();
            BackCommand backCMD = new BackCommand();
            ExpandCommand expandCMD = new ExpandCommand();
            PreviousCommand prediCMD = new PreviousCommand();
            DeleteCommand deleteCMD = new DeleteCommand();
            CreateCommand createCMD = new CreateCommand();
            HelpCommand helpCMD = new HelpCommand();
            FilePingCommand filePingCMD = new FilePingCommand();


            //Subscribe to Event
            Drive.DriveCommand += filesCMD.OnDriveCommand;
            Drive.DriveCommand += backCMD.OnDriveCommand;
            Drive.DriveCommand += expandCMD.OnDriveCommand;
            Drive.DriveCommand += prediCMD.OnDriveCommand;
            Drive.DriveCommand += deleteCMD.OnDriveCommand;
            Drive.DriveCommand += createCMD.OnDriveCommand;
            Drive.DriveCommand += helpCMD.OnDriveCommand;
            Drive.DriveCommand += filePingCMD.OnDriveCommand;
        }
    }

    class MainMenu
    {
        static string[] options = { "Drives Data", "Exit" };
        static int optionsIndex = 0;
        static int reDrawFrom;
        public static int currentTopPos;
        public static int currentLeftPos;


        public static void LoadMainMenu()
        {
            Console.Clear();


            utils.CreateTitle();
            utils.CreateEmptySpace(2);

            Console.WriteLine("𝗧𝘆𝗽𝗲 𝘄𝗵𝗶𝗰𝗵 𝗮𝗰𝘁𝗶𝗼𝗻 𝘆𝗼𝘂 𝘄𝗼𝘂𝗹𝗱 𝗹𝗶𝗸𝗲 𝘁𝗼 𝗽𝗿𝗲𝗳𝗼𝗿𝗺");

            reDrawFrom = Console.CursorTop;
            utils.DrawMenu(options, optionsIndex, reDrawFrom, true, Console.CursorLeft, Console.CursorTop);
            currentTopPos = Console.CursorTop;
            currentLeftPos = Console.CursorLeft;



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
                    switch (optionsIndex)
                    {
                        case 0:
                            DrivesMenu.LoadDrivesMenu();
                            break;
                        case 1:
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {

                    utils.ClearCurrentLine();
                }
            }






        }



    }
}