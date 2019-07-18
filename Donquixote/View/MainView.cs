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

            //Attack();

            //SaveLogs();

            Console.ReadKey(true);
        }

        private void SetSession()
        {
            MainController.SetConsoleTitle();

            MainController.DisplaySoftwareName();

            MainController.ImportPhones();

            MainController.SelectModeSpeed(1);

            MainController.SelectModeSpeed(2);

            MainController.SetMessage();

            MainController.Login();
        }

        //private void Attack() => MainController.Attack();

        //private void SaveLogs() => MainController.SaveLogs();
    }
}
