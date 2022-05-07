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
    /// Логика взаимодействия для SUser.xaml
    /// </summary>
    public partial class SUserWin : Window
    {
        SuperUserReg super =  new SuperUserReg();
        SUser.SUserLogin login = new SUser.SUserLogin();

        public SUserWin()
        {
            InitializeComponent();
            CheckExistSUser();
        }
        void CheckExistSUser()
        {
            BD.user super_admin = Lib.Connector.GetModel().user.FirstOrDefault(a => a.role != null && a.role == "Super_User");
            if (super_admin == null)
            {
                SUserFrame.Navigate(super);
            }
            else
            {
                SUserFrame.Navigate(login);
            }
        }
    }
}
