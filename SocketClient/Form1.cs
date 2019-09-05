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
            MySocket ms = new MySocket(ip.ToString(), cong, noiDung);
            Boolean kq = ms.Gui();
            if(kq)
            {
                tbGuiNhan.Text += "Bạn: " + noiDung + "\r\n";
                tbGuiNhan.Text += "Server: " + ms.KetQua + "\r\n";
                tbNoiDung.Text = "";
                tbNoiDung.Focus();
            }
            else
            {
                MessageBox.Show("Gửi thất bại, bạn hãy kiểm tra lại đường truyền");                
                return;
            }
        }
    }
}
