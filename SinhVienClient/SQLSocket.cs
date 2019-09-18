using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data;

namespace SinhVienClient
{
    public class SQLSocket
    {
        String IP = "127.0.0.1";
        int Port = 11000;

        public byte[] Gui(String query)
        {
            Socket sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(IP);
            try
            {
                sk.Connect(ip, Port);
                if (sk.Connected)
                {
                    sk.Send(Encoding.UTF8.GetBytes(query + "<EOF>"));
                    List<byte> dlNhan = new List<byte>();
                    byte[] boDem = new byte[1024];
                    int dem = sk.Receive(boDem);
                    for (int i = 0; i < dem; i++)
                        dlNhan.Add(boDem[i]);
                    while (dem > 0)
                    {
                        boDem = new byte[1024];
                        dem = sk.Receive(boDem);
                        if (dem > 0)
                            for (int i = 0; i < dem; i++)
                                dlNhan.Add(boDem[i]);
                        else
                            break;
                    }
                    sk.Disconnect(false);
                    sk.Close();
                    sk.Dispose();
                    return dlNhan.ToArray();
                }
                else
                    return null;
            }
            catch
            {
                if (sk.Connected)
                    sk.Disconnect(false);
                sk.Close();
                sk.Dispose();
                return null;
            }
        }

        public DataTable SelectAll()
        {
            String query = "1*";
            byte[] dlNhan = Gui(query);
            if (dlNhan == null || dlNhan.Length == 0)
                return null;
            else
            {
                System.Runtime.Serialization.IFormatter fm = 
                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.MemoryStream ms = new System.IO.MemoryStream(dlNhan);
                DataTable tb = (DataTable)fm.Deserialize(ms);
                return tb;
            }
        }

        public int Insert(String ten, String email, float diem, DateTime ngaySinh)
        {
            String query = "2*" + ten + ";" + email + ";" + diem.ToString() + ";" + ngaySinh.ToString("MM/dd/yyyy");
            byte[] dlNhan = Gui(query);
            if (dlNhan == null)
                return -1;
            else
            {
                String value = Encoding.UTF8.GetString(dlNhan);
                return int.Parse(value);
            }
        }

        public int Update(int id, String ten, String email, float diem, DateTime ngaySinh)
        {
            String query = "3*" + id.ToString() + ";" + ten + ";" + email 
                + ";" + diem.ToString() + ";" + ngaySinh.ToString("MM/dd/yyyy");
            byte[] dlNhan = Gui(query);
            if (dlNhan == null)
                return -1;
            else
            {
                String value = Encoding.UTF8.GetString(dlNhan);
                return int.Parse(value);
            }
        }

        public int Delete(int id)
        {
            String query = "4*" + id.ToString();
            byte[] dlNhan = Gui(query);
            if (dlNhan == null)
                return -1;
            else
            {
                String value = Encoding.UTF8.GetString(dlNhan);
                return int.Parse(value);
            }
        }
    }
}
