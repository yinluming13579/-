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
    public partial class Form24 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form24()
        {
            InitializeComponent();
        }
        private void showData()
        {
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT 求职者编号,求职者姓名,求职者性别,求职者身份证号码,求职者家庭住址,求职者学历层次,求职者聘用状态,求职者月薪要求,求职者会员等级 FROM 求职者");
                DataAdapter = new SqlDataAdapter(userInfo, conn);
                dataset = new DataSet();
                DataAdapter.Fill(dataset);
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = dataset.Tables[0].ToString();
                textBox1.DataBindings.Clear();
                textBox2.DataBindings.Clear();
                textBox3.DataBindings.Clear();
                textBox4.DataBindings.Clear();
                textBox5.DataBindings.Clear();
                textBox6.DataBindings.Clear();
                textBox1.DataBindings.Add("Text", dataset, "table.求职者编号");
                textBox2.DataBindings.Add("Text", dataset, "table.求职者姓名");
                textBox3.DataBindings.Add("Text", dataset, "table.求职者身份证号码");
                textBox4.DataBindings.Add("Text", dataset, "table.求职者家庭住址");
                textBox5.DataBindings.Add("Text", dataset, "table.求职者聘用状态");
                textBox6.DataBindings.Add("Text", dataset, "table.求职者月薪要求");
                if (dataset.Tables[0].Rows[0]["求职者性别"].ToString() == "男")
                {
                    radioButton1.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["求职者性别"].ToString() == "女")
                {
                    radioButton2.Checked = true;
                }
                if (dataset.Tables[0].Rows[0]["求职者学历层次"].ToString() == "初中及以下")
                {
                    radioButton3.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["求职者学历层次"].ToString() == "高中")
                {
                    radioButton4.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["求职者学历层次"].ToString() == "大专")
                {
                    radioButton5.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["求职者学历层次"].ToString() == "本科")
                {
                    radioButton6.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["求职者学历层次"].ToString() == "硕士")
                {
                    radioButton7.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["求职者学历层次"].ToString() == "博士")
                {
                    radioButton8.Checked = true;
                }
                if (dataset.Tables[0].Rows[0]["求职者会员等级"].ToString() == "1")
                {
                    radioButton9.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["求职者会员等级"].ToString() == "2")
                {
                    radioButton10.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["求职者会员等级"].ToString() == "3")
                {
                    radioButton11.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Form24_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            textBox1.ReadOnly = true;
            textBox5.ReadOnly = true;
            showData();
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSQL = "UPDATE 求职者 SET ";
            strSQL += "求职者姓名='" + textBox2.Text + "'";
            if (radioButton1.Checked)
            {
                strSQL += ",求职者性别='" + "男" + "'";
            }
            else if (radioButton2.Checked)
            {
                strSQL += ",求职者性别='" + "女" + "'";
            }
            strSQL += ",求职者身份证号码='" + textBox3.Text + "'";
            strSQL += ",求职者家庭住址='" + textBox4.Text + "'";
            if (radioButton3.Checked)
            {
                strSQL += ",求职者学历层次='" + "初中及以下" + "'";
            }
            else if (radioButton4.Checked == true)
            {
                strSQL += ",求职者学历层次='" + "高中" + "'";
            }
            else if (radioButton5.Checked == true)
            {
                strSQL += ",求职者学历层次='" + "大专" + "'";
            }
            else if (radioButton6.Checked == true)
            {
                strSQL += ",求职者学历层次='" + "本科" + "'";
            }
            else if (radioButton7.Checked == true)
            {
                strSQL += ",求职者学历层次='" + "硕士" + "'";
            }
            else if (radioButton8.Checked == true)
            {
                strSQL += ",求职者学历层次='" + "博士" + "'";
            }
            strSQL += ",求职者月薪要求=" + int.Parse(textBox6.Text);
            if(radioButton9.Checked)
            {
                strSQL += ",求职者会员等级=" + "1";
            }
            else if(radioButton10.Checked)
            {
                strSQL += ",求职者会员等级=" + "2";
            }
            else if(radioButton11.Checked)
            {
                strSQL += ",求职者会员等级=" + "3";
            }
            strSQL += " WHERE 求职者编号='" + textBox1.Text + "'";
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
                    MessageBox.Show("求职者数据更新成功!");
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
            curNo = this.dataGridView1.Rows[index].Cells[5].Value.ToString();
            if (curNo == "初中及以下")
            {
                radioButton3.Checked = true;
            }
            else if (curNo == "高中")
            {
                radioButton4.Checked = true;
            }
            else if (curNo == "大专")
            {
                radioButton5.Checked = true;
            }
            else if (curNo == "本科")
            {
                radioButton6.Checked = true;
            }
            else if (curNo == "硕士")
            {
                radioButton7.Checked = true;
            }
            else if (curNo == "博士")
            {
                radioButton8.Checked = true;
            }
            curNo = this.dataGridView1.Rows[index].Cells[8].Value.ToString();
            if (curNo == "1")
            {
                radioButton9.Checked = true;
            }
            else if (curNo == "2")
            {
                radioButton10.Checked = true;
            }
            else if (curNo == "3")
            {
                radioButton11.Checked = true;
            }
        }
    }
}
