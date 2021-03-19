using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Сервер
{
    class Plans
    {
        public Plans()
        {
            Thread plansThread = new Thread(new ThreadStart(this.checkPlans));
        }

        private void checkPlans()
        {
            while (true)
            {
                // check all plans and start scan 
            }
        }

        public static string[] getPlans()
        {
            return new string[] {"", ""};
        }

        public static bool addPlan() {
            return true;
        }

        public static bool deletePlan()
        {
            return true;
        }
    }
}
