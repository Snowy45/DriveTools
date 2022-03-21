using System;

namespace DriveTools
{
    class Program
    {



        static void Main(string[] args)
        {

            Console.Title = "DriveTools";





            MainMenu.LoadMainMenu();
        }



    }

    class MainMenu
    {
        static string[] options = { "Drives Data", "Exit"};
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

    class utils
    {

        public static void ReDrawMenu(string[] menuItems, int index, bool isDown)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {

            }

        }

        public static void DrawMenu(string[] menuItems, int index, int drawFrom, bool firstDraw, int leftPos, int topPos)
        {
            Console.SetCursorPosition(0, drawFrom);
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == index)
                {
                    utils.bColor(ConsoleColor.White);
                    utils.tColor(ConsoleColor.Black);

                    Console.WriteLine("- " + menuItems[i]);

                }
                else
                {
                    Console.ResetColor();
                    //utils.tColor(ConsoleColor.White);

                    Console.WriteLine(menuItems[i] + "    ");
                }
            }
            if (!firstDraw) Console.SetCursorPosition(leftPos, topPos);
            Console.ResetColor();
        }


        public static void CreateTitle()
        {
            tColor(ConsoleColor.Green);
            Console.WriteLine("██████╗░██████╗░██╗██╗░░░██╗███████╗  ████████╗░█████╗░░█████╗░██╗░░░░░");
            Console.WriteLine("██╔══██╗██╔══██╗██║██║░░░██║██╔════╝  ╚══██╔══╝██╔══██╗██╔══██╗██║░░░░░");
            Console.WriteLine("██║░░██║██████╔╝██║╚██╗░██╔╝█████╗░░  ░░░██║░░░██║░░██║██║░░██║██║░░░░░");
            Console.WriteLine("██║░░██║██╔══██╗██║░╚████╔╝░██╔══╝░░  ░░░██║░░░██║░░██║██║░░██║██║░░░░░");
            Console.WriteLine("██████╔╝██║░░██║██║░░╚██╔╝░░███████╗  ░░░██║░░░╚█████╔╝╚█████╔╝███████╗");
            Console.WriteLine("╚═════╝░╚═╝░░╚═╝╚═╝░░░╚═╝░░░╚══════╝  ░░░╚═╝░░░░╚════╝░░╚════╝░╚══════╝");

        }

        public static void ClearPreviosLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        public static void ClearCurrentLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("  ");
            Console.SetCursorPosition(0, Console.CursorTop);

        }

        public static void CreateEmptySpace(int lines)
        {
            for (int i = 0; i < lines; i++)
            {
                Console.WriteLine("");
            }
        }

        public static void tColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public static void bColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }
    }
}
