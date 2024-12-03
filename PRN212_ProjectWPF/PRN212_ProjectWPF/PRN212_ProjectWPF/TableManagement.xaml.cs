using PRN212_ProjectWPF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for TableManagement.xaml
    /// </summary>
    public partial class TableManagement : UserControl
    {
        public Employee EmployeeObject { get; set; }
        private List<OrderDetail> list = new List<OrderDetail>();
        public TableManagement(Employee employee)
        {
            InitializeComponent();
            EmployeeObject = employee;
            loadTables();          
        }

        private void loadTables()
        {
            var branchId = EmployeeObject.BranchId;
            var listNotActiveTable = ProjectContext.INSTANCE.OrderDetails.Include(x => x.Order).Include(x => x.Table).Where(x => x.Order.Status != 0 && x.Order.Status != 0 && x.Table.BranchId == branchId).Select(x => x.TableId).Distinct().ToList();
            foreach (var item in ProjectContext.INSTANCE.Tables.Where(x => x.BranchId == branchId).ToList())
            {
                Button b = new Button();
                b.Content = item.TableNumber;
                b.Tag = item.TableId;
                if(listNotActiveTable.Contains(item.TableId))
                {
                    b.Background = Brushes.Red;
                }
                else
                {
                    b.Background= Brushes.Green;
                }
               
                b.Click += Button_Click;

                wpTables.Children.Add(b);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int tableId = (int)button.Tag;
            list = ProjectContext.INSTANCE.OrderDetails.Include(x => x.Dish).Include(x => x.Order).Include(x => x.Table).Where(x => x.TableId == tableId && x.Order.Status != 0 && x.Order.Status != 3).ToList();
            dgvDisplay.ItemsSource = list;
            if (!list.IsNullOrEmpty())
            {
                txbTotal.Text = ProjectContext.INSTANCE.Orders.FirstOrDefault(x => x.OrderId == list.First().OrderId).TotalPrice.ToString();
                btnUpdate.IsEnabled = true;
            }
            else
            {
                txbTotal.Text = "0";
                btnUpdate.IsEnabled = false;
            }           
            txbTable.Text = "Table" + button.Content;
            dgvDisplay.Items.Refresh();
        }

        private void btnPayment_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result =  MessageBox.Show("Are you sure ?", "Announce", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                var orderDetail = list.FirstOrDefault();
                if (orderDetail != null)
                {
                    var temp = ProjectContext.INSTANCE.Orders.Where(x => x.OrderId == orderDetail.OrderId).First();
                    temp.Status = 0;
                    temp.EndDate = DateTime.Now;
                }
                ProjectContext.INSTANCE.SaveChanges();
                list.Clear();
                dgvDisplay.Items.Refresh();
                wpTables.Children.Clear();
                loadTables();
            }
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateOrder up = new UpdateOrder(EmployeeObject, list);
            up.ShowDialog();
            dgvDisplay.Items.Refresh();
            if (!list.IsNullOrEmpty())
            {
                txbTotal.Text = ProjectContext.INSTANCE.Orders.FirstOrDefault(x => x.OrderId == list.First().OrderId).TotalPrice.ToString();
            }
            else
            {
                txbTotal.Text = "0";
            }
        }
    }
}
