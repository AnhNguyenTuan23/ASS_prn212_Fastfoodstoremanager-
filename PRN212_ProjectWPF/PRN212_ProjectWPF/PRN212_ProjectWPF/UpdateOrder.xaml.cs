using PRN212_ProjectWPF.Models;
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
using System.Windows.Shapes;

namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        public UpdateOrder(Employee employee, List<OrderDetail> l)
        {
            InitializeComponent();
            Menu menu = new Menu(employee, l,"update");
            MainContentControl.Content = menu;
            menu.SubmitCompleted += MyUserControl_SubmitCompleted;
        }
        public UpdateOrder(Customer cus, List<OrderDetail> l)
        {
            InitializeComponent();
            Order menu = new Order(cus, l, "update");
            MainContentControl.Content = menu;
            menu.SubmitCompleted += MyUserControl_SubmitCompleted;
        }
        private void MyUserControl_SubmitCompleted(object sender, EventArgs e)
        {
            Close();
        }

    }
}
