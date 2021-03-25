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

namespace client.Controls.Plans
{
    /// <summary>
    /// Логика взаимодействия для Plan.xaml
    /// </summary>
    public partial class Plan : UserControl
    {
        private PlanDS plan;
        public Plan(PlanDS plan)
        {
            InitializeComponent();
            this.plan = plan;
            this.TextPath.Text = plan.path;
            this.TextTime.Text = PlanDS.getTimeStringFormat(plan.time);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServiceClient client = ServiceClientCreate.createClient();
            client.removePlan(this.plan);
            client.Close();
            StackPanel parent = (StackPanel)VisualTreeHelper.GetParent(this);
            parent.Children.Remove(this);
        }
    }
}
