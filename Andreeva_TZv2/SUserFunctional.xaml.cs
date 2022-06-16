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
using Andreeva_TZv2.Lib;

namespace Andreeva_TZv2
{
    /// <summary>
    /// Логика взаимодействия для SUserFunctional.xaml
    /// </summary>
    public partial class SUserFunctional : Page
    {
        BD.DayAndNightEntities _andreevaTz = new BD.DayAndNightEntities();
        private BD.info_room _currentRoom = new BD.info_room();
        public SUserFunctional(BD.info_room selectedRoom)
        {
            if (selectedRoom != null)
                _currentRoom = selectedRoom;
            InitializeComponent();
            FillListRoom();
            DataContext = _currentRoom;
        }

        private void FillListRoom()
        {
            Lib.Connector.DataBase().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            UserList_cb.ItemsSource = BD.DayAndNightEntities.GetContext().info_room.ToList();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Lib.Connector.DataBase().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                UserDGrid.ItemsSource = Lib.Connector.DataBase().user.Where(a => a.role == "user").ToList();
                UserList_cb.ItemsSource = BD.DayAndNightEntities.GetContext().info_room.ToList();
            }
        }

        private void AddRoom_btn_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(NumberRoom_tb.Text))
                errors.AppendLine("Введите номер комнаты!");

            if (string.IsNullOrWhiteSpace(CountRoom_tb.Text))
                errors.AppendLine("Введите кол-во комнат!");

            if (string.IsNullOrWhiteSpace(Capacity_tb.Text))
                errors.AppendLine("Введите вместимость!");

            if (_currentRoom.status == null)
                errors.AppendLine("Статус комнаты не задан!");

            if (_currentRoom.type_room == null)
                errors.AppendLine("Тип комнаты не задан!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentRoom.num_room == 0)
            {
                Lib.Connector.DataBase().info_room.Add(_currentRoom);
            }

            try
            {
                Lib.Connector.DataBase().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserList_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id = UserList_cb.SelectedItem.ToString();
            var room = Connector.DataBase().info_room.FirstOrDefault(a => a.num_room.ToString() == id);
            ShortDescription_tb.Text = room.short_description;
            NumberRoom_tb.Text = room.num_room.ToString();
            CountRoom_tb.Text = room.count_room.ToString();
            Capacity_tb.Text = room.capacity.ToString();
            TypeRoom_tb.Text= room.type_room.ToString();
            Price_tb.Text = room.price.ToString();
            StatusRoom_tb.Text = room.status.ToString();
        }

        private void Change_btn_Click(object sender, RoutedEventArgs e)
        {
            if (UserList_cb.SelectedItem != null)
            {

                var id = UserList_cb.SelectedItem.ToString();
                var room = Connector.DataBase().info_room.FirstOrDefault(a => a.num_room.ToString() == id);

                if (NumberRoom_tb == null || CountRoom_tb == null || Capacity_tb == null || TypeRoom_tb == null || Price_tb == null)
                {
                    MessageBox.Show("не все поля заполнены!");
                }
                else
                {
                    try
                    {
                        room.short_description = ShortDescription_tb.Text;
                        room.num_room = int.Parse(NumberRoom_tb.Text);
                        room.count_room = int.Parse(CountRoom_tb.Text);
                        room.capacity = int.Parse(Capacity_tb.Text);
                        room.type_room = TypeRoom_tb.Text;
                        room.price = decimal.Parse(Price_tb.Text);
                        room.status = StatusRoom_tb.Text;

                        Lib.Connector.DataBase().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                    ShortDescription_tb.Text = room.short_description;
                    NumberRoom_tb.Text = room.num_room.ToString();
                    CountRoom_tb.Text = room.count_room.ToString();
                    Capacity_tb.Text = room.capacity.ToString();
                    TypeRoom_tb.Text = room.type_room.ToString();
                    Price_tb.Text = room.price.ToString();
                    StatusRoom_tb.Text = room.status.ToString();
                    MessageBox.Show("Все изменения сохранены!");
                }
            }
            else
            {
                return;
            }


        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            SUserAddUser addUserWin = new SUserAddUser();
            addUserWin.ShowDialog();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var peopleForRemoving = UserDGrid.SelectedItems.Cast<BD.user>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {peopleForRemoving.Count()} элементов?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Lib.Connector.DataBase().user.RemoveRange(peopleForRemoving);
                    Lib.Connector.DataBase().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    UserDGrid.ItemsSource = Lib.Connector.DataBase().user.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteRoom_btn_Click(object sender, RoutedEventArgs e)
        {
            var roomForRemoving = (BD.user)UserList_cb.SelectedItem;

            if (MessageBox.Show($"Вы точно хотите удалить?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Lib.Connector.DataBase().user.Remove(roomForRemoving);
                    Lib.Connector.DataBase().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    UserDGrid.ItemsSource = Lib.Connector.DataBase().user.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnEditStatusUser_Click(object sender, RoutedEventArgs e)
        {
            EditorRoleUser win = new EditorRoleUser();
            win.ShowDialog();
        }

        private void btnEditRoleUser_Click(object sender, RoutedEventArgs e)
        {
            EditorStatusUser win = new EditorStatusUser();
            win.ShowDialog();
        }

        private void EditTypeRoom_btn_Click(object sender, RoutedEventArgs e)
        {
            EditorTypeRoom win = new EditorTypeRoom();
            win.ShowDialog();
            FillListRoom();
        }

        private void EditStatusRoom_btn_Click(object sender, RoutedEventArgs e)
        {
            EditorStatusRoom win = new EditorStatusRoom();
            win.ShowDialog();
            FillListRoom();
        }
    }
}
