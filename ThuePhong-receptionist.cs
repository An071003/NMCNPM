﻿using AppQuanLyKhachSan.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrangChuQLKS;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AppQuanLyKhachSan
{
    public partial class ThuePhong_receptionist : Form
    {
        public ThuePhong_receptionist()
        {
            InitializeComponent();
            ThuePhong_Shown();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Phong_click(object sender, EventArgs e)
        {
            this.Hide();
            QLPhong_receptionist qLPhong = new QLPhong_receptionist();
            qLPhong.ShowDialog();
            this.Close();
        }
        private void Dichvu_click(object sender, EventArgs e)
        {
            this.Hide();
            QLDichVu_receptionist qtdv = new QLDichVu_receptionist();
            qtdv.ShowDialog();
            this.Close();
        }

        public void Dangxuat_click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Hoadon_click(object sender, EventArgs e)
        {
            this.Hide();
            QLHoaDon_receptionist qLHoaDon = new QLHoaDon_receptionist();
            qLHoaDon.ShowDialog();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string TENKHACHHANG = textBox2.Text;
            string MAPHONG = textBox3.Text;
            string query = "SELECT * FROM QL_PHIEUTHUEPHONG WHERE TENKHACHHANG LIKE @TENKHACHHANG AND MAPHONG LIKE @MAPHONG";
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TENKHACHHANG", "%" + TENKHACHHANG + "%");
                cmd.Parameters.AddWithValue("@MAPHONG", "%" + MAPHONG + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                conn.Close();
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = dataTable;
        }

        private void DataFilter(object sender, EventArgs e)
        {
            string TENKHACHHANG = textBox2.Text;
            string MAPHONG = textBox3.Text;
            string query = "SELECT * FROM QL_PHIEUTHUEPHONG WHERE TENKHACHHANG LIKE @TENKHACHHANG AND MAPHONG LIKE @MAPHONG";
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TENKHACHHANG", "%" + TENKHACHHANG + "%");
                cmd.Parameters.AddWithValue("@MAPHONG", "%" + MAPHONG + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
                conn.Close();
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = dataTable;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            LapPTP_receptionist lapPTP = new LapPTP_receptionist();
            lapPTP.ShowDialog();
            this.Show();
            ThuePhong_Shown();
        }

        private void ThuePhong_Shown()
        {
            Database database = new Database();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = database.ThuePhong_Search();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (row >= 0 && row < dataGridView1.Rows.Count)
            {
                if (dataGridView1.Rows[row].Cells.Count > 5)
                {
                    string c1 = dataGridView1.Rows[row].Cells[1].Value?.ToString() ?? string.Empty;
                    string c2 = dataGridView1.Rows[row].Cells[7].Value?.ToString() ?? string.Empty;
                    textBox2.Text = c1;
                    textBox3.Text = c2;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangKyDichVu_receptionist dkdv = new DangKyDichVu_receptionist();
            dkdv.ShowDialog();
            this.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string TENKHACHHANG = textBox2.Text;
            string MAPHONG = textBox3.Text;
            string MAPTP = "";
            string KHACHHANGTHU = "";
            string DIACHI = "";
            string CMND = "";
            string MALKH = "";
            DateTime dateTime = DateTime.MinValue;

            if(TENKHACHHANG == "" || MAPHONG == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo");
                return;
            }


            using (SqlConnection conn = new SqlConnection(Database.connection))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM QL_PHIEUTHUEPHONG WHERE TENKHACHHANG = @TENKHACHHANG AND MAPHONG = @MAPHONG";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TENKHACHHANG", TENKHACHHANG);
                    cmd.Parameters.AddWithValue("@MAPHONG", MAPHONG);

                    int count = (int)cmd.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Không tìm thấy khách hàng và phòng này", "Thông báo");
                        return;
                    }
                }
                conn.Close();
            }
            using (SqlConnection conn = new SqlConnection(Database.connection))
            {
                conn.Open();
                string query = "SELECT CHITIETPTP.MAPTP, CHITIETPTP.KHACHHANGTHU, QL_PHIEUTHUEPHONG.NGAYLAPPHIEU, " +
                      "QL_PHIEUTHUEPHONG.DIACHI, QL_PHIEUTHUEPHONG.CMND, CHITIETPTP.MALKH " +
                      "FROM QL_PHIEUTHUEPHONG " +
                      "JOIN CHITIETPTP ON QL_PHIEUTHUEPHONG.MAPTP = CHITIETPTP.MAPTP " +
                      "WHERE QL_PHIEUTHUEPHONG.TENKHACHHANG = @TENKHACHHANG AND QL_PHIEUTHUEPHONG.MAPHONG = @MAPHONG";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TENKHACHHANG", TENKHACHHANG);
                cmd.Parameters.AddWithValue("@MAPHONG", MAPHONG);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MAPTP = reader["MAPTP"].ToString();
                        KHACHHANGTHU = reader["KHACHHANGTHU"].ToString();
                        dateTime = Convert.ToDateTime(reader["NGAYLAPPHIEU"].ToString());
                        DIACHI = reader["DIACHI"].ToString();
                        CMND = reader["CMND"].ToString();
                        MALKH = reader["MALKH"].ToString();
                    }
                }
                conn.Close();
            }
            this.Hide();
            CNCTPTP_receptionist cNCTPTP = new CNCTPTP_receptionist(MAPTP, MAPHONG, dateTime, KHACHHANGTHU, TENKHACHHANG, DIACHI, CMND, MALKH);
            cNCTPTP.ShowDialog();
            this.Show();
            ThuePhong_Shown();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
