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
    public partial class Form4 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        private SqlCommand cmd = null;
        public Form4()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            if (textBox1.Text=="" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text=="" || comboBox1.Text=="")
            {
                MessageBox.Show("信息填写不完整,请重新填写!");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text="";
                return;
            }
            string strSQL = "INSERT INTO 介绍人员 VALUES(";
            strSQL += "'" + textBox1.Text.Trim() + "'" + ",";
            if (textBox2.Text.Trim() != textBox3.Text.Trim())
            {
                MessageBox.Show("两次密码输入不一致!请重新输入！");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
                radioButton1.Checked = true;
                return;
            }
            strSQL += "'" + textBox2.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox4.Text.Trim() + "'" + ",";
            if(radioButton1.Checked)
            {
                strSQL += "'" + "男" + "'" + ",";
            }
            else if(radioButton2.Checked)
            {
                strSQL += "'" + "女" + "'" + ",";
            }
            strSQL += "'" + textBox5.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox6.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox7.Text.Trim() + "'" + ",";
            strSQL += "'" + comboBox1.Text + "'" + ",";
            if (radioButton3.Checked)
            {
                strSQL += "'" + radioButton3.Text + "'" + ")";
            }
            else if (radioButton4.Checked)
            {
                strSQL += "'" + radioButton4.Text + "'" + ")";
            }
            else if(radioButton5.Checked)
            {
                strSQL += "'" + radioButton5.Text + "'" + ")";
            }
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("注册成功");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
                radioButton1.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
                radioButton1.Checked =true;
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            try
            {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM 中介公司";
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t1");
                comboBox1.Items.Clear();
                for (int i = 0; i < dataset.Tables["t1"].Rows.Count; i++)
                    comboBox1.Items.Add(dataset.Tables["t1"].Rows[i]["中介公司编号"].ToString());
                dataset.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            radioButton1.Checked = true;
            radioButton3.Checked = true;
            textBox8.ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
            radioButton1.Checked = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
                String userInfo = String.Format("SELECT 中介公司名称 FROM 中介公司 WHERE 中介公司编号={0}",comboBox1.Text);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t2");
                textBox8.Text = dataset.Tables["t2"].Rows[0]["中介公司名称"].ToString();
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
        }
    }
}
