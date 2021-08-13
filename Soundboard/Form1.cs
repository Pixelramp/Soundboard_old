using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Soundboard
{

    public partial class Form1 : Form
    {

        public int a = 1;
        // DLL libraries used to manage hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        List<HotkeyKeys> hotkeys = new List<HotkeyKeys>();
        player player = new player();
        int aktSeite = 0;
        List<soundButton> sounder = new List<soundButton>();
        saveLoad save = new saveLoad();
        const string version = "2.2.1";

        public Form1()
        {
            InitializeComponent();
        }
        public void fillListEmpty() // Anzahl wieiviel buttons auf seite
        {
            for (int i = 0; i < 24; i++)
            {
                sounder.Add(new soundButton("Leer", "Leer", Color.Gray, 0));
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            createHotkeysList();
            for (int i = 0; i < hotkeys.Count; i++)
           {
               RegisterHotKey(this.Handle, i, 0, hotkeys[i].key);
          }

            
            label4.Text = label4.Text + " " + version;
            trackBar1.Value = 50;
            label2.Text = "Lautstärke " + trackBar1.Value;
            button2.Enabled = false;
            if (!save.saveExist("pfad"))
            {
                save.createFile();
                fillListEmpty();
            }
            else
            {
                sounder = save.getLoadedList();
                List<int> liste = new List<int>();
                liste = save.getLoadedHokeys();
                for (int i = 0; i < hotkeys.Count; i++)
                {
                    hotkeys[i].soundID = liste[i];
                }
            }

            createButtons();
            speichern();
        }
        private void speichern()
        {
            List<string> hotkeySave = new List<string>();
            for (int i = 0; i < hotkeys.Count; i++)
            {
                hotkeySave.Add(hotkeys[i].soundID.ToString());
            }
            save.saveJson(sounder, hotkeySave);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int id = int.Parse(btn.Name);
            id = id + (24 * aktSeite);
            Console.WriteLine(id);
            OpenFileDialog dialog = new OpenFileDialog();
            if (checkBox2.Checked)
            {
                using (var form = new popUp(sounder[id].Name, sounder[id].Button.BackColor))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        sounder[id].Name = form.soundname;
                        int action = form.actionID;
                        sounder[id].Farbe = form.Color;
                        if (action != -1)   // Hotkey gesetzt
                        {
                            hotkeys[action].soundID = id;
                        }
                    }
                }
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
        public void setButtonNames(int seite)
        {
            for (int i = 0; i < 24; i++)
            {
                string buttonName = "";
                buttonName = sounder[i + (24 * seite)].Name;
                sounder[i].Button.Text = buttonName;
                sounder[i].Button.BackColor = sounder[i + (24 * seite)].Farbe;
            }
        }
        public void createHotkeysList() {
            hotkeys.Add(new HotkeyKeys("Num 0", (int)Keys.NumPad0,0));
            hotkeys.Add(new HotkeyKeys("Num 1", (int)Keys.NumPad1,1));
            hotkeys.Add(new HotkeyKeys("Num 2", (int)Keys.NumPad2,2));
            hotkeys.Add(new HotkeyKeys("Num 3", (int)Keys.NumPad3,3));
            hotkeys.Add(new HotkeyKeys("Num 4", (int)Keys.NumPad4,4));
            hotkeys.Add(new HotkeyKeys("Num 5", (int)Keys.NumPad5,5));
            hotkeys.Add(new HotkeyKeys("Num 6", (int)Keys.NumPad6,6));
            hotkeys.Add(new HotkeyKeys("Num 7", (int)Keys.NumPad7,7));
            hotkeys.Add(new HotkeyKeys("Num 8", (int)Keys.NumPad8,8));
            hotkeys.Add(new HotkeyKeys("Num 9", (int)Keys.NumPad9,9));
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

                newButton.Name = number.ToString();
                newButton.Click += new EventHandler(button1_Click);
                newButton.Location = new Point(posx, posy);
                newButton.BackColor = sounder[i].Farbe;
                newButton.FlatStyle = FlatStyle.Flat;
                
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
            numericUpDown1.Value = aktSeite;
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
            numericUpDown1.Value = aktSeite;
            fillListEmpty();
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 )
            {
                int id = m.WParam.ToInt32();
                Console.WriteLine(id + " Action id, Sound : "+ hotkeys[id].soundID);
                if (hotkeys[id].soundID != -1)
                {
                    player.playSound(sounder[hotkeys[id].soundID].Pfad);
                }
            }
            base.WndProc(ref m);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value < 0)
            {
                numericUpDown1.Value = 0;
            }
            else if ((int)numericUpDown1.Value == 0)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
            if (sounder.Count/24 < (int)numericUpDown1.Value)
            {
                while (sounder.Count / 24 < (int)numericUpDown1.Value)
                {
                    fillListEmpty();
                }
            }

            aktSeite = (int)numericUpDown1.Value;
            fillListEmpty();
            setButtonNames(aktSeite);
        }
    }
}