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
using System.Windows.Shapes;

namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for CustomerDialog.xaml
    /// </summary>
    public partial class CustomerDialog : Window
    {
        private List<string> listStatus = new List<string> { "Active", "Delete" };
        public string Action { get; set; }
        public Customer CustomerObject { get; set; }
        public CustomerDialog(string action, Customer c)
        {
            InitializeComponent();
            Action = action;
            CustomerObject = c;
            DataContext = this;
            load();
        }

        private void load()
        {
            for (int i = 0; i < listStatus.Count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = listStatus[i];
                item.Tag = i;

                cbxStatus.Items.Add(item);
            }
            if (Action.Equals("Update"))
            {
                cbxStatus.Text = CustomerObject.StatusDetail;
                rdbMale.IsChecked = CustomerObject.Gender == true;
                rdFemale.IsChecked = !rdbMale.IsChecked;
            } else
            {
                cbxStatus.SelectedIndex = 0;
                rdbMale.IsChecked = true;
            }
        }
        public Customer getCustomer()
        {
            Customer c = new Customer();
            c.CustomerName = txbName.Text;
            c.Phone = txbPhone.Text;
            c.Email = txbEmail.Text;
            if (cbxStatus.SelectedItem is ComboBoxItem selectedItem)
            {
                if(selectedItem.Tag is int value)
                {
                    byte tagValue = (byte)(value);
                    c.Status = tagValue;
                }             
            }
            c.Gender = rdbMale.IsChecked == true;
            return c;
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
                if(id != -1)
                {
                    if (!ProjectContext.INSTANCE.Customers.Where(x => x.CustomerId != id && x.Email.Equals(txbEmail.Text)).IsNullOrEmpty())
                    {
                        err += "Email was exist!";
                    }
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
                if(id!= -1)
                {
                    if (!ProjectContext.INSTANCE.Customers.Where(x => x.CustomerId!= id && x.Phone.Equals(txbPhone.Text)).IsNullOrEmpty())
                    {
                        err += "Phone was exist!";
                    }
                }
               
            }
            return err;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {            
                if (Action.Equals("Add"))
                {
                if (checkError(-1).IsNullOrEmpty())
                {
                    ProjectContext.INSTANCE.Customers.Add(getCustomer());
                } else
                {
                    MessageBox.Show(checkError(-1), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                }
                else if (Action.Equals("Update"))
                {
                    var temp = ProjectContext.INSTANCE.Customers.First(x => x.CustomerId == CustomerObject.CustomerId);
                if (checkError(temp.CustomerId).IsNullOrEmpty())
                {
                    var selectedCustomer = getCustomer();
                    temp.CustomerName = selectedCustomer.CustomerName;
                    temp.Email = selectedCustomer.Email;
                    temp.Phone = selectedCustomer.Phone;
                    temp.Gender = selectedCustomer.Gender;
                    temp.Status = selectedCustomer.Status;
                    ProjectContext.INSTANCE.Customers.Update(temp);
                }
                else
                {
                    MessageBox.Show(checkError(-1), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                    
                }
                ProjectContext.INSTANCE.SaveChanges();
                MessageBox.Show("Successfully!!!", "Announce", MessageBoxButton.OK, MessageBoxImage.None);
                Close();
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
