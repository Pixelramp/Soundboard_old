using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soundboard
{
    public partial class popUp : Form
    {
        List<HotkeyKeys> hotkeys = new List<HotkeyKeys>();
        public string soundname { get; set; }
        public Color Color { get; set; }
        public int actionID { get; set; }
        public popUp(string soundName, Color color)
        { 
            InitializeComponent();
            textBox1.Text = soundName;
            button1.BackColor = color;
            createHotkeysList();
            for (int i = 0; i < hotkeys.Count; i++)
            {
                comboBox1.Items.Add(hotkeys[i].name);
            }
            comboBox1.SelectedIndex = 0;
        }
        public void createHotkeysList()
        {
            hotkeys.Add(new HotkeyKeys("None", 0,-1));
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
        private void button2_Click(object sender, EventArgs e)  //Löschen!
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void popUp_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = Color.FromArgb(colorDialog1.Color.ToArgb());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Name cannot be empty");
            }
            else
            {
                this.soundname = textBox1.Text;
                this.Color = button1.BackColor;
                this.DialogResult = DialogResult.OK;
                this.actionID = hotkeys[comboBox1.SelectedIndex].actionID;
                this.Close();
            }
            
        }
    }
}
