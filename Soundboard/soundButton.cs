using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soundboard
{
    class soundButton
    {
        private string pfad;
        private string name;
        private int key;
        private Color farbe;
        private Button button;

        public soundButton(string pfad, string name, Color farbe, int Key)
        {
            key = Key;
            Pfad = pfad;
            Name = name;
            Farbe = farbe;
        }
        public int Key { get => key; set => key = value; }
        public string Pfad { get => pfad; set => pfad = value; }
        public string Name { get => name; set => name = value; }
        public Color Farbe { get => farbe; set => farbe = value; }
        public Button Button { get => button; set => button = value; }
    }
}
