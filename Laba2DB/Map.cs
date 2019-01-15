using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace Laba2DB
{
    class Map
    {
        class Field
        {
            char[,] cells;
            public Field(Map map, string connectionString)
            {
                cells = new char[map.Height, map.Width];
                string queryString = "SELECT Value, Row, Column FROM [dbo].[Cells] WHERE MapId = " + map.Id.ToString() + ";";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int row = Int32.Parse(reader["Row"].ToString());
                    int column = Int32.Parse(reader["Column"].ToString());
                    char value = reader["Value"].ToString()[0];
                    cells[row, column] = value;
                }
            }
            public char this[int row, int column]
            {
                get { return cells[row, column]; }
                set { cells[row, column] = value; }
            }
        }
        string Name { get; set; }
        int Id { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        int AuthorId { get; set; }
        string Description { get; set; }
        public Map(int _id, string connectionString)
        {
            Id = _id;
            string queryString = "SELECT * FROM [dbo].[MapInfo] WHERE Id = " + Id.ToString() + ";";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Height = Int32.Parse(reader["Height"].ToString());
            Width = Int32.Parse(reader["Width"].ToString());
            Name = reader["Name"].ToString();
            AuthorId = Int32.Parse(reader["AuthorId"].ToString());
            Description = reader["Description"].ToString();
        }
        public Map(SqlDataReader reader)
        {
            Id = Int32.Parse(reader["Id"].ToString());
            Height = Int32.Parse(reader["Height"].ToString());
            Width = Int32.Parse(reader["Width"].ToString());
            Name = reader["Name"].ToString();
            AuthorId = Int32.Parse(reader["AuthorId"].ToString());
            Description = reader["Description"].ToString();
        }
        public static List<Map> readAllMaps(string connectionString)
        {
            List<Map> res = new List<Map>();
            string queryString = "SELECT * FROM [dbo].[MapInfo];";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                res.Add(new Map(reader));
            }
            return res;
        }
    }
}
