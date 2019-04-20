using System;
using System.Collections.Generic;

public class XL_KhuVuc
{
    public string MaSo, Ten;
    public int SoTang;
}
public class XL_TiepTan
{
    public string MaSo, HoTen, TenDangNhap, MatKhau;
    public XL_KhuVuc KhuVuc = new XL_KhuVuc();
}
public class XL_QuanLyKhuVuc
{
    public string MaSo, HoTen, TenDangNhap, MatKhau;
    public List<XL_KhuVuc> DanhSachKhuVuc = new List<XL_KhuVuc>();
}
public class XL_Phong
{
    public string MaSo, Ten;
    public int TrangThai;
    public XL_LoaiPhong LoaiPhong = new XL_LoaiPhong();
    public XL_KhuVuc KhuVuc = new XL_KhuVuc();
}
public class XL_LoaiPhong
{
    public string MaSo, Ten, TienNghi;
    public int DonGia, SoKhachToiDa;
}
public class XL_PhieuThuePhong
{
    public string MaSo, TenPhong;
    public DateTime NgayBatDau, NgayDuKienTra, NgayTraPhong;
    public List<XL_KhachHang> DanhSachKhachHang = new List<XL_KhachHang>();
}
public class XL_KhachHang
{
    public string HoTen, CMND;
}
public class XL_KhachSan
{
    public string MaSo, Ten, DienThoai, DiaChi;
    public List<XL_KhuVuc> DanhSachKhuVuc = new List<XL_KhuVuc>();
    public List<XL_LoaiPhong> DanhSachLoaiPhong = new List<XL_LoaiPhong>();
}