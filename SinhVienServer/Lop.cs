using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinhVienServer
{
    public class Lop
    {
        public int ID { get; set; }
        public String Ten { get; set; }

        public List<SinhVien> DanhSach()
        {
            BSinhVien bsv = new BSinhVien();
            return bsv.Select("LopID=" + ID.ToString());
        }
    }
}
