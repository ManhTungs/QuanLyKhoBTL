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

namespace QuanLyKhoDemo.View.InputWindow
{
    /// <summary>
    /// Interaction logic for MainInputWindow.xaml
    /// </summary>
    public partial class MainInputWindow : Window
    {
        private ObservableCollection<InputModel> inputModels;
        private List<HangHoa> listHangHoa;
        private List<DonViDo> listNhaCungCap;
        public MainInputWindow()
        {
            InitializeComponent();
            loadInputs();
        }



        private void btn_them(object sender, RoutedEventArgs e)
        {
            if (checkFieldValid())
            {
                var newHoaDonNhap = new HoaDonNhap();
                var newChiTietHoaDonNhap = new ChiTietHoaDonNhap();

                newHoaDonNhap.MaHD = tb_ma_hoa_don_nhap.Text.Trim();
                newHoaDonNhap.MaNCC = (cbb_nha_cung_cap.SelectedItem as NhaCungCap).MaNCC;
                if (dp_ngay_nhap.SelectedDate.HasValue)
                {
                    DateTime date = DateTime.Parse(dp_ngay_nhap.Text.Trim());
                    newHoaDonNhap.NgayNhap = date;
                }

                newChiTietHoaDonNhap.MaHD = tb_ma_hoa_don_nhap.Text.Trim();
                newChiTietHoaDonNhap.IDHangHoa = (cbb_ma_hang_hoa.SelectedItem as HangHoa).IDHangHoa;
                newChiTietHoaDonNhap.SoLuong = int.Parse(tb_so_luong.Text.Trim());

                DataProvider.Ins.DB.HoaDonNhaps.Add(newHoaDonNhap);
                DataProvider.Ins.DB.ChiTietHoaDonNhaps.Add(newChiTietHoaDonNhap);

                InputModel newInputModel = new InputModel();

                newInputModel.chiTietHoaDonNhap = newChiTietHoaDonNhap;
                newInputModel.hoaDonNhap = newHoaDonNhap;


                HangHoa hangHoa = DataProvider.Ins.DB.HangHoas.Where(p => p.IDHangHoa == newChiTietHoaDonNhap.IDHangHoa).FirstOrDefault();
                hangHoa.SoLuong += int.Parse(tb_so_luong.Text.Trim());

                inputModels.Add(newInputModel);
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception ex)
                {

                }
                MessageBox.Show("Nhập kho thành công");
                clearView();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin. Nhập kho thất bại");
            }


        }

        private void clearView()
        {
            tb_ma_hoa_don_nhap.Clear();
            cbb_ma_hang_hoa.SelectedItem = null;
            tb_so_luong.Clear();
            cbb_nha_cung_cap.SelectedItem = null;
            dp_ngay_nhap.SelectedDate = null;
        }

        private void tb_ma_hoa_don_nhap_text_changed(object sender, TextChangedEventArgs e)
        {

        }

        private void tb_ma_vat_tu_text_changed(object sender, TextChangedEventArgs e)
        {

        }

        private void tb_so_luong_text_changed(object sender, TextChangedEventArgs e)
        {

        }

        private void loadInputs()
        {

            inputModels = new ObservableCollection<InputModel>();
            var listObject = DataProvider.Ins.DB.HoaDonNhaps;

            foreach (var item in listObject)
            {
                var chiTietHoaDonNhap = DataProvider.Ins.DB.ChiTietHoaDonNhaps.Where(p => p.MaHD == item.MaHD).FirstOrDefault() as ChiTietHoaDonNhap;
                InputModel inputModel = new InputModel();
                inputModel.chiTietHoaDonNhap = chiTietHoaDonNhap;
                inputModel.hoaDonNhap = item;
                inputModels.Add(inputModel);
            }


            list_HoaDonNhap.ItemsSource = inputModels;


            var listHangHoa = DataProvider.Ins.DB.HangHoas;

            foreach (var item in listHangHoa)
            {
                cbb_ma_hang_hoa.Items.Add(new HangHoa { IDHangHoa = item.IDHangHoa, TenHangHoa = item.TenHangHoa });

            }

            var listNhaCungCap = DataProvider.Ins.DB.NhaCungCaps;

            foreach (var item in listNhaCungCap)
            {
                cbb_nha_cung_cap.Items.Add(new NhaCungCap { MaNCC = item.MaNCC, TenNCC = item.TenNCC });
            }

        }

        private void list_HoaDonNhap_selectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private bool checkFieldValid()
        {
            if (tb_ma_hoa_don_nhap.Text == null || tb_ma_hoa_don_nhap.Text.Equals("") || tb_so_luong.Text == null || tb_so_luong.Text.Equals("") || cbb_ma_hang_hoa.SelectedItem == null || !dp_ngay_nhap.SelectedDate.HasValue)
            {
                return false;
            }
            return true;
        }
    }
}
