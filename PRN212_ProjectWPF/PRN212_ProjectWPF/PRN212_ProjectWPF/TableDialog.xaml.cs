using PRN212_ProjectWPF.Models;
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
using System.Windows.Shapes;

namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for TableDialog.xaml
    /// </summary>
    public partial class TableDialog : Window
    {
        public int BranchId { get; set; }
        public TableDialog(int branchId)
        {
            InitializeComponent();
            BranchId = branchId;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProjectContext.INSTANCE.Tables.Add(new Models.Table
            {
                TableNumber = txbNumber.Text,
                BranchId = BranchId
            });
            ProjectContext.INSTANCE.SaveChanges();
            Close();
        }
    }
}
