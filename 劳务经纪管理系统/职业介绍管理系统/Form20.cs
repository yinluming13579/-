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
    public partial class Form20 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form20()
        {
            InitializeComponent();
        }
        private void showData()
        {
            conn = new SqlConnection(ConnectionString);
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT * FROM 中介公司");
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset = new DataSet();
                DataAdapter.Fill(dataset,"t0");
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = dataset.Tables["t0"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Form20_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || textBox11.Text == "")
            {
                MessageBox.Show("信息填写不完整,请补全全部信息!");
                return;
            }
            conn = new SqlConnection(ConnectionString);
            string strSQL = "INSERT INTO 中介公司 VALUES(";
            strSQL += "'" + textBox1.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox2.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox3.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox4.Text.Trim() + "'" + ",";
            strSQL += textBox5.Text.Trim() + ",";
            strSQL += "'" + textBox6.Text.Trim()+"-"+textBox7.Text.Trim()+"-"+textBox8.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox9.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox10.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox11.Text.Trim() + "'" + ")";
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("添加中介公司成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("中介公司编号已存在!请更换编号!");
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
