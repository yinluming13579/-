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
    public partial class Form33 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        private string curNo = "";
        public Form33()
        {
            InitializeComponent();
        }
        private void showData()
        {
            try
            {
                if (conn == null) conn.Open();
                DataAdapter = new SqlDataAdapter("SELECT 招聘编号,单位名称,职业类型名,需聘人数,已聘人数,岗位简介 FROM 招聘信息,用人单位,职业分类 WHERE 招聘信息.单位编号=用人单位.单位编号 AND 招聘信息.职业类型号=职业分类.职业类型号", conn);
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
        private void Form33_Load(object sender, EventArgs e)
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
            string strSQL = "UPDATE 招聘信息 SET 需聘人数=已聘人数 WHERE 招聘编号='" + curNo + "'";
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("招聘已停止");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("停止招聘失败!");
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
