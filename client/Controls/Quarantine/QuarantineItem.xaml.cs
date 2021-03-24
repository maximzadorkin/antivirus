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
using static ServiceDll.FileDS;

namespace client
{
    /// <summary>
    /// Логика взаимодействия для QuarantineItem.xaml
    /// </summary>
    public partial class QuarantineItem : UserControl
    {

        public QuarantineItem(string path)
        {
            InitializeComponent();
            TextBlock.Text = path;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            ServiceClient client = ServiceClientCreate.createClient();
            FileDS file = new FileDS(TextBlock.Text);
            file.fileHandler = FilesHandler.RemoveFromQuarantine;
            List<FileDS> files = new List<FileDS>();
            files.Add(file);
            client.handlerFiles(files.ToArray());
            client.Close();
        }
    }
}
