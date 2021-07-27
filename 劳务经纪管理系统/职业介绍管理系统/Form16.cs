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
    public partial class Form16 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        private SqlCommand cmd = null;
        public Form16()
        {
            InitializeComponent();
        }
        private void showData()
        {
            string tname = "";
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT 介绍人员编号,介绍人员姓名,介绍人员性别,介绍人员身份证号码,介绍人员电话,介绍人员邮箱,中介公司编号,介绍人员权限 FROM 介绍人员 where 介绍人员编号='{0}'", Form1.user_name);
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
                textBox5.DataBindings.Clear();
                comboBox1.DataBindings.Clear();
                textBox1.DataBindings.Add("Text", dataset, "table.介绍人员编号");
                textBox2.DataBindings.Add("Text", dataset, "table.介绍人员姓名");
                textBox3.DataBindings.Add("Text", dataset, "table.介绍人员身份证号码");
                textBox4.DataBindings.Add("Text", dataset, "table.介绍人员电话");
                textBox5.DataBindings.Add("Text", dataset, "table.介绍人员邮箱");
                comboBox1.DataBindings.Add("Text", dataset, "table.中介公司编号");
                if (dataset.Tables[0].Rows[0]["介绍人员性别"].ToString() == "男")
                {
                    radioButton1.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["介绍人员性别"].ToString() == "女")
                {
                    radioButton2.Checked = true;
                }
                if (dataset.Tables[0].Rows[0]["介绍人员权限"].ToString() == "1")
                {
                    radioButton3.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["介绍人员权限"].ToString() == "2")
                {
                    radioButton4.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["介绍人员权限"].ToString() == "3")
                {
                    radioButton5.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string strSQL = "UPDATE 介绍人员 SET ";
            if (radioButton1.Checked)
            {
                strSQL += "介绍人员性别='" + "男" + "'";
            }
            else if (radioButton2.Checked)
            {
                strSQL += "介绍人员性别='" + "女" + "'";
            }
            strSQL += ",介绍人员姓名='" + textBox2.Text + "'";
            strSQL += ",介绍人员身份证号码='" + textBox3.Text + "'";
            strSQL += ",介绍人员电话='" + textBox4.Text + "'";
            strSQL += ",介绍人员邮箱='" + textBox5.Text + "'";
            strSQL += ",中介公司编号='" + comboBox1.Text + "'";
            if (radioButton3.Checked)
            {
                strSQL += ",介绍人员权限=" + "1";
            }
            else if (radioButton4.Checked)
            {
                strSQL += ",介绍人员权限=" + "2";
            }
            else if (radioButton5.Checked)
            {
                strSQL += ",介绍人员权限=" + "3";
            }
            strSQL += " WHERE 介绍人员编号='" + textBox1.Text + "'";
            int index = dataGridView1.CurrentRow.Index;
            SqlCommand command = null;
            try
            {
                command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = strSQL;
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

        private void Form16_Load(object sender, EventArgs e)
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
                cmd.CommandText = "SELECT * FROM 中介公司";
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t1");
                comboBox1.Items.Clear();
                for (int i = 0; i < dataset.Tables["t1"].Rows.Count; i++)
                    comboBox1.Items.Add(dataset.Tables["t1"].Rows[i]["中介公司编号"].ToString());
                dataset.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox1.ReadOnly = true;
            textBox6.ReadOnly = true;
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
                String userInfo = String.Format("SELECT 中介公司名称 FROM 中介公司 WHERE 中介公司编号='{0}'", comboBox1.Text);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t2");
                textBox6.Text = dataset.Tables["t2"].Rows[0]["中介公司名称"].ToString();
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 1) return;
            int index = dataGridView1.CurrentRow.Index;
            string curNo = this.dataGridView1.Rows[index].Cells[2].Value.ToString();
            if (curNo == "男")
            {
                radioButton1.Checked = true;
            }
            else if (curNo == "女")
            {
                radioButton2.Checked = true;
            }
            curNo = this.dataGridView1.Rows[index].Cells[7].Value.ToString();
            if (curNo == "1")
            {
                radioButton3.Checked = true;
            }
            else if (curNo == "2")
            {
                radioButton4.Checked = true;
            }
            else if (curNo == "3")
            {
                radioButton5.Checked = true;
            }
        }
    }
}
