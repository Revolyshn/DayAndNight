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

namespace Andreeva_TZv2
{
    /// <summary>
    /// Логика взаимодействия для Functional.xaml
    /// </summary>
    public partial class Functional : Window
    {
        BD.user admin;
        static LiberationRoom liberationRoom = new LiberationRoom();
        static Borrow borrow;
        static MainWindow auth = new MainWindow();

        public Functional(BD.user _admin)
        {
            InitializeComponent();
            admin = _admin;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            borrow = new Borrow(admin);
            FunctionFrame.Navigate(borrow);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (auth != null)
            {
                auth.Show();
            }
            else
            {
                auth.Activate();
            }
            auth.WindowHidder();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FunctionFrame.Navigate(liberationRoom);
        }
    }
}
