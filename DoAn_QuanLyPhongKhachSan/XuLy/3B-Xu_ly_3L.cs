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
    public static string Dia_chi_Media = "Media";
    public static CultureInfo Dinh_dang_VN = CultureInfo.GetCultureInfo("vi-VN");

    public static string TaoChuoiHTMLDanhSachPhong(List<XL_Phong> DanhSachPhong)
    {
        //var ChuoiHTMLDanhSachPhong = "<div class='row'>";
        Console.WriteLine(Dia_chi_Media);
        var ChuoiHTMLDanhSachPhong = "<div class='container-fluid'>";
        DanhSachPhong.ForEach(Phong =>
        {
            var TrangThai = "";
            if (Phong.TrangThai == 1)
                TrangThai = "Đã cho thuê";
            else
                TrangThai = "Còn trống";
            var ChuoiHinh = $"<img  src='{Dia_chi_Media}/{Phong.MaSo}.jpg' " +
                                    "style='height:120px; display:block; margin-left:0px;' />";
            var ChuoiThongTin = "<div style='text-align:left;'>" +
                            $"<b>{Phong.Ten}</b>" +
                            $"<br />Trạng thái:{TrangThai}" +
                            $"<br />Loại phòng: {Phong.LoaiPhong.Ten }" +
                            $"<br />Đơn giá: {Phong.LoaiPhong.DonGia.ToString("n0", Dinh_dang_VN) }/đêm" +
                            $"<br />Số khách tối đa: {Phong.LoaiPhong.SoKhachToiDa } người" +
                            $"<br />Tiện nghi: {Phong.LoaiPhong.TienNghi }" +
                            $"<br />{Phong.KhuVuc.Ten}" + $"</div>";
            var ChuoiHTML = "<div class='btn' style='margin:10px 10px 0px 0px;'>" +
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