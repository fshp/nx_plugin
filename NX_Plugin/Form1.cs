using System;
using System.Data;
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
            double d = (Double)((DataRowView)this.screwBindingSource.Current).Row["d"];
            double l = (Double)((DataRowView)this.screwscrew2lengthBindingSource.Current).Row["l"];
            double b = (Double)((DataRowView)this.screwBindingSource.Current).Row["b"];
            double h = (Double)((DataRowView)this.screwBindingSource.Current).Row["h"];
            double C = (Double)((DataRowView)this.screwBindingSource.Current).Row["C"];
            Program.CreateModel(d, l, b, h, C);
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
