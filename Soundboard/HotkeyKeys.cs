using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soundboard
{
    class HotkeyKeys
    {
        public string name { get; set; }
        public int key { get; set; }
        public int soundID { get; set; }

        public int actionID { get; set; }

        public

        HotkeyKeys(string Name, int Key, int actionId)
        {
            name = Name;
            key = Key;
            actionID = actionId;
            soundID = -1;
        }
    }
}
