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
    public partial class Form40 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        private SqlCommand cmd = null;
        public Form40()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tb1 = textBox1.Text;
            if (comboBox2.Text == "like")
            {
                tb1 = "%" + textBox1.Text + "%";
            }
            string strSQL = "SELECT 缴费编号,单位缴费,求职者缴费,求职者薪资,介绍人员.介绍人员编号,介绍人员姓名,介绍人员性别,介绍人员电话,用人单位.单位编号,单位名称,单位所属行业,求职者.求职者编号,求职者姓名,求职者学历层次 FROM 求职者,缴费,用人单位,介绍人员 WHERE 缴费.求职者编号=求职者.求职者编号 AND 缴费.单位编号=用人单位.单位编号 AND 缴费.介绍人员编号=介绍人员.介绍人员编号 AND ";
            if (comboBox2.Text == "like")
            {
                strSQL += comboBox1.Text + " " + comboBox2.Text + "'" + tb1 + "'";
            }
            else
            {
                strSQL += comboBox1.Text + comboBox2.Text + "'" + tb1 + "'";
            }
            try
            {
                cmd.CommandText = strSQL;
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

        private void Form40_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT 缴费编号,单位缴费,求职者缴费,求职者薪资,介绍人员.介绍人员编号,介绍人员姓名,介绍人员性别,介绍人员电话,用人单位.单位编号,单位名称,单位所属行业,求职者.求职者编号,求职者姓名,求职者学历层次 FROM 求职者,缴费,用人单位,介绍人员 WHERE 缴费.求职者编号=求职者.求职者编号 AND 缴费.单位编号=用人单位.单位编号 AND 缴费.介绍人员编号=介绍人员.介绍人员编号";
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t1");
                comboBox1.Items.Clear();
                for (int i = 0; i < dataset.Tables["t1"].Columns.Count; i++)
                    comboBox1.Items.Add(dataset.Tables["t1"].Columns[i].ToString());
                dataset.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "缴费编号" || comboBox1.Text == "介绍人员编号" || comboBox1.Text == "介绍人员姓名" || comboBox1.Text == "介绍人员性别" || comboBox1.Text == "介绍人员电话" || comboBox1.Text == "单位编号" || comboBox1.Text == "单位名称" || comboBox1.Text=="单位所属行业" || comboBox1.Text=="求职者编号" || comboBox1.Text=="求职者姓名" || comboBox1.Text=="求职者学历层次")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("=");
                comboBox2.Items.Add("like");
            }
            else if (comboBox1.Text == "单位缴费" || comboBox1.Text == "求职者缴费" || comboBox1.Text=="求职者薪资")
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("=");
                comboBox2.Items.Add("like");
                comboBox2.Items.Add(">");
                comboBox2.Items.Add("<");
            }
        }
    }
}
