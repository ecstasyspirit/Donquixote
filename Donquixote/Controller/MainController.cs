using Donquixote.Models;
using Donquixote.Models.DataStructuresModels.EnumModels;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace Donquixote.Controller
{
    public class MainController
    {
        public MainModel MainModel = new MainModel();

        public void SetConsoleTitle() => MainModel.SetConsoleTitle();

        public void DisplaySoftwareName()
        {
            string[] array = new string[]
                {
                    "  _/_  _  _     .  _ _/__ ",
                    "/_//_// //_//_//></_// /_'",
                    "          /               "
                };

            int num = 234;
            int num2 = 153;

            foreach (string text in array)
            {
                Console.Write(new string(' ', Console.BufferWidth / 2 - text.Length / 2));
                Console.WriteLine(text, Color.FromArgb(num, num2, 200));
                num -= 10;
                num2 -= 25;
            }
        }

        public void ImportPhones()
        {
            Console.Write(MainModel.GenerateTimestamp() + "Importing phone numbers from 'numbers.txt' ...");

            switch (MainModel.ImportPhones())
            {
                case true:
                    Console.WriteLine(" ✓", Color.FromArgb(234, 153, 200));
                    break;

                case false:
                    Console.WriteLine(" X", Color.FromArgb(194, 53, 200));
                    Console.WriteLine(MainModel.GenerateTimestamp() + "No 'numbers.txt' file found in the startup directory.");

                    Console.ReadKey(true);

                    Environment.Exit(0);
                    break;
            }

            MainModel.SetConsoleTitle();
        }

        public void SelectModeSpeed(int mode)
        {
            var iOptionIndex = 0;

            var availableModes = new ModeEnumModel[] { ModeEnumModel.Spam, ModeEnumModel.Bomb };
            var availableSpeeds = new SpeedEnumModel[] { SpeedEnumModel.Normal, SpeedEnumModel.Medium, SpeedEnumModel.Fast };

            switch (mode)
            {
                case 0:
                    Console.Write($"{MainModel.GenerateTimestamp()}Available modes: ");
                    Console.WriteLine(string.Join(", ", availableModes), Color.FromArgb(234, 153, 200));
                    Console.Write($"{MainModel.GenerateTimestamp()}What mode do you want to use? ");
                    Console.Write(availableModes[iOptionIndex], Color.FromArgb(234, 153, 200));
                    break;
                case 1:
                    Console.Write($"{MainModel.GenerateTimestamp()}Available speeds: ");
                    Console.WriteLine(string.Join(", ", availableSpeeds), Color.FromArgb(234, 153, 200));
                    Console.Write($"{MainModel.GenerateTimestamp()}What speed do you want to use? ");
                    Console.Write(availableSpeeds[iOptionIndex], Color.FromArgb(234, 153, 200));
                    break;
            }

            Console.ForegroundColor = Color.FromArgb(234, 153, 200);

            while (true)
            {
                var oKeyDown = Console.ReadKey().Key;

                if (oKeyDown == ConsoleKey.Enter)
                {
                    Console.ResetColor();

                    switch (mode)
                    {
                        case 0:
                            Console.Write($"\n{MainModel.GenerateTimestamp()}Selected mode [");
                            Console.Write($"{MainModel.SelectedMode}", Color.FromArgb(234, 153, 200));
                            Console.WriteLine($"].");
                            break;

                        case 1:
                            Console.Write($"\n{MainModel.GenerateTimestamp()}Selected speed [");
                            Console.Write($"{MainModel.SelectedSpeed}", Color.FromArgb(234, 153, 200));
                            Console.Write($"] || pause between messages [");
                            Console.Write($"{(int)MainModel.SelectedSpeed} ms", Color.FromArgb(234, 153, 200));
                            Console.WriteLine($"].");
                            break;
                    }
                    break;
                }
                else if (oKeyDown == ConsoleKey.Backspace)
                {
                    switch (mode)
                    {
                        case 0:
                            Console.Write(availableModes[iOptionIndex].ToString().Substring(availableModes[iOptionIndex].ToString().Length - 1, 1));
                            break;
                        case 1:
                            Console.Write(availableSpeeds[iOptionIndex].ToString().Substring(availableSpeeds[iOptionIndex].ToString().Length - 1, 1));
                            break;
                    }
                }
                else
                {
                    switch (mode)
                    {
                        case 0:
                            Console.Write(new string('\b', availableModes[iOptionIndex].ToString().Length + 1)
                                          + new string(' ', availableModes[iOptionIndex].ToString().Length + 1)
                                          + new string('\b', availableModes[iOptionIndex].ToString().Length + 1));

                            switch (oKeyDown)
                            {
                                case ConsoleKey.LeftArrow:
                                case ConsoleKey.DownArrow:
                                    if (iOptionIndex == availableModes.Length - 1)
                                        iOptionIndex = 0;
                                    else
                                        iOptionIndex++;
                                    break;
                                case ConsoleKey.RightArrow:
                                case ConsoleKey.UpArrow:
                                    if (iOptionIndex == 0)
                                        iOptionIndex = availableModes.Length - 1;
                                    else
                                        iOptionIndex--;
                                    break;
                            }

                            Console.Write(availableModes[iOptionIndex].ToString());

                            MainModel.SelectedMode = availableModes[iOptionIndex];
                            break;
                        case 1:
                            Console.Write(new string('\b', availableSpeeds[iOptionIndex].ToString().Length + 1)
                                          + new string(' ', availableSpeeds[iOptionIndex].ToString().Length + 1)
                                          + new string('\b', availableSpeeds[iOptionIndex].ToString().Length + 1));

                            switch (oKeyDown)
                            {
                                case ConsoleKey.RightArrow:
                                case ConsoleKey.DownArrow:
                                    if (iOptionIndex == availableSpeeds.Length - 1)
                                        iOptionIndex = 0;
                                    else
                                        iOptionIndex++;
                                    break;
                                case ConsoleKey.LeftArrow:
                                case ConsoleKey.UpArrow:
                                    if (iOptionIndex == 0)
                                        iOptionIndex = availableSpeeds.Length - 1;
                                    else
                                        iOptionIndex--;
                                    break;
                            }

                            Console.Write(availableSpeeds[iOptionIndex].ToString());

                            MainModel.SelectedSpeed = availableSpeeds[iOptionIndex];
                            break;
                    }
                }
            }
        }

        public void SetMessage()
        {
            while (MainModel.MaliciousMessage.Replace(" ", string.Empty).Length == 0)
            {
                Console.Write(MainModel.GenerateTimestamp() + "Set the message to use for the attack: ");

                Console.ForegroundColor = Color.FromArgb(234, 153, 200);

                var inputBuffer = new byte[2048];
                var inputStream = Console.OpenStandardInput(inputBuffer.Length);
                Console.SetIn(new StreamReader(inputStream, Console.InputEncoding, false, inputBuffer.Length));

                MainModel.MaliciousMessage = Console.ReadLine();

                Console.ResetColor();

                if (MainModel.MaliciousMessage.Replace(" ", string.Empty).Length == 0)
                    Console.WriteLine(MainModel.GenerateTimestamp() + "Invalid message parameter, please set it to at least 1 character.");
            }

            Console.WriteLine(MainModel.GenerateTimestamp() + "This is how the message will appear on the victims' devices:");
            Console.WriteLine($"{new string(' ', 21)}>>");
            Console.WriteLine(new string(' ', 21) + MainModel.MaliciousMessage.Replace("\\n", "\n" + new string(' ', 21)), Color.FromArgb(234, 153, 200));
            Console.WriteLine($"{new string(' ', 21)}<<");
        }

        public void SetRecursivity()
        {
            if (MainModel.SelectedMode == ModeEnumModel.Bomb)
                while (MainModel.MessengerRecursivity <= 0)
                {
                    Console.Write(MainModel.GenerateTimestamp() + "Set the recursivity of the messenger to use for the attack: ");

                    Console.ForegroundColor = Color.FromArgb(234, 153, 200);

                    try
                    {
                        MainModel.MessengerRecursivity = Convert.ToInt32(Console.ReadLine());

                        if (MainModel.MessengerRecursivity <= 0)
                            throw new Exception();
                    }
                    catch (Exception)
                    {
                        Console.ResetColor();

                        Console.Write(MainModel.GenerateTimestamp() + "Invalid recursivity parameter, please set it to at least 1 and only use digits ");
                        Console.Write("[0-9]", Color.FromArgb(194, 53, 200));
                        Console.WriteLine(".");
                    }

                    Console.ResetColor();
                }
        }

        public void Login()
        {
            Console.Write(MainModel.GenerateTimestamp() + "Enter Line2 phone number: ");

            Console.ForegroundColor = Color.FromArgb(234, 153, 200);

            var phone = Console.ReadLine();

            Console.ResetColor();

            Console.Write(MainModel.GenerateTimestamp() + "Enter Line2 password: ");

            Console.ForegroundColor = Color.FromArgb(234, 153, 200);

            var password = Console.ReadLine();

            Console.ResetColor();

            Console.Write(MainModel.GenerateTimestamp() + "Connecting ...");

            var login = MainModel.LoginModel.Login(phone, password);

            switch (login)
            {
                case "":
                    Console.WriteLine(" X", Color.FromArgb(194, 53, 200));
                    Console.WriteLine(MainModel.GenerateTimestamp() + "Login failed.");

                    Console.ReadKey(true);

                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine(" ✓", Color.FromArgb(234, 153, 200));
                    Console.WriteLine($"{MainModel.GenerateTimestamp()}Fetched access token: {login}.");

                    MainModel.AccessToken = login;
                    break;
            }
        }

        public void Attack()
        {
            Console.Write(MainModel.GenerateTimestamp() + "Press any key to start the attack ...");

            Console.ReadKey(true);

            Console.Clear();

            DisplaySoftwareName();

            var messengerResult = MainModel.SendMessages().Result;

            Console.ResetColor();

            SetConsoleTitle();

            Console.WriteLine($"\n{MainModel.GenerateTimestamp()}Attack completed!");
            Console.Write($"{MainModel.GenerateTimestamp()}Sent ");
            Console.Write(messengerResult.PhonesMessaged, Color.FromArgb(234, 153, 200));
            Console.Write($" messages to ");
            Console.Write(messengerResult.PhonesLoaded, Color.FromArgb(234, 153, 200));
            Console.Write("phones in ");
            Console.Write(messengerResult.AttackMode, Color.FromArgb(234, 153, 200));
            Console.Write(" mode at ");
            Console.Write(messengerResult.AttackSpeed, Color.FromArgb(234, 153, 200));
            Console.Write(" speed || (");
            Console.Write(messengerResult.PhoneSkiped, Color.FromArgb(194, 53, 200));
            Console.WriteLine(" skipped).");
        }

        public void SaveLogs()
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter("messaging-failed.txt", true))
                {
                    for (var i = 0; i < MainModel.MessagingFailed.Count; i++)
                        streamWriter.WriteLine(MainModel.MessagingFailed[i].Number);
                }

                Console.WriteLine(MainModel.GenerateTimestamp() + "Saved all the phone numbers that were failed to be messaged under 'messaging-failed.txt'.");
            }
            catch (Exception)
            {
                Console.WriteLine(MainModel.GenerateTimestamp() + "Error while saving the phone numbers that were failed to be messaged.");
            }
        }
    }
}