using PRN212_ProjectWPF.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        public Customer CustomerObject { get; set; }
        public Profile(Customer customerObject)
        {
            InitializeComponent();
            CustomerObject = customerObject;
            DataContext = this;
            if (CustomerObject != null ) {
                rdbMale.IsChecked = CustomerObject.Gender;
                rdFemale.IsChecked = ! rdbMale.IsChecked;
            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword c = new ChangePassword(CustomerObject);
            c.Show();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
             var customer =  ProjectContext.INSTANCE.Customers.FirstOrDefault( x => x.CustomerId == CustomerObject.CustomerId);
            if (customer != null)
            {
                if (checkError(customer.CustomerId).IsNullOrEmpty())
                {
                    customer.CustomerName = txbName.Text;
                    customer.Phone = txbPhone.Text;
                    customer.Email = txbEmail.Text;
                    customer.Gender = rdbMale.IsChecked == true;
                    customer.Status = CustomerObject.Status;
                    ProjectContext.INSTANCE.Update(customer);
                    ProjectContext.INSTANCE.SaveChanges();
                    MessageBox.Show("Update successfully!!", "Announce", MessageBoxButton.OK);
                } else
                {
                    MessageBox.Show(checkError(customer.CustomerId), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
               
            }
        }
        private string checkError(int id)
        {
            string err = "";
            string regexEmail = "^([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$";
            string regexPhone = "^\\d{1,10}$";
            if (txbEmail.Text.IsNullOrEmpty())
            {
                err += "Email can not be empty!";
            }
            else
            {
                if (!Regex.IsMatch(txbEmail.Text, regexEmail))
                {
                    err += "Email is not valid!";
                }
                if (!ProjectContext.INSTANCE.Customers.Where(x => x.CustomerId!=id && x.Email.Equals(txbEmail.Text)).IsNullOrEmpty())
                {
                    err += "Email was exist!";
                }
            }
            if (txbPhone.Text.IsNullOrEmpty())
            {
                err += "Phone can not be empty!";
            }
            else
            {
                if (!Regex.IsMatch(txbPhone.Text, regexPhone))
                {
                    err += "Phone is not valid!";
                }
                if (!ProjectContext.INSTANCE.Customers.Where(x => x.CustomerId!=id && x.Phone.Equals(txbPhone.Text)).IsNullOrEmpty())
                {
                    err += "Phone was exist!";
                }
            }
            return err;
        }
    }
}
