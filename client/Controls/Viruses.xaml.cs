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
    /// Логика взаимодействия для Viruses.xaml
    /// </summary>
    public partial class Viruses : UserControl
    {
        public Viruses()
        {
            InitializeComponent();
            Window.hiddenAfterRepaitClick = false;
            Window.Label.Height = 0;
            this.updateList();
        }

        private void updateList()
        {
            ServiceClient client = ServiceClientCreate.createClient();
            string[] viruses = client.getVirusesFiles();
            client.Close();

            Window.Visibility = Visibility.Visible;
            Window.StackPanel.Children.Clear();
            foreach (string virus in viruses)
            {
                Window.StackPanel.Children.Add(new FileProcessing(virus));
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.updateList();
        }
    }
}
