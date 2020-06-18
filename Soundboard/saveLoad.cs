using System;
using System.Collections.Generic;
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
        public bool fileexist()
        {
            bool antwort = false;
            if (File.Exists("sound.txt"))
            {
                antwort = true;
            }


            return antwort;
        }

        public void createFile()
        {
            FileStream sw = File.Create("sound.txt");
            sw.Close();
        }

        public void addText(List<String> sound)
        {
            TextWriter tw = new StreamWriter("sound.txt");
            foreach (String s in sound)
                tw.WriteLine(s);

            tw.Close();
        }

        public List<String> getList()
        {
            List<String> sound = new List<string>();

            using (var sr = new StreamReader("sound.txt"))
            {
                while (sr.Peek() >= 0)
                    sound.Add(sr.ReadLine());
            }
            Console.WriteLine(sound.Count());

            return sound;
        }

        public void save(List<String> sound)
        {
            File.Delete("sound.txt");
            this.createFile();
            this.addText(sound);
        }

    }
}
