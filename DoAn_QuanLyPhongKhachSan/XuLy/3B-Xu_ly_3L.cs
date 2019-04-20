using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;
using System.Globalization;
using Newtonsoft.Json;

public class XL_TheHien
{
    public static string Dia_chi_Media = @"D:\STUDY\IT\Semester3\CNPM\Program\DoAn_QuanLyPhongKhachSan\Media";
    public static CultureInfo Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");

    public static string TaoChuoiHTMLDanhSachPhong(List<XL_Phong> DanhSachPhong)
    {
        var ChuoiHTMLDanhSachPhong = "<div class='row'>";
        DanhSachPhong.ForEach(Phong =>
        {
            var TrangThai = "";
            if (Phong.TrangThai == 1)
                TrangThai = "Đã cho thuê";
            else
                TrangThai = "Còn trống";
            var ChuoiHinh = $"<img  src='{Dia_chi_Media}/{Phong.MaSo}.png' " +
                                     $" style='width:50px;height:50px' />";
            var ChuoiThongTin = $"<div class='btn' style='text-align:left'> " +
                            $"<br />Tên phòng: {Phong.Ten}" +
                            $"<br />Trạng thái:{TrangThai}" +
                            $"<br />Loại phòng: {Phong.LoaiPhong.Ten }" +
                            $"<br />Đơn giá: {Phong.LoaiPhong.DonGia.ToString("n0", Dinh_dang_VN) }/đêm" +
                            $"<br />Số khách tối đa: {Phong.LoaiPhong.SoKhachToiDa } người" +
                            $"<br />Tiện nghi: {Phong.LoaiPhong.TienNghi }" +
                            $"<br />{Phong.KhuVuc.Ten}" + $"</div>";
            var ChuoiHTML = $"<div class='col-md-3' style='margin-bottom:10px' >" +
                               $"{ChuoiHinh}" + $"{ChuoiThongTin}" +
                             "</div>";
            ChuoiHTMLDanhSachPhong += ChuoiHTML;
        });
        ChuoiHTMLDanhSachPhong += "</div>";
        return ChuoiHTMLDanhSachPhong;
    }
}
public class XL_NghiepVu
{
    public static XL_TiepTan DangNhap(string TenDangNhap, string MatKhau, List<XL_TiepTan> DanhSach)
    {
        var TiepTan = DanhSach.FirstOrDefault(x => x.TenDangNhap == TenDangNhap && x.MatKhau == MatKhau);
        return TiepTan;
    }
    public static List<XL_Phong> TraCuuThongTinPhong(string ChuoiTraCuu, List<XL_Phong> DanhSach)
    {
        var DanhSachKQ = new List<XL_Phong>();
        int number = 2;
        if (ChuoiTraCuu.ToUpper().Contains("Còn trống"))
            number = 0;
        else if (ChuoiTraCuu.ToUpper().Contains("Đã cho thuê"))
            number = 1;
        if (number != 2)
        {
            DanhSachKQ = DanhSach.FindAll(x => x.TrangThai == number);
        }
        else if (Int32.TryParse(ChuoiTraCuu, out number))
        {
            if (number < 5)
            {
                DanhSachKQ = DanhSach.FindAll(x => x.LoaiPhong.SoKhachToiDa == number);
            }
            else
            {
                DanhSachKQ = DanhSach.FindAll(x => x.LoaiPhong.DonGia == number);
            }            
        }
        else
        {
            DanhSachKQ = DanhSach.FindAll(x => x.MaSo.ToUpper().Contains(ChuoiTraCuu.ToUpper()) ||
                        x.Ten.ToUpper().Contains(ChuoiTraCuu.ToUpper()) ||
                        x.KhuVuc.MaSo.ToUpper().Contains(ChuoiTraCuu.ToUpper()) || x.KhuVuc.Ten.ToUpper().Contains(ChuoiTraCuu.ToUpper()) ||
                         x.LoaiPhong.Ten.ToUpper().Contains(ChuoiTraCuu.ToUpper()) || x.LoaiPhong.MaSo.ToUpper().Contains(ChuoiTraCuu.ToUpper()) ||
                         x.LoaiPhong.TienNghi.ToUpper().Contains(ChuoiTraCuu.ToUpper()));
        }
        return DanhSachKQ;
    }
}
public class XL_LuuTru
{
    static DirectoryInfo ThuMucProject = new DirectoryInfo(HostingEnvironment.ApplicationPhysicalPath).Parent.Parent;
    static DirectoryInfo ThuMucDuLieu = ThuMucProject.GetDirectories("Du_lieu")[0];
    static DirectoryInfo ThuMucTiepTan = ThuMucDuLieu.GetDirectories("TiepTan")[0];
    static DirectoryInfo ThuMucPhong = ThuMucDuLieu.GetDirectories("Phong")[0];

    public static List<XL_TiepTan> DocDanhSachTiepTan()
    {
        var DanhSach = new List<XL_TiepTan>();
        var DanhSachTapTin = ThuMucTiepTan.GetFiles("*.json").ToList();
        DanhSachTapTin.ForEach(TapTin =>
        {
            var DuongDan = TapTin.FullName;
            var chuoiJSon = File.ReadAllText(DuongDan);
            var DoiTuong = JsonConvert.DeserializeObject<XL_TiepTan>(chuoiJSon);
            DanhSach.Add(DoiTuong);
        });
        return DanhSach;
    }
    public static List<XL_Phong> DocDanhSachPhongTiepTanQuanLy(XL_TiepTan TiepTan)
    {
        var DanhSach = new List<XL_Phong>();
        var DanhSachTapTin = ThuMucPhong.GetFiles("*.json").ToList();
        DanhSachTapTin.ForEach(TapTin =>
        {
            var DuongDan = TapTin.FullName;
            var chuoiJSon = File.ReadAllText(DuongDan);
            var DoiTuong = JsonConvert.DeserializeObject<XL_Phong>(chuoiJSon);
            if (DoiTuong.KhuVuc.MaSo == TiepTan.KhuVuc.MaSo)
                DanhSach.Add(DoiTuong);
        });
        return DanhSach;
    }
}