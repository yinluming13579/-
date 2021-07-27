﻿using System;
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
    public partial class Form12 : Form
    {
        string ConnectionString = "Data Source=LAPTOP-UO6IFABB;Initial Catalog=职业介绍管理系统;Integrated Security=True";
        private SqlConnection conn = null;
        private SqlDataAdapter DataAdapter = null;
        private DataSet dataset = null;
        public Form12()
        {
            InitializeComponent();
        }
        private void showData()
        {
            string tname = "";
            try
            {
                if (conn == null) conn.Open();
                String userInfo = String.Format("SELECT * FROM 招聘信息 where 单位编号='{0}'", Form1.user_name);
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
                textBox1.DataBindings.Add("Text", dataset, "table.招聘编号");
                textBox2.DataBindings.Add("Text", dataset, "table.单位编号");
                textBox3.DataBindings.Add("Text", dataset, "table.职业类型号");
                textBox4.DataBindings.Add("Text", dataset, "table.岗位简介");
                textBox5.DataBindings.Add("Text", dataset, "table.岗位要求");
                textBox6.DataBindings.Add("Text", dataset, "table.已聘人数");
                textBox7.DataBindings.Add("Text", dataset, "table.需聘人数");
                textBox8.DataBindings.Add("Text", dataset, "table.工资");
                if(dataset.Tables[0].Rows[0]["学历要求"].ToString()=="初中及以下")
                {
                    radioButton1.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["学历要求"].ToString() == "高中")
                {
                    radioButton2.Checked = true;
                }
                else if(dataset.Tables[0].Rows[0]["学历要求"].ToString() == "大专")
                {
                    radioButton3.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["学历要求"].ToString() == "本科")
                {
                    radioButton4.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["学历要求"].ToString() == "硕士")
                {
                    radioButton5.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["学历要求"].ToString() == "博士")
                {
                    radioButton6.Checked = true;
                }
                if (dataset.Tables[0].Rows[0]["招聘信息权限"].ToString() == "1")
                {
                    radioButton7.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["招聘信息权限"].ToString() == "2")
                {
                    radioButton8.Checked = true;
                }
                else if (dataset.Tables[0].Rows[0]["招聘信息权限"].ToString() == "3")
                {
                    radioButton9.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string strSQL = "UPDATE 招聘信息 SET ";
            strSQL += " 职业类型号='" + textBox3.Text+"',";
            strSQL += " 岗位简介='" + textBox4.Text + "',";
            if(radioButton1.Checked)
            {
                strSQL += " 学历要求='" + "初中及以下" + "',";
            }
            else if(radioButton2.Checked)
            {
                strSQL += " 学历要求='" + "高中" + "',";
            }
            else if (radioButton3.Checked)
            {
                strSQL += " 学历要求='" + "大专" + "',";
            }
            else if (radioButton4.Checked)
            {
                strSQL += " 学历要求='" + "本科" + "',";
            }
            else if (radioButton5.Checked)
            {
                strSQL += " 学历要求='" + "硕士" + "',";
            }
            else if (radioButton6.Checked)
            {
                strSQL += " 学历要求='" + "博士" + "',";
            }
            strSQL += " 岗位要求='" + textBox5.Text + "',";
            strSQL += " 需聘人数=" + textBox7.Text+ ",";
            strSQL += " 工资=" + textBox8.Text + ",";
            if (radioButton7.Checked)
            {
                strSQL += " 招聘信息权限=" + "1";
            }
            else if (radioButton8.Checked)
            {
                strSQL += " 招聘信息权限=" + "2";
            }
            else if (radioButton9.Checked)
            {
                strSQL += " 招聘信息权限=" + "3";
            }
            strSQL += " WHERE 招聘编号='" + textBox1.Text + "'";
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
                    MessageBox.Show("招聘信息更新成功!");
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

        private void Form12_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConnectionString);
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox6.ReadOnly = true;
            showData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 1) return;
            int index = dataGridView1.CurrentRow.Index;
            string curNo = this.dataGridView1.Rows[index].Cells[4].Value.ToString();
            if (curNo == "初中及以下")
            {
                radioButton1.Checked = true;
            }
            else if (curNo == "高中")
            {
                radioButton2.Checked = true;
            }
            else if (curNo == "大专")
            {
                radioButton3.Checked = true;
            }
            else if (curNo == "本科")
            {
                radioButton4.Checked = true;
            }
            else if (curNo == "硕士")
            {
                radioButton5.Checked = true;
            }
            else if (curNo == "博士")
            {
                radioButton6.Checked = true;
            }
            curNo = this.dataGridView1.Rows[index].Cells[9].Value.ToString();
            if (curNo == "1")
            {
                radioButton7.Checked = true;
            }
            else if (curNo == "2")
            {
                radioButton8.Checked = true;
            }
            else if (curNo == "3")
            {
                radioButton9.Checked = true;
            }
        }
    }
}
