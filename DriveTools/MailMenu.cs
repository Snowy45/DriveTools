using System;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace DriveTools
{
    public class MailMenu
    {

        static string[] options = { "DriveTools Account", "IDAH.alt.acc@gmail.com", "Add New Account..." };
        static int optionsIndex = 0;
        static int reDrawFrom;
        public static int currentTopPos;
        public static int currentLeftPos;

        public static void LoadMailMenu()
        {
            Console.Clear();

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
                            new MailMessage("dtools.official.acc@gmail.com", "pe?jq}q$5/<Bz$(Tn{~6mH=K;byykAW8bYDX)RHN");
                            return;
                        case 1:
                            
                            break;
                        case 2:
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

    class mailTools
    {
        public static void SendMail(string sender,string password, string receiver, string title, string content)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(sender, password),
                EnableSsl = true,
            };

            smtpClient.Send(sender, receiver, title, content);
        }
    }

    public class MailMessage
    {
        public MailMessage(string sender, string password)
        {
            Console.Clear();
            utils.tColor(ConsoleColor.Yellow);
            Console.Write("Receivers: ");
            string[] receivers = Console.ReadLine().Split(' ');

            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Email Content:");
            string content = Console.ReadLine();

            for (int i = 0; i < receivers.Length; i++)
            {
                mailTools.SendMail(sender, password, receivers[i], title, content);
            }

            utils.tColor(ConsoleColor.Green);
            Console.WriteLine("Email was successfuly sent!");
            Console.ResetColor();

            Thread.Sleep(1000);
            MailMenu.LoadMailMenu();

        }
    }

}
