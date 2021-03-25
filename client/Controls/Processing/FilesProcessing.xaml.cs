using ServiceDll;
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
    /// Логика взаимодействия для FilesProcessing.xaml
    /// </summary>
    public partial class FilesProcessing : UserControl
    {
        public bool hiddenAfterRepaitClick = true;
        public FilesProcessing()
        {
            InitializeComponent();
        }

        private void ButtonRepair_Click(object sender, RoutedEventArgs e)
        {
            ServiceClient client = ServiceClientCreate.createClient();
            List<FileDS> files = new List<FileDS>();
            foreach (FileProcessing fw in StackPanel.Children)
            {
                string path = (string)fw.label.Content;
                FileDS file = new FileDS(path);
                file.fileHandler = fw.selectedCommand;
                files.Add(file);
            }
            client.handlerFiles(files.ToArray());

            client.Close();
            this.StackPanel.Children.Clear();
            if (this.hiddenAfterRepaitClick) this.Visibility = Visibility.Hidden;
        }
    }
}
