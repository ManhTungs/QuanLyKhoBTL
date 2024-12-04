using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using QuanLyKhoBTL.model;
using static MaterialDesignThemes.Wpf.Theme;

namespace QuanLyKhoDemo.View.SupplierWindow
{
    /// <summary>
    /// Interaction logic for MainSuppliersWindow.xaml
    /// </summary>
    public partial class MainSuppliersWindow : Window
    {

        private ObservableCollection<NhaCungCap> listSupplier;
        private NhaCungCap SelectedSupplier { get; set; }
        private int positionSelect = -1;
        private bool checkJustDelete = false;
        public MainSuppliersWindow()
        {
            InitializeComponent();

            loadSupplier();
        }

        private void loadSupplier()
        {
            listSupplier = new ObservableCollection<NhaCungCap>(DataProvider.Ins.DB.NhaCungCaps);


            list_supplier.ItemsSource = listSupplier;
            nbtn_them.IsEnabled = false;
            nbtn_sua.IsEnabled = false;
            nbtn_xoa.IsEnabled = false;
        }

        private void list_supplier_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nbtn_xoa.IsEnabled = true;
            if (listSupplier != null && !checkJustDelete)
            {
                SelectedSupplier = list_supplier.SelectedItem as NhaCungCap;
                tb_ten_nha_cung_cap.Text = SelectedSupplier.TenNCC;
                tb_dia_chi.Text = SelectedSupplier.DiaChi;
                tb_dien_thoai.Text = SelectedSupplier.SDT;
                tb_email.Text = SelectedSupplier.Email;
                //tb_thong_tin_them.Text = SelectedSupplier.;
                dp_ngay_hop_tac.Text = SelectedSupplier.NgayHopTac.ToString();
                positionSelect = list_supplier.SelectedIndex;
                Trace.WriteLine(positionSelect);
            }
            else
            {
                checkJustDelete = false;

            }
        }

        private void btn_them(object sender, RoutedEventArgs e)
        {
            var newSupplier = new NhaCungCap();
            newSupplier.TenNCC = tb_ten_nha_cung_cap.Text.Trim();
            newSupplier.DiaChi = tb_dia_chi.Text.Trim();
            newSupplier.SDT = tb_dien_thoai.Text.Trim();
            newSupplier.Email = tb_email.Text.Trim();
            //newSupplier.MoreInfo = tb_thong_tin_them.Text.Trim();
            if (dp_ngay_hop_tac.SelectedDate.HasValue)
            {
                DateTime date = DateTime.Parse(dp_ngay_hop_tac.Text.Trim());
                newSupplier.NgayHopTac = date;
            }

            nbtn_them.IsEnabled = false;
            DataProvider.Ins.DB.NhaCungCaps.Add(newSupplier);
            DataProvider.Ins.DB.SaveChanges();
            listSupplier.Add(newSupplier);

        }
        private void tb_ten_nha_cung_cap_changed(object sender, TextChangedEventArgs e)
        {
            checkInfoChanged();
        }

        private void tb_dien_thoai_changed(object sender, TextChangedEventArgs e)
        {
            checkInfoChanged();

        }

        private void tb_email_changed(object sender, TextChangedEventArgs e)
        {
            checkInfoChanged();
        }
        private void checkInfoChanged()
        {
            var DisplayName = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.TenNCC == tb_ten_nha_cung_cap.Text.Trim());
            var Phone = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.SDT == tb_dien_thoai.Text);
            var Email = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.Email == tb_email.Text);
            if (tb_ten_nha_cung_cap.Text.Equals("")
                || Phone == null || Phone.Count() != 0 || tb_dien_thoai.Text.Equals("")
                || Email == null || Email.Count() != 0 || tb_email.Text.Equals(""))
            {
                nbtn_them.IsEnabled = false;
                nbtn_sua.IsEnabled = true;
                Trace.WriteLine("This is a trace message.");

                if (tb_ten_nha_cung_cap.Text.Equals("") || tb_dien_thoai.Text.Equals("") || tb_email.Text.Equals(""))
                {
                    nbtn_sua.IsEnabled = false;
                }
            }

            else
            {
                nbtn_them.IsEnabled = true;
                if (SelectedSupplier != null)
                {
                    nbtn_sua.IsEnabled = true;
                    nbtn_xoa.IsEnabled = true;
                }
            }

        }

        private void btn_sua(object sender, RoutedEventArgs e)
        {


            var supplier = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.MaNCC == SelectedSupplier.MaNCC).FirstOrDefault();

            supplier.TenNCC = tb_ten_nha_cung_cap.Text.Trim();
            supplier.DiaChi = tb_dia_chi.Text.Trim();
            supplier.SDT = tb_dien_thoai.Text.Trim();
            supplier.Email = tb_email.Text.Trim();
            //supplier.MoreInfo = tb_thong_tin_them.Text.Trim();
            DateTime date;

            if (dp_ngay_hop_tac.SelectedDate.HasValue)
            {
                date = DateTime.Parse(dp_ngay_hop_tac.Text.Trim());
                supplier.NgayHopTac = date;
                SelectedSupplier.NgayHopTac = date;

            }
            DataProvider.Ins.DB.SaveChanges();

            SelectedSupplier.TenNCC = tb_ten_nha_cung_cap.Text.Trim();
            SelectedSupplier.DiaChi = tb_dia_chi.Text.Trim();
            SelectedSupplier.SDT = tb_dien_thoai.Text.Trim();
            SelectedSupplier.Email = tb_email.Text.Trim();
            //SelectedSupplier.MoreInfo = tb_thong_tin_them.Text.Trim();

            list_supplier.Items.Refresh();

            tb_ten_nha_cung_cap.Text = "";
            tb_dia_chi.Text = "";
            tb_dien_thoai.Text = "";
            tb_email.Text = "";
            //tb_thong_tin_them.Text = "";
            dp_ngay_hop_tac.Text = "";

            nbtn_them.IsEnabled = false;
            nbtn_sua.IsEnabled = false;
            nbtn_xoa.IsEnabled = false;

        }

        private void btn_xoa(object sender, RoutedEventArgs e)
        {
            var supplier = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.MaNCC == SelectedSupplier.MaNCC).FirstOrDefault();

            DataProvider.Ins.DB.NhaCungCaps.Remove(supplier);
            DataProvider.Ins.DB.SaveChanges();

            checkJustDelete = true;
            listSupplier.RemoveAt(positionSelect);
            positionSelect = -1;




        }
    }
}
