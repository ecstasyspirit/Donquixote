using Donquixote.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donquixote.View
{
    class MainView
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

            //MainController.ImportPhones();

            MainController.SelectModeSpeed(1);

            MainController.SelectModeSpeed(2);
        }

        //private void Attack() => MainController.Attack();

        //private void SaveLogs() => MainController.SaveLogs();
    }
}
