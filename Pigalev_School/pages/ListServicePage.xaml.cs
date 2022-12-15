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

namespace Pigalev_School
{
    /// <summary>
    /// Логика взаимодействия для ListServicePage.xaml
    /// </summary>
    public partial class ListServicePage : Page
    {
        bool Admin; // Поле для фиксации админа
        public ListServicePage(bool Admin)
        {
            InitializeComponent();
            this.Admin = Admin;
            lvListService.ItemsSource = Base.BD.Service.ToList();
            tbCurrentCount.Text = Convert.ToString(lvListService.Items.Count);
            tbAllCount.Text = Convert.ToString(Base.BD.Service.ToList().Count);
            cbDiscount.SelectedIndex = 0;
        }

        private void tbDiscount_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            if(textBlock.Uid != null)
            {
                textBlock.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock.Visibility = Visibility.Hidden;
            }
        }

        private void tbOldPrice_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            if (textBlock.Uid != null)
            {
                textBlock.Visibility = Visibility.Visible;
            }
            else
            {
                textBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void tbSearchName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Filter();
        }
        private List<Service> GetServiceDescription(List<Service> services, string Description) // Метод для нахождения туров с заданным описанием
        {
            List<Service> servicesDiscription = new List<Service>();
            foreach (Service service in services)
            {
                if (service.Description != null)
                {
                    if (service.Description.ToLower().Contains(Description.ToLower()))
                    {
                        servicesDiscription.Add(service);
                    }
                }
            }
            return servicesDiscription;
        }
        void Filter()  // метод для одновременной фильтрации, поиска и сортировки
        {
            List<Service> services = new List<Service>();

            int index = cbDiscount.SelectedIndex;
            if (index != 0)
            {
                switch (cbDiscount.SelectedIndex) // Фильтрация по скидке
                {
                    case 1:
                        {
                            services = Base.BD.Service.Where(x => x.Discount >= 0 && x.Discount < 5).ToList();
                        }
                        break;
                    case 2:
                        {
                            services = Base.BD.Service.Where(x => x.Discount >= 5 && x.Discount < 15).ToList();
                        }
                        break;
                    case 3:
                        {
                            services = Base.BD.Service.Where(x => x.Discount >= 15 && x.Discount < 30).ToList();
                        }
                        break;
                    case 4:
                        {
                            services = Base.BD.Service.Where(x => x.Discount >= 30 && x.Discount < 70).ToList();
                        }
                        break;
                    case 5:
                        {
                            services = Base.BD.Service.Where(x => x.Discount >= 70 && x.Discount < 100).ToList();
                        }
                        break;
                }
            }
            else
            {
                services = Base.BD.Service.ToList();
            }
            if (!string.IsNullOrWhiteSpace(tbSearchName.Text)) // Поиск по наименованию
            {
                services = services.Where(x => x.Title.ToLower().Contains(tbSearchName.Text.ToLower())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(tbSearchDescription.Text)) // Поиск по описанию
            {
                services = GetServiceDescription(services, tbSearchDescription.Text);
            }
            switch (cbSorting.SelectedIndex) // Сортировка
            {
                case 1:
                    {
                        services.Sort((x, y) => x.CurrentPrice.CompareTo(y.CurrentPrice));
                    }
                    break;
                case 2:
                    {
                        services.Sort((x, y) => x.CurrentPrice.CompareTo(y.CurrentPrice));
                        services.Reverse();
                    }
                    break;
            }
            lvListService.ItemsSource = services;
            if (services.Count == 0)
            {
                MessageBox.Show("В базе данных отсутствуют данные удовлетворяющие заданным условиям");
            }
            tbCurrentCount.Text = Convert.ToString(lvListService.Items.Count);
        }

        private void cbDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btnChangeService_Loaded(object sender, RoutedEventArgs e)
        {
            Button btnChangeService = sender as Button;
            if(Admin)
            {
                btnChangeService.Visibility = Visibility.Visible;
            }
            else
            {
                btnChangeService.Visibility = Visibility.Collapsed;
            }
        }

        private void btnDeleteService_Loaded(object sender, RoutedEventArgs e)
        {
            Button buttonDeleteService = sender as Button;
            if (Admin)
            {
                buttonDeleteService.Visibility = Visibility.Visible;
            }
            else
            {
                buttonDeleteService.Visibility = Visibility.Collapsed;
            }
        }

        private bool getProverkaInfoAboutService(int index) // Проверка наличия записей на услугу клиентом
        {
            foreach(ClientService clientService in Base.BD.ClientService.ToList())
            {
                if(clientService.ServiceID == index)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnDeleteService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                int index = Convert.ToInt32(btn.Uid);
                Service service = Base.BD.Service.FirstOrDefault(x => x.ID == index);
                if (getProverkaInfoAboutService(index))
                {
                    MessageBox.Show("Удаление услуги из базы данных запрещено, так как на неё есть информация о записях на услуги!!!");
                    return;
                }
                if (MessageBox.Show("Вы уверены что хотите удалить услугу: " + service.Title + "?", "Системное сообщение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    foreach (ServicePhoto servicePhoto in Base.BD.ServicePhoto.ToList()) // Удаление дополнительных фото
                    {
                        if (servicePhoto.ServiceID == index)
                        {
                            Base.BD.ServicePhoto.Remove(servicePhoto);
                        }
                    }
                    Base.BD.Service.Remove(service);
                    Base.BD.SaveChanges();
                    FrameClass.MainFrame.Navigate(new ListServicePage(Admin));
                }
            }
            catch
            {
                MessageBox.Show("При удаление услуги возникла ошибка");
            }
        }

        private void btnEnterService_Loaded(object sender, RoutedEventArgs e)
        {
            Button btnEnterService = (Button)sender;
            if (Admin)
            {
                btnEnterService.Visibility = Visibility.Visible;
            }
            else
            {
                btnEnterService.Visibility = Visibility.Collapsed;
            }
        }

        private void btnEnterService_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Service service = Base.BD.Service.FirstOrDefault(x => x.ID == index);
            FrameClass.MainFrame.Navigate(new signingUpForServicePage(service));
        }

        private void btnUpcomingEntries_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new UpcomingEntriesPage());
        }

        private void btnUpcomingEntries_Loaded(object sender, RoutedEventArgs e)
        {
            if(Admin)
            {
                btnUpcomingEntries.Visibility = Visibility.Visible;
            }
            else
            {
                btnUpcomingEntries.Visibility= Visibility.Collapsed;
            }
        }

        private void btnUpcoming_Loaded(object sender, RoutedEventArgs e)
        {
            if (Admin)
            {
                btnAdd.Visibility = Visibility.Visible;
            }
            else
            {
                btnAdd.Visibility = Visibility.Collapsed;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new AddService());
        }

        private void btnChangeService_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Service service = Base.BD.Service.FirstOrDefault(x => x.ID == index);
            FrameClass.MainFrame.Navigate(new AddService(service));
        }

        private void btnChangePictures_Loaded(object sender, RoutedEventArgs e)
        {
            Button btnChangePictures = sender as Button;
            if (Admin)
            {
                btnChangePictures.Visibility = Visibility.Visible;
            }
            else
            {
                btnChangePictures.Visibility = Visibility.Collapsed;
            }
        }

        private void btnChangePictures_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Service service = Base.BD.Service.FirstOrDefault(x => x.ID == index);
            FrameClass.MainFrame.Navigate(new ChangePicturePage(service));
        }
    }
}
