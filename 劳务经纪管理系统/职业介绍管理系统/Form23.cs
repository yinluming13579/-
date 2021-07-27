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
    public partial class Form23 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        private SqlCommand cmd = null;
        public Form23()
        {
            InitializeComponent();
        }
        private void Form23_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT 求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者家庭住址,求职者学历层次,求职者聘用状态,求职者月薪要求,求职者会员等级 FROM 求职者 ORDER BY 求职者会员等级";
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t1");
                comboBox1.Items.Clear();
                for (int i = 0; i < dataset.Tables["t1"].Columns.Count; i++)
                    comboBox1.Items.Add(dataset.Tables["t1"].Columns[i].ToString());
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = "t1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            comboBox2.Items.Clear();
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text=="" || comboBox2.Text=="" || textBox1.Text=="")
            {
                MessageBox.Show("请完整填写检索条件!");
                return;
            }
            string tb1 = textBox1.Text;
            if (comboBox2.Text == "like") tb1 = "%" + textBox1.Text + "%";
            string strSQL = "SELECT * FROM 求职者 WHERE ";
            if (comboBox2.Text == "like")
            {
                strSQL += comboBox1.Text + " " + comboBox2.Text + "'" + tb1 + "'";
            }
            else
            {
                strSQL += comboBox1.Text + comboBox2.Text + "'" + tb1 + "'";
            }
            try
            {
                cmd.CommandText = strSQL;
                DataAdapter.SelectCommand = cmd;
                dataset.Clear();
                DataAdapter.Fill(dataset, "t2");
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = "t2";
            }
            catch
            {
                MessageBox.Show("请正确设置检索条件!");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text=="求职者编号" || comboBox1.Text == "求职者姓名" || comboBox1.Text == "求职者性别" || comboBox1.Text == "求职者身份证号码" || comboBox1.Text == "求职者家庭住址" || comboBox1.Text == "求职者学历层次" || comboBox1.Text == "求职者聘用状态")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("=");
                comboBox2.Items.Add("like");
            }
            else if(comboBox1.Text == "求职者月薪要求" || comboBox1.Text == "求职者会员等级")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("=");
                comboBox2.Items.Add("like");
                comboBox2.Items.Add(">");
                comboBox2.Items.Add("<");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT 求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者家庭住址,求职者学历层次,求职者聘用状态,求职者月薪要求,求职者会员等级 FROM 求职者 ORDER BY 求职者会员等级";
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t1");
                comboBox1.Items.Clear();
                for (int i = 0; i < dataset.Tables["t1"].Columns.Count; i++)
                    comboBox1.Items.Add(dataset.Tables["t1"].Columns[i].ToString());
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = "t1";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            comboBox2.Items.Clear();
            textBox1.Text = "";
        }
    }
}
