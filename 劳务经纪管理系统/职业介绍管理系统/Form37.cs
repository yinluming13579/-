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
    public partial class Form37 : Form
    {
        public Form37()
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
                String userInfo = String.Format("SELECT C端业绩.介绍人员编号,介绍人员姓名,介绍人员C端业绩 FROM 介绍人员,(SELECT 介绍人员.介绍人员编号,SUM(求职者缴费) AS 介绍人员C端业绩 FROM 缴费,介绍人员 where 介绍人员.介绍人员编号=缴费.介绍人员编号 GROUP BY 介绍人员.介绍人员编号) AS C端业绩 WHERE 介绍人员.介绍人员编号=C端业绩.介绍人员编号 ORDER BY C端业绩.介绍人员C端业绩 DESC");
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

        private void Form37_Load(object sender, EventArgs e)
        {

        }
    }
}
