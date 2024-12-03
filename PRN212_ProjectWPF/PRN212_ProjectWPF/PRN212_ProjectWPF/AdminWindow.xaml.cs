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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public Employee Admin {  get; set; }
        public AdminWindow(Employee admin)
        {
            InitializeComponent();
            MenuManagement menu = new MenuManagement();
            MainContentControl.Content = menu;
            Admin = admin;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuManagement menu = new MenuManagement();
            MainContentControl.Content = menu;
        }

        private void btnTable_Click(object sender, RoutedEventArgs e)
        {
            AdminTableManagement menu = new AdminTableManagement();
            MainContentControl.Content = menu;
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagement employeeManagement = new EmployeeManagement();
            MainContentControl.Content = employeeManagement;
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            StaticReport staticReport = new StaticReport();
            MainContentControl.Content = staticReport;
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

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerManagement cus = new CustomerManagement();
            MainContentControl.Content = cus;
        }

        private void btnBranch_Click(object sender, RoutedEventArgs e)
        {
            BranchManagement cus = new BranchManagement();
            MainContentControl.Content = cus;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword c = new ChangePassword(Admin);
            c.Show();
        }
    }
}
