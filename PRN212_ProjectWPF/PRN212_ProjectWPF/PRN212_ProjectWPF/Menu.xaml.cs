using PRN212_ProjectWPF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        private List<string> filterPrice = new List<string> { "All" ,"<100", "100 to 200", ">200" };

        public string Action {  get; set; }
        public List<OrderDetail> list;
        public Employee EmployeeObject { get; set; }
        public Menu(Employee employee, List<OrderDetail> l, string action)
        {
            InitializeComponent();
            list = l;
            Action = action;
            this.DataContext = this;
            this.EmployeeObject = employee;
            cbxFilter.ItemsSource = filterPrice;
            cbxFilter.SelectedIndex = 0;
            cbxFilter.Items.Refresh();
            txbTime.Text = DateOnly.FromDateTime(DateTime.Now).ToString();
            lsvSelectedDishes.ItemsSource = list;
            lsvSelectedDishes.Items.Refresh();
            loadMenu();
            loadTable();
            getTotal();
        }
        public void loadTable()
        {
            var branchId = EmployeeObject.BranchId;
            var listNotActiveTable = new List<int>();
            if(!string.IsNullOrEmpty(Action))
            {
                cbxTable.SelectedItem = list.First().Table.TableNumber;
                int orderId = list.First().OrderId;
                listNotActiveTable = ProjectContext.INSTANCE.OrderDetails.Include(x => x.Order).Include(x => x.Table).Where(x => x.Order.Status != 0 && x.Order.Status != 3 && x.Table.BranchId == branchId  && x.OrderId != orderId).Select(x => x.TableId).Distinct().ToList();
            }
            else
            {
                listNotActiveTable = ProjectContext.INSTANCE.OrderDetails.Include(x => x.Order).Include(x => x.Table).Where(x => x.Order.Status != 0 && x.Order.Status != 3 && x.Table.BranchId == branchId).Select(x => x.TableId).Distinct().ToList();
            }
            
            var listActiveTable = ProjectContext.INSTANCE.Tables.Where(x => x.BranchId == branchId && !listNotActiveTable.Contains(x.TableId)).Select(x => x.TableNumber).ToList();
            if (listActiveTable.IsNullOrEmpty())
            {
                txbStatus.Visibility = Visibility.Visible;
                txbStatus.Text = "Full";
                cbxTable.Visibility = Visibility.Collapsed;
            }
            else
            {
                cbxTable.Visibility = Visibility.Visible;
                cbxTable.ItemsSource = listActiveTable;
                cbxTable.Items.Refresh();
            }
            
        }
        public void loadMenu()
        {
            var branchId = EmployeeObject.BranchId;
            List<Models.Menu> x = ProjectContext.INSTANCE.Menus.Include(x => x.NumberDishes).Where(x => x.Status == true && x.NumberDishes.Any(x => x.BranchId == branchId)).ToList();
            foreach (var item in x)
            {
                item.TotalNumberForBranch = item.GetTotalNumber(branchId);
            }
            lsvMenu.ItemsSource = x;
            lsvMenu.Items.Refresh();
        }

        private void btnOrder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = sender as Button;
            var data = button.DataContext as Models.Menu;
            if (data != null)
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
                        TableId = ProjectContext.INSTANCE.Tables.Where(x => x.TableNumber.Equals(cbxTable.Text)).Select(x => x.TableId).FirstOrDefault(),   
                    };
                    list.Add(detail);
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

        public event EventHandler SubmitCompleted;
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (cbxTable.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Please choose table before submit order", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (list.IsNullOrEmpty())
            {
                MessageBox.Show("Nothing to order", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var total = 0;
                list.ForEach(x => total += x.Quantity * x.Dish.Price);
                var order = new Models.Order
                {
                    StartDate = DateTime.Now,
                    TotalPrice = total,
                    Status = 2,
                    EmployeeId = EmployeeObject.EmployeeId,
                    OrderDetails = list.Select(x => new OrderDetail
                    {
                        DishId = x.DishId,
                        Quantity = x.Quantity,
                        TableId = x.TableId,
                    }).ToList()
                };
                if (string.IsNullOrEmpty(Action))
                {

                    ProjectContext.INSTANCE.Orders.Add(order);
                    ProjectContext.INSTANCE.SaveChanges();
                    MessageBox.Show("Successfully!!", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
                    list.Clear();
                    lsvSelectedDishes.Items.Refresh();
                }
                else
                {
                    int orderId = list.First().OrderId;
                    var result = ProjectContext.INSTANCE.Orders.First(x => x.OrderId == orderId);
                    result.TotalPrice = order.TotalPrice;
                    result.EmployeeId = order.EmployeeId;
                    result.OrderDetails.Clear();
                    foreach (var detail in order.OrderDetails)
                    {
                        result.OrderDetails.Add(detail);
                    }
                    ProjectContext.INSTANCE.SaveChanges();
                    MessageBox.Show("Successfully!!", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
                    SubmitCompleted.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void cbxTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if( cbxTable.Text != null)
            {
                int tableId = ProjectContext.INSTANCE.Tables.Where(x => x.TableNumber.Equals(cbxTable.SelectedItem)).Select(x => x.TableId).First();
                list.ForEach((x) => { x.TableId = tableId; });
            }
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
                var listMenu = ProjectContext.INSTANCE.Menus.Where(x => x.DishName.Contains(txbSearch.Text) || x.Price.ToString().Contains(txbSearch.Text)).ToList();
                lsvMenu.ItemsSource = listMenu;
                lsvMenu.Items.Refresh();
        }

        private void cbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxFilter.SelectedItem.Equals("All"))
            {
                loadMenu();
            }else if (cbxFilter.SelectedItem.Equals("<100"))
            {
                lsvMenu.ItemsSource= ProjectContext.INSTANCE.Menus.Where(x => x.Price < 100).ToList();
            } else if (cbxFilter.SelectedItem.Equals("100 to 200"))
            {
                lsvMenu.ItemsSource = ProjectContext.INSTANCE.Menus.Where(x => x.Price >= 100 && x.Price<=200).ToList();
            } else
            {
                lsvMenu.ItemsSource = ProjectContext.INSTANCE.Menus.Where(x => x.Price >200).ToList();
            }
            lsvMenu.Items.Refresh();
        }
    }
}