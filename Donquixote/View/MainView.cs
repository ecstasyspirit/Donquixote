using Donquixote.Controller;
using System;

namespace Donquixote.View
{
    public class MainView
    {
        public MainController MainController = new MainController();

        public MainView()
        {
            SetSession();

            Attack();

            SaveLogs();

            Console.ReadKey(true);
        }

        public void SetSession()
        {
            MainController.SetConsoleTitle();

            MainController.DisplaySoftwareName();

            MainController.ImportPhones();

            MainController.SelectModeSpeed(0);

            MainController.SelectModeSpeed(1);

            MainController.SetMessage();

            MainController.SetRecursivity();

            MainController.Login();
        }

        public void Attack() => MainController.Attack();

        public void SaveLogs() => MainController.SaveLogs();
    }
}
