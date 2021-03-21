using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace findAllFiles
{
    class DataBase
    {
        SqliteConnection connection;

        public DataBase(String source)
        {
            connection = new SqliteConnection("Data Source=" + source);
            connection.Open();
        }

        public List<VirusDS> GetViruses(string signature, int position)
        {
            List<VirusDS> viruses = new List<VirusDS>();
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

        public List<string> GetAllPlans() {
            List<string> plans = new List<string>();
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Plans";
            
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string plan = new VirusDS(
                        Int32.Parse(reader.GetString(0)),
                        reader.GetString(1),
                        reader.GetString(2),
                        Int32.Parse(reader.GetString(3)),
                        Int32.Parse(reader.GetString(4))
                    );
                    plans.Add(plan);

                }
            }
            return plans;
        }
    }
}