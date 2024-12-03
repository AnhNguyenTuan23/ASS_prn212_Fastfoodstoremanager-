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
    /// Interaction logic for VerifyPassword.xaml
    /// </summary>
    public partial class VerifyPassword : Window
    {
        public Customer CustomerObject { get; set; }
        public VerifyPassword(Customer customer)
        {
            InitializeComponent();
            CustomerObject = customer;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
