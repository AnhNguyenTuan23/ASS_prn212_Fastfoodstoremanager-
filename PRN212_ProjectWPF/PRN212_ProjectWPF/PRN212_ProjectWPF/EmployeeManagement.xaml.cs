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
    /// Interaction logic for EmployeeManagement.xaml
    /// </summary>
    public partial class EmployeeManagement : UserControl
    {
        public EmployeeManagement()
        {
            InitializeComponent();
            loadBranch();
            load();
        }
        private void loadBranch()
        {
            cbxBranch.ItemsSource = ProjectContext.INSTANCE.Branches.Select(x => x.BranchName).ToList();
            cbxBranch.SelectedIndex = 0;
            cbxBranch.Items.Refresh();
        }
        private void load()
        {
            var list = ProjectContext.INSTANCE.Employees.Include(x => x.Branch).ToList();
            dgEmployeeManagement.ItemsSource = list;
            dgEmployeeManagement.Items.Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDialog em = new EmployeeDialog("Add",new Employee());
            em.ShowDialog();
            load();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployeeManagement.SelectedItem is Employee em)
            {
                var temp = ProjectContext.INSTANCE.Employees.FirstOrDefault(x => x.EmployeeId == em.EmployeeId);
                EmployeeDialog emd = new EmployeeDialog("Update", temp);
                emd.ShowDialog();
                load();
            }
            else
            {
                MessageBox.Show("You have not selected a employee. Try again!!", "Delete Employee", MessageBoxButton.OK, MessageBoxImage.Information);
            }
                
            
        }

      

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var list = ProjectContext.INSTANCE.Employees.Include(x => x.Branch).Where(x => x.Name.Contains(txtSearch.Text) || x.Email.Contains(txtSearch.Text) || x.Phone.Contains(txtSearch.Text)).ToList();
            dgEmployeeManagement.ItemsSource = list;
            dgEmployeeManagement.Items.Refresh();
        }

        private void cbxBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = ProjectContext.INSTANCE.Employees.Include(x => x.Branch).Where(x => x.Branch.BranchName.Equals(cbxBranch.SelectedItem)).ToList();
            dgEmployeeManagement.ItemsSource = list;
            dgEmployeeManagement.Items.Refresh();
        }
    }
}
