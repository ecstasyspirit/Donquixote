﻿using Donquixote.Models;
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

        public static string GenerateTimestamp() => $" {DateTime.Now.ToString("dd/MM | HH:mm:ss")}{new string(' ', 4)}";

        public void ImportPhones()
        {
            Console.Write(GenerateTimestamp() + "Importing phone numbers from 'phones.txt' ...");

            switch (MainModel.ImportPhones())
            {
                case true:
                    Console.WriteLine(" ✓", Color.FromArgb(234, 153, 200));
                    break;

                case false:
                    Console.WriteLine(" X", Color.FromArgb(194, 53, 200));
                    Console.WriteLine(GenerateTimestamp() + "No 'phones.txt' file found in the startup directory.");

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
                case 1:
                    Console.Write($"{GenerateTimestamp()}Available modes: ");
                    Console.WriteLine(string.Join(", ", availableModes), Color.FromArgb(234, 153, 200));
                    Console.Write($"{GenerateTimestamp()}What mode do you want to use? ");
                    Console.Write(availableModes[iOptionIndex], Color.FromArgb(234, 153, 200));
                    break;
                case 2:
                    Console.Write($"{GenerateTimestamp()}Available speeds: ");
                    Console.WriteLine(string.Join(", ", availableSpeeds), Color.FromArgb(234, 153, 200));
                    Console.Write($"{GenerateTimestamp()}What speed do you want to use? ");
                    Console.Write(availableSpeeds[iOptionIndex], Color.FromArgb(234, 153, 200));
                    break;
            }

            Console.ForegroundColor = Color.FromArgb(234, 153, 200);

            while (true)
            {
                var oKeyDown = Console.ReadKey().Key;

                if (oKeyDown == ConsoleKey.Enter)
                {
                    Console.Write("\n");
                    break;
                }
                else if (oKeyDown == ConsoleKey.Backspace)
                {
                    switch (mode)
                    {
                        case 1:
                            Console.Write(availableModes[iOptionIndex].ToString().Substring(availableModes[iOptionIndex].ToString().Length - 1, 1));
                            break;
                        case 2:
                            Console.Write(availableSpeeds[iOptionIndex].ToString().Substring(availableSpeeds[iOptionIndex].ToString().Length - 1, 1));
                            break;
                    }
                }
                else
                {
                    switch (mode)
                    {
                        case 1:
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
                        case 2:
                            Console.Write(new string('\b', availableSpeeds[iOptionIndex].ToString().Length + 1)
                                          + new string(' ', availableSpeeds[iOptionIndex].ToString().Length + 1)
                                          + new string('\b', availableSpeeds[iOptionIndex].ToString().Length + 1));

                            switch (oKeyDown)
                            {
                                case ConsoleKey.LeftArrow:
                                case ConsoleKey.DownArrow:
                                    if (iOptionIndex == availableSpeeds.Length - 1)
                                        iOptionIndex = 0;
                                    else
                                        iOptionIndex++;
                                    break;
                                case ConsoleKey.RightArrow:
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

            Console.ResetColor();
        }

        public void SetMessage()
        {
            Console.Write(GenerateTimestamp() + "Enter the message to use for the attack: ");

            Console.ForegroundColor = Color.FromArgb(234, 153, 200);

            var inputBuffer = new byte[2048];
            var inputStream = Console.OpenStandardInput(inputBuffer.Length);
            Console.SetIn(new StreamReader(inputStream, Console.InputEncoding, false, inputBuffer.Length));

            MainModel.PayloadMessage = Console.ReadLine();

            Console.ResetColor();

            Console.WriteLine(GenerateTimestamp() + "This is how the message will appear on the victims' devices:");
            Console.WriteLine($"{new string(' ', 21)}>>");
            Console.WriteLine(new string(' ', 21) + MainModel.PayloadMessage.Replace("\\n", "\n" + new string(' ', 21)), Color.FromArgb(234, 153, 200));
            Console.WriteLine($"{new string(' ', 21)}<<");
        }

        public void Login()
        {
            Console.Write(GenerateTimestamp() + "Enter Line2 phone number: ");

            Console.ForegroundColor = Color.FromArgb(234, 153, 200);

            var phone = Console.ReadLine();

            Console.ResetColor();

            Console.Write(GenerateTimestamp() + "Enter Line2 password: ");

            Console.ForegroundColor = Color.FromArgb(234, 153, 200);

            var password = Console.ReadLine();

            Console.ResetColor();

            Console.Write(GenerateTimestamp() + "Connecting ...");

            switch (MainModel.Login(phone, password))
            {
                case true:
                    Console.WriteLine(" ✓", Color.FromArgb(234, 153, 200));
                    break;

                case false:
                    Console.WriteLine(" X", Color.FromArgb(194, 53, 200));
                    Console.WriteLine(GenerateTimestamp() + "Login failed.");

                    Console.ReadKey(true);

                    Environment.Exit(0);
                    break;
            }
        }

        public void StartAttack()
        {
            Console.Write(GenerateTimestamp() + "Press any key to start the attack ...");

            Console.ReadKey(true);

            Console.Clear();

            DisplaySoftwareName();

            var checkerResult = MainModel.SendMessages().Result;

            Console.ResetColor();

            SetConsoleTitle();

            Console.WriteLine($"\n{GenerateTimestamp()}Checking completed!");
            Console.Write($"{GenerateTimestamp()}Sent ");
            Console.Write(checkerResult.PhonesMessaged, Color.FromArgb(234, 153, 200));
            Console.Write($" messages to ");
            Console.Write(checkerResult.PhonesLoaded, Color.FromArgb(234, 153, 200));
            Console.Write("phones in ");
            Console.Write(checkerResult.AttackMode, Color.FromArgb(234, 153, 200));
            Console.Write(" mode at ");
            Console.Write(checkerResult.AttackSpeed, Color.FromArgb(234, 153, 200));
            Console.Write(" speed || (");
            Console.Write(checkerResult.PhoneSkiped, Color.FromArgb(194, 53, 200));
            Console.WriteLine(" skipped).");
        }
    }
}