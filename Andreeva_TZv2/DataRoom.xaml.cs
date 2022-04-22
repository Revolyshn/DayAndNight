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
    /// Логика взаимодействия для DataRoom.xaml
    /// </summary>
    public partial class DataRoom : Page
    {
        private InformationRoom infoRoom;

        BD.DayAndNightEntities _andreevaTz = new BD.DayAndNightEntities();

        DateTime enterDate;
        DateTime lastDate;
        TextBox textBox;
        int countDay;
        TextBox price;
        public DataRoom(TextBox box, int _countDay, TextBox _price, DateTime _enterDate)
        {
            InitializeComponent();
            textBox = box;
            countDay = _countDay;
            price = _price;
            enterDate = _enterDate;
            ComboBoxDATA();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateLast;
            ListRoom.Items.Clear();
            BD.BorrowRoom borrowRoom;


            foreach (BD.InfoRoom room in _andreevaTz.InfoRoom)
            {
                lastDate = enterDate.AddDays(countDay);

                borrowRoom = (BD.BorrowRoom)_andreevaTz.BorrowRoom.FirstOrDefault(a => a.Room == room.NumberRoom);
                if (borrowRoom != null)
                {
                    dateLast = borrowRoom.SettlementDate.AddDays(borrowRoom.CountDay);
                    if ((!(borrowRoom.SettlementDate < enterDate) && !(dateLast > enterDate)) ||
                        (!(borrowRoom.SettlementDate < lastDate) && !(dateLast > lastDate)))
                    {
                        Check(room);
                    }

                }
                else
                {
                    Check(room);
                }
            }
        }
        private void Check(BD.InfoRoom room)
        {
            if ((TypeRoom.Text == room.TypeRoom) &&
                (int.Parse(CountPeople.Text) >= room.Capacity)&&
                (int.Parse(CountRoom.Text) == room.CountRoom))
            {
                CreateButtonRoom(room);

            }
        }

        private void CreateButtonRoom(BD.InfoRoom room)
        {
            Button numberRoom = new Button
            {
                Content = room.NumberRoom,

            };
            numberRoom.Width = 60;
            numberRoom.Height = 20;
            numberRoom.Click += InformationRoom;

            ListRoom.Items.Add(numberRoom);
        }

        private void InformationRoom(object sender, RoutedEventArgs e)
        {
            int numberRoom = int.Parse((sender as Button).Content.ToString());
            infoRoom = new InformationRoom(numberRoom, textBox, countDay, price);
            InfoRoom.Navigate(infoRoom);

        }

        private void ComboBoxDATA()
        {
            for (int i = 1; i <= 5; i++)
            {
                CountPeople.Items.Add(i);
                CountRoom.Items.Add(i);
            }
            TypeRoom.Items.Add("стандарт");
            TypeRoom.Items.Add("люкс");
            TypeRoom.Items.Add("апартаменты");
        }
    }
}
