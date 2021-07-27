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
    public partial class Form1 : Form
    {
        public static string user_pwd, user_identity, user_name;
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = checkBox3.Checked = checkBox4.Checked= checkBox5.Checked= checkBox6.Checked= checkBox7.Checked=checkBox8.Checked=false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = checkBox8.Checked= true;
            checkBox1.Checked = checkBox3.Checked = checkBox4.Checked = checkBox5.Checked = checkBox6.Checked = checkBox7.Checked = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox3.Checked = checkBox4.Checked=true;
            checkBox1.Checked = checkBox2.Checked = checkBox5.Checked = checkBox6.Checked = checkBox7.Checked = checkBox8.Checked= false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox5.Checked = checkBox6.Checked = checkBox7.Checked=true;
            checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = checkBox4.Checked = checkBox8.Checked= false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            textBox1.Text = currentTime.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            textBox1.Text =currentTime.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            user_name = username.Text;
            user_pwd = userpwd.Text;
            user_identity = "";
            foreach (Control ctl in groupBox1.Controls)
            {
                RadioButton rad = (RadioButton)ctl;
                if (rad.Checked)
                {
                    user_identity = rad.Text;
                }
            }
            switch (user_identity)
            {
                case "求职者":
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
                            String userInfo = String.Format("SELECT 求职者登录密码 FROM 求职者 where 求职者编号='{0}'", Form1.user_name);
                            cmd.CommandText = userInfo;
                            DataAdapter.SelectCommand = cmd;
                            DataAdapter.Fill(dataset, "t1");
                            if(dataset.Tables["t1"].Rows[0]["求职者登录密码"].ToString()==user_pwd)
                            {
                                Form5 form5 = new Form5();
                                form5.Show();
                            }
                            else
                            {
                                MessageBox.Show("用户名或密码或身份错误,请重新输入!");
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("用户名或密码或身份错误,请重新输入!");
                            break;
                        }
                        finally
                        {
                            if (conn != null) conn.Dispose();
                            if (dataset != null) dataset.Dispose();
                            if (DataAdapter != null) DataAdapter.Dispose();
                        }
                        break;
                    }
                case "用人单位":
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
                            String userInfo = String.Format("SELECT 单位登录密码 FROM 用人单位 where 单位编号='{0}'", Form1.user_name);
                            cmd.CommandText = userInfo;
                            DataAdapter.SelectCommand = cmd;
                            DataAdapter.Fill(dataset, "t2");
                            if (dataset.Tables["t2"].Rows[0]["单位登录密码"].ToString() == user_pwd)
                            {
                                Form9 form9 = new Form9();
                                form9.Show();
                            }
                            else
                            {
                                MessageBox.Show("用户名或密码或身份错误,请重新输入!");
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("用户名或密码或身份错误,请重新输入!");
                            break;
                        }
                        finally
                        {
                            if (conn != null) conn.Dispose();
                            if (dataset != null) dataset.Dispose();
                            if (DataAdapter != null) DataAdapter.Dispose();
                        }
                        break;
                    }
                case "介绍人员":
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
                            String userInfo = String.Format("SELECT 介绍人员登录密码 FROM 介绍人员 where 介绍人员编号='{0}'", Form1.user_name);
                            cmd.CommandText = userInfo;
                            DataAdapter.SelectCommand = cmd;
                            DataAdapter.Fill(dataset, "t3");
                            if (dataset.Tables["t3"].Rows[0]["介绍人员登录密码"].ToString() == user_pwd)
                            {
                                Form15 form15 = new Form15();
                                form15.Show();
                            }
                            else
                            {
                                MessageBox.Show("用户名或密码或身份错误,请重新输入!");
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("用户名或密码或身份错误,请重新输入!");
                            break;
                        }
                        finally
                        {
                            if (conn != null) conn.Dispose();
                            if (dataset != null) dataset.Dispose();
                            if (DataAdapter != null) DataAdapter.Dispose();
                        }
                        break;
                    }
                case "超级管理员":
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
                            String userInfo = String.Format("SELECT 超级管理员登录密码 FROM 超级管理员 where 超级管理员编号='{0}'", Form1.user_name);
                            cmd.CommandText = userInfo;
                            DataAdapter.SelectCommand = cmd;
                            DataAdapter.Fill(dataset, "t4");
                            if (dataset.Tables["t4"].Rows[0]["超级管理员登录密码"].ToString() == user_pwd)
                            {
                                Form19 form19 = new Form19();
                                form19.Show();
                            }
                            else
                            {
                                MessageBox.Show("用户名或密码或身份错误,请重新输入!");
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("用户名或密码或身份错误,请重新输入!");
                            break;
                        }
                        finally
                        {
                            if (conn != null) conn.Dispose();
                            if (dataset != null) dataset.Dispose();
                            if (DataAdapter != null) DataAdapter.Dispose();
                        }
                        break;
                    }
            }
        }
    }
}
