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
using System.Windows.Shapes;

namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txbEmail.Text) && !String.IsNullOrEmpty(pwbPassword.Password))
            {
                Employee a = checkLoginEmployee(txbEmail.Text, pwbPassword.Password);
                if (a != null)
                {
                    if (checkAdmin(txbEmail.Text, pwbPassword.Password))
                    {
                        AdminWindow adminWindow = new AdminWindow(a);
                        adminWindow.Show();

                        Close();
                    }
                    else
                    {
                        StaffWindow staffWindow = new StaffWindow(a);
                        staffWindow.Show();
                        Close();
                    }
                } else if(checkLoginCustomer(txbEmail.Text, pwbPassword.Password) != null)
                {
                    var x = checkLoginCustomer(txbEmail.Text, pwbPassword.Password);
                    CustomerWindow customerWindow = new CustomerWindow(x);
                    customerWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Wrong user or password!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private Customer checkLoginCustomer(string userName, string password)
        {
            var result = ProjectContext.INSTANCE.Customers
               .Where(x => x.Email.Equals(userName) && x.Password.Equals(password) && x.Status != 3)
               .FirstOrDefault();
            return result;
        }
        private Employee checkLoginEmployee(string userName, string password)
        {
            var account = ProjectContext.INSTANCE.Employees
                .Where(x => x.Email.Equals(userName) && x.Password.Equals(password) && x.Status == true)
                .FirstOrDefault();
            return account;
        }
        private bool checkAdmin(string userName, string password)
        {
            var account = checkLoginEmployee(userName, password);
            if(account != null)
            {
                return account.Role.Equals("Admin");
            } else
            {
                return false;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register r = new Register();
            r.Show();
            Close();
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
