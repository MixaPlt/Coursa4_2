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
        public class Field
        {
            char[,] cells;
            public Field(Map map, string connectionString)
            {
                cells = new char[map.Height, map.Width];
                string queryString = "SELECT * FROM [dbo].[Cell] WHERE MapId = " + map.Id.ToString() + ";";
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
        public string Name { get; set; }
        public int Id { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public string AuthorName { get { return Author.name; } }
        public User Author;
        public Field field;

        public void loadField(string connectionString)
        {
            field = new Field(this, connectionString);
        }

        public void loadAuthor(string connectionString)
        {
            Author = User.loadUserById(AuthorId, connectionString);
        }

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
