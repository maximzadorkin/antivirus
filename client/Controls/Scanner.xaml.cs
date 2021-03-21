using Microsoft.WindowsAPICodePack.Dialogs;
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
        private bool isFileScan;
        private string scanPath = "";
        static int size = 2048;


        public Scanner()
        {
            InitializeComponent();
            Result.Visibility = Visibility.Hidden;
        }

        private void startScan()
        {
            Label label = (Label)this.StatusBar.Items.GetItemAt(0);
            ProgressBar progressBar = (ProgressBar)this.StatusBar.Items.GetItemAt(2);

            // logic of scan ScanResult.StackPanel.Children.Add(new FileProcessing("path"));
            Result.Visibility = Visibility.Visible;
        }

        private void startRepair()
        {
            Label label = (Label)this.StatusBar.Items.GetItemAt(0);
            ProgressBar progressBar = (ProgressBar)this.StatusBar.Items.GetItemAt(2);
        }

        private void choosePath(bool isFile)
        {
            string path = openPathDialog(isFile);
            if (path.Length == 0) return;

            LabelSelectedPath.Content = path;
            this.isFileScan = isFile;
            this.scanPath = path;
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
            this.choosePath(true);
            this.ButtonPower.IsEnabled = true;
        }

        private void ButtonChoseDir_Click(object sender, RoutedEventArgs e) { 
            this.choosePath(false);
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
                progressBar.Value = 1;
                
                this.ButtonPower.Content = stopScan;

                Result.StackPanel.Children.Clear();
                Result.Label.Content = "";
                Result.ButtonRepair.IsEnabled = false;

                this.startScan();
            } else
            {
                Label label = (Label)this.StatusBar.Items.GetItemAt(0);
                ProgressBar progressBar = (ProgressBar)this.StatusBar.Items.GetItemAt(2);
                label.Content = "";
                progressBar.Value = 0;

                this.ButtonPower.Content = startScan;

                Result.ButtonRepair.IsEnabled = true;
            }
        }

        private void ButtonRepair_Click(object sender, RoutedEventArgs e)
        {
            this.ButtonChoseFile.IsEnabled = false;
            this.ButtonChoseDir.IsEnabled = false;
            this.ButtonPower.IsEnabled = false;
            Result.ButtonRepair.IsEnabled = false;


            Label label = (Label)this.StatusBar.Items.GetItemAt(0);
            ProgressBar progressBar = (ProgressBar)this.StatusBar.Items.GetItemAt(2);
            label.Content = "Выполнение...";
            progressBar.Value = 1;

            this.startRepair();


            this.ButtonChoseFile.IsEnabled = true;
            this.ButtonChoseDir.IsEnabled = true;
            this.ButtonPower.IsEnabled = true;
        }
    }
}
