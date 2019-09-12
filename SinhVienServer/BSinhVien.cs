using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinhVienServer
{
    public class BSinhVien
    {
        DataAccess da = new DataAccess();

        // Select, Insert, Update, Delete
        public List<SinhVien> SelectAll()
        {
            String query = "Select * From tbSinhVien";
            DataTable tb = da.Read(query);
            if (tb != null && tb.Rows.Count > 0)
            {
                List<SinhVien> ds = new List<SinhVien>();
                foreach (DataRow r in tb.Rows)
                {
                    SinhVien sv = new SinhVien();
                    sv.ID = int.Parse(r["ID"].ToString());
                    sv.Ten = r["Ten"].ToString();
                    sv.Email = r["Email"].ToString();
                    sv.DiemTB = float.Parse(r["DiemTB"].ToString());
                    sv.NgaySinh = DateTime.Parse(r["NgaySinh"].ToString());
                    ds.Add(sv);
                }
                return ds;
            }
            else
                return null;
        }

        public DataTable SelectAllToTable()
        {
            String query = "Select * From tbSinhVien";
            DataTable tb = da.Read(query);
            return tb;
        }

        public DataTable SelectToTable(String condition)
        {
            String query = "Select * from tbSinhVien Where " + condition;
            DataTable tb = da.Read(query);
            return tb;
        }

        public List<SinhVien> Select(String condition)
        {
            String query = "Select * from tbSinhVien Where " + condition;
            DataTable tb = da.Read(query);
            if (tb != null && tb.Rows.Count > 0)
            {
                List<SinhVien> ds = new List<SinhVien>();
                foreach (DataRow r in tb.Rows)
                {
                    SinhVien sv = new SinhVien();
                    sv.ID = int.Parse(r["ID"].ToString());
                    sv.Ten = r["Ten"].ToString();
                    sv.Email = r["Email"].ToString();
                    sv.DiemTB = float.Parse(r["DiemTB"].ToString());
                    sv.NgaySinh = DateTime.Parse(r["NgaySinh"].ToString());
                    ds.Add(sv);
                }
                return ds;
            }
            else
                return null;
        }

        public int Insert(String ten, String email, float diemTB, DateTime ngaySinh)
        {
            String query = "Insert into tbSinhVien(ten, email, diemtb, ngaysinh) Values(N'"
                + ten + "', N'"
                + email + "',"
                + diemTB.ToString() + ",N'"
                + ngaySinh.ToString("MM/dd/yyyy") + "')";
            int dem = da.Write(query);
            return dem;
        }

        public int Update(int id, String ten, String email, float diemTB, DateTime ngaySinh)
        {
            String query = "Update tbSinhVien Set Ten=N'"
                + ten + "', Email = N'"
                + email + "', DiemTB = "
                + diemTB.ToString() + ", NgaySinh=N'"
                + ngaySinh.ToString("MM/dd/yyyy") + "' Where id=" + id.ToString();
            int dem = da.Write(query);
            return dem;
        }

        public int Delete(int id)
        {
            String query = "Delete tbSinhVien Where id=" + id.ToString();
            int dem = da.Write(query);
            return dem;
        }
    }
}
