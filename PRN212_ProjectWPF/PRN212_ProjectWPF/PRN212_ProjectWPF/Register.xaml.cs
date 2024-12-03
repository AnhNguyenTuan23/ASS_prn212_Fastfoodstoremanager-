using PRN212_ProjectWPF.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (checkError().IsNullOrEmpty())
            {
                Customer customer = new Customer()
                {
                    CustomerName = txbName.Text,
                    Email = txbEmail.Text,
                    Password = txbPassword.Text,
                    Phone = txbPhone.Text,
                    Gender = rdbMale.IsChecked,
                    Status = 0
                };
                ProjectContext.INSTANCE.Customers.Add(customer);
                ProjectContext.INSTANCE.SaveChanges();
                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Login loginForm = new Login();
                loginForm.Show();
                Close();
            }
            else
            {
                MessageBox.Show(checkError(), "Announce", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private string sendOTP(string gmail) {
            string content = getOTP();
            string fromEmail = "";
            string fromPassword = "";
            MailMessage  message  = new MailMessage();
            message.From = new MailAddress(fromEmail);
            message.Subject = "OTP to verify";
            message.To.Add(new MailAddress("anhnthe176241@fpt.edu.vn"));
            message.Body = content;
            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true
            };
            client.Send(message);
            return content;
        }

        private string getOTP()
        {
            Random random = new Random();

            int randomNumber1 = random.Next(0,10); 
            int randomNumber2 = random.Next(0,10);
            int randomNumber3 = random.Next(0,10);

            return randomNumber1+""+randomNumber2+""+randomNumber3;
        }
        private string checkError()
        {
            string err = "";
            string regexEmail = "^([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$";
            string regexPhone = "^\\d{1,10}$";
            if (txbEmail.Text.IsNullOrEmpty())
            {
                err += "Email can not be empty!";
            } else
            {
                if(!Regex.IsMatch(txbEmail.Text, regexEmail))
                {
                    err += "Email is not valid!";
                }
                if(!ProjectContext.INSTANCE.Customers.Where(x => x.Email.Equals(txbEmail.Text)).IsNullOrEmpty())
                {
                    err += "Email was exist!";
                }
            }
            if (txbPhone.Text.IsNullOrEmpty())
            {
                err += "Phone can not be empty!";
            }
            else
            {
                if (!Regex.IsMatch(txbPhone.Text, regexPhone))
                {
                    err += "Phone is not valid!";
                }
                if (!ProjectContext.INSTANCE.Customers.Where(x => x.Phone.Equals(txbPhone.Text)).IsNullOrEmpty())
                {
                    err += "Phone was exist!";
                }
            }
            return err;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Login l = new Login();
            l.Show();
            Close();
        }
    }
}
