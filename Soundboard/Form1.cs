using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Soundboard
{
    public partial class Form1 : Form
    {

        player player = new player();
        int aktSeite = 0;
        int buttonCount = 0;
        List<string> soundPath = new List<string>();
        List<Button> buttons = new List<Button>();
        List<string> name = new List<string>();
        saveLoad save = new saveLoad();
        public Form1()
        {
            InitializeComponent();
        }

        public void fillListEmpty() // Anzahl wieiviel buttons auf seite
        {
            for (int i = 0; i < 24; i++)
            {
                soundPath.Add("Leer");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Value = 50;
            label2.Text = "Lautstärke " + trackBar1.Value;
            button2.Enabled = false;
            label1.Text = Convert.ToString(aktSeite);
            if (!save.fileexist())       //Wenn die datei nicht Existiert
            {
                save.createFile();
                fillListEmpty();
                save.addText(soundPath);
            }
            else
            {
                soundPath = save.getList();
            }
            createButtons();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int id = int.Parse(btn.Name);
            id = id + 1 + (24 * aktSeite);
            Console.WriteLine("Lade Sound ID : " + id);

            Console.WriteLine(save.fileexist());
            OpenFileDialog dialog = new OpenFileDialog();
            if (checkBox2.Checked) // Wenn löschen akktivieret
            {
                soundPath[id - 1] = "Leer";
                setButtonNames(aktSeite);
                save.save(soundPath);
            }
            else
            {
                if (soundPath[id - 1] == "Leer")                    //Wenn kein Sound exitiert
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFileName = dialog.FileName;
                        soundPath[id - 1] = selectedFileName;
                        setButtonNames(aktSeite);

                        save.save(soundPath);

                    }
                    

                }
                else
                {
                    if (checkBox1.Checked)
                    {
                        player.playSpam(soundPath[id - 1]);
                    }
                    else
                    {
                        player.playSound(soundPath[id - 1]);
                    }


                    //Spiele Sound
                }
            }

        }

        public void setButtonNames(int seite)
        {
            for (int i = 0; i < 24; i++)
            {
               
                string buttonName = "";
                int pos = soundPath[i + (24 * seite)].LastIndexOf("\\") + 1;
                buttonName = soundPath[i + (24 * seite)].Substring(pos, soundPath[i + (24 * seite)].Length - pos);
                buttons[i].Text = buttonName;
            }
        }

        public void createButtons()
        {
            int posx = 10;       // Unterschied 120
            int posy = 120;     // Unterschied 80 Pro Reihe
            int zahler = 0;     // MAximal 6 Pro Reihe
            for (int i = 0; i < 24; i++)
            {
                Button newButton = new Button();
                newButton.Name = buttons.Count.ToString();
                newButton.Click += new EventHandler(button1_Click);
                newButton.Location = new Point(posx, posy);
                posx += 130;
                if (zahler == 5)
                {
                    posy += 90;
                    posx = 10;
                    zahler = 0;

                }
                else
                {
                    zahler++;
                }
                newButton.Size = new Size(130, 75);
                buttons.Add(newButton);
                this.Controls.Add(newButton);
                buttonCount++;
            }
            setButtonNames(aktSeite);
        }
        public bool doesSiteExist(int seite)
        {
            bool exist = true;
            int anzahlsollte = 23 * (seite + 1);
            if (soundPath.Count < anzahlsollte)
            {
                exist = false;
            }
            return exist;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            aktSeite--;
            label1.Text = Convert.ToString(aktSeite);
            Console.WriteLine(doesSiteExist(aktSeite));
            if (aktSeite == 0)
            {
                button2.Enabled = false;
            }
            setButtonNames(aktSeite);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            aktSeite++;
            if (!doesSiteExist(aktSeite))
            {
                fillListEmpty();
                save.save(soundPath);
            }
            Console.WriteLine(doesSiteExist(aktSeite));
            label1.Text = Convert.ToString(aktSeite);
            setButtonNames(aktSeite);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            player.stopSound();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            player.setVolume(trackBar1.Value);
            label2.Text = "Lautstärke " + trackBar1.Value;
        }
    }
}
