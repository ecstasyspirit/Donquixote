using BetterHttpClient;
using Donquixote.Models.ConnectionsModels;
using Donquixote.Models.DataStructuresModels.DataModels;
using Donquixote.Models.DataStructuresModels.EnumModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace Donquixote.Models
{
    public class MainModel
    {
        public string MaliciousMessage = "";
        public string AccessToken = "";

        public StatusEnumModel CurrentStatus = StatusEnumModel.Setup;
        public ConnectionEnumModel SelectedConnection = ConnectionEnumModel.Direct;
        public ModeEnumModel SelectedMode = ModeEnumModel.Spam;
        public SpeedEnumModel SelectedSpeed = SpeedEnumModel.Normal;

        public int MessengerRecursivity = 0;
        public int PhoneNumbersLoaded = 0;
        public int PhoneNumbersMessengerIndex = 0;
        public int PhoneNumbersMessaged = 0;
        public int PhoneNumbersSkiped = 0;
        public int ReEnqueuedPhoneNumbers = 0;

        public object DisplayLock = new object();

        public Random RandomProxySelector = new Random((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);

        public ConcurrentQueue<PhoneDataModel> PhoneNumbers = new ConcurrentQueue<PhoneDataModel>();

        public List<string> Proxies = new List<string>();

        public List<PhoneDataModel> MessagingFailed = new List<PhoneDataModel>();

        public LoginModel LoginModel = new LoginModel();
        public MessageModel MessageModel = new MessageModel();

        public static string GenerateTimestamp() => $" {DateTime.Now.ToString("dd/MM | HH:mm:ss")}{new string(' ', 4)}";

        public float CalculateProgress()
        {
            var progressRate = 0f;

            if (PhoneNumbersMessengerIndex > progressRate)
                progressRate = PhoneNumbersMessengerIndex * 100 / PhoneNumbersLoaded;

            return progressRate;
        }

        public void SetConsoleTitle()
        {
            switch (CurrentStatus)
            {
                case StatusEnumModel.Setup:
                case StatusEnumModel.Finished:
                    Console.Title = $"Donquixote :: {CurrentStatus}";
                    break;

                case StatusEnumModel.Attacking:
                    Console.Title = $"Donquixote :: {CurrentStatus} | Messaged {PhoneNumbersMessengerIndex} on {PhoneNumbersLoaded} + {ReEnqueuedPhoneNumbers} retries | Failed to message {MessagingFailed.Count} | {CalculateProgress()}% done";
                    break;
            }
        }

        public bool ImportPhones()
        {
            var fileName = "numbers.txt";

            if (File.Exists(fileName))
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    var line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line.Length < 1 || line.Length > 10)
                            line = streamReader.ReadLine();
                        else
                        {
                            PhoneNumbers.Enqueue(
                                new PhoneDataModel()
                                {
                                    Number = line
                                });

                            line = streamReader.ReadLine();
                        }
                    }
                }

                PhoneNumbersLoaded = PhoneNumbers.Count;

                if (PhoneNumbers.Count > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public bool ImportProxies()
        {
            var fileName = "proxies.txt";

            if (File.Exists(fileName))
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    var line = streamReader.ReadLine();

                    while (line != null)
                    {
                        if (line.Length < 1)
                            line = streamReader.ReadLine();
                        else
                        {
                            Proxies.Add(line);

                            line = streamReader.ReadLine();
                        }
                    }
                }

                if (Proxies.Count > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public Task<AttackResultDataModel> SendMessages()
        {
            CurrentStatus = StatusEnumModel.Attacking;

            SetConsoleTitle();

            SpawnThreads();

            var result = new AttackResultDataModel()
            {
                AttackMode = SelectedMode,
                AttackSpeed = SelectedSpeed,
                PhonesLoaded = PhoneNumbersLoaded,
                PhonesMessaged = PhoneNumbersMessaged,
                PhoneSkiped = PhoneNumbersSkiped
            };

            CurrentStatus = StatusEnumModel.Finished;

            return Task.FromResult(result);
        }

        public void SpawnThreads()
        {
            var messaging = true;
            var workerThreads = 0;
            var workingThreads = 1;

            if (workingThreads > PhoneNumbers.Count)
                workingThreads = PhoneNumbers.Count;

            ThreadPool.SetMinThreads(workingThreads, 0);
            ThreadPool.SetMaxThreads(workingThreads, 0);

            for (int i = 0; i < workingThreads; i++)
            {
                Thread.Sleep(5);

                ThreadPool.QueueUserWorkItem(new WaitCallback(AttackAssistant));
            }

            ThreadPool.GetMaxThreads(out int maxWorkerThreads, out _);

            while (messaging)
            {
                ThreadPool.GetAvailableThreads(out workerThreads, out _);

                if (workerThreads == maxWorkerThreads)
                    messaging = false;
                else
                    Thread.Sleep(500);

                SetConsoleTitle();
            }
        }

        public void AttackAssistant(object _)
        {
            var client = new HttpClient()
            {
                UserAgent = "Line2/12.3 (iPad; iOS 11.2.5; Scale/2.00)",
                Accept = "*/*",
                AcceptEncoding = "br, gzip, deflate",
                AcceptLanguage = "en;q=1",
                Timeout = TimeSpan.FromSeconds(6),
                NumberOfAttempts = 3,
                Proxy = new Proxy(Proxies[RandomProxySelector.Next(0, Proxies.Count - 1)])
            };

            var successSpaceCount = Console.BufferWidth - 56;
            var failureSpaceCount = Console.BufferWidth - 52;

            switch (SelectedMode)
            {
                case ModeEnumModel.Spam:

                    while (!PhoneNumbers.IsEmpty)
                    {
                        PhoneNumbers.TryDequeue(out var phone);

                        if (phone.Number == null || phone.Number == string.Empty || phone.NumberOfAttempts >= 3)
                            return;

                        switch (MessageModel.SendMessage(client, AccessToken, phone.Number, MaliciousMessage))
                        {
                            case 0:
                                DisplayStatus(0, phone.Number, successSpaceCount);
                                break;

                            case 1:
                                DisplayStatus(1, phone.Number, failureSpaceCount);

                                if (phone.NumberOfAttempts < 3)
                                {
                                    PhoneNumbers.Enqueue(phone);

                                    ReEnqueuedPhoneNumbers++;
                                }
                                else
                                    MessagingFailed.Add(phone);
                                break;
                        }

                        PhoneNumbersMessengerIndex++;

                        Thread.Sleep((int)SelectedSpeed);
                    }
                    break;

                case ModeEnumModel.Bomb:
                    while (!PhoneNumbers.IsEmpty)
                    {
                        PhoneNumbers.TryDequeue(out var phone);

                        if (phone.Number == null || phone.Number == string.Empty)
                            return;

                        for (var i = 0; i < MessengerRecursivity; i++)
                        {
                            switch (MessageModel.SendMessage(client, AccessToken, phone.Number, MaliciousMessage))
                            {
                                case 0:
                                    DisplayStatus(0, phone.Number, successSpaceCount);
                                    break;

                                case 1:
                                    DisplayStatus(1, phone.Number, failureSpaceCount);

                                    if (phone.NumberOfAttempts < 3)
                                    {
                                        PhoneNumbers.Enqueue(phone);

                                        ReEnqueuedPhoneNumbers++;
                                    }
                                    else
                                        MessagingFailed.Add(phone);
                                    break;
                            }

                            Thread.Sleep((int)SelectedSpeed);
                        }

                        PhoneNumbersMessengerIndex++;
                    }
                    break;
            }
        }

        public void DisplayStatus(int status, string phoneNumber, int spaceCount)
        {
            lock (DisplayLock)
            {
                switch (status)
                {
                    case 0:
                        Console.Write($"{GenerateTimestamp()}Messaged {phoneNumber} successfully.{new string(' ', spaceCount)}");
                        Console.WriteLine("✓", Color.FromArgb(234, 153, 200));
                        break;

                    case 1:
                        Console.Write($"{GenerateTimestamp()}Failed to message {phoneNumber}.{new string(' ', spaceCount)}");
                        Console.WriteLine("X", Color.FromArgb(194, 53, 200));
                        break;
                }
            }
        }
    }
}