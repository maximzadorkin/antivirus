using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Timers;

namespace ServiceDll
{
    public class ScheduledInspections
    {
        public System.Timers.Timer timer;

        public ScheduledInspections(){}

        public void startWatching()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 30000;
            timer.Elapsed += checkScheduledInspections;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void checkScheduledInspections(Object source, ElapsedEventArgs e)
        {
            DataBase db = new DataBase();
            List<PlanDS> plans = db.getAllPlans();
            foreach (PlanDS plan in plans)
            {
                DateTime now = DateTime.Now;
                bool isShouldInspect = PlanDS.getTimeStringFormat(now).Equals(PlanDS.getTimeStringFormat(plan.time));
                if (isShouldInspect)
                {
                    Thread inspectThread = new Thread(new ThreadStart(
                        () => this.startInspection(plan)
                    ));
                    inspectThread.Start();
                }
            }
        }


        private void startInspection(PlanDS plan)
        {
            DataBase db = new DataBase();
            db.removePlan(plan);

            Scanner scanner = new Scanner();
            scanner.start(plan.path);


            while (scanner.getScanStatus()) { }

            string[] result = scanner.getScanResult().Split('\n');

            if (result.Length <= 3) return;

            for (int i = 3; i < result.Length - 1; i += 1) {
                db.addToFoundViruses(result[i]);
            }
        }
    }
}
