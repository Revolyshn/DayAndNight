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
    /// Логика взаимодействия для SUserHome.xaml
    /// </summary>
    public partial class SUserHome : Window
    {
        public SUserHome()
        {
            InitializeComponent();
            CheckExistSUser();
        }
        void CheckExistSUser()
        {
            BD.user super_admin = Lib.Connector.DataBase().user.FirstOrDefault(a => a.role != null && a.role == "Super_User");
            if (super_admin == null)
            {
                SUserFrame.Navigate(Lib.Pages.registration);
            }
            else
            {
                SUserFrame.Navigate(Lib.Pages.authorization);
            }
        }
    }
}
