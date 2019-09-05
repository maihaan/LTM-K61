using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace SocketClient
{
    public class MySocket
    {
        // Thuoc tinh
        public String IP { get; set; }
        public int Cong { get; set; }
        public String NoiDung { get; set; }
        public String KetQua { get; set; }

        public int NguoiGuiID { get; set; }
        public int NguoiNhanID { get; set; }
        


        // Phuong thuc
        public MySocket(String ip, int cong, String noiDung, int nguoiGuiID, int nguoiNhanID)
        {
            IP = ip;
            Cong = cong;
            NoiDung = noiDung;
            NguoiGuiID = nguoiGuiID;
            NguoiNhanID = nguoiNhanID;
        }

        public Boolean Gui()
        {
            // Chuyen doi du lieu can gui thanh mang byte
            // Cu phap gui du lieu nhu sau: 
            // NguoiGuiID <$> NguoiNhanID <$> NoiDung <EOF>
            String duLieuGui = NguoiGuiID.ToString() + "<$>" 
                + NguoiNhanID.ToString() + "<$>" + NoiDung + "<EOF>";

            byte[] boDem = Encoding.UTF8.GetBytes(duLieuGui);


            // Gui du lieu
            Socket sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(IP);
            try
            {
                sk.Connect(ip, Cong);
                if(sk.Connected)
                {
                    sk.Send(boDem);
                    // Nhan du lieu tra loi
                    byte[] traLoi = new byte[1024];
                    int dem = sk.Receive(traLoi);
                    KetQua = Encoding.UTF8.GetString(traLoi, 0, dem);
                    while(!KetQua.Contains("<EOF>"))
                    {
                        traLoi = new byte[1024];
                        dem = sk.Receive(traLoi);
                        KetQua = Encoding.UTF8.GetString(traLoi, 0, dem);
                    }
                    // Cu phap ma Server gui tra cho Client: NguoiGuiID <$> NoiDung <EOF>
                    KetQua = KetQua.Replace("\0", "");
                    KetQua = KetQua.Substring(KetQua.Length - 5);
                    //char[] spliter = new char[] { '<', '$', '>' };
                    //String nguoiGui = KetQua.Split(spliter)[0];
                    //String noiDung = KetQua.Split(spliter)[1];

                    sk.Disconnect(false);
                    sk.Dispose();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }
    }
}
