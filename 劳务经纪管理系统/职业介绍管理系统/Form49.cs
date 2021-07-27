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
    public partial class Form49 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form49()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pwd = "";
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT 介绍人员登录密码 FROM 介绍人员 where 介绍人员编号='{0}'", Form1.user_name);
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset = new DataSet();
                DataAdapter.Fill(dataset, "t1");
                pwd = dataset.Tables["t1"].Rows[0]["介绍人员登录密码"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (textBox1.Text != pwd)
            {
                MessageBox.Show("原密码输入错误,请重新输入!");
                textBox1.Text = textBox2.Text = textBox3.Text = "";
                return;
            }
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("两次密码输入不一致,请重新输入!");
                textBox1.Text = textBox2.Text = textBox3.Text = "";
                return;
            }
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("请填写全部选项!");
                textBox1.Text = textBox2.Text = textBox3.Text = "";
                return;
            }
            if (textBox1.Text == textBox2.Text)
            {
                MessageBox.Show("新密码不能与原密码相同!");
                textBox1.Text = textBox2.Text = textBox3.Text = "";
                return;
            }
            string strSQL = "UPDATE 介绍人员 SET ";
            strSQL += "介绍人员登录密码='" + textBox2.Text + "'";
            strSQL += " WHERE 介绍人员编号='" + Form1.user_name + "'";
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("密码修改成功!");
                }
                textBox1.Text = textBox2.Text = textBox3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox1.Text = textBox2.Text = textBox3.Text = "";
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
        }

        private void Form49_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
        }
    }
}
