using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andreeva_TZv2.Lib
{
    internal class Connector
    {
        private static BD.user profile;
        private static BD.DayAndNightEntities connector;

        public static BD.user GetMyProfile()
        {
            return profile = new BD.user();
        }
        public static BD.DayAndNightEntities GetModel()
        {
            return connector = new BD.DayAndNightEntities();
        }

    }
}
