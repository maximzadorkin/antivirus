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

            this.ButtonPower.IsEnabled = true;
        }

        private void ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            this.monitoringStarted = !this.monitoringStarted;
            ButtonPower.Content = this.monitoringStarted ? "Остановить" : "Запуск";
            if (this.monitoringStarted) {
                // запустить мониторинг
                Results.Visibility = Visibility.Hidden;
            } else {
                // остановить мониторинг
                //foreach () {
                //    Results.StackPanel.Children.Add();
                //}
                Results.Visibility = Visibility.Visible;
            }
        }
    }
}
