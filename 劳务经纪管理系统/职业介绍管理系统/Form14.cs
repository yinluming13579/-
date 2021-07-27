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
    public partial class Form14 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form14()
        {
            InitializeComponent();
        }
        private void showData()
        {
            conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                String userInfo = String.Format("SELECT 求职者姓名,求职者性别 FROM 求职者 WHERE 求职者编号='{0}'",Form1.user_name);
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
            textBox2.Text = dataset.Tables["t1"].Rows[0]["求职者姓名"].ToString();
            if(dataset.Tables["t1"].Rows[0]["求职者性别"].ToString()=="男")
            {
                radioButton1.Checked = true;
            }
            else if (dataset.Tables["t1"].Rows[0]["求职者性别"].ToString() == "女")
            {
                radioButton2.Checked = true;
            }
        }
        private void Form14_Load(object sender, EventArgs e)
        {
            textBox1.Text = Form1.user_name;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            panel1.Enabled = false;
            showData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "")
            {
                MessageBox.Show("信息填写不完整,请重新填写!");
                textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = textBox10.Text = "";
                return;
            }
            conn = new SqlConnection(ConnectionString);
            string strSQL = "INSERT INTO 简历 VALUES(";
            strSQL += "'" + textBox1.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox2.Text.Trim() + "'" + ",";
            if (radioButton1.Checked)
            {
                strSQL += "'" + "男" + "'" + ",";
            }
            else if (radioButton2.Checked)
            {
                strSQL += "'" + "女" + "'" + ",";
            }
            strSQL += textBox3.Text.Trim()  + ",";
            strSQL += "'" + textBox4.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox5.Text.Trim() + "'" + ",";
            if (radioButton3.Checked)
            {
                strSQL += "'" + "群众" + "'" + ",";
            }
            else if (radioButton4.Checked)
            {
                strSQL += "'" + "共青团员" + "'" + ",";
            }
            else if(radioButton5.Checked)
            {
                strSQL += "'" + "中共党员" + "'" + ",";
            }
            strSQL += "'" + textBox6.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox7.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox8.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox9.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox10.Text.Trim() + "'" + ")";
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("简历创建成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("简历不能重复创建!");
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
            showData();
        }
    }
}
