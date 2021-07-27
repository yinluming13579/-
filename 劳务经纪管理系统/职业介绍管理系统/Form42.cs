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
    public partial class Form42 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form42()
        {
            InitializeComponent();
        }
        private void showData()
        {
            string tname = "";
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT * FROM 简历 where 求职者简历编号='{0}'", Form1.user_name);
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
                textBox6.DataBindings.Clear();
                textBox7.DataBindings.Clear();
                textBox8.DataBindings.Clear();
                textBox9.DataBindings.Clear();
                textBox10.DataBindings.Clear();
                textBox1.DataBindings.Add("Text", dataset, "table.求职者简历编号");
                textBox2.DataBindings.Add("Text", dataset, "table.求职者姓名");
                textBox3.DataBindings.Add("Text", dataset, "table.求职者年龄");
                textBox4.DataBindings.Add("Text", dataset, "table.求职者民族");
                textBox5.DataBindings.Add("Text", dataset, "table.求职者籍贯");
                textBox6.DataBindings.Add("Text", dataset, "table.求职者毕业院校");
                textBox7.DataBindings.Add("Text", dataset, "table.求职者学习经历");
                textBox8.DataBindings.Add("Text", dataset, "table.求职者工作经历");
                textBox9.DataBindings.Add("Text", dataset, "table.求职者获得荣誉");
                textBox10.DataBindings.Add("Text", dataset, "table.求职者自我评价");
                if (dataset.Tables[0].Rows[0]["求职者性别"].ToString() == "男")
                {
                    radioButton1.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["求职者性别"].ToString() == "女")
                {
                    radioButton2.Checked = true;
                }
                if (dataset.Tables[0].Rows[0]["求职者政治面貌"].ToString() == "群众")
                {
                    radioButton3.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["求职者政治面貌"].ToString() == "共青团员")
                {
                    radioButton4.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["求职者政治面貌"].ToString() == "中共党员")
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
            if(textBox1.Text=="" || textBox2.Text=="" || textBox3.Text=="" || textBox4.Text=="" || textBox5.Text=="" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "")
            {
                MessageBox.Show("要更新的信息不能为空!");
                return;
            }
            string strSQL = "UPDATE 简历 SET ";
            strSQL += "求职者姓名='" + textBox2.Text + "'";
            if (radioButton1.Checked)
            {
                strSQL += ",求职者性别='" + "男" + "'";
            }
            else if (radioButton2.Checked)
            {
                strSQL += ",求职者性别='" + "女" + "'";
            }
            strSQL += ",求职者年龄='" + textBox3.Text + "'";
            strSQL += ",求职者民族='" + textBox4.Text + "'";
            strSQL += ",求职者籍贯='" + textBox5.Text + "'";
            if (radioButton3.Checked)
            {
                strSQL += ",求职者政治面貌='" + "群众" + "'";
            }
            else if (radioButton4.Checked == true)
            {
                strSQL += ",求职者政治面貌='" + "共青团员" + "'";
            }
            else if (radioButton5.Checked == true)
            {
                strSQL += ",求职者政治面貌='" + "中共党员" + "'";
            }
            strSQL += ",求职者毕业院校='" + textBox6.Text + "'";
            strSQL += ",求职者学习经历='" + textBox7.Text + "'";
            strSQL += ",求职者工作经历='" + textBox8.Text + "'";
            strSQL += ",求职者获得荣誉='" + textBox9.Text + "'";
            strSQL += ",求职者自我评价='" + textBox10.Text + "'";
            strSQL += " WHERE 求职者简历编号='" + textBox1.Text + "'";
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
                    MessageBox.Show("简历信息更新成功!");
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

        private void Form42_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            panel1.Enabled = false;
            showData();
        }
    }
}
