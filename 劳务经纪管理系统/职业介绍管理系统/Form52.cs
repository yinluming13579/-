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
    public partial class Form52 : Form
    {
        public Form52()
        {
            InitializeComponent();
        }

        private void Form52_Load(object sender, EventArgs e)
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
                String userInfo = String.Format("SELECT 辞退编号,求职者.求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者学历层次,用人单位.单位编号,单位名称,单位所属行业,单位法人,单位联系电话,单位简介,介绍人员.介绍人员编号,介绍人员姓名,介绍人员性别,介绍人员电话,辞退原因 FROM 辞退,求职者,用人单位,介绍人员 where 辞退.求职者编号=求职者.求职者编号 and 辞退.单位编号=用人单位.单位编号 and 辞退.介绍人员编号=介绍人员.介绍人员编号");
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
    }
}
