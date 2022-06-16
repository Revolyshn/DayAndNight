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
    /// Логика взаимодействия для EditorStatusRoom.xaml
    /// </summary>
    public partial class EditorStatusRoom : Window
    {
        private BD.room_status _currentStatus = new BD.room_status();

        public EditorStatusRoom()
        {
            InitializeComponent();
            DataContext = _currentStatus;
        }

        private void LoadToListOfRoles()
        {
            BD.DayAndNightEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            ListSR.ItemsSource = BD.DayAndNightEntities.GetContext().room_status.ToList();
        }


        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadToListOfRoles();
            }
        }

        private void btnAddSR_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tbSRName.Text))
                errors.AppendLine("Введите корректное название роли!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentStatus.name == null)
            {
                _currentStatus.name = tbSRName.Text;
                Lib.Connector.DataBase().room_status.Add(_currentStatus);
            }

            try
            {
                Lib.Connector.DataBase().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadToListOfRoles();
        }

        private void btnDeleteSR_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите удалить данные?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var statuses = ListSR.SelectedItems.Cast<BD.room_status>().ToList();
                    BD.DayAndNightEntities.GetContext().room_status.RemoveRange(statuses);
                    BD.DayAndNightEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    LoadToListOfRoles();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
