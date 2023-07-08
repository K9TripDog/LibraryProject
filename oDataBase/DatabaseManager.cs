using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using Microsoft.Data.Sqlite;
using Windows.UI.Xaml.Controls;

namespace DB
{
    public class DatabaseManager
    {
        SqliteConnection Connection;
        public DatabaseManager(string dbPath)
        {
            Connection = new SqliteConnection($"Filename={dbPath}");

         //   CreateDatabase(); need to make if statment
         //   that if the file dosent exist this will make this Defult DataBase in the CreateDataBase Method

        }

        private void CreateDatabase()
        {
            using (Connection)
            {
                Connection.Open();

                var command = Connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE Item (
	            Id	INTEGER NOT NULL UNIQUE,
	            Name	TEXT NOT NULL,
	            Price	INTEGER NOT NULL DEFAULT 0,
	            Genre	TEXT NOT NULL,
	            Publisher	TEXT NOT NULL,
	            Quantity	INTEGER NOT NULL DEFAULT 0,
	            Discount_Price	INTEGER,
	            PublishDate	INTEGER,
	            RentDate	INTEGER,
	            Description	TEXT,
	            Author      TEXT,
	            PRIMARY KEY(Id AUTOINCREMENT)
             );";
                command.ExecuteNonQuery();

                command = Connection.CreateCommand();
                command.CommandText = @"
               INSERT INTO Item (Name, Price, Genre, Publisher, Quantity) VALUES
               ('Book1', '120', 'Economy', 'Books.com', '10'),
               ('Magazine2', '70', 'newsletter', 'CNBC', '20'),
               ('Book2', '150', 'Comedy', 'ComedyChannel', '15'),
               ('Magazine1', '50', 'games', 'Youtube', '30'
             );";
                command.ExecuteNonQuery();

                Connection.Close();
            }
        }

        public void ShowDataTry(TextBlock output)
        {
            using (Connection)
            {
                Connection.Open();

                StringBuilder sb = new StringBuilder();

                var command = Connection.CreateCommand();
                command.CommandText = "SELECT* FROM Item";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sb.AppendLine(reader.GetString(1));

                        output.Text = sb.ToString() + Environment.NewLine;
                    }
                }

                Connection.Close();
            }
        }
        public void RemoveItem(string name)
        {
            using (Connection)
            {
                Connection.Open();

                var command = Connection.CreateCommand();
                command.CommandText = "DELETE FROM Item WHERE Name = @name";
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();

                Connection.Close();
            }
        }

    }

}
