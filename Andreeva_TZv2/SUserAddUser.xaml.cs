using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace Andreeva_TZv2
{
    /// <summary>
    /// Логика взаимодействия для SUserAddUser.xaml
    /// </summary>
    public partial class SUserAddUser : Window
    {
        public SUserAddUser()
        {

            InitializeComponent();

            
            CbUserStatus.ItemsSource = BD.DayAndNightEntities.GetContext().user_status.ToList();
            CbUserRole.ItemsSource = BD.DayAndNightEntities.GetContext().role.Where(a => a.name != "Super_User").ToList();

        }

        private void btnSafeUserChanges_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TbNameUser.Text))
                errors.AppendLine("Введите ФИО!");

            if (string.IsNullOrWhiteSpace(TbLogin.Text))
                errors.AppendLine("Введите логин!");

            if (string.IsNullOrWhiteSpace(TbPassword.Text))
                errors.AppendLine("Введите пароль!");

            if (CbUserStatus.SelectedItem == null)
                errors.AppendLine("Статус не указан!");

            if (CbUserRole.SelectedItem == null)
                errors.AppendLine("Роль не указана!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            using (BD.DayAndNightEntities db = new BD.DayAndNightEntities())
            {
                var user = new BD.user
                {
                    name = TbNameUser.Text,
                    login = TbLogin.Text,
                    password = TbPassword.Text,
                    status = CbUserStatus.Text,
                    role = CbUserRole.Text
                };

                db.user.Add(user);
                try
                {
                    db.SaveChanges();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            
        }
    }
}
