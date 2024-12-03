using PRN212_ProjectWPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
    /// Interaction logic for CustomerManagement.xaml
    /// </summary>
    public partial class CustomerManagement : UserControl
    {
        public CustomerManagement()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {
            var list = ProjectContext.INSTANCE.Customers.ToList();
            dgCustomerManagement.ItemsSource = list;
            dgCustomerManagement.Items.Refresh();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CustomerDialog customerDialog = new CustomerDialog("Add", new Customer());
            customerDialog.ShowDialog();
            load();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomerManagement.SelectedItem is Customer em)
            {
                var temp = ProjectContext.INSTANCE.Customers.FirstOrDefault(x => x.CustomerId == em.CustomerId);
                CustomerDialog customerDialog = new CustomerDialog("Update", temp);
                customerDialog.ShowDialog();
                load();
            }
            else
            {
                MessageBox.Show("You have not selected a employee. Try again!!", "Delete Employee", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var list = ProjectContext.INSTANCE.Customers.Where(x => x.CustomerName.Contains(txtSearch.Text) || x.Email.Contains(txtSearch.Text) || x.Phone.Contains(txtSearch.Text)).ToList();
            dgCustomerManagement.ItemsSource = list;
            dgCustomerManagement.Items.Refresh();
        }
    }
}
