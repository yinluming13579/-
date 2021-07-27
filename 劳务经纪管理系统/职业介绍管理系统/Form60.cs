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
    public partial class Form60 : Form
    {
        public Form60()
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
                String userInfo = String.Format("SELECT B端业绩.中介公司编号,中介公司名称,中介公司B端业绩 FROM 中介公司,(SELECT 中介公司.中介公司编号,SUM(单位缴费) AS 中介公司B端业绩 FROM 缴费,介绍人员,中介公司 where 介绍人员.介绍人员编号=缴费.介绍人员编号 AND 介绍人员.中介公司编号=中介公司.中介公司编号 GROUP BY 中介公司.中介公司编号) AS B端业绩 WHERE 中介公司.中介公司编号=B端业绩.中介公司编号 ORDER BY B端业绩.中介公司B端业绩 DESC");
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

        private void Form60_Load(object sender, EventArgs e)
        {

        }
    }
}
