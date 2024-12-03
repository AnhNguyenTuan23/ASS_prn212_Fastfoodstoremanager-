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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public Customer CustomerObject { get; set; }
        public CustomerWindow(Customer customerObject)
        {
            InitializeComponent();
            CustomerObject = customerObject;
            Order o = new Order(CustomerObject, new List<OrderDetail>(), "");
            MainContentControl.Content = o;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            Order o = new Order(CustomerObject,new List<OrderDetail>(),"");
            MainContentControl.Content = o;
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile p  = new Profile(CustomerObject);
            MainContentControl.Content = p;
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            OrderHistory o = new OrderHistory(CustomerObject);
            MainContentControl.Content = o;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login l = new Login();
            l.Show();
            Close();
        }
    }
}
