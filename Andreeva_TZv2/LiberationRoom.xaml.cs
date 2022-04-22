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
    /// Логика взаимодействия для LiberationRoom.xaml
    /// </summary>
    public partial class LiberationRoom : Page
    {
        BD.DayAndNightEntities andreeva_tz = new BD.DayAndNightEntities();
        public LiberationRoom()
        {
            InitializeComponent();
            UpdateCombobox();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(LoginClient.SelectedIndex > -1 && NumberRoom.SelectedIndex > -1 && DataOtbitiya.Text != null)
            {
                int i = int.Parse(NumberRoom.Text);
                BD.BorrowRoom borrowRoom = andreeva_tz.BorrowRoom.FirstOrDefault(a => a.InfoRoom.NumberRoom == i && a.Client == LoginClient.Text);

                DateTime date = DateTime.Parse(DataOtbitiya.Text);
                if (borrowRoom.SettlementDate <= date && date < borrowRoom.SettlementDate.AddDays(borrowRoom.CountDay))
                {
                    BD.BookingHistory client = new BD.BookingHistory
                    {
                        borrowRoom = borrowRoom.ID,

                        DepartureDate = DateTime.Parse(DataOtbitiya.Text)
                    };
                    andreeva_tz.BorrowRoom.Remove(borrowRoom);
                    if (Comment.Text != null) client.Comment = Comment.Text;

                    andreeva_tz.BookingHistory.Add(client);
                    

                    andreeva_tz.SaveChanges();
                }else
                {
                    MessageBox.Show("Некорректные данные");
                }
            }

            
        }

        void UpdateCombobox()
        {
            foreach (BD.Client r in andreeva_tz.Client.ToList())
            {
                LoginClient.Items.Add(r.Login);
            }
            foreach (BD.InfoRoom r in andreeva_tz.InfoRoom.ToList())
            {
                NumberRoom.Items.Add(r.NumberRoom);
            }
        }
    }
}
