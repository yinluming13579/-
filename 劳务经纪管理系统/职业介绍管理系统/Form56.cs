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
    public partial class Form56 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        private SqlCommand cmd = null;
        public Form56()
        {
            InitializeComponent();
        }
        private void showData()
        {
            conn = new SqlConnection(ConnectionString);
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT * FROM 招聘信息");
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset = new DataSet();
                DataAdapter.Fill(dataset);
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = dataset.Tables[0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Form56_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            try
            {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                DataAdapter = new SqlDataAdapter();
                dataset = new DataSet();
                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM 职业分类";
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t1");
                comboBox1.Items.Clear();
                for (int i = 0; i < dataset.Tables["t1"].Rows.Count; i++)
                    comboBox1.Items.Add(dataset.Tables["t1"].Rows[i]["职业类型号"].ToString());
                dataset.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox3.ReadOnly = true;
            textBox6.Text = "0";
            textBox6.ReadOnly = true;
            showData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() == "" || textBox1.Text.Trim() == "" || textBox2.Text.Trim()=="" || textBox3.Text.Trim()=="" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox6.Text.Trim()=="" || textBox7.Text.Trim() == "" || textBox8.Text.Trim() == "")
            {
                MessageBox.Show("请填写全部信息!");
                return;
            }
            conn = new SqlConnection(ConnectionString);
            string strSQL = "INSERT INTO 招聘信息 VALUES(";
            strSQL += "'" + textBox1.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox2.Text.Trim() + "'" + ",";
            strSQL += "'" + comboBox1.Text.Trim() + "'" + ",";
            strSQL += "'" + textBox4.Text.Trim() + "'" + ",";
            if (radioButton1.Checked)
            {
                strSQL += "'" + "初中及以下" + "'" + ",";
            }
            else if (radioButton2.Checked)
            {
                strSQL += "'" + "高中" + "'" + ",";
            }
            else if (radioButton3.Checked)
            {
                strSQL += "'" + "大专" + "'" + ",";
            }
            else if (radioButton4.Checked)
            {
                strSQL += "'" + "本科" + "'" + ",";
            }
            else if (radioButton5.Checked)
            {
                strSQL += "'" + "硕士" + "'" + ",";
            }
            else if (radioButton6.Checked)
            {
                strSQL += "'" + "博士" + "'" + ",";
            }
            strSQL += "'" + textBox5.Text.Trim() + "'" + ",";
            strSQL += textBox6.Text.Trim() + ",";
            strSQL += textBox7.Text.Trim() + ",";
            strSQL += textBox8.Text.Trim() + ",";
            if (radioButton7.Checked)
            {
                strSQL += "1" + ")";
            }
            else if (radioButton8.Checked)
            {
                strSQL += "2" + ")";
            }
            else if (radioButton9.Checked)
            {
                strSQL += "3" + ")";
            }
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
                conn.Open();
                int n = command.ExecuteNonQuery();
                if (n > 0) MessageBox.Show("招聘信息发布成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("招聘编号已存在,请更换招聘编号发布!");
            }
            finally
            {
                if (conn != null) conn.Close();
                command.Dispose();
            }
            showData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
                String userInfo = String.Format("SELECT 职业类型名 FROM 职业分类 WHERE 职业类型号='{0}'", comboBox1.Text);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t2");
                textBox3.Text = dataset.Tables["t2"].Rows[0]["职业类型名"].ToString();
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
