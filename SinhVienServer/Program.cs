using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.IO;

namespace SinhVienServer
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
                    // Neu data bat dau bang 1: Select 1*[dieuKien]
                    // Neu data bat dau bang 2: Insert 2*Nguyen Manh Duc;ducnt@gmail.com;8.4;27/8/1981
                    // Neu data bat dau bang 3: Update 3*3;Nguyen Manh Duc; ducabc@gmail.com;9.0;27/8/1981
                    // Neu data bat dau bang 4: Delete 4*3  
                    // Cu phap lenh cua Client: [number]*[DuLieu]
                    String loaiQuery = data.Split('*')[0];
                    String duLieu = data.Split('*')[1];
                    if(loaiQuery.Equals("1"))
                    {
                        DataTable tb;
                        BSinhVien bsv = new BSinhVien();
                        if (String.IsNullOrEmpty(duLieu))
                        {
                            // Select All                            
                            tb = bsv.SelectAllToTable();                            
                        }
                        else
                        {
                            // Select theo dieu kien
                            tb = bsv.SelectToTable(duLieu);
                        }
                        MemoryStream ms = new MemoryStream();
                        System.Runtime.Serialization.IFormatter fm =
                            new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        fm.Serialize(ms, tb);
                        byte[] bData = ms.GetBuffer();
                        client.Send(bData);
                    }
                    else if(loaiQuery.Equals("2"))
                    {
                        String ten = duLieu.Split(';')[0];
                        String email = duLieu.Split(';')[1];
                        float diemTB = float.Parse(duLieu.Split(';')[2]);
                        DateTime ngaySinh = DateTime.Parse(duLieu.Split(';')[3]);
                        BSinhVien bsv = new BSinhVien();
                        int dem = bsv.Insert(ten, email, diemTB, ngaySinh);
                        client.Send(Encoding.UTF8.GetBytes(dem.ToString()));
                    }
                    else if (loaiQuery.Equals("3"))
                    {
                        int id = int.Parse(duLieu.Split(';')[0]);
                        String ten = duLieu.Split(';')[1];
                        String email = duLieu.Split(';')[2];
                        float diemTB = float.Parse(duLieu.Split(';')[3]);
                        DateTime ngaySinh = DateTime.Parse(duLieu.Split(';')[4]);
                        BSinhVien bsv = new BSinhVien();
                        int dem = bsv.Update(id, ten, email, diemTB, ngaySinh);
                        client.Send(Encoding.UTF8.GetBytes(dem.ToString()));
                    }
                    else if (loaiQuery.Equals("4"))
                    {
                        int id = int.Parse(duLieu);
                        BSinhVien bsv = new BSinhVien();
                        int dem = bsv.Delete(id);
                        client.Send(Encoding.UTF8.GetBytes(dem.ToString()));
                    }
                    client.Disconnect(false);
                    client.Close();
                    client.Dispose();
                }
            }
        }
    }
}
