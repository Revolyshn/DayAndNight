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
    /// Логика взаимодействия для SuperUserReg.xaml
    /// </summary>
    public partial class SuperUserReg : Page
    {
        public SuperUserReg()
        {
            InitializeComponent();
        }

        private void regSuperUser_btn_Click(object sender, RoutedEventArgs e)
        {
            BD.user SuperUser;
            BD.role role;
            BD.user_status status;

            if (loginSuperUser_tb != null && passwordSuperUser_tb != null && nameSuperUser_tb != null)
            {
                var roleUser = Lib.Connector.DataBase().role.FirstOrDefault(a => a.name == "Super_User");
                var statusUser = Lib.Connector.DataBase().role.FirstOrDefault(a => a.name == "Работает");

                role = new BD.role
                {
                    name = "Super_User"
                };

                status = new BD.user_status
                {
                    name = "Работает"
                };

                if (roleUser == null && statusUser == null)
                {
                    Lib.Connector.DataBase().role.Add(role);
                    Lib.Connector.DataBase().user_status.Add(status);
                }

                SuperUser = new BD.user
                {
                    name = nameSuperUser_tb.Text,
                    role = role.name,
                    status = status.name,
                    password = passwordSuperUser_tb.Password,
                    login = loginSuperUser_tb.Text,
                };


                Lib.Connector.DataBase().user.Add(SuperUser);
                Lib.Connector.DataBase().SaveChanges();

                NavigationService.Navigate(Lib.Pages.authorization);
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }
    }
}
