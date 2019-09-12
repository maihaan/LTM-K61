using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinhVienServer
{
    public class SinhVien
    {
        public int ID { get; set; }
        public String Ten { get; set; }
        public String Email { get; set; }
        public float DiemTB { get; set; }
        public DateTime NgaySinh { get; set; }
        public int LopID { get; set; }

        public Lop GetLop()
        {
            BLop bl = new BLop();
            return bl.Select("LopID=" + LopID.ToString())[0];
        }
    }
}
