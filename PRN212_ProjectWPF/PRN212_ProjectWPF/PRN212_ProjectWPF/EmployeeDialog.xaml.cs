using PRN212_ProjectWPF.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
    /// Interaction logic for EmployeeDialog.xaml
    /// </summary>
    public partial class EmployeeDialog : Window
    {
        public string Action { get; set; }
        public Employee EmployeeObject { get; set; }
        public EmployeeDialog(string action, Employee em)
        {
            InitializeComponent();
            Action = action;
            EmployeeObject = em;
            DataContext = this;
            loadBranch();
            load();
            if (Action.Equals("Update"))
            {
                stpPassword.Visibility = Visibility.Collapsed;
            }
        }
        private void loadBranch()
        {
            cbxBranch.ItemsSource = ProjectContext.INSTANCE.Branches.Select(x => x.BranchName).ToList();
            cbxBranch.SelectedIndex = 0;
            cbxBranch.Items.Refresh();
        }
        private void load()
        {
            cbxRole.ItemsSource = ProjectContext.INSTANCE.Employees.Select(x => x.Role).Distinct().ToList();
            cbxRole.Items.Refresh();
            if (Action.Equals("Update"))
            {
                if (EmployeeObject.Status == true)
                {
                    rdDelete.IsChecked = true;
                }
                else
                {
                    rdbActive.IsChecked = true;
                }
            }
            else if (Action.Equals("Add"))
            {
                rdbActive.IsChecked = true;
            }
        }

        private Employee getEmployee()
        {
            int branchId = ProjectContext.INSTANCE.Branches
                          .Where(x => x.BranchName == cbxBranch.SelectedItem)
                          .Select(x => x.BranchId)
                          .FirstOrDefault();

            Employee emp = new Employee
            {
                Name = txbName.Text,
                Email = txbEmail.Text,
                Role = cbxRole.SelectedItem.ToString(),
                Phone = txbPhone.Text,
                Birthday = DateOnly.FromDateTime(dpkBirthday.SelectedDate.Value),
                BranchId = branchId,
                Status = rdbActive.IsChecked == true
            };

            if (Action.Equals("Add"))
            {
                emp.Password = pwbPass.Password;
            }

            return emp;
        }

        private string checkError(int id)
        {
            string err = "";
            string regexEmail = "^([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$";
            string regexPhone = "^\\d{10}$"; // Cập nhật regex để chỉ chấp nhận đúng 10 số

            if (txbEmail.Text.IsNullOrEmpty())
            {
                err += "Email can not be empty!\n";
            }
            else
            {
                if (!Regex.IsMatch(txbEmail.Text, regexEmail))
                {
                    err += "Email is not valid!\n";
                }
                if (id != -1)
                {
                    if (!ProjectContext.INSTANCE.Employees.Where(x => x.EmployeeId != id && x.Email.Equals(txbEmail.Text)).IsNullOrEmpty())
                    {
                        err += "Email already exists!\n";
                    }
                }
            }

            if (txbPhone.Text.IsNullOrEmpty())
            {
                err += "Phone can not be empty!\n";
            }
            else
            {
                if (!Regex.IsMatch(txbPhone.Text, regexPhone))
                {
                    err += "Phone must be exactly 10 digits!\n"; // Thông báo rõ ràng hơn về lỗi số điện thoại
                }
                if (id != -1)
                {
                    if (!ProjectContext.INSTANCE.Employees.Where(x => x.EmployeeId != id && x.Phone.Equals(txbPhone.Text)).IsNullOrEmpty())
                    {
                        err += "Phone already exists!\n";
                    }
                }
            }

            if (dpkBirthday.SelectedDate == null)
            {
                err += "No date selected!\n";
            }
            else
            {
                DateTime selectedDate = (DateTime)dpkBirthday.SelectedDate;
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - selectedDate.Year;

                // Kiểm tra nếu ngày sinh nhật chưa qua trong năm hiện tại, trừ thêm 1 tuổi
                if (currentDate < selectedDate.AddYears(age))
                {
                    age--;
                }

                if (age < 16)
                {
                    err += "This person is not old enough to register (must be at least 16 years old)!\n";
                }
            }

            return err;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Action.Equals("Add"))
            {
                if (checkError(-1).IsNullOrEmpty())
                {
                    ProjectContext.INSTANCE.Employees.Add(getEmployee());
                    ProjectContext.INSTANCE.SaveChanges();
                    MessageBox.Show("Successfully added!", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show(checkError(-1), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; // Dừng lại nếu có lỗi
                }
            }
            else if (Action.Equals("Update"))
            {
                var temp = ProjectContext.INSTANCE.Employees.FirstOrDefault(x => x.EmployeeId == EmployeeObject.EmployeeId);
                if (temp != null)
                {
                    if (checkError(temp.EmployeeId).IsNullOrEmpty())
                    {
                        var selectedEmployee = getEmployee();
                        temp.Name = selectedEmployee.Name;
                        temp.Email = selectedEmployee.Email;
                        temp.Role = selectedEmployee.Role;
                        temp.Phone = selectedEmployee.Phone;
                        temp.Birthday = selectedEmployee.Birthday;
                        temp.Status = selectedEmployee.Status;
                        ProjectContext.INSTANCE.Employees.Update(temp);
                        ProjectContext.INSTANCE.SaveChanges();
                        MessageBox.Show("Successfully updated!", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(checkError(temp.EmployeeId), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return; // Dừng lại nếu có lỗi
                    }
                }
                else
                {
                    MessageBox.Show("Employee not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
