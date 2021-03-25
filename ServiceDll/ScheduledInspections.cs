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
            List<FileDS> filesForInspect = new List<FileDS>();
            bool isFile = File.Exists(plan.path);

            if (isFile) filesForInspect.Add(new FileDS(plan.path));
            else
            {
                string[] allEntries = Directory.GetFiles(plan.path, "*.*", SearchOption.AllDirectories);
                string[] entries = Array.FindAll(
                    allEntries,
                    s => DangersDetection.SupportedExtensions.Contains(Path.GetExtension(s).ToLower())
                );

                foreach (string path in entries)
                {
                    filesForInspect.Add(new FileDS(path));
                }
            }

            DataBase db = new DataBase();
            db.removePlan(plan);

            this.filesInspector(filesForInspect);
        }

        private void filesInspector(List<FileDS> filesForInspect)
        {
            DangersDetection detection = new DangersDetection();
            DataBase db = new DataBase();
            foreach (FileDS file in filesForInspect)
            {
                file.danger = detection.detectDanger(file.path);
                if (file.danger)
                    db.addToFoundViruses(file.path);
            }
        }
    }
}
