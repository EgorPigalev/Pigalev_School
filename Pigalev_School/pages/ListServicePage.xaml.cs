﻿using System;
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
        }
    }
}
