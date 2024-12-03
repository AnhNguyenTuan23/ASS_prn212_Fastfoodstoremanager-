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
    /// Interaction logic for BranchManagement.xaml
    /// </summary>
    public partial class BranchManagement : UserControl
    {
        public BranchManagement()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            dgBranchManagement.ItemsSource = ProjectContext.INSTANCE.Branches.ToList();
            dgBranchManagement.Items.Refresh();
        }
        public Branch getBranch()
        {
            var x = new Branch();
            x.BranchName = txbName.Text;
            x.Address = txbAddress.Text;
            x.Phone = txbPhone.Text;
            x.Status = rdbActive.IsChecked == true;
            return x;
        }
        private string checkError(int id)
        {
            string err = "";
            string regexPhone = "^\\d{1,10}$";
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
                if(id != -1)
                {
                    if (!ProjectContext.INSTANCE.Branches.Where(x => x.BranchId != id && x.Phone.Replace("-", "").Equals(txbPhone.Text)).IsNullOrEmpty())
                    {
                        err += "Phone was exist!";
                    }
                }
               
            }
            return err;
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgBranchManagement.SelectedItem is Branch b)
            {
                var x = ProjectContext.INSTANCE.Branches.FirstOrDefault(x => x.BranchId == b.BranchId);
                if (x != null)
                {
                    if (checkError(x.BranchId).IsNullOrEmpty())
                    {
                        x.BranchName = txbName.Text;
                        x.Address = txbAddress.Text;
                        x.Phone = txbPhone.Text;

                        // Cập nhật trạng thái dựa trên lựa chọn RadioButton
                        x.Status = rdbActive.IsChecked == true;

                        ProjectContext.INSTANCE.Branches.Update(x);
                        ProjectContext.INSTANCE.SaveChanges();
                        MessageBox.Show("Branch updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        load(); // Gọi hàm load để làm mới danh sách sau khi cập nhật
                    }
                    else
                    {
                        MessageBox.Show(checkError(x.BranchId), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have not selected a branch. Try again!", "Update Branch", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (checkError(-1).IsNullOrEmpty())
            {
                var x = getBranch();
                ProjectContext.INSTANCE.Branches.Add(x);
                ProjectContext.INSTANCE.SaveChanges();
                load();
            } else
            {
                MessageBox.Show(checkError(-1), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }


        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dgBranchManagement.ItemsSource = ProjectContext.INSTANCE.Branches.Where(x => x.BranchName.Contains(txtSearch.Text) || x.Address.Contains(txtSearch.Text) || x.Phone.Contains(txtSearch.Text)).ToList();
            dgBranchManagement.Items.Refresh();
        }

        private void dgBranchManagement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgBranchManagement.SelectedItem is Branch b)
            {
                txbName.Text = b.BranchName;
                txbAddress.Text = b.Address;
                txbPhone.Text = b.Phone;
                rdbActive.IsChecked = b.Status;
                rdDelete.IsChecked = !rdbActive.IsChecked;
            }
        }
    }
}
