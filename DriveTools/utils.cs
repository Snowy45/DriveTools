using System;
using System.Collections.Generic;
using System.IO;

namespace DriveTools
{
    public class utils
    {
        // Utilites for the DrivesMenu
        //-----------------------------------------------------------------------------------------------------------------

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
                drivers.Add(drive.Name + " | Ready: " + drive.IsReady.ToString().ToUpper());
                DrivesMenu.driversList.Add(drive.Name + " | Ready: " + drive.IsReady.ToString().ToUpper(), drive);

            }
            DrivesMenu.options = drivers.ToArray();
        }

        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Draws a menu that highlights the selected block.
        /// Parameters need to be filled and then a highlight would be drawn, better to use in a single screen that doesn't
        /// need to read any more inputs.
        ///
        /// Look at: https://pastebin.com/kt3iye0m
        /// for navigation controls for the menu
        /// </summary>
        /// <param name="menuItems">List of the menu items that will be shown</param>
        /// <param name="index">Which one of the menu items is currently selected</param>
        /// <param name="drawFrom">The start of the first menu item</param>
        /// <param name="firstDraw">If false then it would reset the cursor back to desired position</param>
        /// <param name="leftPos">Left position of the cursor before the draw</param>
        /// <param name="topPos">Top position of the cursor before the draw</param>

        
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


        //Sets console text color
        public static void tColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        //Sets console background color
        public static void bColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }

        public static string removeArgsFromRaw(string[] args, int amountOfArgs, string rawInput)
        {
            int subString = 0;
            string newString = rawInput;

            for (int i = 0; i < amountOfArgs; i++)
            {
                subString += args[i].Length + 1;
            }
            //fping -s 4 lore
            return newString.Substring(subString).TrimStart().TrimEnd();

        }

    }
}

