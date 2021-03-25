using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace client
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        static Mutex appIsOpen;
        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew = false;
            string isOpenMutex = "MZAntivirusClient-UIhGj8LPm";
            appIsOpen = new Mutex(true, isOpenMutex, out createdNew);
            
            if (!createdNew)
            {
                MessageBox.Show("Приложение уже запущено");
                Shutdown(1);
            }
            
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ServiceClient client = ServiceClientCreate.createClient();
            client.stopScanner();
            client.stopMonitoring();
            base.OnExit(e);
        }
    }
}
