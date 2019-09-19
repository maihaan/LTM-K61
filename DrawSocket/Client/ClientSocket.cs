using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class ClientSocket
    {
        public void Send(int x, int y)
        {
            byte[] boDem = Encoding.UTF8.GetBytes(x.ToString() + " " + y.ToString() + "<EOF>");
            Socket sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            try
            {
                sk.Connect(ip, 11000);
                if (sk.Connected)
                {
                    sk.Send(boDem);
                    sk.Disconnect(false);
                    sk.Dispose();
                }
            }
            catch { }
        }
    }
}
