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
    /// Логика взаимодействия для EditorStatusUser.xaml
    /// </summary>
    public partial class EditorStatusUser : Window
    {
        private BD.user_status _currentStatus = new BD.user_status();
        public EditorStatusUser()
        {
            InitializeComponent();
            DataContext = _currentStatus;
        }

        private void btnAddStatusUser_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tbStatusUser.Text))
                errors.AppendLine("Введите корректное название роли!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentStatus.name == null)
            {
                _currentStatus.name = tbStatusUser.Text;
                Lib.Connector.DataBase().user_status.Add(_currentStatus);
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

        private void LoadToListOfRoles()
        {
            BD.DayAndNightEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            ListStatusUser.ItemsSource = BD.DayAndNightEntities.GetContext().user_status.ToList();
        }

        private void btnDeleteStatusUser_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите удалить данные?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var statuses = ListStatusUser.SelectedItems.Cast<BD.user_status>().ToList();
                    BD.DayAndNightEntities.GetContext().user_status.RemoveRange(statuses);
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

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadToListOfRoles();
            }
        }
    }
}
