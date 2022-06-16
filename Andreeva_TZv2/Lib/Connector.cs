using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andreeva_TZv2.Lib
{
    internal class Connector
    {
        private static BD.DayAndNightEntities basa = new BD.DayAndNightEntities();

        private static BD.user_status status = new BD.user_status();
        private static BD.user user = new BD.user();
        private static BD.booking_history booking = new BD.booking_history();
        private static BD.borrow_room borrow = new BD.borrow_room();
        private static BD.info_room room = new BD.info_room();
        private static BD.role role = new BD.role();

        public static BD.DayAndNightEntities DataBase()
        {
            return basa;
        }
        public static BD.user_status Status()
        {
            return status;
        }
        public static BD.user UserData()
        {
            return user;
        }
        public static BD.booking_history BookongData()
        {
            return booking;
        }
        public static BD.borrow_room BorrowData()
        {
            return borrow;
        }
        public static BD.info_room RoomData()
        {
            return room;
        }
        public static BD.role RoleUser()
        {
            return role;
        }
    }
    class Pages
    {
        public static SuperUserReg registration = new SuperUserReg();
        public static SUserAuth authorization = new SUserAuth();
        public static SUserHome Home = new SUserHome();
        public static SUserFunctional functional = new SUserFunctional(null);
    }
}
