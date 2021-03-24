using Microsoft.WindowsAPICodePack.Dialogs;
using ServiceDll;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
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
    /// Логика взаимодействия для Scanner.xaml
    /// </summary>
    public partial class Scanner : UserControl
    {
        private string scanPath = "";

        public Scanner()
        {
            InitializeComponent();
            Result.Visibility = Visibility.Hidden;
        }

        private void printResults()
        {
            ServiceClient client = ServiceClientCreate.createClient();
            string results = client.getScanResult();
            client.Close();

            string[] lines = results.Split('\n');
            string description = $"{lines[0]}\n{lines[1]}\n{lines[2]}";
            Result.Label.Content = description;

            if (lines.Length <= 3) return;

            Result.StackPanel.Children.Clear();
            for (int i = 3; i < lines.Length; i += 1)
            {
                string line = lines.ElementAt(i);
                if (line.Length == 0) break;
                Result.StackPanel.Children.Add(new FileProcessing(line));
            }

            Label label = (Label)StatusBar.Items.GetItemAt(0);
            ProgressBar progressBar = (ProgressBar)this.StatusBar.Items.GetItemAt(2);
            label.Content = "";
            progressBar.Value = 100;
        }

        private void logger(IProgress<string[]> progressSender)
        {
            ServiceClient client = ServiceClientCreate.createClient();
            string results = client.getScanResult();
            client.Close();
            
            string[] lines = results.Split('\n');

            string description = $"{lines[0]}\n{lines[1]}\n{lines[2]}";
            
            double totalFiles = Double.Parse(lines[0].Split(':')[1]);
            double checkedFiles = Double.Parse(lines[1].Split(':')[1]);

            double full = 100;
            int progress = (int)Math.Round(full * (checkedFiles / totalFiles));

            string[] result = { description, progress.ToString() };

            progressSender.Report(result);
        }

        async private void start()
        {
            ServiceClient client = ServiceClientCreate.createClient();
            client.startScanner(this.scanPath);
            Result.Visibility = Visibility.Visible;
            client.Close();

            Label label = (Label)StatusBar.Items.GetItemAt(0);
            ProgressBar progressBar = (ProgressBar)this.StatusBar.Items.GetItemAt(2);

            var progress = new Progress<string[]>(r => {
                label.Content = r[0];
                progressBar.Value = Int32.Parse(r[1]);
            });

            await Task.Run(() =>
            {
                ServiceClient local = ServiceClientCreate.createClient();
                while (local.getScanStatus()) {
                    this.logger(progress);
                    Thread.Sleep(1000);
                }
                local.Close();
            });

            this.stop();
        }

        private void stop()
        {
            ServiceClient client = ServiceClientCreate.createClient();
            client.stopScanner();
            client.Close();

            ButtonPower.Content = "Начать сканирование";
            Result.Visibility = Visibility.Visible;
            
            this.printResults();
        }

        private bool choosePath(bool isFile)
        {
            string path = openPathDialog(isFile);
            if (path.Length == 0) return false;

            LabelSelectedPath.Content = path;
            this.scanPath = path;
            return true;
        }

        private string openPathDialog(bool isFile)
        {
            string title = isFile ? "Выбрать файл" : "Выбрать директорию";

            var dlg = new CommonOpenFileDialog();
            dlg.Title = title;
            dlg.IsFolderPicker = !isFile;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                return dlg.FileName;

            return "";
        }




        private void ButtonChoseFile_Click(object sender, RoutedEventArgs e) { 
            bool isChoosed = this.choosePath(true);
            if (isChoosed)
                this.ButtonPower.IsEnabled = true;
        }

        private void ButtonChoseDir_Click(object sender, RoutedEventArgs e) {
            bool isChoosed = this.choosePath(false);
            if (isChoosed)
                this.ButtonPower.IsEnabled = true;
        }

        private void ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            string startScan = "Начать сканирование";
            string stopScan = "Остановить сканирование";

            bool isStart = (string)ButtonPower.Content == startScan;
            if (isStart)
            {
                Label label = (Label)this.StatusBar.Items.GetItemAt(0);
                ProgressBar progressBar = (ProgressBar)this.StatusBar.Items.GetItemAt(2);
                label.Content = "Сканирование началось...";
                progressBar.Value = 0;
                ButtonPower.Content = stopScan;
                Result.StackPanel.Children.Clear();
                Result.Label.Content = "";
                this.start();
            } else
            {
                Label label = (Label)this.StatusBar.Items.GetItemAt(0);
                ProgressBar progressBar = (ProgressBar)this.StatusBar.Items.GetItemAt(2);
                label.Content = "";
                progressBar.Value = 0;
                ButtonPower.Content = startScan;
                this.stop();
            }
        }
    }
}
