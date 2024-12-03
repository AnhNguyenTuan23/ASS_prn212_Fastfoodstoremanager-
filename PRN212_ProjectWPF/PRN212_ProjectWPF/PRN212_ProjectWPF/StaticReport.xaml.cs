using PRN212_ProjectWPF.Models;
using Microsoft.EntityFrameworkCore;
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

namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for StaticReport.xaml
    /// </summary>
    public partial class StaticReport : UserControl
    {
        public StaticReport()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {
            var list = ProjectContext.INSTANCE.OrderDetails.Include(x => x.Dish).Include(x => x.Order).Include(x => x.Table).OrderByDescending(x => x.Order.StartDate).ToList();
            dgStaticReport.ItemsSource = list;
            dgStaticReport.Items.Refresh();
        }
    }
}
