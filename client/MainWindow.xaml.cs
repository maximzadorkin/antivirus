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

                //string test = client.Method1("test");

                //if (test.Length < 1) {
                //    throw new Exception("Проверка соединения не удалась");
                //}
            }
            catch (Exception _ex)
            {
                client = null;
                return false;
            }
            return true;
        }

        private void SetVisibilityMainWindow(bool visibility)
        {
            if (visibility)
            {
                this.ButtonScanner.Visibility = Visibility.Visible;
                this.ButtonMonitoring.Visibility = Visibility.Visible;
                this.ButtonPlans.Visibility = Visibility.Visible;
                this.ButtonQuarantine.Visibility = Visibility.Visible;
            } else
            {
                this.ButtonScanner.Visibility = Visibility.Hidden;
                this.ButtonMonitoring.Visibility = Visibility.Hidden;
                this.ButtonPlans.Visibility = Visibility.Hidden;
                this.ButtonQuarantine.Visibility = Visibility.Hidden;
            }
        }

        private void HiddenAllControls()
        {
            this.WindowScanner.Visibility = Visibility.Hidden;
            this.WindowMonitoring.Visibility = Visibility.Hidden;
            this.WindowPlans.Visibility = Visibility.Hidden;
            this.WindowQuarantine.Visibility = Visibility.Hidden;
        }

        private void ButtonScanner_Click(object sender, RoutedEventArgs e)
        {
            this.SetVisibilityMainWindow(false);
            this.NavigationPanel.Visibility = Visibility.Visible;
            this.WindowScanner.Visibility = Visibility.Visible;
        }

        private void ButtonMonitoring_Click(object sender, RoutedEventArgs e)
        {
            this.SetVisibilityMainWindow(false);
            this.NavigationPanel.Visibility = Visibility.Visible;
            this.WindowMonitoring.Visibility = Visibility.Visible;
        }

        private void ButtonPlans_Click(object sender, RoutedEventArgs e)
        {
            this.SetVisibilityMainWindow(false);
            this.NavigationPanel.Visibility = Visibility.Visible;
            this.WindowPlans.Visibility = Visibility.Visible;
        }

        private void ButtonQuarantine_Click(object sender, RoutedEventArgs e)
        {
            this.SetVisibilityMainWindow(false);
            this.NavigationPanel.Visibility = Visibility.Visible;
            this.WindowQuarantine.Visibility = Visibility.Visible;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationPanel.Visibility = Visibility.Hidden;
            this.HiddenAllControls();
            this.SetVisibilityMainWindow(true);
        }

        private void ButtonCloseApp_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
