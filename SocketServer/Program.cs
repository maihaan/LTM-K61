using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
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
                    // Doc du lieu
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

                    char[] spliter = new char[] { '<', '$', '>' };
                    String nguoiGuiID = data.Split(spliter)[0];
                    String nguoiNhanID = data.Split(spliter)[1];
                    String noiDung = data.Split(spliter)[2];

                    DataAccess da = new DataAccess();
                    String query = "INSERT INTO tbTinNhan(NguoiGuiID, NguoiNhanID, NoiDung) Values("
                        + nguoiGuiID + ","
                        + nguoiNhanID + ",N'"
                        + noiDung + "')";

                    da.Write(query);
                    byte[] traLoi = Encoding.UTF8.GetBytes(data.Length.ToString());
                    client.Send(traLoi);
                    client.Disconnect(false);
                    client.Dispose();
                }
            }
        }
    }
}
