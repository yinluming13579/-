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
    public partial class Form44 : Form
    {
        public Form44()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
                String userInfo = String.Format("SELECT 求职者简历编号,简历.求职者姓名,简历.求职者性别,求职者年龄,求职者民族,求职者籍贯,求职者政治面貌,求职者毕业院校,求职者学习经历,求职者工作经历,求职者获得荣誉,求职者自我评价 FROM 求职者,简历 where 求职者.求职者编号=简历.求职者简历编号 and 求职者.求职者聘用状态='未聘用' and 求职者会员等级=(select 介绍人员权限 from 介绍人员 where 介绍人员编号='{0}')", Form1.user_name);
                cmd.CommandText = userInfo;
                DataAdapter.SelectCommand = cmd;
                DataAdapter.Fill(dataset, "t2");
                    textBox1.Text = dataset.Tables["t2"].Rows[0]["求职者简历编号"].ToString();
                    textBox2.Text = dataset.Tables["t2"].Rows[0]["求职者姓名"].ToString();
                    textBox3.Text = dataset.Tables["t2"].Rows[0]["求职者性别"].ToString();
                    textBox4.Text = dataset.Tables["t2"].Rows[0]["求职者年龄"].ToString();
                    textBox5.Text = dataset.Tables["t2"].Rows[0]["求职者民族"].ToString();
                    textBox6.Text = dataset.Tables["t2"].Rows[0]["求职者籍贯"].ToString();
                    textBox7.Text = dataset.Tables["t2"].Rows[0]["求职者政治面貌"].ToString();
                    textBox8.Text = dataset.Tables["t2"].Rows[0]["求职者毕业院校"].ToString();
                    textBox9.Text = dataset.Tables["t2"].Rows[0]["求职者学习经历"].ToString();
                    textBox10.Text = dataset.Tables["t2"].Rows[0]["求职者工作经历"].ToString();
                    textBox11.Text = dataset.Tables["t2"].Rows[0]["求职者获得荣誉"].ToString();
                    textBox12.Text = dataset.Tables["t2"].Rows[0]["求职者自我评价"].ToString();
                dataGridView1.DataSource = dataset;
                dataGridView1.DataMember = "t2";
            }
            catch (Exception ex)
            {
                MessageBox.Show("暂无求职者简历信息");
            }
            finally
            {
                if (conn != null) conn.Dispose();
                if (dataset != null) dataset.Dispose();
                if (DataAdapter != null) DataAdapter.Dispose();
            }
        }

        private void Form44_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;
            textBox9.ReadOnly = true;
            textBox10.ReadOnly = true;
            textBox11.ReadOnly = true;
            textBox12.ReadOnly = true;
        }
    }
}
