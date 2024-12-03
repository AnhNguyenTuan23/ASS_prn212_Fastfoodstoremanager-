using PRN212_ProjectWPF.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public Customer CustomerObject { get; set; }

        public Employee EmployeeObject { get; set; }
        public ChangePassword(Customer customer)
        {
            InitializeComponent();
            CustomerObject = customer;
        }

        public ChangePassword(Employee employee)
        {
            InitializeComponent();
            EmployeeObject = employee;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private string checkCondition()
        {
            string oldPassword = pwbCurrentPassword.Password;
            string newPassword = pwbNewpassword.Password;
            string confirmPassword = pwbConfirmPassword.Password;
            if (CustomerObject != null)
            {
                if (!oldPassword.Equals(CustomerObject.Password))
                {
                    pwbCurrentPassword.Password = oldPassword;
                    pwbNewpassword.Password = newPassword;
                    pwbConfirmPassword.Password = confirmPassword;
                    return "Wrong current password for customer";
                }
            }
            else if (EmployeeObject != null)
            {
                if (!oldPassword.Equals(EmployeeObject.Password))
                {
                    pwbCurrentPassword.Password = oldPassword;
                    pwbNewpassword.Password = newPassword;
                    pwbConfirmPassword.Password = confirmPassword;
                    return "Wrong current password for employee";
                }
            }
            if (!newPassword.Equals(confirmPassword))
            {
                pwbCurrentPassword.Password = oldPassword;
                pwbNewpassword.Password = newPassword;
                pwbConfirmPassword.Password = confirmPassword;
                return "The password confirmation does not match";
            }
            return "";
        }
        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (checkCondition().IsNullOrEmpty())
            {
                if(CustomerObject != null)
                {
                    var x = ProjectContext.INSTANCE.Customers.FirstOrDefault(x => x.CustomerId == CustomerObject.CustomerId);
                    x.Password = pwbConfirmPassword.Password;
                    ProjectContext.INSTANCE.Customers.Update(x);
                    ProjectContext.INSTANCE.SaveChanges();
                } else if (EmployeeObject != null)
                {
                    var x = ProjectContext.INSTANCE.Employees.FirstOrDefault(x => x.EmployeeId == EmployeeObject.EmployeeId);
                    x.Password = pwbConfirmPassword.Password;
                    ProjectContext.INSTANCE.Employees.Update(x);
                    ProjectContext.INSTANCE.SaveChanges();
                }
                
                MessageBox.Show("Update successfully!!", "Announce", MessageBoxButton.OK, MessageBoxImage.None);
                Close();
            }else
            {
                MessageBox.Show(checkCondition(), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
