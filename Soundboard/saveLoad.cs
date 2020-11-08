using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace Soundboard
{

    class saveLoad
   {
        
        public void saveJson(List<soundButton> list, List<string> hotkey)
        {
            TextWriter tw = new StreamWriter("name.dat");
            TextWriter tw1 = new StreamWriter("pfad.dat");
            TextWriter tw2 = new StreamWriter("color.dat");
            TextWriter tw3 = new StreamWriter("hotkey.dat");
            List<string> name = new List<string>();
            List<string> pfad = new List<string>();
            List<string> farbe = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                name.Add(list[i].Name);
                pfad.Add(list[i].Pfad);
                farbe.Add(list[i].Farbe.ToArgb().ToString());
            }

            foreach (String s in name)
            {
                tw.WriteLine(s);
            }
            tw.Close();

            foreach (String s in pfad)
            {
                tw1.WriteLine(s);
            }
            tw1.Close();

            foreach (String s in farbe)
            {
                tw2.WriteLine(s);
            }
            tw2.Close();

            foreach (String s in hotkey)
            {
                tw3.WriteLine(s);
            }
            tw3.Close();
        }
        public void createFile()
        {
           // File.Create("name.dat");
           // File.Create("pfad.dat");
          //  File.Create("color.dat");
        }
        public bool saveExist(string  save)
        {

            return File.Exists(save+".dat");
        }
        public List<soundButton> getLoadedList()
        {
            List<soundButton> list = new List<soundButton>();
            List<string> name = new List<string>();
            List<string> pfad = new List<string>();
            List<string> farbe = new List<string>();

            string[] readText = File.ReadAllLines("name.dat", Encoding.UTF8);
            foreach (string s in readText)
            {
                name.Add(s);
            }
            readText = File.ReadAllLines("pfad.dat", Encoding.UTF8);
            foreach (string s in readText)
            {
                pfad.Add(s);
            }

            readText = File.ReadAllLines("color.dat", Encoding.UTF8);
            foreach (string s in readText)
            {
                farbe.Add(s);
            }
            for (int i = 0; i < pfad.Count; i++)
            {
                int colo = int.Parse(farbe[i]);
                list.Add(new soundButton(pfad[i], name[i], Color.FromArgb(colo),0));
            }

            return list;
        }
        public List<int> getLoadedHokeys()
        {
            List<int> hotkeyList = new List<int>();
            List<string> hotkeyS = new List<string>();
            string[] readText = File.ReadAllLines("hotkey.dat", Encoding.UTF8);
            foreach (string s in readText)
            {
                hotkeyS.Add(s);
            }
            hotkeyList = hotkeyS.ConvertAll(int.Parse);
            return hotkeyList;
        }

    }
}
