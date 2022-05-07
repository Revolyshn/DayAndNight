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

namespace Andreeva_TZv2
{
    /// <summary>
    /// Логика взаимодействия для InformationRoom.xaml
    /// </summary>
    public partial class InformationRoom : Page
    {
        TextBox box;
        int numberRoom;
        int countDay;
        TextBox price;

        decimal priceRoom;

        BD.DayAndNightEntities andreeva_TZ = new BD.DayAndNightEntities();
        public InformationRoom(int _numberRoom, TextBox text, int _countDay, TextBox _price)
        {
            InitializeComponent();
            numberRoom = _numberRoom;
            box = text;
            countDay = _countDay;
            price = _price;
            if (numberRoom != 0)
            {
                BD.info_room basa = (BD.info_room)andreeva_TZ.info_room.Where(a => a.num_room == numberRoom).FirstOrDefault();
                if (basa != null)
                {
                    CountRoom.Text = basa.count_room.ToString();
                    priceRoom = basa.price;
                    Price.Text = basa.price.ToString();
                    Info.Text = basa.short_description;
                }
            }
            else
            {
                MessageBox.Show("dfjhskfhcgj");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            price.Text = (priceRoom * countDay).ToString();
            box.Text = (numberRoom).ToString(); 
            NavigationService.GoBack();
        }
    }
}
