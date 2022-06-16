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
    /// Логика взаимодействия для EditorTypeRoom.xaml
    /// </summary>
    public partial class EditorTypeRoom : Window
    {
        private BD.type_room _currentType = new BD.type_room();

        public EditorTypeRoom()
        {
            InitializeComponent();
            DataContext = _currentType;
        }

        private void LoadToListOfRoles()
        {
            BD.DayAndNightEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            ListTR.ItemsSource = BD.DayAndNightEntities.GetContext().type_room.ToList();
        }

        private void btnAddTR_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tbNameTR.Text))
                errors.AppendLine("Введите корректное название роли!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentType.name == null)
            {
                _currentType.name = tbNameTR.Text;
                Lib.Connector.DataBase().type_room.Add(_currentType);
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

        private void btnDeleteTR_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите удалить данные?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var types = ListTR.SelectedItems.Cast<BD.type_room>().ToList();
                    BD.DayAndNightEntities.GetContext().type_room.RemoveRange(types);
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
