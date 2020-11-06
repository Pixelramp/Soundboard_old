using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
        List<soundButton> sounder = new List<soundButton>();
        saveLoad save = new saveLoad();
        const string version = "1.2";
        public Form1()
        {
            InitializeComponent();
        }
        public void fillListEmpty() // Anzahl wieiviel buttons auf seite
        {
            for (int i = 0; i < 24; i++)
            {
                sounder.Add(new soundButton("Leer", "Leer", Color.FromArgb(1)));
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = label4.Text + " " + version;
            trackBar1.Value = 50;
            label2.Text = "Lautstärke " + trackBar1.Value;
            button2.Enabled = false;
            label1.Text = Convert.ToString(aktSeite);
            if (!save.saveExist("pfad"))
            {
                save.createFile();
                fillListEmpty();
            }
            else
            {
                sounder = save.getLoadedList();
            }

            createButtons();
            save.saveJson(sounder);
        }
        private void speichern()
        {
            save.saveJson(sounder);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int id = int.Parse(btn.Name);
            id = id + (24 * aktSeite);
            OpenFileDialog dialog = new OpenFileDialog();
            if (checkBox3.Checked)
            {
                ColorDialog colorDialog1 = new ColorDialog();
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {

                    sounder[id - (24 * aktSeite)].Button.BackColor = Color.FromArgb(colorDialog1.Color.ToArgb());
                    sounder[id].Farbe = Color.FromArgb(colorDialog1.Color.ToArgb());
                    speichern();
                }
            }
            else
            {
                if (checkBox2.Checked) // Wenn löschen akktivieret
                {
                    sounder[id].Pfad = "Leer";
                    sounder[id].Name = "Leer";
                    setButtonNames(aktSeite);
                    speichern();
                }
                else
                {
                    if (sounder[id].Pfad == "Leer")                    //Wenn kein Sound exitiert
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            string selectedFileName = dialog.FileName;
                            sounder[id].Pfad = selectedFileName;
                            setButtonNames(aktSeite);
                            int pos = selectedFileName.LastIndexOf("\\") + 1;
                            sounder[id].Name = Interaction.InputBox("Name of the Sound", "Name", selectedFileName.Substring(pos, selectedFileName.Length - pos));
                            setButtonNames(aktSeite);
                            Console.WriteLine(id);
                            speichern();
                        }
                    }
                    else
                    {
                        if (checkBox1.Checked)
                        {
                            player.playSpam(sounder[id].Pfad);
                        }
                        else
                        {
                            player.playSound(sounder[id].Pfad);
                        }
                    }
                }
            }
        }
        public void setButtonNames(int seite)
        {
            for (int i = 0; i < 24; i++)
            {
                string buttonName = "";
                Console.WriteLine(sounder[i + (24 * seite)].Name);
                buttonName = sounder[i + (24 * seite)].Name;
                sounder[i].Button.Text = buttonName;
                sounder[i].Button.BackColor = sounder[i + (24 * seite)].Farbe;
            }
        }
        public void createButtons()
        {
            int posx = 10;       // Unterschied 120
            int posy = 120;     // Unterschied 80 Pro Reihe
            int zahler = 0;     // MAximal 6 Pro Reihe
            int number = 0;
            for (int i = 0; i < 24; i++)
            {
                Button newButton = new Button();
                // int number = sounder.Count - 24;

                Console.WriteLine(number);
                newButton.Name = number.ToString();
                newButton.Click += new EventHandler(button1_Click);
                newButton.Location = new Point(posx, posy);
                newButton.BackColor = sounder[i].Farbe;
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
                sounder[i].Button = newButton;
                this.Controls.Add(sounder[i].Button);
                buttonCount++;
                number++;
            }
            setButtonNames(aktSeite);
        }
        public bool doesSiteExist(int seite)
        {
            bool exist = true;
            int anzahlsollte = 23 * (seite + 1);
            if (sounder.Count < anzahlsollte)
            {
                exist = false;
            }
            return exist;
        }
        public bool doesSiteExistC(int seite)
        {
            bool exist = true;
            int anzahlsollte = 23 * (seite + 1);
            return exist;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            aktSeite--;
            label1.Text = Convert.ToString(aktSeite);
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
            //ÜBERPRÜFEN OB NEUE SEITE EXISTIERT WENN:
            fillListEmpty();
            //DANNACH
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

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox3.Enabled = false;
            }
            else
            {
                checkBox3.Enabled = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e) // Pause
        {
            player.pause();
        }

        private void button5_Click(object sender, EventArgs e) // Play
        {
            player.resume();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}