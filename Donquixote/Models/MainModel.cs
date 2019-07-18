using Donquixote.Models.DataStructuresModels.EnumModels;
using Donquixote.Models.DataStructuresModels.DataModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Donquixote.Models
{
    public class MainModel
    {
        #region Public variables
        public string PayloadMessage = "";
        public string AccessToken = "";

        public StatusEnumModel CurrentStatus = StatusEnumModel.Setup;
        public ModeEnumModel SelectedMode = ModeEnumModel.Spam;
        public SpeedEnumModel SelectedSpeed = SpeedEnumModel.Normal;

        public int PhonesLoaded = 0;
        public int PhonesMessengerIndex = 0;
        public int PhonesMessaged = 0;
        public int PhonesSkiped = 0;

        public ConcurrentQueue<PhoneDataModel> Phones = new ConcurrentQueue<PhoneDataModel>();

        public List<PhoneDataModel> MessageFailed = new List<PhoneDataModel>();
        #endregion

        public float CalculateProgress()
        {
            var progressRate = 0f;

            if (PhonesMessengerIndex > progressRate)
                progressRate = PhonesMessengerIndex * 100 / PhonesLoaded;

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
                    Console.Title = $"Donquixote :: {CurrentStatus} | Messaged {PhonesMessengerIndex} on {PhonesLoaded} | {CalculateProgress()}% checked";
                    break;
            }
        }

        public bool ImportPhones()
        {
            var fileName = "phones.txt";

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
                            Phones.Enqueue(
                                new PhoneDataModel()
                                {
                                    Number = line
                                });

                            line = streamReader.ReadLine();
                        }
                    }
                }

                PhonesLoaded = Phones.Count;

                if (Phones.Count > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public bool Login(string phone, string password)
        {
            Thread.Sleep(5000);

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
                PhonesLoaded = PhonesLoaded,
                PhonesMessaged = PhonesMessaged,
                PhoneSkiped = PhonesSkiped
            };

            CurrentStatus = StatusEnumModel.Finished;

            return Task.FromResult(result);
        }

        public void SpawnThreads()
        {
            var checking = true;
            var workerThreads = 0;
            var checkingThreads = 5;

            if (checkingThreads > Phones.Count)
                checkingThreads = Phones.Count;

            ThreadPool.SetMinThreads(checkingThreads, 0);
            ThreadPool.SetMaxThreads(checkingThreads, 0);

            for (var i = 0; i < 3600; i++)
            {
                Phones.TryDequeue(out _);

                PhonesMessengerIndex++;
            }

            for (int i = 0; i < checkingThreads; i++)
            {
                Thread.Sleep(5);

                //ThreadPool.QueueUserWorkItem(new WaitCallback(RequestDetails));
            }

            ThreadPool.GetMaxThreads(out int maxWorkerThreads, out _);

            while (checking)
            {
                ThreadPool.GetAvailableThreads(out workerThreads, out _);

                if (workerThreads == maxWorkerThreads)
                    checking = false;
                else
                    Thread.Sleep(500);

                SetConsoleTitle();
            }
        }
    }
}
