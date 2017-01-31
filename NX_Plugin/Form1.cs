using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NX_Plugin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            updateNumbers(null, null);
        }

        private void updateNumbers(object sender, EventArgs e)
        {
            numericUpDown3.Maximum = numericUpDown1.Value - 5;
            numericUpDown4.Maximum = numericUpDown2.Value/2 - 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.CreateModel((double)numericUpDown1.Value, (double)(numericUpDown2.Value) / 2.0, (double)numericUpDown3.Value, (double)numericUpDown4.Value/2.0);
        }
    }
}
