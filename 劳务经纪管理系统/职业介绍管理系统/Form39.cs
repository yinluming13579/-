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
    public partial class Form39 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form39()
        {
            InitializeComponent();
        }
        private void showData()
        {
            string tname = "";
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT * FROM 缴费");
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
                textBox1.DataBindings.Add("Text", dataset, "table.缴费编号");
                textBox2.DataBindings.Add("Text", dataset, "table.介绍人员编号");
                textBox3.DataBindings.Add("Text", dataset, "table.单位编号");
                textBox4.DataBindings.Add("Text", dataset, "table.求职者编号");
                textBox5.DataBindings.Add("Text", dataset, "table.招聘编号");
                textBox6.DataBindings.Add("Text", dataset, "table.单位缴费");
                textBox7.DataBindings.Add("Text", dataset, "table.求职者缴费");
                textBox8.DataBindings.Add("Text", dataset, "table.求职者薪资");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Form39_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            showData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSQL = "UPDATE 缴费 SET ";
            strSQL += "单位缴费=" + textBox6.Text + ",";
            strSQL += "求职者缴费=" + textBox7.Text + ",";
            strSQL += "求职者薪资=" + textBox8.Text;
            strSQL += " WHERE 缴费编号='" + textBox1.Text + "'";
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
