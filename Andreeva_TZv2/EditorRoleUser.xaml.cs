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
    /// Логика взаимодействия для EditorRoleUser.xaml
    /// </summary>
    public partial class EditorRoleUser : Window
    {
        private BD.role _currentRole = new BD.role();

        public EditorRoleUser()
        {
            InitializeComponent();
            DataContext = _currentRole;
        }

        private void btnAddRole_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(tbRoleName.Text))
                errors.AppendLine("Введите корректное название роли!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentRole.name == null)
            {
                _currentRole.name = tbRoleName.Text;
                Lib.Connector.DataBase().role.Add(_currentRole);
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
            ListRoleUser.ItemsSource = BD.DayAndNightEntities.GetContext().role.ToList();
        }

        private void ListRoleUser_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadToListOfRoles();
            }
        }

        private void btnDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            

            if (MessageBox.Show($"Вы точно хотите удалить данные?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    var roles = ListRoleUser.SelectedItems.Cast<BD.role>().ToList();
                    BD.DayAndNightEntities.GetContext().role.RemoveRange(roles);
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
