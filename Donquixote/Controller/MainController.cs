using Donquixote.Models;
using Donquixote.Models.DataStructuresModels.EnumModels;
using System;
using System.Drawing;
using System.IO;
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
            Console.Write(MainModel.GenerateTimestamp() + "Importing phone numbers from 'numbers.txt'...");

            switch (MainModel.ImportPhones())
            {
                case true:
                    Console.WriteLine(" ✓", Color.FromArgb(234, 153, 200));
                    break;

                case false:
                    Console.WriteLine(" X", Color.FromArgb(194, 53, 200));
                    Console.Write($"{MainModel.GenerateTimestamp()}No 'numbers.txt' file found in the startup directory.\n{MainModel.GenerateTimestamp()}Press any key to exit...");

                    Console.ReadKey(true);

                    Environment.Exit(0);
                    break;
            }
        }

        public void SelectConnectionModeSpeed(int mode)
        {
            var optionIndex = 0;

            var availableConnections = (ConnectionEnumModel[])Enum.GetValues(typeof(ConnectionEnumModel));
            var availableModes = (ModeEnumModel[])Enum.GetValues(typeof(ModeEnumModel));
            var availableSpeeds = (SpeedEnumModel[])Enum.GetValues(typeof(SpeedEnumModel));

            switch (mode)
            {
                case 0:
                    Console.Write($"{MainModel.GenerateTimestamp()}Available connections: ");
                    Console.WriteLine(string.Join(", ", availableConnections), Color.FromArgb(234, 153, 200));
                    Console.Write($"{MainModel.GenerateTimestamp()}What connection do you want to use? ");
                    Console.Write(availableConnections[optionIndex], Color.FromArgb(234, 153, 200));
                    break;

                case 1:
                    Console.Write($"{MainModel.GenerateTimestamp()}Available modes: ");
                    Console.WriteLine(string.Join(", ", availableModes), Color.FromArgb(234, 153, 200));
                    Console.Write($"{MainModel.GenerateTimestamp()}What mode do you want to use? ");
                    Console.Write(availableModes[optionIndex], Color.FromArgb(234, 153, 200));
                    break;

                case 2:
                    Console.Write($"{MainModel.GenerateTimestamp()}Available speeds: ");
                    Console.WriteLine(string.Join(", ", availableSpeeds), Color.FromArgb(234, 153, 200));
                    Console.Write($"{MainModel.GenerateTimestamp()}What speed do you want to use? ");
                    Console.Write(availableSpeeds[optionIndex], Color.FromArgb(234, 153, 200));
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
                            Console.Write($"\n{MainModel.GenerateTimestamp()}Selected connection [");
                            Console.Write($"{MainModel.SelectedConnection}", Color.FromArgb(234, 153, 200));
                            Console.WriteLine($"].");

                            if (MainModel.SelectedConnection == ConnectionEnumModel.Proxy)
                                ImportProxies();
                            break;

                        case 1:
                            Console.Write($"\n{MainModel.GenerateTimestamp()}Selected mode [");
                            Console.Write($"{MainModel.SelectedMode}", Color.FromArgb(234, 153, 200));
                            Console.WriteLine($"].");
                            break;

                        case 2:
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
                            Console.Write(availableConnections[optionIndex].ToString().Substring(availableConnections[optionIndex].ToString().Length - 1, 1));
                            break;

                        case 1:
                            Console.Write(availableModes[optionIndex].ToString().Substring(availableModes[optionIndex].ToString().Length - 1, 1));
                            break;

                        case 2:
                            Console.Write(availableSpeeds[optionIndex].ToString().Substring(availableSpeeds[optionIndex].ToString().Length - 1, 1));
                            break;
                    }
                }
                else
                {
                    switch (mode)
                    {
                        case 0:
                            Console.Write(new string('\b', availableConnections[optionIndex].ToString().Length + 1)
                                          + new string(' ', availableConnections[optionIndex].ToString().Length + 1)
                                          + new string('\b', availableConnections[optionIndex].ToString().Length + 1));

                            switch (oKeyDown)
                            {
                                case ConsoleKey.LeftArrow:
                                case ConsoleKey.DownArrow:
                                    if (optionIndex == availableModes.Length - 1)
                                        optionIndex = 0;
                                    else
                                        optionIndex++;
                                    break;

                                case ConsoleKey.RightArrow:
                                case ConsoleKey.UpArrow:
                                    if (optionIndex == 0)
                                        optionIndex = availableConnections.Length - 1;
                                    else
                                        optionIndex--;
                                    break;
                            }

                            Console.Write(availableConnections[optionIndex].ToString());

                            MainModel.SelectedConnection = availableConnections[optionIndex];
                            break;

                        case 1:
                            Console.Write(new string('\b', availableModes[optionIndex].ToString().Length + 1)
                                          + new string(' ', availableModes[optionIndex].ToString().Length + 1)
                                          + new string('\b', availableModes[optionIndex].ToString().Length + 1));

                            switch (oKeyDown)
                            {
                                case ConsoleKey.LeftArrow:
                                case ConsoleKey.DownArrow:
                                    if (optionIndex == availableModes.Length - 1)
                                        optionIndex = 0;
                                    else
                                        optionIndex++;
                                    break;

                                case ConsoleKey.RightArrow:
                                case ConsoleKey.UpArrow:
                                    if (optionIndex == 0)
                                        optionIndex = availableModes.Length - 1;
                                    else
                                        optionIndex--;
                                    break;
                            }

                            Console.Write(availableModes[optionIndex].ToString());

                            MainModel.SelectedMode = availableModes[optionIndex];
                            break;

                        case 2:
                            Console.Write(new string('\b', availableSpeeds[optionIndex].ToString().Length + 1)
                                          + new string(' ', availableSpeeds[optionIndex].ToString().Length + 1)
                                          + new string('\b', availableSpeeds[optionIndex].ToString().Length + 1));

                            switch (oKeyDown)
                            {
                                case ConsoleKey.LeftArrow:
                                case ConsoleKey.DownArrow:
                                    if (optionIndex == availableModes.Length - 1)
                                        optionIndex = 0;
                                    else
                                        optionIndex++;
                                    break;

                                case ConsoleKey.RightArrow:
                                case ConsoleKey.UpArrow:
                                    if (optionIndex == 0)
                                        optionIndex = availableSpeeds.Length - 1;
                                    else
                                        optionIndex--;
                                    break;
                            }

                            Console.Write(availableSpeeds[optionIndex].ToString());

                            MainModel.SelectedSpeed = availableSpeeds[optionIndex];
                            break;
                    }
                }
            }
        }

        public void ImportProxies()
        {
            Console.Write(MainModel.GenerateTimestamp() + "Importing proxies from 'proxies.txt'...");

            switch (MainModel.ImportProxies())
            {
                case true:
                    Console.WriteLine(" ✓", Color.FromArgb(234, 153, 200));
                    break;

                case false:
                    Console.WriteLine(" X", Color.FromArgb(194, 53, 200));
                    Console.Write($"{MainModel.GenerateTimestamp()}No 'proxies.txt' file found in the startup directory.\n{MainModel.GenerateTimestamp()}Press any key to exit...");

                    Console.ReadKey(true);

                    Environment.Exit(0);
                    break;
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
            {
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

                        Console.Write(MainModel.GenerateTimestamp() + "Invalid recursivity parameter, please set it to at least 1 and only use digits.");
                        Console.Write("[0-9]", Color.FromArgb(194, 53, 200));
                        Console.WriteLine(".");
                    }

                    Console.ResetColor();
                }

                Console.Write(MainModel.GenerateTimestamp() + "Recursivity parameter set to [");
                Console.Write($"{MainModel.MessengerRecursivity} times/phone number", Color.FromArgb(234, 153, 200));
                Console.WriteLine("].");
            }
        }

        public void SetThreads()
        {
            while (MainModel.WorkerThreads <= 0)
            {
                Console.Write(
                    $"{MainModel.GenerateTimestamp()}Recommended for connections: 'Direct' -> 1 - 2 ||" +
                    " 'Proxy' [free] -> 150 - 200 & [paid] -> 2 - 5\n" +
                    $"{MainModel.GenerateTimestamp()}Set the threads count to use for the attack: ");

                Console.ForegroundColor = Color.FromArgb(234, 153, 200);

                try
                {
                    MainModel.WorkerThreads = Convert.ToInt32(Console.ReadLine());

                    if (MainModel.WorkerThreads <= 0)
                        throw new Exception();
                }
                catch (Exception)
                {
                    Console.ResetColor();

                    Console.Write(MainModel.GenerateTimestamp() + "Invalid threads parameter, please set it to at least 1 and only use digits ");
                    Console.Write("[0-9]", Color.FromArgb(194, 53, 200));
                    Console.WriteLine(".");
                }

                Console.ResetColor();

                if (MainModel.MaliciousMessage.Replace(" ", string.Empty).Length == 0)
                    Console.WriteLine(MainModel.GenerateTimestamp() + "Invalid message parameter, please set it to at least 1 character.");
            }

            Console.Write(MainModel.GenerateTimestamp() + "Thread parameter set to [");
            Console.Write($"{MainModel.WorkerThreads} times/phone number", Color.FromArgb(234, 153, 200));
            Console.WriteLine("].");
        }

        public void Login()
        {
            Console.Write(MainModel.GenerateTimestamp() + "Enter Line2 phone number: ");

            Console.ForegroundColor = Color.Black;

            var phone = Console.ReadLine();

            Console.ResetColor();

            Console.Write(MainModel.GenerateTimestamp() + "Enter Line2 password: ");

            Console.ForegroundColor = Color.Black;

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