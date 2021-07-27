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
    public partial class Form10 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form10()
        {
            InitializeComponent();
        }
        private void showData()
        {
            string tname = "";
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT 单位编号,单位工商注册号,单位名称,单位所在地,单位所属行业,单位法人,单位联系电话,单位邮箱,单位简介,单位网址 FROM 用人单位 where 单位编号='{0}'", Form1.user_name);
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset = new DataSet();
                DataAdapter.Fill(dataset);
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = dataset.Tables[0].ToString();
                tname = dataset.Tables[0].ToString();
                textBox1.DataBindings.Clear();
                textBox2.DataBindings.Clear();
                textBox3.DataBindings.Clear();
                textBox4.DataBindings.Clear();
                comboBox1.DataBindings.Clear();
                textBox6.DataBindings.Clear();
                textBox7.DataBindings.Clear();
                textBox8.DataBindings.Clear();
                textBox9.DataBindings.Clear();
                textBox10.DataBindings.Clear();
                textBox1.DataBindings.Add("Text", dataset, "table.单位编号");
                textBox2.DataBindings.Add("Text", dataset, "table.单位工商注册号");
                textBox3.DataBindings.Add("Text", dataset, "table.单位名称");
                textBox4.DataBindings.Add("Text", dataset, "table.单位所在地");
                comboBox1.DataBindings.Add("Text", dataset, "table.单位所属行业");
                textBox6.DataBindings.Add("Text", dataset, "table.单位法人");
                textBox7.DataBindings.Add("Text", dataset, "table.单位联系电话");
                textBox8.DataBindings.Add("Text", dataset, "table.单位邮箱");
                textBox9.DataBindings.Add("Text", dataset, "table.单位简介");
                textBox10.DataBindings.Add("Text", dataset, "table.单位网址");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            comboBox1.Items.Add("农、林、牧、渔");
            comboBox1.Items.Add("采矿业");
            comboBox1.Items.Add("制造业");
            comboBox1.Items.Add("电力、燃气及水的生产和供应业");
            comboBox1.Items.Add("建筑业");
            comboBox1.Items.Add("交通运输、仓储和邮政业");
            comboBox1.Items.Add("信息传输、计算机服务和软件业");
            comboBox1.Items.Add("批发和零售业");
            comboBox1.Items.Add("住宿和餐饮业");
            comboBox1.Items.Add("金融业");
            comboBox1.Items.Add("房地产业");
            comboBox1.Items.Add("租赁和商务服务业");
            comboBox1.Items.Add("科学研究、技术服务和地质勘查业");
            comboBox1.Items.Add("水利、环境和公共设施管理业");
            comboBox1.Items.Add("居民服务和其他服务业");
            textBox1.ReadOnly = true;
            showData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSQL = "Update 用人单位 set ";
            strSQL += "单位工商注册号='" + textBox2.Text + "',";
            strSQL += "单位名称='" + textBox3.Text + "',";
            strSQL += "单位所在地='" + textBox4.Text + "',";
            strSQL += "单位所属行业='" + comboBox1.Text + "',";
            strSQL += "单位法人='" + textBox6.Text + "',";
            strSQL += "单位联系电话='" + textBox7.Text + "',";
            strSQL += "单位邮箱='" + textBox8.Text + "',";
            strSQL += "单位简介='" + textBox9.Text + "',";
            strSQL += "单位网址='" + textBox10.Text + "'";
            strSQL += " WHERE 单位编号='" + textBox1.Text + "'";
            int index = dataGridView1.CurrentRow.Index;
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0)
                {
                    MessageBox.Show("成功更新数据!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
            showData();
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
            dataGridView1.Rows[index].Selected = true;
        }
    }
}
