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
    public partial class Form48 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form48()
        {
            InitializeComponent();
        }
        private void showData()
        {
            conn = new SqlConnection(ConnectionString);
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("select 求职者.求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者家庭住址,求职者学历层次,求职者会员等级,缴费编号,介绍人员编号,缴费.招聘编号,求职者薪资,职业类型号,岗位简介,学历要求,岗位要求,已聘人数,需聘人数,工资,招聘信息权限 from 求职者,缴费,招聘信息 where 缴费.求职者编号=求职者.求职者编号 and 缴费.招聘编号=招聘信息.招聘编号 and 求职者聘用状态='聘用成功' and 招聘信息.招聘编号 in (select 招聘编号 from 招聘信息 where 单位编号='{0}')", Form1.user_name);
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset = new DataSet();
                DataAdapter.Fill(dataset, "t1");
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = dataset.Tables["t1"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void showData1()
        {
            int count=0;
            conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                String userInfo = String.Format("SELECT * FROM 辞退");
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t2");
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
            if (dataset.Tables["t2"].Rows.Count == 0)
            {
                count = 0;
            }
            else
            {
                count = int.Parse(dataset.Tables["t2"].Rows[dataset.Tables["t2"].Rows.Count - 1]["辞退编号"].ToString());
            }
            count++;
            textBox1.Text = count.ToString();
        }
        private void Form48_Load(object sender, EventArgs e)
        {
            showData();
            showData1();
            textBox1.ReadOnly = true;
            comboBox1.Items.Add("求职者态度消极,工作散漫");
            comboBox1.Items.Add("求职者失职致公司遭受损失");
            comboBox1.Items.Add("求职者无法胜任该工作岗位");
            comboBox1.Items.Add("求职者工作岗位因部门调动而被裁撤");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text=="")
            {
                MessageBox.Show("请选择辞退理由!");
                return;
            }
            conn = new SqlConnection(ConnectionString);
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("select 求职者.求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者家庭住址,求职者学历层次,求职者会员等级,缴费编号,介绍人员编号,缴费.招聘编号,缴费.单位编号,求职者薪资,职业类型号,岗位简介,学历要求,岗位要求,已聘人数,需聘人数,工资,招聘信息权限 from 求职者,缴费,招聘信息 where 缴费.求职者编号=求职者.求职者编号 and 缴费.招聘编号=招聘信息.招聘编号 and 求职者聘用状态='聘用成功' and 招聘信息.招聘编号 in (select 招聘编号 from 招聘信息 where 单位编号='{0}')", Form1.user_name);
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset = new DataSet();
                DataAdapter.Fill(dataset, "t1");
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = dataset.Tables["t1"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (dataset.Tables["t1"].Rows.Count == 0)
            {
                MessageBox.Show("当前已无可辞退的求职者!");
                return;
            }
            int index = dataGridView1.CurrentRow.Index;
            if (MessageBox.Show("确定要辞退"+dataset.Tables["t1"].Rows[index]["求职者姓名"].ToString()+"吗?","提示", MessageBoxButtons.OKCancel)==DialogResult.Cancel)
            {
                return;
            }
            conn = new SqlConnection(ConnectionString);
            string strSQL = "DELETE FROM 缴费 WHERE ";
            strSQL += "缴费编号='" + dataset.Tables["t1"].Rows[index]["缴费编号"].ToString() + "'";
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("缴费信息删除成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn = new SqlConnection(ConnectionString);
            strSQL = "UPDATE 求职者 SET 求职者聘用状态='未聘用'";
            strSQL += " WHERE 求职者编号='" + dataset.Tables["t1"].Rows[index]["求职者编号"].ToString() + "'";
            command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("求职者信息更新成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn = new SqlConnection(ConnectionString);
            strSQL = "UPDATE 招聘信息 SET 已聘人数=已聘人数-1";
            strSQL += " WHERE 招聘编号='" + dataset.Tables["t1"].Rows[index]["招聘编号"].ToString() + "'";
            command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("招聘信息更新成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn = new SqlConnection(ConnectionString);
            strSQL = "INSERT INTO 辞退 VALUES(";
            strSQL += int.Parse(textBox1.Text.Trim())+ ",";
            strSQL += "'" + dataset.Tables["t1"].Rows[index]["求职者编号"] + "'" + ",";
            strSQL += "'" + dataset.Tables["t1"].Rows[index]["介绍人员编号"] + "'" + ",";
            strSQL += "'" + dataset.Tables["t1"].Rows[index]["单位编号"] + "'" + ",";
            strSQL += "'" + comboBox1.Text + "'" + ")";
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("辞退信息记录成功");
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
    }
}
