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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace client
{
    /// <summary>
    /// Логика взаимодействия для Monitoring.xaml
    /// </summary>
    public partial class Monitoring : UserControl
    {
        private string path = null;
        private bool monitoringStarted = false;
        public Monitoring()
        {
            InitializeComponent();
        }

        private void ButtonChoseDir_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Выбрать директорию для мониторинга";
            dlg.IsFolderPicker = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                this.path = dlg.FileName;

            this.LabelPath.Content = this.path;
            this.ButtonPower.IsEnabled = true;
        }

        async private void ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            this.monitoringStarted = !this.monitoringStarted;
            ButtonPower.Content = this.monitoringStarted ? "Остановить" : "Запуск";
            ServiceClient client = ServiceClientCreate.createClient();
            if (this.monitoringStarted) {
                // запустить мониторинг
                client.startMonitoring(this.path);
                Results.ButtonRepair.IsEnabled = false;
                Results.Visibility = Visibility.Visible;
            } else {
                // остановить мониторинг
                client.stopMonitoring();
                Results.ButtonRepair.IsEnabled = true;
            }
            client.Close();

            var progress = new Progress<string[]>(log => {
                string information = log[0];
                Results.Label.Content = information;
                if (log.Length < 3) return;
                Results.StackPanel.Children.Clear();
                for (int i = 1; i < log.Length; i += 1)
                {
                    string virus = log[i];
                    if (virus.Length == 0) break;

                    Results.StackPanel.Children.Add(
                        new FileProcessing(virus)
                    );
                }
            });

            await Task.Run(() =>
            {
                ServiceClient local = ServiceClientCreate.createClient();
                while (local.getMonitoringStatus())
                {
                    this.logger(progress);
                    Thread.Sleep(1000);
                }
                local.Close();
            });
        }

        private void logger(IProgress<string[]> progress)
        {
            ServiceClient client = ServiceClientCreate.createClient();
            string log = client.logMonitoring();
            client.Close();
            progress.Report(log.Split('\n'));
        }
    }
}
