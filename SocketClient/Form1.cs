using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;

namespace SocketClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btGui_Click(object sender, EventArgs e)
        {
            // Kiem tra cac dau vao
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            if(!IPAddress.TryParse(tbIP.Text, out ip))
            {
                MessageBox.Show("Bạn phải nhập địa chỉ IP hợp lệ");
                tbIP.Focus();
                return;
            }

            int cong = 0;
            if(!int.TryParse(tbCong.Text, out cong))
            {
                MessageBox.Show("Bạn phải nhập một cổng hợp lệ");
                tbCong.Focus();
                return;
            }

            String noiDung = tbNoiDung.Text.Trim();
            if(noiDung.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập nội dung tin nhắn");
                tbNoiDung.Focus();
                return;
            }

            // Gui
            int nguoiGuiID = int.Parse(tbNguoiGuiID.Text);
            int nguoiNhanID = int.Parse(tbNguoiNhanID.Text);

            MySocket ms = new MySocket(ip.ToString(), cong, noiDung, nguoiGuiID, nguoiNhanID);
            Boolean kq = ms.Gui();
            if(ms.NoiDung.EndsWith("(*)"))
            {
                tbGuiNhan.Text += "Bạn: " + noiDung + "\r\n";
                tbGuiNhan.Text += "Server: Đã nhận\r\n";
                tbNoiDung.Text = "";
                tbNoiDung.Focus();
            }
            else
            {
                MessageBox.Show("Gửi thất bại, bạn hãy kiểm tra lại đường truyền");                
                return;
            }
        }

        private void tmAuto_Tick(object sender, EventArgs e)
        {
            // Hoi Server
            if (tbNguoiGuiID.Text.Length == 0)
                return;
            try
            {
                int nguoiGuiID = int.Parse(tbNguoiGuiID.Text);
                MySocket ms = new MySocket(tbIP.Text, int.Parse(tbCong.Text), "<?>", nguoiGuiID, nguoiGuiID);
                ms.Gui();
                // Xu ly ket qua Server tra ve
                if (ms.KetQua != null && ms.KetQua.Count > 0)
                {
                    foreach (var tinNhan in ms.KetQua)
                    {
                        tbGuiNhan.Text += tinNhan.Key.ToString() + ": "
                            + tinNhan.Value.ToString() + "\r\n";
                    }
                }
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tmAuto.Start();
        }
    }
}
