using System;
using System.Collections.Generic;

public class XL_KhuVuc
{
    string MaSo, Ten;
    int SoTang;
}
public class XL_TiepTan
{
    string MaSo, HoTen, TenDangNhap, MatKhau;
    XL_KhuVuc KhuVuc = new XL_KhuVuc();
}
public class XL_QuanLyKhuVuc
{
    string MaSo, HoTen, TenDangNhap, MatKhau;
    List<XL_KhuVuc> DanhSachKhuVuc = new List<XL_KhuVuc>();
}
public class XL_Phong
{
    string MaSo, Ten, TrangThai;
    XL_LoaiPhong LoaiPhong = new XL_LoaiPhong();
    XL_KhuVuc KhuVuc = new XL_KhuVuc();
}
public class XL_LoaiPhong
{
    string MaSo, Ten, TienNghi;
    int DonGia, SoKhachToiDa;
}
public class XL_PhieuThuePhong
{
    string MaSo, TenPhong;
    DateTime NgayBatDau, NgayDuKienTra, NgayTraPhong;
    List<XL_KhachHang> DanhSachKhachHang = new List<XL_KhachHang>();
}
public class XL_KhachHang
{
    string HoTen, CMND;
}