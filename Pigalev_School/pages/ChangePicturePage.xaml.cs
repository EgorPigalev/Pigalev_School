using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace Pigalev_School
{
    /// <summary>
    /// Логика взаимодействия для ChangePicturePage.xaml
    /// </summary>
    public partial class ChangePicturePage : Page
    {
        Service service;
        public ChangePicturePage(Service service)
        {
            InitializeComponent();
            this.service = service;
            tbNameService.Text = "Услуга: " + service.Title;
            lvListImages.ItemsSource = Base.BD.ServicePhoto.Where(x => x.ServiceID == service.ID).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new ListServicePage(true));
        }

        private void btnAddPicture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path;
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.ShowDialog();
                path = OFD.FileName;
                if(path != null)
                {
                    string newFilePath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\image\\" + System.IO.Path.GetFileName(path); // Путь куда копировать файл
                    if(!File.Exists(newFilePath))
                    {
                        File.Copy(path, newFilePath);
                    }
                    else
                    {
                        MessageBox.Show("Такая картинка уже есть! Добавлено старое фото");
                    }
                    ServicePhoto servicePhoto = new ServicePhoto();
                    servicePhoto.ServiceID = service.ID;
                    servicePhoto.PhotoPath = newFilePath;
                    Base.BD.ServicePhoto.Add(servicePhoto);
                    Base.BD.SaveChanges();
                    FrameClass.MainFrame.Navigate(new ChangePicturePage(service));
                }
            }
            catch
            {
                MessageBox.Show("При добавлении нового фото возникла ошибка!");
            }
        }

        private void btnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int index = Convert.ToInt32(btn.Uid);
                ServicePhoto servicePhoto = Base.BD.ServicePhoto.FirstOrDefault(x => x.ID == index);
                if (MessageBox.Show("Вы уверены, что хотите удалить картинку?", "Системное сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Base.BD.ServicePhoto.Remove(servicePhoto);
                    Base.BD.SaveChanges();
                    FrameClass.MainFrame.Navigate(new ChangePicturePage(service));
                }
            }
            catch
            {
                MessageBox.Show("При удаление картинки возникла ошибка");
            }
        }
    }
}
