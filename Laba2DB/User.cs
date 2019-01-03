using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2DB
{
    class User
    {
        public User(int _id)
        {
            id = _id;
        }

        public int id;
        public static List<User> loadUsers()
        {
            List<User> list = new List<User>();
            list.Add(new User(0));
            return list;
        }
    }
}
