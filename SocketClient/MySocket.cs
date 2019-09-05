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
        public Dictionary<int, String> KetQua { get; set; }

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
                    String tKetQua = Encoding.UTF8.GetString(traLoi, 0, dem);
                    while(!tKetQua.Contains("<EOF>"))
                    {
                        traLoi = new byte[1024];
                        dem = sk.Receive(traLoi);
                        tKetQua += Encoding.UTF8.GetString(traLoi, 0, dem);
                    }
                    // Cu phap ma Server gui tra cho Client: 
                    // NguoiGuiID <$> NoiDung <EOE> NguoiGuiID <$> NoiDung <EOE> ... <EOF>
                    tKetQua = tKetQua.Replace("\0", "");
                    tKetQua = tKetQua.Substring(0, tKetQua.Length - 5);
                    if(tKetQua.Contains("<EOE>"))
                    {
                        // Co nhieu tin nhan
                        String[] spliterTinNhan = new String[] { "<EOE>" };
                        foreach (String tinNhan in tKetQua.Split(spliterTinNhan, StringSplitOptions.None))
                        {
                            String[] spliter = new String[] { "<$>" };
                            String nguoiGui = tinNhan.Split(spliter, StringSplitOptions.RemoveEmptyEntries)[0];
                            String noiDung = tinNhan.Split(spliter, StringSplitOptions.RemoveEmptyEntries)[1];
                            KetQua.Add(int.Parse(nguoiGui), noiDung);
                        }
                    }
                    else
                    {
                        // Co 1 tin nhan: Roi vao 3 truong hop
                        // 0<$>0<EOF>: Khong co gi dau ma hoi
                        // 0<$>1<EOF>: Toi da nhan duoc tin nhan va se chuyen cho nguoi nhan khi co the
                        // NguoiGuiID <$> NoiDung <EOF>: ban co 1 tin nhan
                        String[] spliter = new String[] { "<$>" };
                        String nguoiGui = tKetQua.Split(spliter, StringSplitOptions.RemoveEmptyEntries)[0];
                        String noiDung = tKetQua.Split(spliter, StringSplitOptions.RemoveEmptyEntries)[1];
                        if (nguoiGui.Equals("0"))
                        {
                            if (noiDung.Equals("1"))
                                NoiDung += "(*)"; // Tin nhan da duoc Server xu ly
                        }
                        else
                        {
                            KetQua.Add(int.Parse(nguoiGui), noiDung);
                        }
                    }


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
