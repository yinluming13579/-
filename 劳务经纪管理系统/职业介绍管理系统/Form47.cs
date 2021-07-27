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
    public partial class Form47 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form47()
        {
            InitializeComponent();
        }
        private void showData()
        {
            conn = new SqlConnection(ConnectionString);
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT 求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者家庭住址,求职者学历层次,求职者聘用状态,求职者月薪要求,求职者会员等级 FROM 求职者 where 求职者编号='{0}'", Form1.user_name);
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset = new DataSet();
                DataAdapter.Fill(dataset);
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = dataset.Tables[0].ToString();
                textBox1.DataBindings.Clear();
                textBox2.DataBindings.Clear();
                textBox3.DataBindings.Clear();
                textBox1.DataBindings.Add("Text", dataset, "table.求职者学历层次");
                textBox2.DataBindings.Add("Text", dataset, "table.求职者月薪要求");
                textBox3.DataBindings.Add("Text", dataset, "table.求职者会员等级");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void showData1()
        {
            conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                String userInfo = String.Format("SELECT * FROM 简历 WHERE 求职者简历编号='{0}'", Form1.user_name);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t1");
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
            if (dataset.Tables["t1"].Rows.Count != 0)
            {
                textBox4.Text = "已填写个人简历";
            }
            else
            {
                textBox4.Text = "尚未填写个人简历";
            }
        }
        private void Form47_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;
            textBox9.ReadOnly = true;
            showData();
            showData1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch(textBox1.Text)
            {
                case "初中及以下":
                    textBox5.Text = "60";
                    break;
                case "高中":
                    textBox5.Text = "70";
                    break;
                case "大专":
                    textBox5.Text = "80";
                    break;
                case "本科":
                    textBox5.Text = "90";
                    break;
                case "硕士":
                    textBox5.Text = "95";
                    break;
                case "博士":
                    textBox5.Text = "100";
                    break;
            }
            if(int.Parse(textBox2.Text)<=4000)
            {
                textBox6.Text = "100";
            }
            else if(int.Parse(textBox2.Text) <= 8000)
            {
                textBox6.Text = "80";
            }
            else if (int.Parse(textBox2.Text) <= 12000)
            {
                textBox6.Text = "70";
            }
            else
            {
                textBox6.Text = "60";
            }
            if (int.Parse(textBox3.Text)==1)
            {
                textBox7.Text = "60";
            }
            else if (int.Parse(textBox3.Text)==2)
            {
                textBox7.Text = "80";
            }
            else if (int.Parse(textBox3.Text)==3)
            {
                textBox7.Text = "100";
            }
            if (textBox4.Text == "已填写个人简历")
            {
                textBox8.Text = "100";
            }
            else if (textBox4.Text == "尚未填写个人简历")
            {
                textBox8.Text = "80";
            }
            textBox9.Text = (0.4 * int.Parse(textBox5.Text) + 0.2 * int.Parse(textBox6.Text) + 0.1 * int.Parse(textBox7.Text) + 0.3 * int.Parse(textBox8.Text)).ToString();
        }
    }
}
