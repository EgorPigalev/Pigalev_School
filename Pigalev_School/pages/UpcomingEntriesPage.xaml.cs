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
    /// Логика взаимодействия для UpcomingEntriesPage.xaml
    /// </summary>
    public partial class UpcomingEntriesPage : Page
    {
        public UpcomingEntriesPage()
        {
            InitializeComponent();
            List<ClientService> clientServices = Base.BD.ClientService.ToList();
            clientServices = clientServices.Where(x => x.StartTime >= DateTime.Now).ToList(); // Фильтрация по дате начала
            DateTime endDateTime = DateTime.Today.AddDays(2).AddTicks(-1); // Конец завтрашнего дня
            clientServices = clientServices.Where(x => x.StartTime < endDateTime).ToList(); // Фильтрация по дате окончания
            clientServices.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            lvListClientService.ItemsSource = clientServices;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new ListServicePage(true));
        }
    }
}
