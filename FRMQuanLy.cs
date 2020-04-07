using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyKhachSan2
{
    public partial class FRMQuanLy : Form
    {
        SqlConnection con = new SqlConnection();
        public FRMQuanLy()
        {
            InitializeComponent();
        }

        private void FRMQuanLy_Load(object sender, EventArgs e)
        {
            string connectionstring = "Data Source=localhost\\SQLEXPRESS101;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
            con.ConnectionString = connectionstring;
            con.Open();
            string sql = "select * from XQuanLyKhachSan";
            SqlDataAdapter adp = new SqlDataAdapter(sql,con);
            DataTable tableXQuanLyKhachSan = new DataTable();
            adp.Fill(tableXXKhachSan);
            DataGridView1.DataSource = tableXQuanLyKhachSan;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhong.Text = DataGridView1.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTenPhong.Text = DataGridView1.CurrentRow.Cells["TenPhong"].Value.ToString();
            txtDonGia.Text = DataGridView1.CurrentRow.Cells["DonGia"].Value.ToString();
            txtMaPhong.Enabled = false;
        }
        private void loaddatagridview()
        {
            string sql = "select * from XQuanLyKhachSan";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tableXQuanLyKhachSan = new DataTable();
            adp.Fill(tableXQuanLyKhachSan);
            DataGridView1.DataSource = tableXQuanLyKhachSan;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            txtMaPhong.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "delete from XQuanLyKhachSan where maphong = '" + txtMaPhong.Text + "'";
            SqlCommand cmd = new SqlCommand(sql,con);
            cmd.ExecuteNonQuery();
            loaddatagridview();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("chua nhap ma phong");
                txtMaPhong.Focus();
                return;
            }
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show("chua nhap ten phong");
                txtTenPhong.Focus();
                return;
            }
             else 
            {
                string sql = "insert into XQuanLyKhachSan values ('" + txtMaPhong.Text + "', '" +
                    txtTenPhong.Text + "'";
                if (txtDonGia.Text != "")
                {
                    sql = sql + "," + txtDonGia.Text.Trim();
                }
                sql = sql + ")";
               
                try
                {
                    MessageBox.Show(sql);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    loaddatagridview();
                }catch(Exception d)
                {
                    MessageBox.Show(d.ToString());
                }
                finally
                {
                    
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show("chua nhap ten phong");
                txtTenPhong.Focus();
                return;
            }
            if (txtDonGia.Text == "")
            {
                MessageBox.Show("chua nhap don gia");
                txtDonGia.Focus();
                return;
            }
            else
            {
                
                string sql = "update XQuanLyKhachSan set TenPhong=@TenPhong, DonGia=@DonGia where MaPhong =@MaPhong";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("MaPhong", txtMaPhong.Text);
                cmd.Parameters.AddWithValue("tenphong", txtTenPhong.Text);
                cmd.Parameters.AddWithValue("DonGia", txtDonGia.Text);
                cmd.ExecuteNonQuery();
                loaddatagridview();
            }
        }

        private void txtdongia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
       
       
    }
}