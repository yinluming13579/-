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
    public partial class Form2 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            textBox7.ReadOnly = true;
            textBox7.Text = "未聘用";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="" || textBox2.Text=="" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox8.Text == "")
            {
                MessageBox.Show("信息填写不完整,请重新填写!");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox8.Text = "";
                radioButton1.Checked = radioButton6.Checked = radioButton9.Checked = true;
                return;
            }
            conn = new SqlConnection(ConnectionString);
            string strSQL = "INSERT INTO 求职者 VALUES(";
            strSQL += "'" + textBox1.Text.Trim() + "'" + ",";
            if(textBox2.Text.Trim()!=textBox3.Text.Trim())
            {
                MessageBox.Show("两次密码输入不一致!请重新输入！");
                textBox1.Text = textBox2.Text=textBox3.Text = textBox4.Text = textBox5.Text= textBox6.Text= textBox8.Text="";
                radioButton1.Checked = radioButton6.Checked = radioButton9.Checked = true;
                return;
            }
            strSQL += "'" + textBox2.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox4.Text.Trim() + "'" + ",";
            if (radioButton1.Checked)
            {
                strSQL += "'" + "男" + "'" + ",";
            }
            else if (radioButton2.Checked)
            {
                strSQL += "'" + "女" + "'" + ",";
            }
            strSQL += "'" + textBox5.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox6.Text.Trim() + "'" + ",";
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
            strSQL +="'"+textBox7.Text.Trim() +"'"+",";
            strSQL += textBox8.Text.Trim() + ",";
            if (radioButton9.Checked)
            {
                strSQL +="1"+ ")";
            }
            else if (radioButton10.Checked)
            {
                strSQL +="2"+ ")";
            }
            else if (radioButton11.Checked)
            {
                strSQL +="3"+ ")";
            }
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("求职者注册成功");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox8.Text = "";
                radioButton1.Checked = radioButton6.Checked = radioButton9.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("用户名已被占用!请重新填写!");
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox8.Text = "";
                radioButton1.Checked = radioButton6.Checked = radioButton9.Checked = true;
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text=textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox8.Text = "";
            radioButton1.Checked = radioButton6.Checked = radioButton9.Checked = true;
        }
    }
}
