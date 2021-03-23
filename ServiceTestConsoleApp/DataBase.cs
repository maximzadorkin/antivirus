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
            connection.Close();
            return viruses;
        }

        public List<PlanDS> getAllPlans()
        {
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
            connection.Close();
            return plans;
        }

        public bool addPlan(string path)
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Plans";

            connection.Close();
            return true;
        }

        public bool removePlan(PlanDS plan)
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Plans";

            connection.Close();
            return true;
        }
    }
}
