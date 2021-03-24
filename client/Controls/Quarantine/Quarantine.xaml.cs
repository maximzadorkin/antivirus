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

namespace client
{
    /// <summary>
    /// Логика взаимодействия для Quarantine.xaml
    /// </summary>
    public partial class Quarantine : UserControl
    {
        public Quarantine()
        {
            InitializeComponent();
            this.alwaysGetQuarantineList();
        }

        async private void alwaysGetQuarantineList()
        {
            var progress = new Progress<string[]>(viruses => {
                StackPanel.Children.Clear();
                foreach (string virus in viruses)
                {
                    StackPanel.Children.Add(new QuarantineItem(virus));
                }
            });

            await Task.Run(() =>
            {
                while (true)
                {
                    ServiceClient client = ServiceClientCreate.createClient();
                    string[] viruses = client.getQuarantineList();
                    this.setViruses(progress, viruses);
                    client.Close();
                    Thread.Sleep(1000);
                }
            });
        }

        private void setViruses(IProgress<string[]> progressSender, string[] viruses)
        {
            progressSender.Report(viruses);
        }

        public void GetQuarantineList()
        {
            ServiceClient client = ServiceClientCreate.createClient();
            string[] viruses = client.getQuarantineList();
            StackPanel.Children.Clear();
            foreach (string virus in viruses)
            {
                StackPanel.Children.Add(new QuarantineItem(virus));
            }
            client.Close();
        }
    }
}
