using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearMe
{
    class User
    {
        private string email { get; set; }
        private string name { get; set; }
        private string avatar { get; set; }
        private List<string> friendsList { get; set; }
    }
}
