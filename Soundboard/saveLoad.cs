using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Soundboard
{
    class saveLoad
    {
        public bool fileexist(string saveFile)
        {
            bool antwort = false;
            if (File.Exists(saveFile+".txt"))
            {
                antwort = true;
            }
            return antwort;
        }

        public void createFile(string saveFile)
        {
            FileStream sw = File.Create(saveFile+".txt");
            sw.Close();
        }

        public void addText(List<String> sound,string saveFile)
        {
            TextWriter tw = new StreamWriter(saveFile+".txt");
            foreach (String s in sound)
                tw.WriteLine(s);

            tw.Close();
        }
        public void addText(List<Color> sound, string saveFile)
        {
            TextWriter tw = new StreamWriter(saveFile + ".txt");
            foreach (Color s in sound)
                tw.WriteLine(s.ToArgb());

            tw.Close();
        }
        public List<String> getList(string saveFile)
        {
            List<String> sound = new List<string>();

            using (var sr = new StreamReader(saveFile+".txt"))
            {
                while (sr.Peek() >= 0)
                    sound.Add(sr.ReadLine());
            }
            Console.WriteLine(sound.Count());

            return sound;
        }

        public List<Color> getListC(string saveFile)
        {
            List<Color> sound = new List<Color>();

            using (var sr = new StreamReader(saveFile + ".txt"))
            {
                string jaja = "";
                while (sr.Peek() >= 0)
                {
                    jaja = sr.ReadLine();
                    int far = int.Parse(jaja);
                    sound.Add(Color.FromArgb(far));
                }
                Console.WriteLine(sound.Count());
            }
            return sound;
        }

        public void save(List<String> sound, string saveFile)
        {
            File.Delete(saveFile+".txt");
            this.createFile(saveFile);
            this.addText(sound,saveFile);
        }
        public void saveC(List<Color> sound, string saveFile)
        {
            File.Delete(saveFile + ".txt");
            this.createFile(saveFile);
            this.addText(sound, saveFile);
        }

    }
}
