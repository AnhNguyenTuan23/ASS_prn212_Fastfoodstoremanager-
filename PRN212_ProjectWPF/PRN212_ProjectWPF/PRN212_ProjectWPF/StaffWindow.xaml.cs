using PRN212_ProjectWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        public Employee employee {  get; set; }
        public StaffWindow(Employee account)
        {
            InitializeComponent();
            this.employee = account;
            Menu menu = new Menu(employee , new List<OrderDetail>(),"");
            MainContentControl.Content = menu;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(employee, new List<OrderDetail>(),"");
            MainContentControl.Content = menu;
        }

        private void btnTable_Click(object sender, RoutedEventArgs e)
        {
            TableManagement tableManagement = new TableManagement(employee);
            MainContentControl.Content = tableManagement;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Logout?", "Announce", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Login login = new Login();
                login.Show();
                Close();
                
            }
        }

        private void btnRequest_Click(object sender, RoutedEventArgs e)
        {
            OrderRequest o = new OrderRequest(employee);
            MainContentControl.Content = o;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword c = new ChangePassword(employee);
            c.Show();
        }
    }
}
