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
using QuanLyKhoDemo.View.CustomerWindow;
using QuanLyKhoDemo.View.InputWindow;
using QuanLyKhoDemo.View.ObjectWindow;
using QuanLyKhoDemo.View.OutPutWindow;
using QuanLyKhoDemo.View.SupplierWindow;
using QuanLyKhoDemo.View.UnitWindow;



namespace QuanLyKhoDemo.View
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : Window
    {

        CollectionView view;
        bool isFiltering = false;
        private ObservableCollection<InputModel> inputModels;
        public MainWindow2()
        {
            InitializeComponent();
            loadHoaDonNhap();
        }

        private void btn_show_unit_window(object sender, RoutedEventArgs e)
        {

            MainUnitWindow mainUnitWindow = new MainUnitWindow();
            mainUnitWindow.ShowDialog();

        }

        private void btn_show_suppliers_window(object sender, RoutedEventArgs e)
        {
            MainSuppliersWindow mainSuppliersWindow = new MainSuppliersWindow();
            mainSuppliersWindow.ShowDialog();
        }

        private void btn_show_customers_window(object sender, RoutedEventArgs e)
        {
            MainCustomersWindow mainCustomersWindow = new MainCustomersWindow();
            mainCustomersWindow.ShowDialog();

        }

        private void btn_show_object_window(object sender, RoutedEventArgs e)
        {
            MainObjectWindow mainObjectWindow = new MainObjectWindow();
            mainObjectWindow.ShowDialog();

        }

        private void btn_show_input_window(object sender, RoutedEventArgs e)
        {
            MainInputWindow mainInputWindow = new MainInputWindow();
            mainInputWindow.ShowDialog();
        }

        private void btn_show_output_window(object sender, RoutedEventArgs e)
        {
            MainOutputWindow mainOutputWindow = new MainOutputWindow();
            mainOutputWindow.ShowDialog();
        }

        void loadHoaDonNhap()
        {

            inputModels = new ObservableCollection<InputModel>();
            var listObject = DataProvider.Ins.DB.HoaDonNhaps;
            if (listObject != null)
            {
                foreach (var item in listObject)
                {
                    var chiTietHoaDonNhap = DataProvider.Ins.DB.ChiTietHoaDonNhaps.Where(p => p.MaHD == item.MaHD).FirstOrDefault() as ChiTietHoaDonNhap;
                    InputModel inputModel = new InputModel();
                    inputModel.chiTietHoaDonNhap = chiTietHoaDonNhap;
                    inputModel.hoaDonNhap = item;
                    inputModels.Add(inputModel);


                }
                list_hoaDonNhap.ItemsSource = inputModels;


            }

            btn_bo_loc.IsEnabled = false;
        }

        private void btn_loc_Click(object sender, RoutedEventArgs e)
        {


            if (dp_ngay_bat_dau.SelectedDate.HasValue && dp_ngay_ket_thuc.SelectedDate.HasValue)
            {

                DateTime ngayBatDau;
                DateTime ngayKetThuc;

                ngayBatDau = DateTime.Parse(dp_ngay_bat_dau.Text.Trim());
                ngayKetThuc = DateTime.Parse(dp_ngay_ket_thuc.Text.Trim());
                if (ngayBatDau < ngayKetThuc)
                {
                    view = (CollectionView)CollectionViewSource.GetDefaultView(inputModels);
                    view.Filter = FilterByDateRange;

                    isFiltering = true;
                    btn_bo_loc.IsEnabled = true;

                }
                else
                {
                    MessageBox.Show("vui lòng nhập đúng thông tin");
                }



            }
            else
            {
                MessageBox.Show("vui lòng nhập đủ thông tin");
            }

        }

        private bool FilterByDateRange(object item)
        {
            DateTime ngayBatDau;
            DateTime ngayKetThuc;

            ngayBatDau = DateTime.Parse(dp_ngay_bat_dau.Text.Trim());
            ngayKetThuc = DateTime.Parse(dp_ngay_ket_thuc.Text.Trim());
            if (item is InputModel inputModel)
            {

                if (inputModel.hoaDonNhap.NgayNhap < ngayBatDau)
                    return false;

                if (inputModel.hoaDonNhap.NgayNhap > ngayKetThuc)
                    return false;

                return true;
            }
            return false;
        }

        private void btn_bo_loc_click(object sender, RoutedEventArgs e)
        {
            isFiltering = false;
            btn_bo_loc.IsEnabled = false;
            view.Filter = null;
            view.Refresh();

            dp_ngay_bat_dau.SelectedDate = null;
            dp_ngay_ket_thuc.SelectedDate = null;

        }
    }
}
