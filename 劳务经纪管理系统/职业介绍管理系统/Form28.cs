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
    public partial class Form28 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        private string curNo = "";
        public Form28()
        {
            InitializeComponent();
        }
        private void showData()
        {
            try
            {
                if (conn == null) conn.Open();
                DataAdapter = new SqlDataAdapter("SELECT 缴费编号,求职者.求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者学历层次,用人单位.单位编号,单位名称,单位所在地,求职者薪资,招聘编号 FROM 求职者,缴费,用人单位 where 求职者.求职者编号=缴费.求职者编号 AND 缴费.单位编号=用人单位.单位编号 AND 求职者聘用状态='聘用成功'", conn);
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
        private void Form28_Load(object sender, EventArgs e)
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
            string strSQL = "DELETE FROM 缴费 WHERE 缴费编号='" + curNo + "'";
            MessageBox.Show(strSQL);
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("缴费信息删除成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("缴费信息注销失败!请检查依赖关系!");
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
            curNo = this.dataGridView1.Rows[index].Cells[1].Value.ToString();
            strSQL = "UPDATE 求职者 SET 求职者聘用状态='未聘用' WHERE 求职者编号='" + curNo + "'";
            MessageBox.Show(strSQL);
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("求职者信息更新成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("求职者信息更新失败!");
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }

            curNo = this.dataGridView1.Rows[index].Cells[10].Value.ToString();
            strSQL = "UPDATE 招聘信息 SET 已聘人数=已聘人数-1 WHERE 招聘编号='" + curNo + "'";
            MessageBox.Show(strSQL);
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if(n>0)
                {
                    MessageBox.Show("招聘信息更改成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("招聘信息更改失败!");
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
