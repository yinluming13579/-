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
    public partial class Form43 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form43()
        {
            InitializeComponent();
        }
        private void showData()
        {
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("select 求职者.求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者家庭住址,求职者学历层次,求职者月薪要求,求职者会员等级,缴费编号,介绍人员编号,缴费.单位编号,缴费.招聘编号,求职者薪资,职业类型号,岗位简介,学历要求,岗位要求,已聘人数,需聘人数,工资,招聘信息权限 from 求职者,缴费,招聘信息 where 缴费.求职者编号=求职者.求职者编号 and 缴费.招聘编号=招聘信息.招聘编号 and 求职者聘用状态='匹配成功' and 已聘人数<需聘人数 and 招聘信息.招聘编号 in (select 招聘编号 from 招聘信息 where 单位编号='{0}')", Form1.user_name);
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset = new DataSet();
                DataAdapter.Fill(dataset,"t1");
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = dataset.Tables["t1"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void check1()
        {
            conn = new SqlConnection(ConnectionString);
            String strSQL = String.Format("UPDATE 求职者 SET 求职者聘用状态='未聘用' WHERE 求职者聘用状态='匹配成功' AND 求职者编号 IN (SELECT 求职者.求职者编号 FROM 求职者,缴费,招聘信息 WHERE 求职者.求职者编号=缴费.求职者编号 AND 招聘信息.招聘编号=缴费.招聘编号 AND 已聘人数=需聘人数 AND 缴费.单位编号='{0}')", Form1.user_name);
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
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
        private void check2()
        {
            conn = new SqlConnection(ConnectionString);
            String strSQL = String.Format("DELETE FROM 缴费 WHERE 缴费编号 IN (SELECT 缴费编号 FROM 缴费,求职者 WHERE 缴费.求职者编号=求职者.求职者编号 AND 求职者.求职者聘用状态='未聘用')");
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
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
        private void Form43_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            showData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataset.Tables["t1"].Rows.Count==0)
            {
                MessageBox.Show("当前已无可录取的求职者!");
                return;
            }
            SqlConnection conn = null;
            conn = new SqlConnection(ConnectionString);
            int index = dataGridView1.CurrentRow.Index;
            String strSQL = String.Format("UPDATE 求职者 SET 求职者聘用状态='聘用成功' WHERE 求职者编号='{0}'", dataset.Tables["t1"].Rows[index]["求职者编号"].ToString());
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
                    MessageBox.Show("修改求职者状态成功!");
                }
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
            conn = new SqlConnection(ConnectionString);
            strSQL = "UPDATE 缴费 SET ";
            if (dataset.Tables["t1"].Rows[index]["求职者会员等级"].ToString() == "1")
            {
                strSQL += "求职者缴费='" + "100" + "'" + ",";
            }
            else if (dataset.Tables["t1"].Rows[index]["求职者会员等级"].ToString() == "2")
            {
                strSQL += "求职者缴费='" + "200" + "'"+ ",";
            }
            else if (dataset.Tables["t1"].Rows[index]["求职者会员等级"].ToString() == "3")
            {
                strSQL += "求职者缴费='" + "300" + "'" + ",";
            }
            if (dataset.Tables["t1"].Rows[index]["招聘信息权限"].ToString() == "1")
            {
                strSQL += "单位缴费='" + "100" + "'" + ",";
            }
            else if (dataset.Tables["t1"].Rows[index]["招聘信息权限"].ToString() == "2")
            {
                strSQL += "单位缴费='" + "200" + "'" + ",";
            }
            else if (dataset.Tables["t1"].Rows[index]["招聘信息权限"].ToString() == "3")
            {
                strSQL += "单位缴费='" + "300" + "'" + ",";
            }
            strSQL += "求职者薪资="+dataset.Tables["t1"].Rows[index]["工资"].ToString();
            strSQL += " WHERE 缴费编号='" + dataset.Tables["t1"].Rows[index]["缴费编号"].ToString() + "'";
            command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("缴费信息更新成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn = new SqlConnection(ConnectionString);
            strSQL = "UPDATE 招聘信息 SET 已聘人数=已聘人数+1";
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            showData();
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
            dataGridView1.Rows[index].Selected = true;
            check1();
            check2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataset.Tables["t1"].Rows.Count == 0)
            {
                MessageBox.Show("当前已无可查看简历的求职者!");
                return;
            }
            DataSet dataset1 = null;
            try
            {
                conn = new SqlConnection(ConnectionString);
                int index = dataGridView1.CurrentRow.Index;
                if (conn == null) conn.Open();
                String userInfo = String.Format("select * from 简历 where 求职者简历编号='{0}'", dataset.Tables["t1"].Rows[index]["求职者编号"].ToString());
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset1 = new DataSet();
                DataAdapter.Fill(dataset1, "t2");
                if(dataset1.Tables["t2"].Rows.Count==0)
                {
                    MessageBox.Show("该求职者还没有填写简历!");
                    return;
                }
                dataGridView2.DataSource = dataset1;
                dataGridView2.DataMember = dataset1.Tables["t2"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataset.Tables["t1"].Rows.Count == 0)
            {
                MessageBox.Show("当前已无可拒绝的求职者!");
                return;
            }
            int index = dataGridView1.CurrentRow.Index;
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
            showData();
        }
    }
}
