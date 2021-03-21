using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;
using ServiceReference;

namespace client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ServiceClient client = null;

        public MainWindow()
        {
            bool clientIsStarted = MainWindow.createClient();
            if (clientIsStarted)
                InitializeComponent();
            else
                Environment.Exit(0);
        }

        static private bool createClient()
        {
            try
            {
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport);

                binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
                binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

                string uri = "net.tcp://192.168.1.2:9002/AntivirusZMService";

                EndpointAddress endpoint = new EndpointAddress(new Uri(uri));

                client = new ServiceClient(binding, endpoint);

                client.ClientCredentials.Windows.ClientCredential.Domain = "";

                string test = client.Method1("test");

                if (test.Length < 1) {
                    throw new Exception("Проверка соединения не удалась");
                }
            }
            catch (Exception _ex)
            {
                client = null;
                return false;
            }
            return true;
        }
    }
}
