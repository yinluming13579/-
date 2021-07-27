using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 职业介绍管理系统
{
    public partial class Form19 : Form
    {
        public Form19()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form22 form22 = new Form22();
            form22.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form29 form29 = new Form29();
            form29.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form35 form35 = new Form35();
            form35.Show();
        }

        private void Form19_Load(object sender, EventArgs e)
        {

        }
    }
}
