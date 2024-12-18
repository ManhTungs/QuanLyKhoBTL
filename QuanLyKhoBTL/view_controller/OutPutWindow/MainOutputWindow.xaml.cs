using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QuanLyKhoBTL.model;

namespace QuanLyKhoDemo.View.OutPutWindow
{
    /// <summary>
    /// Interaction logic for MainOutputWindow.xaml
    /// </summary>
    public partial class MainOutputWindow : Window
    {
        private ObservableCollection<OutputModel> outputModels;
        public MainOutputWindow()
        {
            InitializeComponent();
            loadInputWindow();
        }

        private void loadInputWindow()
        {
            outputModels = new ObservableCollection<OutputModel>();
            var listObject = DataProvider.Ins.DB.HoaDonXuats;

            foreach (var item in listObject)
            {
                var chitietHoaDonXuat = DataProvider.Ins.DB.ChiTietHoaDonXuats.Where(p => p.MaHD == item.MaHD).FirstOrDefault() as ChiTietHoaDonXuat;
                OutputModel outputModel = new OutputModel();
                outputModel.chiTietHoaDonXuat = chitietHoaDonXuat;
                outputModel.hoaDonXuat = item;
                outputModels.Add(outputModel);
            }


            list_HoaDonXuat.ItemsSource = outputModels;


            var listHangHoa = DataProvider.Ins.DB.HangHoas;

            foreach (var item in listHangHoa)
            {
                cbb_ma_hang_hoa.Items.Add(new HangHoa { IDHangHoa = item.IDHangHoa, TenHangHoa = item.TenHangHoa });

            }

            var listKhachHang = DataProvider.Ins.DB.KhachHangs;

            foreach (var item in listKhachHang)
            {
                cbb_ma_khach_hang.Items.Add(new KhachHang { Id = item.Id, TenKH = item.TenKH });
            }
        }

        private void list_HoaDonXuat_selectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_them_click(object sender, RoutedEventArgs e)
        {
            if (checkFieldValid())
            {
                var hangHoa = cbb_ma_hang_hoa.SelectedItem as HangHoa;
                var hangHoaDB = DataProvider.Ins.DB.HangHoas.Where(p => p.IDHangHoa.Equals(hangHoa.IDHangHoa)).FirstOrDefault() as HangHoa;
                if (hangHoaDB.SoLuong >= int.Parse(tb_so_luong.Text.Trim()))
                {
                    var newHoaDonXuat = new HoaDonXuat();
                    var newChiTietHoaDonXuat = new ChiTietHoaDonXuat();

                    newHoaDonXuat.MaHD = tb_ma_hoa_don_xuat.Text.Trim();
                    newHoaDonXuat.MaKH = (cbb_ma_khach_hang.SelectedItem as KhachHang).Id;




                    newHoaDonXuat.TongGia = decimal.Parse(tb_so_luong.Text.Trim()) * hangHoaDB.GiaThanh;


                    if (dp_ngay_xuat.SelectedDate.HasValue)
                    {
                        DateTime date = DateTime.Parse(dp_ngay_xuat.Text.Trim());
                        newHoaDonXuat.NgayXuat = date;
                    }



                    newChiTietHoaDonXuat.MaHD = tb_ma_hoa_don_xuat.Text.Trim();
                    newChiTietHoaDonXuat.IDHangHoa = (cbb_ma_hang_hoa.SelectedItem as HangHoa).IDHangHoa;
                    newChiTietHoaDonXuat.TenHangHoa = (cbb_ma_hang_hoa.SelectedItem as HangHoa).TenHangHoa;
                    newChiTietHoaDonXuat.SoLuong = int.Parse(tb_so_luong.Text.Trim());

                    DataProvider.Ins.DB.HoaDonXuats.Add(newHoaDonXuat);
                    DataProvider.Ins.DB.ChiTietHoaDonXuats.Add(newChiTietHoaDonXuat);

                    OutputModel newOutputModel = new OutputModel();

                    newOutputModel.chiTietHoaDonXuat = newChiTietHoaDonXuat;
                    newOutputModel.hoaDonXuat = newHoaDonXuat;


                    HangHoa hangHoaTrongKho = DataProvider.Ins.DB.HangHoas.Where(p => p.IDHangHoa == newChiTietHoaDonXuat.IDHangHoa).FirstOrDefault();
                    hangHoaTrongKho.SoLuong -= int.Parse(tb_so_luong.Text.Trim());

                    outputModels.Add(newOutputModel);
                    try
                    {
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                    }
                    MessageBox.Show("Xuất kho thành công");
                    clearView();
                }
                else
                {
                    MessageBox.Show("Số lượng xuất quá với số hàng có trong kho. Xuất kho thất bại");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin. Xuất kho thất bại");
            }


        }

        private void clearView()
        {
            tb_ma_hoa_don_xuat.Clear();
            cbb_ma_hang_hoa.SelectedItem = null;
            tb_so_luong.Clear();
            cbb_ma_khach_hang.SelectedItem = null;
            dp_ngay_xuat.SelectedDate = null;
        }

        private bool checkFieldValid()
        {
            if (tb_ma_hoa_don_xuat.Text == null || tb_ma_hoa_don_xuat.Text.Equals("") || tb_so_luong.Text == null || tb_so_luong.Text.Equals("") || cbb_ma_hang_hoa.SelectedItem == null || cbb_ma_khach_hang.SelectedItem == null || !dp_ngay_xuat.SelectedDate.HasValue)
            {
                return false;
            }
            return true;
        }
    }
}
