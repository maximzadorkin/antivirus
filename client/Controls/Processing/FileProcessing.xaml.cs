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
    /// Логика взаимодействия для FileProcessing.xaml
    /// </summary>
    public partial class FileProcessing : UserControl
    {
        public string selectedCommand = "000";

        public FileProcessing(string filepath)
        {
            InitializeComponent();
            label.Content = filepath;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rbtn = (RadioButton)e.Source;
            switch (rbtn.Content)
            {
                default:
                    selectedCommand = "000";
                    return;
                case "В карантин":
                    selectedCommand = "001";
                    return;
                case "Удалить":
                    selectedCommand = "002";
                    return;
            }
        }
    }
}
