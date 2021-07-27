using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace 职业介绍管理系统
{
    public partial class Form53 : Form
    {
        public Form53()
        {
            InitializeComponent();
        }

        private void Form53_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            SqlDataAdapter DataAdapter = null;
            DataSet dataset = null;
            try
            {
                string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                String userInfo = String.Format("SELECT * FROM 简历");
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t1");
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = "t1";
                textBox2.Text = dataset.Tables["t1"].Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Dispose();
                if (dataset != null) dataset.Dispose();
                if (DataAdapter != null) DataAdapter.Dispose();
            }
            try
            {
                string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                String userInfo = String.Format("SELECT * FROM 求职者");
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t2");
                textBox1.Text = dataset.Tables["t2"].Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Dispose();
                if (dataset != null) dataset.Dispose();
                if (DataAdapter != null) DataAdapter.Dispose();
            }
            textBox3.Text = (float.Parse(textBox2.Text) / float.Parse(textBox1.Text)*100).ToString();
        }
    }
}
