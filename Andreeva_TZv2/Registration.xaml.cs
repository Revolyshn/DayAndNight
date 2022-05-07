using System;
using System.Collections.Generic;
using System.Data.Entity;
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


namespace Andreeva_TZv2
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        ComboBox login;
        BD.DayAndNightEntities andreeva_tz = new BD.DayAndNightEntities();
        public Registration(ComboBox _login)
        {
            login = _login;
            InitializeComponent();

        }

        private void TextBox_TouchEnter(object sender, TouchEventArgs e)
        {
            //отправка смс на телефон
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {  
            if((BD.client)andreeva_tz.client.Where(a => a.phone == PhoneClient.Text).FirstOrDefault() == null)
            {
                BD.client client = new BD.client
                {
                    phone = PhoneClient.Text,
                    name = NameBox.Text
                };
                andreeva_tz.client.Add(client);
                andreeva_tz.SaveChanges();
                ComboBoxLogin();
            }
            else
            {
                MessageBox.Show("Такой номер есть, возможно клиент у нас был");
            }
            NavigationService.GoBack();
        }
        private void ComboBoxLogin()
        {
            login.Items.Clear();
            foreach (BD.client r in andreeva_tz.client.ToList())
            {
                login.Items.Add(r.phone);
            }
        }


        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "Введите имя клиента")
            {
                NameBox.Text = "";
            }
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "")
            {
                NameBox.Text = "Введите имя клиента";
            }
        }

        private void PhoneClient_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PhoneClient.Text == "")
            {
                PhoneClient.Text = "Введите телефон клиента";
            }
        }

        private void PhoneClient_GotFocus(object sender, RoutedEventArgs e)
        {

            if (PhoneClient.Text == "Введите телефон клиента")
            {
                PhoneClient.Text = "";
            }
        }
    }

}