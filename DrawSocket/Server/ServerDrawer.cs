using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public class ServerDrawer
    {
        public void Listen(Panel p)
        {
            Socket svSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 11000);
            svSocket.Bind(iep);
            svSocket.Listen(100);
            while (true)
            {
                Socket client = svSocket.Accept();
                if (client != null)
                {
                    String data = "";
                    byte[] boDem = new byte[1024];
                    int demNhan = client.Receive(boDem);
                    data += Encoding.UTF8.GetString(boDem).Trim().Substring(0, demNhan);
                    while (!data.Contains("<EOF>"))
                    {
                        boDem = new byte[1024];
                        demNhan = client.Receive(boDem);
                        data += Encoding.UTF8.GetString(boDem).Trim().Substring(0, demNhan);
                    }
                    data = data.Replace("\0", "");
                    data = data.Substring(0, data.Length - 5); // Loai bo <EOF>
                    // Data la hai toa do x va y cach nhau boi 1 khoang trang
                    int x = int.Parse(data.Split(' ')[0]);
                    int y = int.Parse(data.Split(' ')[1]);

                    // Ve 1 diem tai toa do x, y tren Panel p
                    Graphics g = p.CreateGraphics();
                    g.DrawRectangle(Pens.DarkRed, new Rectangle(x, y, 1, 1));

                    client.Disconnect(false);
                    client.Dispose();
                }
            }
        }
    }
}
