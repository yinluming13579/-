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
    public partial class Form30 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        private SqlCommand cmd = null;
        public Form30()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text=="")
            {
                MessageBox.Show("请填写查询条件!");
                return;
            }
            conn = new SqlConnection(ConnectionString);
            string strSQL = "";
            if (comboBox1.Text == "求职者姓名")
            {
                strSQL = "SELECT 求职者姓名,AVG(求职者薪资) AS 求职者薪资 FROM 缴费,求职者 WHERE 缴费.求职者编号=求职者.求职者编号 GROUP BY 求职者姓名";
            }
            else if(comboBox1.Text == "求职者性别")
            {
                strSQL = "SELECT 求职者性别,AVG(求职者薪资) AS 求职者薪资 FROM 缴费,求职者 WHERE 缴费.求职者编号=求职者.求职者编号 GROUP BY 求职者性别";
            }
            else if(comboBox1.Text == "求职者学历层次")
            {
                strSQL = "SELECT 求职者学历层次,AVG(求职者薪资) AS 求职者薪资 FROM 缴费,求职者 WHERE 缴费.求职者编号=求职者.求职者编号 GROUP BY 求职者学历层次";
            }
            else if(comboBox1.Text == "求职者会员等级")
            {
                strSQL = "SELECT 求职者会员等级,AVG(求职者薪资) AS 求职者薪资 FROM 缴费,求职者 WHERE 缴费.求职者编号=求职者.求职者编号 GROUP BY 求职者会员等级";
            }
            else if(comboBox1.Text == "单位名称")
            {
                strSQL = "SELECT 单位名称,AVG(求职者薪资) AS 求职者薪资 FROM 缴费,用人单位 WHERE 缴费.单位编号=用人单位.单位编号 GROUP BY 单位名称";
            }
            else if(comboBox1.Text == "单位所在地")
            {
                strSQL = "SELECT 单位所在地,AVG(求职者薪资) AS 求职者薪资 FROM 缴费,用人单位 WHERE 缴费.单位编号=用人单位.单位编号 GROUP BY 单位所在地";
            }
            else if (comboBox1.Text == "中介公司名称")
            {
                strSQL = "SELECT 中介公司名称,AVG(求职者薪资) AS 求职者薪资 FROM 缴费,介绍人员,中介公司 WHERE 缴费.介绍人员编号=介绍人员.介绍人员编号 AND 介绍人员.中介公司编号=中介公司.中介公司编号 GROUP BY 中介公司名称";
            }
            else if(comboBox1.Text == "职业类型名")
            {
                strSQL = "SELECT 职业类型名,AVG(求职者薪资) AS 求职者薪资 FROM 缴费,招聘信息,职业分类 WHERE 缴费.招聘编号=招聘信息.招聘编号 AND 招聘信息.职业类型号=职业分类.职业类型号 GROUP BY 职业类型名";
            }
            try
            {
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText =strSQL;
                DataAdapter.SelectCommand = cmd;
                dataset.Clear();
                DataAdapter.Fill(dataset, "t2");
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = "t2";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();

            }
        }

        private void Form30_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("求职者姓名");
            comboBox1.Items.Add("求职者性别");
            comboBox1.Items.Add("求职者学历层次");
            comboBox1.Items.Add("求职者会员等级");
            comboBox1.Items.Add("单位名称");
            comboBox1.Items.Add("单位所在地");
            comboBox1.Items.Add("中介公司名称");
            comboBox1.Items.Add("职业类型名");
        }
    }
}
