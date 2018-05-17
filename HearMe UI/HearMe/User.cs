using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearMe
{
    class User
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string avatar { get; set; }
        public string availability { get; set; }
        public List<string> friendsList { get; set; }
    }
}
