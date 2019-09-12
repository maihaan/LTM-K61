using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SinhVienServer
{    
    public class BLop
    {
        DataAccess da = new DataAccess();
        public List<Lop> SelectAll()
        {
            String query = "Select * From tbLop";
            DataTable tb = da.Read(query);
            if (tb != null && tb.Rows.Count > 0)
            {
                List<Lop> ds = new List<Lop>();
                foreach (DataRow r in tb.Rows)
                {
                    Lop sv = new Lop();
                    sv.ID = int.Parse(r["ID"].ToString());
                    sv.Ten = r["Ten"].ToString();
                    ds.Add(sv);
                }
                return ds;
            }
            else
                return null;
        }

        public DataTable SelectAllToTable()
        {
            String query = "Select * From tbLop";
            DataTable tb = da.Read(query);
            return tb;
        }

        public DataTable SelectToTable(String condition)
        {
            String query = "Select * from tbLop Where " + condition;
            DataTable tb = da.Read(query);
            return tb;
        }

        public List<Lop> Select(String condition)
        {
            String query = "Select * from tbLop Where " + condition;
            DataTable tb = da.Read(query);
            if (tb != null && tb.Rows.Count > 0)
            {
                List<Lop> ds = new List<Lop>();
                foreach (DataRow r in tb.Rows)
                {
                    Lop sv = new Lop();
                    sv.ID = int.Parse(r["ID"].ToString());
                    sv.Ten = r["Ten"].ToString();
                    ds.Add(sv);
                }
                return ds;
            }
            else
                return null;
        }

        public int Insert(String ten)
        {
            String query = "Insert into tbLop(ten) Values(N'"
                + ten + "')";
            int dem = da.Write(query);
            return dem;
        }

        public int Update(int id, String ten)
        {
            String query = "Update tbLop Set Ten=N'"
                + ten + "' Where id=" + id.ToString();
            int dem = da.Write(query);
            return dem;
        }

        public int Delete(int id)
        {
            // Kiem tra xem lop do co sv nao khong
            List<Lop> ds = Select("id=" + id);
            if(ds.Count > 0)
            {
                Lop l = ds[0];
                if(l.DanhSach().Count > 0)
                {
                    // Khong the xoa
                    return -2;
                }
            }
            String query = "Delete tbLop Where id=" + id.ToString();
            int dem = da.Write(query);
            return dem;
        }
    }
}
