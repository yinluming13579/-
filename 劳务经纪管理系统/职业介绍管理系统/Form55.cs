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
    public partial class Form55 : Form
    {
        public Form55()
        {
            InitializeComponent();
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
                String userInfo = String.Format("SELECT 用人单位.单位编号,单位名称,单位所在地,单位所属行业,单位法人,单位联系电话,单位邮箱,单位简介,单位网址,辞退次数 FROM 用人单位,(SELECT 单位编号,COUNT(*) AS 辞退次数 FROM 辞退 GROUP BY 单位编号) AS 统计 WHERE 用人单位.单位编号=统计.单位编号 ORDER BY 辞退次数 DESC");
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

        private void Form55_Load(object sender, EventArgs e)
        {

        }
    }
}
