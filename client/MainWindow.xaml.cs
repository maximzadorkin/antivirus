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

        public MainWindow()
        {
        }

        private void SetVisibilityMainWindow(bool visibility)
        {
            if (visibility)
            {
                this.ButtonScanner.Visibility = Visibility.Visible;
                this.ButtonMonitoring.Visibility = Visibility.Visible;
                this.ButtonPlans.Visibility = Visibility.Visible;
                this.ButtonQuarantine.Visibility = Visibility.Visible;
                this.ButtonViruses.Visibility = Visibility.Visible;
            } else
            {
                this.ButtonScanner.Visibility = Visibility.Hidden;
                this.ButtonMonitoring.Visibility = Visibility.Hidden;
                this.ButtonPlans.Visibility = Visibility.Hidden;
                this.ButtonQuarantine.Visibility = Visibility.Hidden;
                this.ButtonViruses.Visibility = Visibility.Hidden;
            }
        }

        private void HiddenAllControls()
        {
            this.WindowScanner.Visibility = Visibility.Hidden;
            this.WindowMonitoring.Visibility = Visibility.Hidden;
            this.WindowPlans.Visibility = Visibility.Hidden;
            this.WindowQuarantine.Visibility = Visibility.Hidden;
            this.WindowViruses.Visibility = Visibility.Hidden;
        }

        private void ButtonScanner_Click(object sender, RoutedEventArgs e)
        {
            this.LabelTabsName.Content = "Сканирование";
            this.SetVisibilityMainWindow(false);
            this.NavigationPanel.Visibility = Visibility.Visible;
            this.WindowScanner.Visibility = Visibility.Visible;
        }

        private void ButtonMonitoring_Click(object sender, RoutedEventArgs e)
        {
            this.LabelTabsName.Content = "Мониторинг";
            this.SetVisibilityMainWindow(false);
            this.NavigationPanel.Visibility = Visibility.Visible;
            this.WindowMonitoring.Visibility = Visibility.Visible;
        }

        private void ButtonPlans_Click(object sender, RoutedEventArgs e)
        {
            this.LabelTabsName.Content = "Запланированные проверки";
            this.SetVisibilityMainWindow(false);
            this.NavigationPanel.Visibility = Visibility.Visible;
            this.WindowPlans.Visibility = Visibility.Visible;
        }

        private void ButtonQuarantine_Click(object sender, RoutedEventArgs e)
        {
            this.LabelTabsName.Content = "Карантин";
            this.SetVisibilityMainWindow(false);
            this.NavigationPanel.Visibility = Visibility.Visible;
            this.WindowQuarantine.Visibility = Visibility.Visible;
        }
        
        private void ButtonViruses_Click(object sender, RoutedEventArgs e)
        {
            this.LabelTabsName.Content = "Найденные вирусы";
            this.SetVisibilityMainWindow(false);
            this.NavigationPanel.Visibility = Visibility.Visible;
            this.WindowViruses.Visibility = Visibility.Visible;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationPanel.Visibility = Visibility.Hidden;
            this.HiddenAllControls();
            this.SetVisibilityMainWindow(true);
        }
    }
}
