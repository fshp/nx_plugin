﻿using System;
using System.Windows.Forms;

namespace NX_Plugin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*double d = (Double)((DataRowView)this.boltBindingSource.Current).Row["d"];
            double l = (double)numericUpDown1.Value;
            double b = (Double)((DataRowView)this.boltBindingSource.Current).Row["b"];
            double h = (Double)((DataRowView)this.boltBindingSource.Current).Row["h"];
            double C = (Double)((DataRowView)this.boltBindingSource.Current).Row["C"];
            Program.CreateModel(d, l, b, h, C);*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.screw2length". При необходимости она может быть перемещена или удалена.
            this.screw2lengthTableAdapter.Fill(this.dbDataSet.screw2length);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dbDataSet.screw". При необходимости она может быть перемещена или удалена.
            this.screwTableAdapter.Fill(this.dbDataSet.screw);
        }
    }
}
