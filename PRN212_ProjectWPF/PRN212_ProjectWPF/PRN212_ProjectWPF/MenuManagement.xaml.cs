using PRN212_ProjectWPF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Windows;
using System.Windows.Controls;


namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for MenuManagement.xaml
    /// </summary>
    public partial class MenuManagement : UserControl
    {
        private List<string> filterPrice = new List<string> { "All", "<100", "100 to 200", ">200" };

        public MenuManagement()
        {
            InitializeComponent();
            cbxFilter.ItemsSource = filterPrice;
            cbxFilter.SelectedIndex = 0;
            cbxFilter.Items.Refresh();
            loadBranch();
            loadMenu();
        }

        private void loadBranch()
        {
            // Load available branches
            cbxBranch.ItemsSource = ProjectContext.INSTANCE.Branches.Select(x => x.BranchName).ToList();
            cbxBranch.SelectedIndex = 0;
            cbxBranch.Items.Refresh();
        }

        public void loadMenu()
        {
            // Get the selected branch ID
            var branchId = ProjectContext.INSTANCE.Branches
                .Where(x => x.BranchName.Equals(cbxBranch.SelectedItem))
                .Select(x => x.BranchId)
                .FirstOrDefault();

            // Fetch menu items for the selected branch
            List<Models.Menu> menuItems = ProjectContext.INSTANCE.Menus
                .Include(x => x.NumberDishes) // Include related NumberDishes for the branch
                .Where(x => x.NumberDishes.Any(dish => dish.BranchId == branchId))
                .ToList();

            // Update total number for each menu item
            foreach (var item in menuItems)
            {
                item.TotalNumberForBranch = item.GetTotalNumber(branchId);
            }

            // Set the menu items to the data grid
            dgvDisplay.ItemsSource = menuItems;
            dgvDisplay.Items.Refresh();  // Ensure the data grid refreshes
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var branchId = ProjectContext.INSTANCE.Branches
                .Where(x => x.BranchName == cbxBranch.SelectedItem)
                .Select(x => x.BranchId)
                .First();

            DishDialog d = new DishDialog(new Models.Menu(), branchId)
            {
                Action = "Add"
            };

            if (d.ShowDialog() == true) // Check if DialogResult is true
            {
                MessageBox.Show("Dish added successfully!", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
                loadMenu(); // Reload menu to reflect the added dish
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgvDisplay.SelectedItem is Models.Menu selectedDish)
            {
                var branchId = ProjectContext.INSTANCE.Branches
                    .Where(x => x.BranchName == cbxBranch.SelectedItem)
                    .Select(x => x.BranchId)
                    .First();

                DishDialog d = new DishDialog(selectedDish, branchId)
                {
                    Action = "Update"
                };

                if (d.ShowDialog() == true) // Check if DialogResult is true
                {
                    MessageBox.Show("Dish updated successfully!", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
                    loadMenu(); // Reload menu to reflect the updated dish
                }
            }
            else
            {
                MessageBox.Show("Please select a dish before updating!", "Announce", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtSearch.Text.IsNullOrEmpty())
            {
                var listMenu = ProjectContext.INSTANCE.Menus
                    .Where(x => x.DishName.Contains(txtSearch.Text) || x.Price.ToString().Contains(txtSearch.Text))
                    .ToList();
                dgvDisplay.ItemsSource = listMenu;
                dgvDisplay.Items.Refresh(); // Refresh the data grid with filtered results
            }
        }

        private void cbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxFilter.SelectedItem.Equals("All"))
            {
                loadMenu(); // Load all menu items
            }
            else if (cbxFilter.SelectedItem.Equals("<100"))
            {
                dgvDisplay.ItemsSource = ProjectContext.INSTANCE.Menus.Where(x => x.Price < 100).ToList();
            }
            else if (cbxFilter.SelectedItem.Equals("100 to 200"))
            {
                dgvDisplay.ItemsSource = ProjectContext.INSTANCE.Menus.Where(x => x.Price >= 100 && x.Price <= 200).ToList();
            }
            else
            {
                dgvDisplay.ItemsSource = ProjectContext.INSTANCE.Menus.Where(x => x.Price > 200).ToList();
            }
            dgvDisplay.Items.Refresh(); // Refresh the data grid with filtered results
        }

        private void cbxBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadMenu(); // Reload menu when branch changes
        }
    }
}
