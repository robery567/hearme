using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearMe
{
    class User
    {
        private string name;
        private string avatar;
        private List<string> friendsList;

        public string Name { get => name; set => name = value; }
        public string Avatar { get => avatar; set => avatar = value; }
        public List<string> FriendsList { get => friendsList; set => friendsList = value; }

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => base.ToString();
    }
}
