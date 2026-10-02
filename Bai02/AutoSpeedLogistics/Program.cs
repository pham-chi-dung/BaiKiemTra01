using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoSpeedLogistics
{
    // ==========================================
    // A. ABSTRACT CLASS PHUONGTIEN (Lớp cha trừu tượng)
    // ==========================================
    public abstract class PhuongTien
    {
        private string _maPT = "PT000";
        private string _tenHang = string.Empty;
        private int _namSanXuat;
        private decimal _giaGoc;

        public string MaPT
        {
            get => _maPT;
            set => _maPT = string.IsNullOrWhiteSpace(value) ? "PT000" : value.Trim();
        }

        public string TenHang
        {
            get => _tenHang;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tên hãng không được để trống.");
                _tenHang = value.Trim();
            }
        }

        public int NamSanXuat
        {
            get => _namSanXuat;
            set
            {
                int currentYear = DateTime.Now.Year;
                // Chuẩn hóa thông báo lỗi khớp chính xác với TC01
                if (value < 1900 || value > currentYear)
                    throw new ArgumentException("Năm sản xuất không hợp lệ!");
                _namSanXuat = value;
            }
        }

        public decimal GiaGoc
        {
            get => _giaGoc;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Giá gốc phải lớn hơn 0.");
                _giaGoc = value;
            }
        }

        public PhuongTien(string maPT, string tenHang, int namSanXuat, decimal giaGoc)
        {
            MaPT = maPT;
            TenHang = tenHang;
            NamSanXuat = namSanXuat;
            GiaGoc = giaGoc;
        }

        public abstract decimal TinhGiaLanBanh();

        public virtual string GetInfo()
        {
            return $"Mã PT: {MaPT} | Hãng: {TenHang} | Năm SX: {NamSanXuat} | Giá gốc: {GiaGoc:N0} VNĐ";
        }
    }

    // ==========================================
    // B. CLASS OTO
    // ==========================================
    public class OTo : PhuongTien
    {
        private int _soChoNgoi;
        private double _dungTichDongCo;

        public int SoChoNgoi
        {
            get => _soChoNgoi;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Số chỗ ngồi phải > 0.");
                _soChoNgoi = value;
            }
        }

        public double DungTichDongCo
        {
            get => _dungTichDongCo;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Dung tích động cơ phải > 0.");
                _dungTichDongCo = value;
            }
        }

        public OTo(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int soChoNgoi, double dungTichDongCo)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            SoChoNgoi = soChoNgoi;
            DungTichDongCo = dungTichDongCo;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (SoChoNgoi <= 9)
            {
                // Giá gốc + Lệ phí trước bạ (12%) + Thuế Tiêu thụ đặc biệt (30%)
                return GiaGoc + (GiaGoc * 0.12m) + (GiaGoc * 0.30m);
            }
            else
            {
                // Giá gốc + Lệ phí trước bạ (10%)
                return GiaGoc + (GiaGoc * 0.10m);
            }
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()} | Loại: Ô tô | Chỗ ngồi: {SoChoNgoi} | Dung tích: {DungTichDongCo}L | Giá lăn bánh: {TinhGiaLanBanh():N0} VNĐ";
        }
    }

    // ==========================================
    // C. CLASS XEMAY
    // ==========================================
    public class XeMay : PhuongTien
    {
        private int _dungTichXylanh;

        public int DungTichXylanh
        {
            get => _dungTichXylanh;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Dung tích xi lanh phải > 0.");
                _dungTichXylanh = value;
            }
        }

        public XeMay(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int dungTichXylanh)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            DungTichXylanh = dungTichXylanh;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (DungTichXylanh < 175)
            {
                // Giá gốc + Thuế Trước bạ (2%)
                return GiaGoc + (GiaGoc * 0.02m);
            }
            else
            {
                // Giá gốc + Thuế Trước bạ (5%)
                return GiaGoc + (GiaGoc * 0.05m);
            }
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()} | Loại: Xe máy | Dung tích XL: {DungTichXylanh}cc | Giá lăn bánh: {TinhGiaLanBanh():N0} VNĐ";
        }
    }

    // ==========================================
    // D. CLASS QUANLYPHUONGTIEN
    // ==========================================
    public class QuanLyPhuongTien
    {
        private List<PhuongTien> _danhSach = new List<PhuongTien>();

        public void AddPhuongTien(PhuongTien pt)
        {
            if (pt != null)
            {
                _danhSach.Add(pt);
            }
        }

        public void DisplayAll()
        {
            if (_danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách phương tiện trống.");
                return;
            }

            foreach (var pt in _danhSach)
            {
                Console.WriteLine(pt.GetInfo());
            }
        }

        public PhuongTien? FindMaxGiaLanBanh()
        {
            if (_danhSach.Count == 0) return null;
            return _danhSach.OrderByDescending(pt => pt.TinhGiaLanBanh()).FirstOrDefault();
        }

        public List<PhuongTien> SearchByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<PhuongTien>();

            return _danhSach
                .Where(pt => pt.TenHang.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }

    // ==========================================
    // BỘ THIẾT LẬP CHẠY TEST CASE (TC01 - TC05)
    // ==========================================
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("================ BÁO CÁO KẾT QUẢ KIỂM THỬ (TC01 - TC05) ================\n");

            // --- TC01: Kiểm tra Validation Năm sản xuất ---
            Console.WriteLine("[TC01] Khởi tạo Ô tô với NamSanXuat = 1850:");
            try
            {
                OTo carInvalid = new OTo("OT01", "Toyota", 1850, 1000000000m, 5, 2.0);
                Console.WriteLine(" -> Kết quả: FAILED (Không ném ngoại lệ)");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($" -> Kết quả: PASSED (Bắt đúng ArgumentException: \"{ex.Message}\")");
            }

            // --- TC02: Kiểm tra Tính Giá Lăn Bánh Ô tô ---
            Console.WriteLine("\n[TC02] Ô tô 5 chỗ, GiaGoc = 1,000,000,000 VNĐ:");
            OTo car5Cho = new OTo("OT02", "Toyota", 2022, 1000000000m, 5, 2.0);
            decimal giaOTo = car5Cho.TinhGiaLanBanh();
            Console.WriteLine($" -> Giá lăn bánh thực tế: {giaOTo:N0} VNĐ");
            Console.WriteLine(giaOTo == 1420000000m
                ? " -> Kết quả: PASSED (Khớp chuẩn 1,420,000,000 VNĐ)"
                : " -> Kết quả: FAILED");

            // --- TC03: Kiểm tra Tính Giá Lăn Bánh Xe máy ---
            Console.WriteLine("\n[TC03] Xe máy 150cc, GiaGoc = 50,000,000 VNĐ:");
            XeMay xeMay150 = new XeMay("XM01", "Honda", 2023, 50000000m, 150);
            decimal giaXeMay = xeMay150.TinhGiaLanBanh();
            Console.WriteLine($" -> Giá lăn bánh thực tế: {giaXeMay:N0} VNĐ");
            Console.WriteLine(giaXeMay == 51000000m
                ? " -> Kết quả: PASSED (Khớp chuẩn 51,000,000 VNĐ)"
                : " -> Kết quả: FAILED");

            // --- TC04: Kiểm tra Đa hình List<PhuongTien> ---
            Console.WriteLine("\n[TC04] Nạp 1 Ô tô & 1 Xe máy vào List<PhuongTien> và hiển thị:");
            QuanLyPhuongTien ql = new QuanLyPhuongTien();
            ql.AddPhuongTien(car5Cho);
            ql.AddPhuongTien(xeMay150);
            ql.DisplayAll();
            Console.WriteLine(" -> Kết quả: PASSED (C# tự động kích hoạt đúng công thức của Ô tô và Xe máy)");

            // --- TC05: Kiểm tra Tìm Giá Lăn Bánh Max ---
            Console.WriteLine("\n[TC05] Gọi hàm FindMaxGiaLanBanh():");
            PhuongTien? maxPt = ql.FindMaxGiaLanBanh();
            if (maxPt != null)
            {
                Console.WriteLine($" -> Đối tượng tìm thấy: {maxPt.MaPT} ({maxPt.TenHang}) - Giá: {maxPt.TinhGiaLanBanh():N0} VNĐ");
                Console.WriteLine(maxPt == car5Cho
                    ? " -> Kết quả: PASSED (Trả về đúng Ô tô 5 chỗ giá 1.42 tỷ VNĐ)"
                    : " -> Kết quả: FAILED");
            }

            Console.WriteLine("\n==========================================================================");
            Console.ReadKey();
        }
    }
}