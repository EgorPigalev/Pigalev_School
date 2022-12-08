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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool Admin;
        public MainWindow()
        {
            InitializeComponent();
            Base.BD = new BaseData();
            FrameClass.MainFrame = fMain;
            Admin = false;
            FrameClass.MainFrame.Navigate(new ListServicePage(Admin));
        }

        private void tbLoginAdmin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(Admin == false)
            {
                LoginAdminWindow loginAdminWindow = new LoginAdminWindow();
                loginAdminWindow.ShowDialog();
                if (Admin == true)
                {
                    tbLoginAdmin.Text = "Активирован режим администратора";
                    FrameClass.MainFrame.Navigate(new ListServicePage(Admin));
                }
            }
            else
            {
                Admin = false;
                MessageBox.Show("Режим администратора выключен");
            }
        }
    }
}
