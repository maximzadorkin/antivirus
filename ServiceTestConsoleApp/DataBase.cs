using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace ServiceTestConsoleApp
{
    class DataBase
    {
        private string source = $"{Directory.GetCurrentDirectory()}\\AntivirusZM.db";
        SqliteConnection connection;

        public DataBase()
        {
            this.connection = new SqliteConnection("Data Source=" + this.source);
        }

        public List<VirusDS> getViruses(string signature, int position)
        {
            List<VirusDS> viruses = new List<VirusDS>();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $@"
                SELECT * FROM viruses 
                    WHERE Signature Like '{signature}%' 
                                AND OffsetBegin <= {position} 
                                AND OffsetEnd >= {position}
            ";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    VirusDS virus = new VirusDS(
                            Int32.Parse(reader.GetString(0)),
                            reader.GetString(1),
                            reader.GetString(2),
                            Int32.Parse(reader.GetString(3)),
                            Int32.Parse(reader.GetString(4))
                    );
                    viruses.Add(virus);

                }
            }
            connection.Close();
            return viruses;
        }

        public List<PlanDS> getAllPlans()
        {
            connection.Open();
            List<PlanDS> plans = new List<PlanDS>();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM plans";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    PlanDS plan = new PlanDS(
                        reader.GetString(1),
                        PlanDS.getTimeFromStringFormat(reader.GetString(2)),
                        Int32.Parse(reader.GetString(0))
                    );
                    plans.Add(plan);
                }
            }
            connection.Close();
            return plans;
        }

        public void addPlan(PlanDS plan)
        {
            List<PlanDS> ps = this.getAllPlans();
            foreach (PlanDS p in ps)
            {
                bool haveThisTime = p.getTimeStringFormat() == plan.getTimeStringFormat();
                bool haveThisPath = p.path == plan.path;
                if (haveThisTime && haveThisPath)
                    return;
            }

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"insert into plans(path, time) values(\"{plan.path}\", \"{plan.getTimeStringFormat()}\")";
            command.ExecuteScalar();
            connection.Close();
        }

        public void removePlan(PlanDS plan)
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM plans WHERE id = '{plan.id}'";
            command.ExecuteScalar();
            connection.Close();
        }

        public void addToQuarantine(string path)
        {
            List<string> q = this.getQuarantineFiles();
            if (q.Contains(path)) return;

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"insert into quarantine(path) values(\"{path}\")";
            command.ExecuteScalar();
            connection.Close();
        }

        public void removeFromQuarantine(string path)
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM quarantine WHERE path = '{path}'";
            command.ExecuteScalar();
            connection.Close();
        }

        public List<string> getQuarantineFiles()
        {
            connection.Open();
            List<string> quarantine = new List<string>();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM quarantine";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    quarantine.Add(reader.GetString(1));
                }
            }
            connection.Close();
            return quarantine;
        }

        public void addToFoundViruses(string path)
        {
            List<string> vs = this.getVirusesFiles();
            if (vs.Contains(path)) return;

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"insert into found_viruses(path) values(\"{path}\")";
            command.ExecuteScalar();
            connection.Close();
        }

        public void removeFromFoundViruses(string path)
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM found_viruses WHERE path = '{path}'";
            command.ExecuteScalar();
            connection.Close();
        }

        public List<string> getVirusesFiles()
        {
            connection.Open();
            List<string> viruses = new List<string>();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM found_viruses";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    viruses.Add(reader.GetString(0));
                }
            }
            connection.Close();
            return viruses;
        }
    }
}
