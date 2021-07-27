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
    public partial class Form3 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        public Form3()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            if (textBox1.Text=="" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == ""  || textBox8.Text == "" || textBox9.Text=="" || textBox10.Text=="" || textBox11.Text=="" || textBox12.Text=="")
            {
                MessageBox.Show("信息填写不完整,请重新填写!");
                comboBox1.Text = "农、林、牧、渔";
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = "";
                return;
            }
            string strSQL = "INSERT INTO 用人单位 VALUES(";
            strSQL += "'" + textBox1.Text.Trim() + "'" + ",";
            if (textBox2.Text.Trim() != textBox3.Text.Trim())
            {
                MessageBox.Show("两次密码输入不一致!请重新输入！");
                comboBox1.Text = "农、林、牧、渔";
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text="";
                return;
            }
            strSQL += "'" + textBox2.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox4.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox5.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox6.Text.Trim() + "'" + ",";
            strSQL += "'" + comboBox1.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox8.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox9.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox10.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox11.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox12.Text.Trim() + "'" + ")";
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("用人单位注册成功");
                comboBox1.Text = "农、林、牧、渔";
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("用户名已存在!请直接登录!");
                comboBox1.Text = "农、林、牧、渔";
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = "";
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            comboBox1.Items.Add("农、林、牧、渔");
            comboBox1.Items.Add("采矿业");
            comboBox1.Items.Add("制造业");
            comboBox1.Items.Add("电力、燃气及水的生产和供应业");
            comboBox1.Items.Add("建筑业");
            comboBox1.Items.Add("交通运输、仓储和邮政业");
            comboBox1.Items.Add("信息传输、计算机服务和软件业");
            comboBox1.Items.Add("批发和零售业");
            comboBox1.Items.Add("住宿和餐饮业");
            comboBox1.Items.Add("金融业");
            comboBox1.Items.Add("房地产业");
            comboBox1.Items.Add("租赁和商务服务业");
            comboBox1.Items.Add("科学研究、技术服务和地质勘查业");
            comboBox1.Items.Add("水利、环境和公共设施管理业");
            comboBox1.Items.Add("居民服务和其他服务业");
            comboBox1.Text = "农、林、牧、渔";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "农、林、牧、渔";
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text = "";
        }
    }
}
