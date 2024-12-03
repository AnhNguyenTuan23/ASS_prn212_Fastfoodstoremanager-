using PRN212_ProjectWPF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : UserControl
    {
        private int tableId { get; set; }
        private List<string> filterPrice = new List<string> { "All", "<100", "100 to 200", ">200" };

        public string Action { get; set; }
        public List<OrderDetail> list;
        public Customer CustomerObject {  get; set; }
        public Order(Customer customer, List<OrderDetail> l, string action)
        {
            InitializeComponent();
            CustomerObject = customer;
            list = l;
            Action = action;
            this.DataContext = this;
            cbxFilter.ItemsSource = filterPrice;
            cbxFilter.SelectedIndex = 0;
            cbxFilter.Items.Refresh();
            lsvSelectedDishes.ItemsSource = list;
            lsvSelectedDishes.Items.Refresh();
            loadBranch();
            loadMenu();
            getTotal();
            checkTable();
        }
        private void btnOrder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = sender as Button;
            var data = button.DataContext as Models.Menu;
            if (data != null)
            {   
                if(data.TotalNumberForBranch == 0)
                {
                    MessageBox.Show("This dish is no longer available", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var orderDish = list.FirstOrDefault(x => x.DishId == data.DishId);
                    if (orderDish != null)
                    {
                        orderDish.Quantity += 1;
                    }
                    else
                    {
                        OrderDetail detail = new OrderDetail()
                        {
                            DishId = data.DishId,
                            Quantity = 1,
                            Dish = data,
                        };
                        list.Add(detail);
                    }
                }
                

            }
            lsvSelectedDishes.ItemsSource = list;
            lsvSelectedDishes.Items.Refresh();
            getTotal();
        }
        private void getTotal()
        {
            int total = 0;
            if (!list.IsNullOrEmpty())
            {
                foreach (var item in list)
                {
                    total += item.Quantity * (item.Dish.Price);
                }
            }
            txbTotal.Text = total.ToString();
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            var button = sender as Button;
            var data = button.DataContext as OrderDetail;
            if (data != null)
            {
                list.Remove(data);
            }
            lsvSelectedDishes.ItemsSource = list;
            lsvSelectedDishes.Items.Refresh();
            getTotal();

        }
        public void loadBranch()
        {
            var listBranch = ProjectContext.INSTANCE.Branches.Select(x => x.BranchName).ToList();
            cbxBranch.ItemsSource = listBranch;
            cbxBranch.SelectedIndex = 0;
            cbxBranch.Items.Refresh();
        }

        public void loadMenu()
        {
            int branchId = ProjectContext.INSTANCE.Branches.Where(x => x.BranchName.Equals(cbxBranch.SelectedItem)).Select(x => x.BranchId).FirstOrDefault();
            List<Models.Menu> x = ProjectContext.INSTANCE.Menus.Include(x => x.NumberDishes).Where(x => x.Status == true && x.NumberDishes.Any(x => x.BranchId == branchId)).ToList();
            foreach (var item in x)
            {
                item.TotalNumberForBranch = item.GetTotalNumber(branchId);
            }
            lsvMenu.ItemsSource = x;
            lsvMenu.Items.Refresh();
        }

        private void cbxBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadMenu();
        }

        private void checkTable()
        {
            int branchId = ProjectContext.INSTANCE.Branches.Where(x => x.BranchName.Equals(cbxBranch.SelectedItem)).Select(x => x.BranchId).FirstOrDefault();
            var listActiveTable = ProjectContext.INSTANCE.OrderDetails.Include(x => x.Order).Include(x => x.Table).Where(x => x.Order.Status == 0 && x.Table.BranchId == branchId).Select(x => x.TableId).Distinct().ToList();
            if (listActiveTable.IsNullOrEmpty())
            {
                MessageBox.Show("This branch has full!! Change branch please", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                tableId = listActiveTable.First();
            }
        }
        public event EventHandler SubmitCompleted;

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nếu danh sách món ăn rỗng
            if (list == null || !list.Any())
            {
                MessageBox.Show("Nothing to order", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Kiểm tra lỗi, hiển thị thông báo lỗi nếu có
            var error = checkError();
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tính tổng giá trị đơn hàng
            int total = list.Sum(x => x.Quantity * x.Dish.Price);

            // Tạo đối tượng Order
            var order = new Models.Order
            {
                StartDate = dtpkStart.Value ?? DateTime.Now, // Kiểm tra null cho dtpkStart.Value
                TotalPrice = total,
                Status = 1,
                CustomerId = CustomerObject.CustomerId,
                OrderDetails = list.Select(x => new OrderDetail
                {
                    DishId = x.DishId,
                    Quantity = x.Quantity,
                    TableId = tableId,
                }).ToList()
            };

            // Kiểm tra nếu đây là hành động thêm mới
            if (string.IsNullOrEmpty(Action))
            {
                ProjectContext.INSTANCE.Orders.Add(order);
                ProjectContext.INSTANCE.SaveChanges();
                MessageBox.Show("Successfully added! Remember that changes can only be made 1 hour before start time.", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);

                // Xóa danh sách sau khi thêm thành công và làm mới hiển thị
                list.Clear();
                lsvSelectedDishes.Items.Refresh();
            }
            else // Hành động cập nhật
            {
                int orderId = list.First().OrderId;
                var existingOrder = ProjectContext.INSTANCE.Orders.FirstOrDefault(x => x.OrderId == orderId);

                if (existingOrder != null)
                {
                    // Cập nhật thông tin Order
                    existingOrder.TotalPrice = total;
                    existingOrder.EmployeeId = order.EmployeeId;
                    existingOrder.OrderDetails.Clear();

                    // Thêm lại các OrderDetail mới
                    foreach (var detail in order.OrderDetails)
                    {
                        existingOrder.OrderDetails.Add(detail);
                    }

                    ProjectContext.INSTANCE.SaveChanges();
                    MessageBox.Show("Successfully updated!", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Gọi event SubmitCompleted nếu có
                    SubmitCompleted?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Order not found. Please check the order details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    
    private string checkError()
        {
            string err = "";
            if (dtpkStart.Value == null)
            {
                err = "Please enter a start date and time!";
            }
            else if (dtpkStart.Value.Value < DateTime.Now)
            {
                err = "Please make sure the start date and time is in the future!";
            }
            return err;
        }

        private void cbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxFilter.SelectedItem.Equals("All"))
            {
                loadMenu();
            }
            else if (cbxFilter.SelectedItem.Equals("<100"))
            {
                lsvMenu.ItemsSource = ProjectContext.INSTANCE.Menus.Where(x => x.Price < 100).ToList();
            }
            else if (cbxFilter.SelectedItem.Equals("100 to 200"))
            {
                lsvMenu.ItemsSource = ProjectContext.INSTANCE.Menus.Where(x => x.Price >= 100 && x.Price <= 200).ToList();
            }
            else
            {
                lsvMenu.ItemsSource = ProjectContext.INSTANCE.Menus.Where(x => x.Price > 200).ToList();
            }
            lsvMenu.Items.Refresh();
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
                var listMenu = ProjectContext.INSTANCE.Menus.Where(x => x.DishName.Contains(txbSearch.Text) || x.Price.ToString().Contains(txbSearch.Text)).ToList();
                lsvMenu.ItemsSource = listMenu;
                lsvMenu.Items.Refresh();
        }

        private void txbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if (int.TryParse(textBox.Text, out int number))
                {
                    var dish = list.FirstOrDefault(x => x.DishId == (int)textBox.Tag);
                    if (dish != null)
                    {
                        dish.Quantity = number;
                    }
                    getTotal();
                }
                else
                {
                    var dish = list.FirstOrDefault(x => x.DishId == (int)textBox.Tag);
                    if (dish != null)
                    {
                        dish.Quantity = 0;
                    }
                    getTotal();
                }
            }
        }

        private void btnDeleteAll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (list.IsNullOrEmpty())
            {
                MessageBox.Show("Nothing to delete", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure to delete all ?", "WARNING", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    list.Clear();
                    lsvSelectedDishes.ItemsSource = list;
                    lsvSelectedDishes.Items.Refresh();
                    getTotal();
                }
            }
        }
    }
}
