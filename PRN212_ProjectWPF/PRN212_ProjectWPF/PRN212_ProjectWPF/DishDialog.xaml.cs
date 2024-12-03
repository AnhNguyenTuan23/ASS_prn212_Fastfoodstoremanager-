using PRN212_ProjectWPF.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace PRN212_ProjectWPF
{
    /// <summary>
    /// Interaction logic for DishDialog.xaml
    /// </summary>
    public partial class DishDialog : Window
    {
        public string SourceImage {  get; set; }
        public string Action { get; set; }
        public Models.Menu DishObject { get; set; }

        public int BrandID { get; set; }
        public DishDialog(Models.Menu dish, int branchId)
        {
            InitializeComponent();
            DataContext = this;
            DishObject = dish;
            BrandID = branchId;
            if (DishObject != null)
            {
                if (dish.DishId == 0)
                {
                    rdbActive.IsChecked = true;
                }
                else
                {
                    rdbActive.IsChecked = DishObject.Status == true;
                    rdbDelete.IsChecked = !rdbActive.IsChecked;
                }

                SourceImage = DishObject.Image ?? "";
            }
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            if (txbDishName.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Please enter dish name before upload image", "Announce", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                // Mở hộp thoại chọn tệp
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (.png;.jpeg;*.jpg;*.bmp;*.gif;*.tiff;*.webp)|*.png;*.jpeg;*.jpg;*.bmp;*.gif;*.tiff;*.webp|All files (.)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    // Nhập tên mới cho hình ảnh
                    string fileExtension = Path.GetExtension(selectedFilePath);
                    string newFileName = txbDishName.Text + fileExtension;

                    // Đường dẫn đến thư mục images trong dự án
                    string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                    string imagesDirectory = Path.Combine(projectDirectory, "Images");

                    // Tạo thư mục images nếu chưa tồn tại
                    if (!Directory.Exists(imagesDirectory))
                    {
                        Directory.CreateDirectory(imagesDirectory);
                    }

                    // Đường dẫn đầy đủ của tệp mới
                    string newFilePath = Path.Combine(imagesDirectory, newFileName);

                    try
                    {
                        // Đọc nội dung của tệp và sao chép vào thư mục đích
                        using (FileStream sourceStream = new FileStream(selectedFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (FileStream destinationStream = new FileStream(newFilePath, FileMode.Create, FileAccess.Write))
                            {
                                sourceStream.CopyTo(destinationStream);
                            }
                        }

                        // Tạo đường dẫn tương đối
                        string relativePath = $"Images/{newFileName}";
                        SourceImage = relativePath;

                        // Tải lại ảnh vào Image control
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad; // Tải ảnh vào bộ nhớ
                        bitmap.UriSource = new Uri(newFilePath, UriKind.Absolute);
                        bitmap.EndInit();

                        imgDish.Source = bitmap; // Cập nhật ảnh hiển thị

                        MessageBox.Show($"Image uploaded and saved as {newFileName} in the images folder.");
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "File Access Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private Models.Menu getDish()
        {
            Models.Menu  m = new Models.Menu();
            m.DishId = DishObject.DishId;
            m.DishName = txbDishName.Text;
            m.Price = int.Parse(txbPrice.Text);
            m.Image = SourceImage;
            m.Status = rdbActive.IsChecked == true;
            m.NumberDishes.Add(new NumberDish()
            {
                TotalNumber = int.Parse(txbTotalNumber.Text),
                BranchId = BrandID
            }) ;
            return m;
        }

        private string checkError()
        {
            string err = "";
            if(int.TryParse(txbPrice.Text, out int num))
            {
                if(num <= 0)
                {
                    err = "Price >0";
                }
            }
            else
            {
                err = "Price must be integer";
            }
            return err;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (checkError().IsNullOrEmpty())
            {
                DishObject = getDish();
                DishObject.Image = SourceImage; // Đảm bảo đường dẫn ảnh được cập nhật

                if (Action.Equals("Add"))
                {
                    ProjectContext.INSTANCE.Menus.Add(DishObject);
                    try
                    {
                        ProjectContext.INSTANCE.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.InnerException?.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else if (Action.Equals("Update"))
                {
                    var selectedDish = ProjectContext.INSTANCE.Menus.First(x => x.DishId == DishObject.DishId);
                    selectedDish.DishName = DishObject.DishName;
                    selectedDish.Price = DishObject.Price;
                    selectedDish.Status = DishObject.Status;
                    selectedDish.Image = DishObject.Image; // Cập nhật ảnh mới
                    foreach (var item in selectedDish.NumberDishes.ToList())
                    {
                        if (item.BranchId == DishObject.NumberDishes.First().BranchId)
                        {
                            item.TotalNumber = DishObject.NumberDishes.First().TotalNumber;
                        }
                    }
                    ProjectContext.INSTANCE.Menus.Update(selectedDish);
                    ProjectContext.INSTANCE.SaveChanges();
                }

                imgDish.Source = new BitmapImage(new Uri(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, SourceImage), UriKind.Absolute));

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show(checkError(), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
