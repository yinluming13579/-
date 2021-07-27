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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
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
            textBox10.ReadOnly = true;
            textBox11.ReadOnly = true;
            textBox12.ReadOnly = true;
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
                String userInfo = String.Format("SELECT 求职者.求职者编号,求职者姓名,求职者性别,求职者聘用状态,单位名称,单位所在地,单位所属行业,单位联系电话,单位邮箱,单位简介,单位网址,求职者缴费,求职者薪资,介绍人员姓名,介绍人员电话 FROM 求职者,缴费,用人单位,介绍人员 where 求职者.求职者编号=缴费.求职者编号 AND 缴费.单位编号=用人单位.单位编号 AND 缴费.介绍人员编号=介绍人员.介绍人员编号 AND 求职者.求职者编号='{0}'", Form1.user_name);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t2");
                if (dataset.Tables["t2"].Rows.Count != 0 && dataset.Tables["t2"].Rows[0]["求职者聘用状态"].ToString()=="聘用成功")
                {
                    textBox1.Text = dataset.Tables["t2"].Rows[0]["求职者聘用状态"].ToString();
                    textBox2.Text = dataset.Tables["t2"].Rows[0]["单位名称"].ToString();
                    textBox3.Text = dataset.Tables["t2"].Rows[0]["单位所在地"].ToString();
                    textBox4.Text = dataset.Tables["t2"].Rows[0]["单位所属行业"].ToString();
                    textBox5.Text = dataset.Tables["t2"].Rows[0]["单位联系电话"].ToString();
                    textBox6.Text = dataset.Tables["t2"].Rows[0]["单位邮箱"].ToString();
                    textBox7.Text = dataset.Tables["t2"].Rows[0]["单位简介"].ToString();
                    textBox8.Text = dataset.Tables["t2"].Rows[0]["单位网址"].ToString();
                    textBox9.Text = dataset.Tables["t2"].Rows[0]["求职者缴费"].ToString();
                    textBox10.Text = dataset.Tables["t2"].Rows[0]["求职者薪资"].ToString();
                    textBox11.Text = dataset.Tables["t2"].Rows[0]["介绍人员姓名"].ToString();
                    textBox12.Text = dataset.Tables["t2"].Rows[0]["介绍人员电话"].ToString();
                }
                else if(dataset.Tables["t2"].Rows.Count != 0 && dataset.Tables["t2"].Rows[0]["求职者聘用状态"].ToString() == "匹配成功")
                {
                    textBox1.Text = "匹配成功!请联系用人单位面试!如有疑问可与介绍人员联系!";
                    textBox2.Text = dataset.Tables["t2"].Rows[0]["单位名称"].ToString();
                    textBox3.Text = dataset.Tables["t2"].Rows[0]["单位所在地"].ToString();
                    textBox4.Text = dataset.Tables["t2"].Rows[0]["单位所属行业"].ToString();
                    textBox5.Text = dataset.Tables["t2"].Rows[0]["单位联系电话"].ToString();
                    textBox6.Text = dataset.Tables["t2"].Rows[0]["单位邮箱"].ToString();
                    textBox7.Text = dataset.Tables["t2"].Rows[0]["单位简介"].ToString();
                    textBox8.Text = dataset.Tables["t2"].Rows[0]["单位网址"].ToString();
                    textBox9.Text = dataset.Tables["t2"].Rows[0]["求职者缴费"].ToString();
                    textBox10.Text = "等待录取";
                    textBox11.Text = dataset.Tables["t2"].Rows[0]["介绍人员姓名"].ToString();
                    textBox12.Text = dataset.Tables["t2"].Rows[0]["介绍人员电话"].ToString();
                }
                else
                {
                    textBox1.Text = "求职者等待匹配!";
                }
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = "t2";
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
