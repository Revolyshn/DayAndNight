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
    /// Логика взаимодействия для SUserFunctional.xaml
    /// </summary>
    public partial class SUserFunctional : Page
    {
        BD.DayAndNightEntities _andreevaTz = new BD.DayAndNightEntities();
        private bool IsClicked = false;

        public SUserFunctional()
        {
            InitializeComponent();
            FillListRoom();
            FillListUser();
        }

        private void FillListRoom()
        {
            foreach (BD.info_room inforoom in _andreevaTz.info_room)
            {
                BD.borrow_room borrowRoom;

                borrowRoom = (BD.borrow_room)_andreevaTz.borrow_room.FirstOrDefault(a => a.room == inforoom.num_room);
                if (borrowRoom != null)
                {

                }
                else
                {
                    UserList_cb.Items.Add(inforoom.num_room.ToString());
                }
            }

        }
        private void FillListUser()
        {
            foreach (BD.user userinfo in _andreevaTz.user.Where(a => a.role == "User").ToList())
            {  
                    Button userbtn = new Button
                    {
                        Content = userinfo.name,
                        Width = 250,
                        Height = 20
                    };
                    ListUser.Children.Add(userbtn);
                
            }


        }

        private void AddRoom_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserList_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id = UserList_cb.SelectedItem.ToString();
            var room = Lib.Connector.DataBase().info_room.Where(a => a.num_room.ToString() == id).FirstOrDefault();
            ShortDescription_tb.Text = room.short_description;
            NumberRoom_tb.Text = room.num_room.ToString();
            CountRoom_tb.Text = room.count_room.ToString();
            Capacity_tb.Text = room.capacity.ToString();
            TypeRoom_tb.Text= room.type_room.ToString();
            Price_tb.Text = room.price.ToString();
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsClicked)
            {
                IsClicked = true;
                ShortDescription_tb.IsReadOnly = false;
                NumberRoom_tb.IsReadOnly=false;
                CountRoom_tb.IsReadOnly = false;
                Capacity_tb.IsReadOnly = false;
                TypeRoom_tb.IsReadOnly = false;
                Price_tb.IsReadOnly = false;
                Change_btn.Content = "Отменить";
                SafeChange_btn.IsEnabled = true;

            }
            else
            {
                IsClicked = false;
                ShortDescription_tb.IsReadOnly = true;
                NumberRoom_tb.IsReadOnly = true;
                CountRoom_tb.IsReadOnly = true;
                Capacity_tb.IsReadOnly = true;
                TypeRoom_tb.IsReadOnly = true;
                Price_tb.IsReadOnly = true;

                var id = UserList_cb.SelectedItem.ToString();
                var room = Lib.Connector.DataBase().info_room.Where(a => a.num_room.ToString() == id).FirstOrDefault();
                ShortDescription_tb.Text = room.short_description;
                NumberRoom_tb.Text = room.num_room.ToString();
                CountRoom_tb.Text = room.count_room.ToString();
                Capacity_tb.Text = room.capacity.ToString();
                TypeRoom_tb.Text = room.type_room.ToString();
                Price_tb.Text = room.price.ToString();
                Change_btn.Content = "Изменить";
                SafeChange_btn.IsEnabled = false;
            }
        }

        private void SafeChange_btn_Click(object sender, RoutedEventArgs e)
        {
            var id = UserList_cb.SelectedItem.ToString();
            var room = Lib.Connector.DataBase().info_room.Where(a => a.num_room.ToString() == id).FirstOrDefault();

            if(NumberRoom_tb == null  || CountRoom_tb == null || Capacity_tb == null || TypeRoom_tb == null || Price_tb == null)
            {
                MessageBox.Show("не все поля заполнены!");
            }else{
                room.short_description = ShortDescription_tb.Text;
                room.num_room = int.Parse(NumberRoom_tb.Text);
                room.count_room = int.Parse(CountRoom_tb.Text);
                room.capacity = int.Parse(Capacity_tb.Text);
                room.type_room = TypeRoom_tb.Text;
                room.price = decimal.Parse(Price_tb.Text);

                Lib.Connector.DataBase().SaveChanges();

                ShortDescription_tb.Text = room.short_description;
                NumberRoom_tb.Text = room.num_room.ToString();
                CountRoom_tb.Text = room.count_room.ToString();
                Capacity_tb.Text = room.capacity.ToString();
                TypeRoom_tb.Text = room.type_room.ToString();
                Price_tb.Text = room.price.ToString();
                Change_btn.Content = "Изменить";
                SafeChange_btn.IsEnabled = false;
                MessageBox.Show("Все изменения сохранены!");
            }
        }
    }
}
