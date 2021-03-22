using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace ServiceTestConsoleApp
{
    class DataBase
    {
        private string source = $"{Directory.GetCurrentDirectory()}\\AntivirusZM.db";
        public List<VirusDS> getViruses(string signature, int position)
        {
            Console.WriteLine(this.source);
            List<VirusDS> viruses = new List<VirusDS>();
            SqliteConnection connection = new SqliteConnection("Data Source=" + this.source);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Viruses WHERE Signature Like '{signature}%' AND OffsetBegin <= {position} AND OffsetEnd >= {position}";

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
            return viruses;
        }

        public List<PlanDS> getAllPlans()
        {
            SqliteConnection connection = new SqliteConnection("Data Source=" + this.source);
            connection.Open();
            List<PlanDS> plans = new List<PlanDS>();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Plans";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    PlanDS plan = new PlanDS(
                        Int32.Parse(reader.GetString(0)),
                        reader.GetString(1)
                    );
                    plans.Add(plan);
                }
            }
            return plans;
        }

        public bool addPlan(string path)
        {
            SqliteConnection connection = new SqliteConnection("Data Source=" + this.source);
            connection.Open();

            return true;
        }

        public bool removePlan(PlanDS plan)
        {
            SqliteConnection connection = new SqliteConnection("Data Source=" + this.source);
            connection.Open();

            return true;
        }
    }
}
