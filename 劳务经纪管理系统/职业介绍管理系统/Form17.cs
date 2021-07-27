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
    public partial class Form17 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form17()
        {
            InitializeComponent();
        }
        private void showData()
        {
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT * FROM 招聘信息 where 已聘人数<需聘人数 and 招聘信息权限<=(SELECT 介绍人员权限 FROM 介绍人员 WHERE 介绍人员编号='{0}')",Form1.user_name);
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset = new DataSet();
                DataAdapter.Fill(dataset);
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = dataset.Tables[0].ToString();
                textBox1.DataBindings.Clear();
                textBox2.DataBindings.Clear();
                textBox3.DataBindings.Clear();
                textBox4.DataBindings.Clear();
                textBox5.DataBindings.Clear();
                textBox6.DataBindings.Clear();
                textBox7.DataBindings.Clear();
                textBox8.DataBindings.Clear();
                textBox9.DataBindings.Clear();
                textBox10.DataBindings.Clear();
                textBox1.DataBindings.Add("Text", dataset, "table.招聘编号");
                textBox2.DataBindings.Add("Text", dataset, "table.单位编号");
                textBox3.DataBindings.Add("Text", dataset, "table.职业类型号");
                textBox4.DataBindings.Add("Text", dataset, "table.岗位简介");
                textBox5.DataBindings.Add("Text", dataset, "table.学历要求");
                textBox6.DataBindings.Add("Text", dataset, "table.岗位要求");
                textBox7.DataBindings.Add("Text", dataset, "table.已聘人数");
                textBox8.DataBindings.Add("Text", dataset, "table.需聘人数");
                textBox9.DataBindings.Add("Text", dataset, "table.工资");
                textBox10.DataBindings.Add("Text", dataset, "table.招聘信息权限");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void showData1()
        {
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT 求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者家庭住址,求职者学历层次,求职者聘用状态,求职者月薪要求,求职者会员等级 FROM 求职者 where 求职者聘用状态='未聘用' and 求职者会员等级=(SELECT 介绍人员权限 FROM 介绍人员 WHERE 介绍人员编号='{0}')", Form1.user_name);
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset = new DataSet();
                DataAdapter.Fill(dataset);
                dataGridView2.DataSource = dataset;
                dataGridView2.DataMember = dataset.Tables[0].ToString();
                textBox11.DataBindings.Clear();
                textBox12.DataBindings.Clear();
                textBox13.DataBindings.Clear();
                textBox14.DataBindings.Clear();
                textBox15.DataBindings.Clear();
                textBox16.DataBindings.Clear();
                textBox17.DataBindings.Clear();
                textBox18.DataBindings.Clear();
                textBox19.DataBindings.Clear();
                textBox11.DataBindings.Add("Text", dataset, "table.求职者编号");
                textBox12.DataBindings.Add("Text", dataset, "table.求职者姓名");
                textBox13.DataBindings.Add("Text", dataset, "table.求职者性别");
                textBox14.DataBindings.Add("Text", dataset, "table.求职者身份证号码");
                textBox15.DataBindings.Add("Text", dataset, "table.求职者家庭住址");
                textBox16.DataBindings.Add("Text", dataset, "table.求职者学历层次");
                textBox17.DataBindings.Add("Text", dataset, "table.求职者聘用状态");
                textBox18.DataBindings.Add("Text", dataset, "table.求职者月薪要求");
                textBox19.DataBindings.Add("Text", dataset, "table.求职者会员等级");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form17_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
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
            textBox13.ReadOnly = true;
            textBox14.ReadOnly = true;
            textBox15.ReadOnly = true;
            textBox16.ReadOnly = true;
            textBox17.ReadOnly = true;
            textBox18.ReadOnly = true;
            textBox19.ReadOnly = true;
            showData();
            showData1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox20.Text=="")
            {
                MessageBox.Show("请填写缴费编号!");
                return;
            }
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
                String userInfo = String.Format("SELECT 求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者家庭住址,求职者学历层次,求职者聘用状态,求职者月薪要求,求职者会员等级 FROM 求职者 where 求职者聘用状态='未聘用' and 求职者会员等级=(SELECT 介绍人员权限 FROM 介绍人员 WHERE 介绍人员编号='{0}')", Form1.user_name);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t3");
                if (dataset.Tables["t3"].Rows.Count == 0)
                {
                    MessageBox.Show("没有可以匹配的求职者!");
                    return;
                }
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
            try
            {
                string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                String userInfo = String.Format("SELECT * FROM 招聘信息 where 已聘人数<需聘人数 and 招聘信息权限<=(SELECT 介绍人员权限 FROM 介绍人员 WHERE 介绍人员编号='{0}')", Form1.user_name);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t4");
                if (dataset.Tables["t4"].Rows.Count == 0)
                {
                    MessageBox.Show("没有可以匹配的招聘信息!");
                    return;
                }
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
            conn = new SqlConnection(ConnectionString);
            string strSQL = "INSERT INTO 缴费 VALUES(";
            strSQL += "'" + textBox20.Text + "'" + ",";
            strSQL += "'" + Form1.user_name + "'" + ",";
            strSQL += "'" + textBox2.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox11.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox1.Text.Trim() + "'" + ",";
            strSQL += "0" + ",";
            strSQL += "0" + ",";
            strSQL +="0" +")";
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("缴费信息建立成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("缴费编号已存在!请更换缴费编号!");
                return;
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
            conn = new SqlConnection(ConnectionString);
            strSQL = "UPDATE 求职者 SET ";
            strSQL += "求职者聘用状态='" + "匹配成功" + "'";
            strSQL += " WHERE 求职者编号='" + textBox11.Text + "'";
            int index = dataGridView1.CurrentRow.Index;
            command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("求职者状态更新成功!");
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
            showData();
            showData1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox11.Text=="")
            {
                MessageBox.Show("求职者编号为空!");
                return;
            }
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
                String userInfo = String.Format("SELECT 辞退编号,求职者.求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者学历层次,用人单位.单位编号,单位名称,单位所属行业,介绍人员.介绍人员编号,介绍人员姓名,介绍人员性别,介绍人员电话,介绍人员邮箱,辞退原因 FROM 辞退,求职者,用人单位,介绍人员 where 辞退.求职者编号=求职者.求职者编号 and 辞退.单位编号=用人单位.单位编号 and 辞退.介绍人员编号=介绍人员.介绍人员编号 and 辞退.求职者编号='{0}'", textBox11.Text);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t5");
                dataGridView3.DataSource = dataset;
                dataGridView3.DataMember = "t5";
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
