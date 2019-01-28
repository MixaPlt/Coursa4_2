using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;

namespace Laba2DB
{
    class Date
    {
        int year;
        int month;
        int day;
        string presentation;
        public Date(string target) : this()
        {
            target = target.Split(' ')[0];
            presentation = target;
            try
            {
                var f = target.Split('.');
                day = Int32.Parse(f[0]);
                month = Int32.Parse(f[1]);
                year = Int32.Parse(f[2]);
            }
            catch { }
        }
        public Date()
        {
            presentation = "";
            year = 0;
            month = 0;
            day = 0;
        }
        public string toString()
        {
            return presentation;
        }
    }

    class User
    {
        public User(int _id, string _name, int _permission, int _expereance, Date regdate)
        {
            id = _id;
            name = _name;
            permission = _permission;
            expereance = _expereance;
            Registered_date = regdate;
        }

        public int id { get; set; }
        public string name { get; set; }
        public int permission { get; set; }
        public int expereance { get; set; }
        public string password;
        public string Registrated { get { return Registered_date.toString(); } }
        public Date Registered_date;

        public static List<User> loadUsers(string connectionString)
        {
            List<User> list = new List<User>();
            string queryString = "SELECT * FROM [dbo].[UserInfo]";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                list.Add(new User(Int32.Parse(reader["id"].ToString()),
                    reader["Name"].ToString(),
                    Int32.Parse(reader["Permission"].ToString()),
                    Int32.Parse(reader["Expereance"].ToString()),
                    new Date(reader["Registration_date"].ToString())
                    ));
            }
            connection.Close();
            return list;
        }

        public static User loadUserById(int id, string connectionString)
        {
            string queryString = "SELECT * FROM [dbo].[UserInfo] WHERE Id = " + id.ToString() + ";";
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
                        Int32.Parse(reader["Expereance"].ToString()),
                    new Date(reader["Registration_date"].ToString())
                        );
            }
            catch { res = new User(0, "", 0, 0, new Date()); }
            connection.Close();
            return res;
        }

        public static List<User> loadUsersByName(string name, string connectionString)
        {
            List<User> list = new List<User>();
            string queryString = "SELECT * FROM [dbo].[UserInfo] WHERE Name = '" + name + "';";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new User(Int32.Parse(reader["id"].ToString()),
                    reader["Name"].ToString(),
                    Int32.Parse(reader["Permission"].ToString()),
                    Int32.Parse(reader["Expereance"].ToString()),
                    new Date(reader["Registration_date"].ToString())
                    ));
                
                list[list.Count - 1].password = reader["Password"].ToString();
            }
            connection.Close();
            return list;
        }
    }
}
