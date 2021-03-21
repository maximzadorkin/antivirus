using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace WindowsService
{
    [RunInstaller(true)]
    public partial class AntivirusServiceInstaller : System.Configuration.Install.Installer
    {
        public AntivirusServiceInstaller()
        {
            // InitializeComponent();
            serviceProcessInstaller = new ServiceProcessInstaller();
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller1 = new ServiceInstaller();
            serviceInstaller1.ServiceName = "AntivirusZM";
            serviceInstaller1.DisplayName = "AntivirusZM";
            serviceInstaller1.Description = "AntivirusZM";
            serviceInstaller1.StartType = ServiceStartMode.Automatic;
            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller1);
        }
    }
}
