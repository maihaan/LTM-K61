using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Data;

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

                    String[] spliter = new String[] { "<$>" };
                    String nguoiGuiID = data.Split(spliter, StringSplitOptions.RemoveEmptyEntries)[0];
                    String nguoiNhanID = data.Split(spliter, StringSplitOptions.RemoveEmptyEntries)[1];
                    String noiDung = data.Split(spliter, StringSplitOptions.RemoveEmptyEntries)[2];

                    DataAccess da = new DataAccess();
                    

                    // Phan loai tin nhan
                    if(nguoiGuiID.Equals(nguoiNhanID) && noiDung.Equals("<?>"))
                    {
                        // Client hoi Server xem co tin nhan gi gui cho minh khong -> Server khong luu tin nhan
                        // Server phai tim trong CSDL cac tin nhan duoc gui den cho Client
                        String queryHoi = "SELECT * FROM tbTinNhan WHERE NguoiNhanID=" + nguoiNhanID
                            + " AND DaDoc=0";
                        DataTable tb = da.Read(queryHoi);
                        if(tb != null && tb.Rows.Count > 0)
                        {
                            // Co tin nhan
                            int demTN = 0;
                            foreach(DataRow r in tb.Rows)
                            {
                                demTN++;
                                // Gui tin nhan den cho Client
                                String tinNhan = r["NguoiGuiID"].ToString() + "<$>"
                                    + r["NoiDung"].ToString();
                                if (demTN < tb.Rows.Count)
                                    tinNhan += "<EOE>";
                                else
                                    tinNhan += "<EOF>";
                                byte[] bufferTinNhan = Encoding.UTF8.GetBytes(tinNhan);
                                client.Send(bufferTinNhan);

                                // Cap nhat trang thai cua tin nhan la da doc
                                String queryCapNhat = "UPDATE tbTinNhan SET DaDoc=1 Where ID=" + r["ID"].ToString();
                                da.Write(queryCapNhat);
                            }
                        }
                        else
                        {
                            // Khong co tin nhan
                            String tinNhan = "0<$>0<EOF>";
                            byte[] bufferTinNhan = Encoding.UTF8.GetBytes(tinNhan);
                            client.Send(bufferTinNhan);
                        }
                    }
                    else
                    {
                        // Client gui den cho mot Client khac, Server phai luu tin nhan
                        String query = "INSERT INTO tbTinNhan(NguoiGuiID, NguoiNhanID, NoiDung, DaDoc) Values("
                            + nguoiGuiID + ","
                            + nguoiNhanID + ",N'"
                            + noiDung + "', 0)";
                        da.Write(query);
                        String tinNhan = "0<$>1<EOF>";
                        byte[] traLoi = Encoding.UTF8.GetBytes(tinNhan);
                        client.Send(traLoi);
                    }

                    client.Disconnect(false);
                    client.Dispose();
                }
            }
        }
    }
}
