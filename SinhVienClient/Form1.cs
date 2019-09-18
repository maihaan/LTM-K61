using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SinhVienClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Doc du lieu o dong duoc chon va hien thi vao cac Controls
            tbTen.Text = dgvDanhSach.SelectedRows[0].Cells["Ten"].Value.ToString();
            tbEmail.Text = dgvDanhSach.SelectedRows[0].Cells["Email"].Value.ToString();
            tbDiemTB.Text = dgvDanhSach.SelectedRows[0].Cells["DiemTB"].Value.ToString();
            dpNgaySinh.Value = DateTime.Parse(dgvDanhSach.SelectedRows[0].Cells["NgaySinh"].Value.ToString());
        }

        private void btThem_Click(object sender, EventArgs e)
        {

        }

        private void btLuu_Click(object sender, EventArgs e)
        {

        }

        private void btXoa_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DocDL();
            btLuu.Enabled = false;
            btXoa.Enabled = false;
        }

        private void DocDL()
        {
            SQLSocket sk = new SQLSocket();
            DataTable tb = sk.SelectAll();
            dgvDanhSach.DataSource = tb;
            dgvDanhSach.Refresh();
        }

        private void LamMoi()
        {
            tbTen.Text = "";
            tbDiemTB.Text = "";
            tbEmail.Text = "";
        }
    }
}
