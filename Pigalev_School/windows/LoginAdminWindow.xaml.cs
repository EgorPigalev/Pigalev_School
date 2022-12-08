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

namespace Pigalev_School
{
    /// <summary>
    /// Логика взаимодействия для LoginAdminWindow.xaml
    /// </summary>
    public partial class LoginAdminWindow : Window
    {
        public LoginAdminWindow()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(tbKodAdmin.Text == "000")
            {
                MainWindow.Admin = true;
                MessageBox.Show("Вы активировали режим администратора");
                this.Close();
            }
            else
            {
                MessageBox.Show("Код введён не верно");
            }
        }
    }
}
