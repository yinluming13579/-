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
    public partial class Form27 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        public Form27()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            if (textBox1.Text ==""  || textBox3.Text =="" || textBox4.Text =="" || textBox5.Text =="" || textBox7.Text=="")
            {
                MessageBox.Show("请填写全部选项!");
                return;
            }
            string strSQL = "INSERT INTO 求职者 VALUES(";
            strSQL += "'" + textBox1.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox2.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox3.Text.Trim() + "'" + ",";
            if (radioButton1.Checked)
            {
                strSQL += "'" + "男" + "'" + ",";
            }
            else if (radioButton2.Checked)
            {
                strSQL += "'" + "女" + "'" + ",";
            }
            strSQL += "'" + textBox4.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox5.Text.Trim() + "'" + ",";
            if (radioButton3.Checked)
            {
                strSQL += "'" + "初中及以下" + "'" + ",";
            }
            else if (radioButton4.Checked)
            {
                strSQL += "'" + "高中" + "'" + ",";
            }
            else if (radioButton5.Checked)
            {
                strSQL += "'" + "大专" + "'" + ",";
            }
            else if (radioButton6.Checked)
            {
                strSQL += "'" + "本科" + "'" + ",";
            }
            else if (radioButton7.Checked)
            {
                strSQL += "'" + "硕士" + "'" + ",";
            }
            else if (radioButton8.Checked)
            {
                strSQL += "'" + "博士" + "'" + ",";
            }
            strSQL += "'" + textBox6.Text.Trim() + "'"+",";
            strSQL += textBox7.Text.Trim() + ",";
            if (radioButton9.Checked)
            {
                strSQL += "1" + ")";
            }
            else if (radioButton10.Checked)
            {
                strSQL += "2" + ")";
            }
            else if (radioButton11.Checked)
            {
                strSQL += "3" + ")";
            }
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("求职者注册成功，初始密码为88888888,请尽快登陆修改密码!");
                radioButton1.Checked = radioButton6.Checked = radioButton9.Checked = true;
                textBox1.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox7.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
        }

        private void Form27_Load(object sender, EventArgs e)
        {
            textBox2.Text = "88888888";
            textBox6.Text = "未聘用";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = radioButton6.Checked = radioButton9.Checked = true;
            textBox1.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox7.Text = "";
        }
    }
}
