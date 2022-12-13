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
    /// Логика взаимодействия для AddService.xaml
    /// </summary>
    public partial class AddService : Page
    {
        Service service;
        bool flagUpdate = false;
        public AddService()
        {
            InitializeComponent();
        }

        public AddService(Service service)
        {
            InitializeComponent();
            this.service = service;
            flagUpdate = true;
            tbHeader.Text = "Изменение услуги";
            btnAddService.Content = "Изменить услугу";
            tbName.Text = service.Title;
            tbCost.Text = Convert.ToString(service.Cost);
            tbDurationInSeconds.Text = Convert.ToString(service.DurationInSeconds);
            tbDiscount.Text = Convert.ToString(service.Discount);
            tbDescription.Text = Convert.ToString(service.Description);

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new ListServicePage(true));
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.Uid);
            Service service = Base.BD.Service.FirstOrDefault(x => x.ID == index);
            FrameClass.MainFrame.Navigate(new AddService(service));
        }

        private void tbCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".") && (!tbCost.Text.Contains(".") && tbCost.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }
    }
}
