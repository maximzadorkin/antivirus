using client.Controls.Plans;
using Microsoft.WindowsAPICodePack.Dialogs;
using ServiceDll;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace client
{
    /// <summary>
    /// Логика взаимодействия для Plans.xaml
    /// </summary>
    public partial class Plans : UserControl
    {
        private string scanPath = "";
        public Plans()
        {
            InitializeComponent();
            this.alwaysGetPlansList();
        }

        async private void alwaysGetPlansList()
        {
            var progress = new Progress<PlanDS[]>(plans => {
                StackPanel.Children.Clear();
                foreach (PlanDS plan in plans)
                {
                    StackPanel.Children.Add(new Plan(plan));
                }
            });

            await Task.Run(() =>
            {
                while (true)
                {
                    if (this.Visibility == Visibility.Visible)
                    {
                        ServiceClient client = ServiceClientCreate.createClient();
                        PlanDS[] plans = client.getAllPlans();
                        this.setPlans(progress, plans);
                        client.Close();
                    }
                    Thread.Sleep(800);
                }
            });
        }

        private void setPlans(IProgress<PlanDS[]> progressSender, PlanDS[] plans)
        {
            progressSender.Report(plans);
        }

        private void ButtonAddDate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool pickerIsEmpty = this.DatePicker.Value.ToString().Length == 0;
            bool pathIsEmpty = this.scanPath.Length == 0;
            if (pickerIsEmpty || pathIsEmpty)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            ServiceClient client = ServiceClientCreate.createClient();
            DateTime picker = (DateTime)this.DatePicker.Value;

            string[] date = picker.ToString().Split(' ')[0].Split('.');
            string[] time = picker.ToString().Split(' ')[1].Split(':');
            
            string day = date[0];
            string month = date[1];
            string year = date[2];
            string hour = time[0];
            string min = time[1];

            string currentStringFormat = $"{year}-{month}-{day}|{hour}:{min}";
            DateTime currentTime = PlanDS.getTimeFromStringFormat(currentStringFormat);
            DateTime now = DateTime.Now;
            now.AddSeconds(-now.Second);
            now.AddMilliseconds(-now.Millisecond);
            
            if (now > currentTime)
            {
                MessageBox.Show($"Неверная дата. Введите дату, не ранее {DateTime.Now.ToString()}");
                return;
            }

            PlanDS plan = new PlanDS(this.scanPath, currentTime);
            bool successAdd = client.addPlan(plan);
            if (!successAdd)
                MessageBox.Show("Аналогичный план уже имеется в списке");
            client.Close();
        }

        private bool choosePath(bool isFile)
        {
            string path = openPathDialog(isFile);
            if (path.Length == 0) return false;

            LabelPath.Content = path;
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

        private void ButtonChooseFile_Click(object sender, RoutedEventArgs e)
        {
            this.choosePath(true);
        }

        private void ButtonChooseDir_Click(object sender, RoutedEventArgs e)
        {
            this.choosePath(false);
        }
    }
}
