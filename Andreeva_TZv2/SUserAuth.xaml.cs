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
    /// Логика взаимодействия для SUserAuth.xaml
    /// </summary>
    public partial class SUserAuth : Page
    {
        static string allowChar = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";

        public SUserAuth(string _login, string _password)
        {
            InitializeComponent();
            loginSuperUser_tb.Text = _login;
            passwordSuperUser_tb.Password = _password;
        }
        public SUserAuth()
        {
            InitializeComponent();
        }

        private void CreateCaptcha()
        {
            string pwd = "";
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                pwd += allowChar.Substring(random.Next(0, allowChar.Length), 1);
            }
            captcha.Text = pwd;
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (captcha.Text == ValidateCaptcha.Text)
                {
                    NavigationService.Navigate(Lib.Pages.functional);
                }
                else
                {
                    CreateCaptcha();
                }
            }
        }
        private void loginSuperUser_btn_Click(object sender, RoutedEventArgs e)
        {
            if (loginSuperUser_tb != null && passwordSuperUser_tb != null)
            {
                var user = Lib.Connector.DataBase().user.FirstOrDefault(a => a.login == loginSuperUser_tb.Text && a.password == passwordSuperUser_tb.Password);
                if (user != null)
                {
                    CreateCaptcha();
                    CapTcha.Visibility = Visibility.Visible;
                }
            }
            return;
        }
    }
}
