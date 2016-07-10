using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeTwitter
{
    class User
    {
        public string UserName { get; set; }
        public List<string> Following = new List<string>();
    }
}
