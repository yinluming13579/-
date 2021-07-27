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
    public partial class Form18 : Form
    {
        public Form18()
        {
            InitializeComponent();
        }

        private void Form18_Load(object sender, EventArgs e)
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
                String userInfo = String.Format("SELECT 缴费编号,介绍人员.介绍人员编号,介绍人员姓名,介绍人员性别,介绍人员身份证号码,介绍人员电话,单位缴费,求职者缴费 FROM 缴费,介绍人员,求职者 WHERE 缴费.求职者编号=求职者.求职者编号 AND 求职者聘用状态='聘用成功' AND 缴费.介绍人员编号=介绍人员.介绍人员编号 AND 介绍人员.介绍人员编号='{0}'", Form1.user_name);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t2");
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
                String userInfo = String.Format("SELECT SUM(单位缴费) AS 费用 FROM 缴费 where 介绍人员编号='{0}'", Form1.user_name);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t1");
                if(dataset.Tables["t1"].Rows[0]["费用"].ToString()=="")
                {
                    textBox1.Text = "未查询到B端业绩!";
                    return;
                }
                textBox1.DataBindings.Clear();
                textBox1.DataBindings.Add("Text", dataset, "t1.费用");
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

        private void button2_Click(object sender, EventArgs e)
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
                String userInfo = String.Format("SELECT SUM(求职者缴费) AS 费用 FROM 缴费 where 介绍人员编号='{0}'", Form1.user_name);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t3");
                if (dataset.Tables["t3"].Rows[0]["费用"].ToString() == "")
                {
                    textBox2.Text = "未查询到C端业绩!";
                    return;
                }
                textBox2.DataBindings.Clear();
                textBox2.DataBindings.Add("Text", dataset, "t3.费用");
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
