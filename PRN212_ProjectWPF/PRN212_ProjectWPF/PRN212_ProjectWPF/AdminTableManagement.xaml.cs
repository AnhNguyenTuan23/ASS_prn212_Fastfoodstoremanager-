using PRN212_ProjectWPF.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for AdminTableManagement.xaml
    /// </summary>
    public partial class AdminTableManagement : UserControl
    {
        private List<OrderDetail> list = new List<OrderDetail>();
        public AdminTableManagement()
        {
            InitializeComponent();
            loadBranch();
            loadTables();
        }

        private void loadBranch()
        {
            cbxBranch.ItemsSource = ProjectContext.INSTANCE.Branches.Select(x => x.BranchName).ToList();
            cbxBranch.SelectedIndex = 0;
            cbxBranch.Items.Refresh();
        }
        private void loadTables()
        {
            wpTables.Children.Clear();
            var listNotActiveTable = ProjectContext.INSTANCE.OrderDetails.Include(x => x.Order).Where(x => x.Order.Status != 0).Select(x => x.TableId).Distinct().ToList();
            foreach (var item in ProjectContext.INSTANCE.Tables.Include(x => x.Branch).Where(x=> x.Branch.BranchName == cbxBranch.SelectedItem).ToList())
            {
                Button b = new Button();
                b.Content = item.TableNumber;
                b.Tag = item.TableId;
                if (listNotActiveTable.Contains(item.TableId))
                {
                    b.Background = Brushes.Red;
                }
                else
                {
                    b.Background = Brushes.Green;
                }

                b.Click += Button_Click;

                wpTables.Children.Add(b);
            }
        }
            private void Button_Click(object sender, RoutedEventArgs e)
            {
                Button button = (Button)sender;
                int tableId = (int)button.Tag;
                list = ProjectContext.INSTANCE.OrderDetails.Include(x => x.Dish).Include(x => x.Order).Where(x => x.TableId == tableId && x.Order.Status != 0).ToList();
                dgvDisplay.ItemsSource = list;
                if (!list.IsNullOrEmpty())
                {
                    txbTotal.Text = ProjectContext.INSTANCE.Orders.FirstOrDefault(x => x.OrderId == list.First().OrderId).TotalPrice.ToString();
                }
                else
                {
                    txbTotal.Text = "0";
                }
            txbTable.Tag = tableId;
                txbTable.Text = button.Content.ToString();
                dgvDisplay.Items.Refresh();
            }

        private void txbTable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbTable.Tag != null)
            {
                var table = ProjectContext.INSTANCE.Tables.FirstOrDefault(x => x.TableId == (int)txbTable.Tag);
                if (table != null)
                {
                    table.TableNumber = txbTable.Text;
                    ProjectContext.INSTANCE.Tables.Update(table);
                    ProjectContext.INSTANCE.SaveChanges();
                    wpTables.Children.Clear();
                    loadTables();
                }
            }            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int branchId = ProjectContext.INSTANCE.Branches.Where(x => x.BranchName == cbxBranch.SelectedItem).Select(x => x.BranchId).First();
            TableDialog table = new TableDialog(branchId);
            table.ShowDialog();
            wpTables.Children.Clear();
            loadTables();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txbTable.Tag != null)
            {
                var table = ProjectContext.INSTANCE.Tables.FirstOrDefault(x => x.TableId == (int)txbTable.Tag);
                if (table != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure to delete ?", "Infomation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(result == MessageBoxResult.Yes)
                    {
                        var t = ProjectContext.INSTANCE.Tables.FirstOrDefault(x => x.TableId == (int)txbTable.Tag);
                        ProjectContext.INSTANCE.Tables.Remove(t);
                        ProjectContext.INSTANCE.SaveChanges();
                        wpTables.Children.Clear();
                        loadTables();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose table before delete", "Infomation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void cbxBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            wpTables.Children.Clear();
            loadTables();
        }
    }
}
