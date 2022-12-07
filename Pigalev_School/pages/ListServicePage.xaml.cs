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
        public ListServicePage()
        {
            InitializeComponent();
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
        private List<Service> GetServiceDescription(List<Service> services, string Description) // метод для нахождения туров с таким описанием
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
                switch (cbDiscount.SelectedIndex)
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
            switch (cbSorting.SelectedIndex)
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
    }
}
