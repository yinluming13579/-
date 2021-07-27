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
    public partial class Form45 : Form
    {
        public Form45()
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
                String userInfo = String.Format("SELECT 介绍人员.介绍人员编号,求职者.求职者编号,求职者姓名,求职者性别,求职者学历层次,求职者月薪要求,招聘信息.招聘编号,学历要求,岗位要求,单位名称,单位联系电话 FROM 求职者,缴费,招聘信息,用人单位,介绍人员 where 求职者.求职者编号=缴费.求职者编号 and 用人单位.单位编号=缴费.单位编号 and 招聘信息.招聘编号=缴费.招聘编号 and 介绍人员.介绍人员编号=缴费.介绍人员编号 and 求职者聘用状态='匹配成功' and 介绍人员.介绍人员编号='{0}'", Form1.user_name);
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

        private void Form45_Load(object sender, EventArgs e)
        {

        }
    }
}
