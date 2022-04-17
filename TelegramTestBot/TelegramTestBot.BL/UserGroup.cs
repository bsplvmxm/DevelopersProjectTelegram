using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    public class UserGroup
    {
        public string GroupName { get; private set; }

        public List<UserName> Users { get; private set; }

        public UserGroup()
        {
            Users = new List<UserName>();
            GroupName = "New group";
        }

        public UserGroup(string groupName)
        {
            GroupName = groupName;
            Users = new List<UserName>();
        }
    }
}
