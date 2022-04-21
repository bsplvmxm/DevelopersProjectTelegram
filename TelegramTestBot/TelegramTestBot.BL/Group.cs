using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    class Group
    {
        public string GroupName { get; set; }

        public List<User> Users { get; set; }
        public Group(string groupName)
        {
            GroupName = groupName;
            Users = new List<User>();
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new NullReferenceException();
            }
            Users.Add(user);
        }

        public void RemoveUser(User user)
        {
            if (user == null)
            {
                throw new NullReferenceException();
            }
            Users.Remove(user);
        }

        public override string ToString()
        {
            return GroupName;
        }
    }
}
