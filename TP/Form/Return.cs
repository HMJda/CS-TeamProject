﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TP
{
    public partial class Return : Form
    {
        private string DB_Server_Info = "Data Source = localhost;" +
           "User ID = system; Password = 1;";
        private string categori = "음료";

        public Return()
        {
            InitializeComponent();
            dataview();
        }
        private void dataview()
        {
            try
            {
                string sqltxt = "select * from 제품";
                OracleConnection conn = new OracleConnection(DB_Server_Info);
                conn.Open();
                OracleDataAdapter adapt = new OracleDataAdapter();
                adapt.SelectCommand = new OracleCommand(sqltxt, conn);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                adapt.Fill(ds);
                dt.Reset();
                dt = ds.Tables[0];
                dataGridView1.Columns.Clear();

                dt.DefaultView.RowFilter = $"카테고리 ='{categori}'";
                dataGridView1.AllowUserToAddRows = false; //빈레코드 표시x
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "chk",
                    HeaderText = "선택"
                };
                dataGridView1.Columns.Add(chkCol);
                dataGridView1.DataSource = dt;   //데이터 추가 부분
                dataGridView1.Columns.Add("발주량", "발주량");
                dataGridView1.Columns.Add("비고", "비고");

                //크기 조절부분 
                dataGridView1.Columns[0].Width = 35;


                //dataGridView1.ReadOnly = true; //전부 읽기 전용           
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;
                dataGridView1.Columns[6].ReadOnly = true;
                dataGridView1.Columns[7].ReadOnly = true;

                conn.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //검색부분
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["chk"].Value)) //체크된 데이터 선택 부분
                {
                    try
                    {
                        string sqltxt = "insert into 주문 + values";
                        OracleConnection conn = new OracleConnection(DB_Server_Info);
                        conn.Open();

                        OracleCommand oc = new OracleCommand();
                        oc.CommandText = sqltxt + "('" + "'" + dataGridView1.Rows[i].Cells[2] + ")"; //발주 번호 //주문고객// 발주제품//수량//배송지 // 주문일자// 
                    }
                    catch (OracleException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }

            }

            //save 부분
        }

        private void Order_FormClosed(object sender, FormClosedEventArgs e)
        {
            //닫혔을때 save 하는지 물어보는 부분 
            MessageBox.Show("저장하시겠습니까?"); //예,아니요,취소 부분 되게 
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                categori = radioButton1.Text;
                dataview();

            }
            else if (radioButton2.Checked == true)
            {
                categori = radioButton2.Text;
                dataview();
            }
            else
            {
                categori = radioButton3.Text;
                dataview();
            }
        }
    }
}