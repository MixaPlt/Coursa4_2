using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace Laba2DB
{
    class User
    {
        public User(int _id, string _name, int _permission, int _expereance)
        {
            id = _id;
            name = _name;
            permission = _permission;
            expereance = _expereance;
        }

        public int id { get; set; }
        public string name { get; set; }
        public int permission { get; set; }
        public int expereance { get; set; }
        public string password;

        public static List<User> loadUsers(string connectionString)
        {
            List<User> list = new List<User>();
            string queryString = "SELECT Id, Name, Permission, Expereance FROM [dbo].[UserInfo]";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                list.Add(new User(Int32.Parse(reader["id"].ToString()),
                    reader["Name"].ToString(),
                    Int32.Parse(reader["Permission"].ToString()),
                    Int32.Parse(reader["Expereance"].ToString())
                    ));
            }
            connection.Close();
            return list;
        }

        public static User loadUserById(int id, string connectionString)
        {
            string queryString = "SELECT Id, Name, Permission, Expereance FROM [dbo].[UserInfo] WHERE Id = " + id.ToString() + ";";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            User res;
            try
            {
                res = new User(Int32.Parse(reader["id"].ToString()),
                        reader["Name"].ToString(),
                        Int32.Parse(reader["Permission"].ToString()),
                        Int32.Parse(reader["Expereance"].ToString())
                        );
            }
            catch { res = new User(0, "", 0, 0); }
            connection.Close();
            return res;
        }

        public static List<User> loadUsersByName(string name, string connectionString)
        {
            List<User> list = new List<User>();
            string queryString = "SELECT Id, Name, Password, Permission, Expereance FROM [dbo].[UserInfo] WHERE Name = '" + name + "';";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new User(Int32.Parse(reader["id"].ToString()),
                    reader["Name"].ToString(),
                    Int32.Parse(reader["Permission"].ToString()),
                    Int32.Parse(reader["Expereance"].ToString())
                    ));
                
                list[list.Count - 1].password = reader["Password"].ToString();
            }
            connection.Close();
            return list;
        }
    }
}
