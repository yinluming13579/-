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
    public partial class Form25 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        private string curNo = "";
        public Form25()
        {
            InitializeComponent();
        }
        private void delete()
        {
            if (dataGridView1.Rows.Count <= 1) return;
            int index = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows[index].Selected = true;
            curNo = this.dataGridView1.Rows[index].Cells[0].Value.ToString();
            SqlCommand command = null;
            string strSQL = "DELETE FROM 简历 WHERE 求职者简历编号='" + curNo + "'";
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("简历信息注销成功!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("没有查询到求职者简历信息!");
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
        }
        private void showData()
        {
            try
            {
                if (conn == null) conn.Open();
                DataAdapter = new SqlDataAdapter("SELECT 求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者家庭住址,求职者学历层次,求职者聘用状态,求职者月薪要求,求职者会员等级 FROM 求职者", conn);
                dataset = new DataSet();
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
                if (conn != null) conn.Close();
                DataAdapter.Dispose();
                dataset.Dispose();
            }
        }
        private void Form25_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            showData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 1) return;
            int index = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows[index].Selected = true;
            curNo = this.dataGridView1.Rows[index].Cells[0].Value.ToString();
            SqlCommand command = null;
            string strSQL = "DELETE FROM 求职者 WHERE 求职者编号='" + curNo + "'";
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
                MessageBox.Show("求职者注销失败!请先注销求职者关联信息!");
                return;
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
            delete();
            showData();
        }
    }
}
