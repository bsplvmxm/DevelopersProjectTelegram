using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramTestBot.BL
{
    public class Groups
    {
        public string NameOfGroup { get; set; }
        public long ChatId { get; private set; }
        public List<Groups> AllGroups { get; set; }

        public Groups(string nameOfGroup)
        {
            NameOfGroup = nameOfGroup;
            AllGroups = new List<Groups>();
        }

        public void ChangeName(string newName)
        {
            if (newName == null)
            {
                throw new NullReferenceException();
            }

            NameOfGroup = newName;
        }
    }
}
