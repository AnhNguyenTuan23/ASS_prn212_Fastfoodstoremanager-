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
    /// Interaction logic for OrderRequest.xaml
    /// </summary>
    public partial class OrderRequest : UserControl
    {
        public static Employee EmployeeObject { get; set; }
        public OrderRequest(Employee e)
        {
            InitializeComponent();
            EmployeeObject = e;
            load();
        }
        private void load()
        {
            var now = DateTime.Now;
            var listOrderInBranch = ProjectContext.INSTANCE.OrderDetails.Include(x => x.Table).Where(x => x.Table.BranchId == EmployeeObject.BranchId).Select(x => x.OrderId).ToList();
            var listUpdate = ProjectContext.INSTANCE.Orders
            .Include(x => x.OrderDetails)
            .Where(x => listOrderInBranch.Contains(x.OrderId))
            .ToList() // Materialize the query results to client-side
            .Where(x => x.Status == 1 && (now - x.StartDate).Value.TotalMinutes >= 30)
            .ToList();
            foreach (var order in listUpdate)
            {
                order.Status = 3;
            }
            ProjectContext.INSTANCE.SaveChanges();
            var list = ProjectContext.INSTANCE.Orders.Include(x => x.OrderDetails).Where(x => listOrderInBranch.Contains(x.OrderId) && x.Status == 1).ToList();
            foreach (var item in list)
            {
                    item.BranchName = ProjectContext.INSTANCE.Tables.Include(x => x.Branch).Where(x => x.TableId == item.OrderDetails.First().TableId).Select(x => x.Branch.BranchName).FirstOrDefault();
                    foreach (var item1 in item.OrderDetails)
                    {
                        item1.DishName = ProjectContext.INSTANCE.Menus.Where(x => x.DishId == item1.DishId).Select(x => x.DishName).FirstOrDefault();
                    }
                
            }
            lsvRequest.ItemsSource = list;
            lsvRequest.Items.Refresh();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(lsvRequest.SelectedItem is Models.Order o)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure to serving this order?", "Announce", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var x = ProjectContext.INSTANCE.Orders.FirstOrDefault(x => x.OrderId == o.OrderId);
                    x.Status = 2;
                    ProjectContext.INSTANCE.Orders.Update(x);
                    ProjectContext.INSTANCE.SaveChanges();
                    load();
                }
                
            }
            else
            {
                MessageBox.Show("Plese select before submit", "Announce", MessageBoxButton.OK);
            }
            
        }
    }
}
