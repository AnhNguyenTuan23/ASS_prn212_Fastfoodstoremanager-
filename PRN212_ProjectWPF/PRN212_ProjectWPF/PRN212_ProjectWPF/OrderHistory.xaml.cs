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
using System.Xml.Linq;

namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for OrderHistory.xaml
    /// </summary>
    public partial class OrderHistory : UserControl
    {
        public Customer CustomerObject {  get; set; }
        public OrderHistory(Customer customer)
        {
            InitializeComponent();
            CustomerObject = customer;
            load();
        }
        private void load()
        {
            var list = ProjectContext.INSTANCE.Orders.Include(x => x.OrderDetails).Where(x => x.CustomerId == CustomerObject.CustomerId).OrderBy(x => x.StartDate).ToList();
            foreach (var item in list)
            {
                item.BranchName = ProjectContext.INSTANCE.Tables.Include(x => x.Branch).Where(x => x.TableId == item.OrderDetails.First().TableId).Select(x => x.Branch.BranchName).FirstOrDefault();
                foreach (var item1 in item.OrderDetails)
                {
                    item1.DishName = ProjectContext.INSTANCE.Menus.Where(x => x.DishId == item1.DishId).Select(x => x.DishName).FirstOrDefault();
                }

            }
            lsvHistory.ItemsSource = list;
            lsvHistory.Items.Refresh();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lsvHistory.SelectedItem is Models.Order b)
            {
                if (b.Status == 3)
                {
                    MessageBox.Show("This order has been deleted and cannot be updated!", "Update Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return; // Ngừng thực hiện nếu trạng thái là 3
                }

                DateTime dateTime = DateTime.Now;
                TimeSpan? difference = dateTime - b.StartDate;
                if((b.StartDate < dateTime) || Math.Abs(difference.Value.TotalHours) < 2)
                {
                    MessageBox.Show("Update time has passed!!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    var x = ProjectContext.INSTANCE.OrderDetails.Include(x => x.Dish).Where(x => x.OrderId == b.OrderId).ToList();
                    UpdateOrder up = new UpdateOrder(CustomerObject, x);
                    up.ShowDialog();
                    load();
                }


            }
            else
            {
                MessageBox.Show("You have not selected a order. Try again!!", "Update Branch", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lsvHistory.SelectedItem is Models.Order b)
            {
                DateTime dateTime = DateTime.Now;
                TimeSpan? difference = dateTime - b.StartDate;
                if (Math.Abs(difference.Value.TotalHours) < 1)
                {
                    MessageBox.Show("Update time has passed!!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                   var x =  ProjectContext.INSTANCE.Orders.Where(x => x.OrderId == b.OrderId).FirstOrDefault();
                    x.Status = 3;

                }
                load();

            }
            else
            {
                MessageBox.Show("You have not selected a order. Try again!!", "Delete Branch", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
    }
}
