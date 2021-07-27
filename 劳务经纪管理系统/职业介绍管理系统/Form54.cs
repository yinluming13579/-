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
    public partial class Form54 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        private string curNo = "";
        public Form54()
        {
            InitializeComponent();
        }

        private void Form54_Load(object sender, EventArgs e)
        {

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
                String userInfo = String.Format("SELECT 求职者.求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者家庭住址,求职者学历层次,求职者月薪要求,求职者会员等级,辞退次数 FROM 求职者,(SELECT 求职者编号,COUNT(*) AS 辞退次数 FROM 辞退 GROUP BY 求职者编号) AS 统计 WHERE 求职者.求职者编号=统计.求职者编号 AND 求职者聘用状态='未聘用' ORDER BY 辞退次数 DESC");
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t1");
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = "t1";
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
            conn = new SqlConnection(ConnectionString);
            if (dataGridView1.Rows.Count <= 1) return;
            int index = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows[index].Selected = true;
            curNo = this.dataGridView1.Rows[index].Cells[0].Value.ToString();
            SqlCommand command = null;
            string strSQL = "DELETE FROM 辞退 WHERE 求职者编号='" + curNo + "'";
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("辞退信息注销成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("辞退信息注销失败!");
                return;
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
            conn = new SqlConnection(ConnectionString);
            if (dataGridView1.Rows.Count <= 1) return;
            dataGridView1.Rows[index].Selected = true;
            curNo = this.dataGridView1.Rows[index].Cells[0].Value.ToString();
            command = null;
            strSQL = "DELETE FROM 求职者 WHERE 求职者编号='" + curNo + "'";
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("求职者信息注销成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("求职者信息注销失败!");
                return;
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
            conn = new SqlConnection(ConnectionString);
            if (dataGridView1.Rows.Count <= 1) return;
            dataGridView1.Rows[index].Selected = true;
            curNo = this.dataGridView1.Rows[index].Cells[0].Value.ToString();
            command = null;
            strSQL = "DELETE FROM 简历 WHERE 求职者简历编号='" + curNo + "'";
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("求职者简历信息注销成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("求职者简历信息注销失败!");
                return;
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
            button1_Click(sender, e);
        }
    }
}
