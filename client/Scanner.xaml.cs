using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public Scanner()
        {
            InitializeComponent();
            ScanStackPanelResult.Visibility = Visibility.Hidden;
        }

        private void startScan()
        {
            Label label = (Label)this.ScanStatusBar.Items.GetItemAt(0);
            ProgressBar progressBar = (ProgressBar)this.ScanStatusBar.Items.GetItemAt(2);

            ScanStackPanelResult.Visibility = Visibility.Visible;

            // logic of scan ScanStackPanelResult.Children.Add(new FileProcessing("path"));
        }

        private void startRepair()
        {
            Label label = (Label)this.ScanStatusBar.Items.GetItemAt(0);
            ProgressBar progressBar = (ProgressBar)this.ScanStatusBar.Items.GetItemAt(2);
        }

        private void choosePath(bool isFile)
        {
            string path = openPathDialog(isFile);
            if (path.Length == 0) return;

            ScanLabelSelectedPath.Content = path;
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




        private void ScanButtonChoseFile_Click(object sender, RoutedEventArgs e) { 
            this.choosePath(true);
            this.ScanButtonPower.IsEnabled = true;
            this.ScanStatusBar.Visibility = Visibility.Hidden;
        }

        private void ScanButtonChoseDir_Click(object sender, RoutedEventArgs e) { 
            this.choosePath(false);
            this.ScanButtonPower.IsEnabled = true;
            this.ScanStatusBar.Visibility = Visibility.Hidden;
        }

        private void ScanButtonPower_Click(object sender, RoutedEventArgs e)
        {
            string startScan = "Начать сканирование";
            string stopScan = "Остановить сканирование";

            bool isStart = (string)ScanButtonPower.Content == startScan;
            if (isStart)
            {
                Label label = (Label)this.ScanStatusBar.Items.GetItemAt(0);
                ProgressBar progressBar = (ProgressBar)this.ScanStatusBar.Items.GetItemAt(2);
                label.Content = "Сканирование началось...";
                progressBar.Value = 1;
                
                this.ScanButtonPower.Content = stopScan;

                ScanStackPanelResult.Children.Clear();
                ScanLabelResult.Content = "";
                this.ScanButtonRepair.IsEnabled = false;

                this.startScan();
            } else
            {
                Label label = (Label)this.ScanStatusBar.Items.GetItemAt(0);
                ProgressBar progressBar = (ProgressBar)this.ScanStatusBar.Items.GetItemAt(2);
                label.Content = "";
                progressBar.Value = 0;

                this.ScanButtonPower.Content = startScan;

                this.ScanButtonRepair.IsEnabled = true;
            }
        }

        private void ScanButtonRepair_Click(object sender, RoutedEventArgs e)
        {
            this.ScanButtonChoseFile.IsEnabled = false;
            this.ScanButtonChoseDir.IsEnabled = false;
            this.ScanButtonPower.IsEnabled = false;
            this.ScanButtonRepair.IsEnabled = false;


            Label label = (Label)this.ScanStatusBar.Items.GetItemAt(0);
            ProgressBar progressBar = (ProgressBar)this.ScanStatusBar.Items.GetItemAt(2);
            label.Content = "Выполнение...";
            progressBar.Value = 1;

            this.startRepair();


            this.ScanButtonChoseFile.IsEnabled = true;
            this.ScanButtonChoseDir.IsEnabled = true;
            this.ScanButtonPower.IsEnabled = true;
        }
    }
}
