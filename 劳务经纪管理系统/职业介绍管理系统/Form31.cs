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
    public partial class Form31 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        private SqlCommand cmd = null;
        public Form31()
        {
            InitializeComponent();
        }

        private void Form31_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("求职者姓名");
            comboBox1.Items.Add("求职者性别");
            comboBox1.Items.Add("求职者家庭住址");
            comboBox1.Items.Add("求职者学历层次");
            comboBox1.Items.Add("求职者会员等级");
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
                strSQL = "SELECT 求职者姓名,AVG(求职者月薪要求) AS 求职者平均月薪要求 FROM 求职者 GROUP BY 求职者姓名";
            }
            else if (comboBox1.Text == "求职者性别")
            {
                strSQL = "SELECT 求职者性别,AVG(求职者月薪要求) AS 求职者平均月薪要求 FROM 求职者 GROUP BY 求职者性别";
            }
            else if (comboBox1.Text == "求职者学历层次")
            {
                strSQL = "SELECT 求职者学历层次,AVG(求职者月薪要求) AS 求职者平均月薪要求 FROM 求职者 GROUP BY 求职者学历层次";
            }
            else if (comboBox1.Text == "求职者会员等级")
            {
                strSQL = "SELECT 求职者会员等级,AVG(求职者月薪要求) AS 求职者平均月薪要求 FROM 求职者 GROUP BY 求职者会员等级";
            }
            else if (comboBox1.Text == "求职者家庭住址")
            {
                strSQL = "SELECT 求职者家庭住址,AVG(求职者月薪要求) AS 求职者平均月薪要求 FROM 求职者 GROUP BY 求职者家庭住址";
            }
            try
            {
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strSQL;
                DataAdapter.SelectCommand = cmd;
                dataset.Clear();
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
                if (conn != null) conn.Close();

            }
        }
    }
}
