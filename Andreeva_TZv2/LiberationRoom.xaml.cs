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
                BD.borrow_room borrowRoom = andreeva_tz.borrow_room.FirstOrDefault(a => a.info_room.num_room == i && a.client == LoginClient.Text);

                DateTime date = DateTime.Parse(DataOtbitiya.Text);
                if (borrowRoom.date_settlement <= date && date < borrowRoom.date_settlement.AddDays(borrowRoom.count_day))
                {
                    BD.booking_history bk_history = new BD.booking_history
                    {
                        borrow_room = borrowRoom.id,

                        date_departure = DateTime.Parse(DataOtbitiya.Text)
                    };
                    andreeva_tz.borrow_room.Remove(borrowRoom);
                    //if (Comment.Text != null) bk_history.Comment = Comment.Text;

                    andreeva_tz.booking_history.Add(bk_history);
                    

                    andreeva_tz.SaveChanges();
                }else
                {
                    MessageBox.Show("Некорректные данные");
                }
            }

            
        }

        void UpdateCombobox()
        {
            foreach (BD.client r in andreeva_tz.client.ToList())
            {
                LoginClient.Items.Add(r.phone);
            }
            foreach (BD.info_room r in andreeva_tz.info_room.ToList())
            {
                NumberRoom.Items.Add(r.num_room);
            }
        }
    }
}
