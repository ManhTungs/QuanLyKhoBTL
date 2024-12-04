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

namespace QuanLyKhoDemo.View.CustomerWindow
{
    /// <summary>
    /// Interaction logic for MainCustomersWindow.xaml
    /// </summary>
    public partial class MainCustomersWindow : Window
    {
        private ObservableCollection<KhachHang> listCustomer;
        private KhachHang SelectedCustomer { get; set; }
        private int positionSelect = -1;
        private bool checkJustDelete = false;
        public MainCustomersWindow()
        {
            InitializeComponent();
            loadCustomer();
        }

        private void loadCustomer()
        {
            listCustomer = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);

            list_Customer.ItemsSource = listCustomer;
            nbtn_them.IsEnabled = false;
            nbtn_sua.IsEnabled = false;
            nbtn_xoa.IsEnabled = false;
        }
        private void tb_ten_khach_hang_changed(object sender, TextChangedEventArgs e)
        {
            checkInfoChanged();
        }

        private void tb_email_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkInfoChanged();
        }

        private void tb_so_dien_thoai_change(object sender, TextChangedEventArgs e)
        {
            checkInfoChanged();
        }

        private void checkInfoChanged()
        {
            var DisplayName = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == tb_ten_khach_hang.Text.Trim());
            var Phone = DataProvider.Ins.DB.KhachHangs.Where(x => x.SDT == tb_so_dien_thoai.Text);
            var Email = DataProvider.Ins.DB.KhachHangs.Where(x => x.Email == tb_email.Text);
            if (tb_ten_khach_hang.Text.Equals("")
                || Phone == null || Phone.Count() != 0 || tb_so_dien_thoai.Text.Equals("")
                || Email == null || Email.Count() != 0 || tb_email.Text.Equals(""))
            {
                nbtn_them.IsEnabled = false;
                nbtn_sua.IsEnabled = true;
                Trace.WriteLine("This is a trace message.");

                if (tb_ten_khach_hang.Text.Equals("") || tb_so_dien_thoai.Text.Equals("") || tb_email.Text.Equals(""))
                {
                    nbtn_sua.IsEnabled = false;
                }
            }

            else
            {
                nbtn_them.IsEnabled = true;
                if (SelectedCustomer != null)
                {
                    nbtn_sua.IsEnabled = true;
                    nbtn_xoa.IsEnabled = true;
                }
            }

        }

        private void btn_them(object sender, RoutedEventArgs e)
        {
            var newCustomer = new KhachHang();
            newCustomer.TenKH = tb_ten_khach_hang.Text.Trim();
            newCustomer.DiaChi = tb_dia_chi.Text.Trim();
            newCustomer.SDT = tb_so_dien_thoai.Text.Trim();
            newCustomer.Email = tb_email.Text.Trim();
            //newCustomer.MoreInfo = tb_thong_tin_them.Text.Trim();
            if (dp_ngay_hop_tac.SelectedDate.HasValue)
            {
                DateTime date = DateTime.Parse(dp_ngay_hop_tac.Text.Trim());
                newCustomer.NgayHoptac = date;
            }

            nbtn_them.IsEnabled = false;
            DataProvider.Ins.DB.KhachHangs.Add(newCustomer);
            DataProvider.Ins.DB.SaveChanges();
            listCustomer.Add(newCustomer);
        }

        private void btn_xoa(object sender, RoutedEventArgs e)
        {
            var customer = DataProvider.Ins.DB.KhachHangs.Where(x => x.Id == SelectedCustomer.Id).FirstOrDefault();

            DataProvider.Ins.DB.KhachHangs.Remove(customer);
            DataProvider.Ins.DB.SaveChanges();

            checkJustDelete = true;
            listCustomer.RemoveAt(positionSelect);
            positionSelect = -1;
        }

        private void list_customer_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nbtn_xoa.IsEnabled = true;
            if (list_Customer != null && !checkJustDelete)
            {
                SelectedCustomer = list_Customer.SelectedItem as KhachHang;
                tb_ten_khach_hang.Text = SelectedCustomer.TenKH;
                tb_dia_chi.Text = SelectedCustomer.DiaChi;
                tb_so_dien_thoai.Text = SelectedCustomer.SDT;
                tb_email.Text = SelectedCustomer.Email;
                //tb_thong_tin_them.Text = SelectedCustomer.MoreInfo;
                dp_ngay_hop_tac.Text = SelectedCustomer.NgayHoptac.ToString();
                positionSelect = list_Customer.SelectedIndex;
                Trace.WriteLine(positionSelect);
            }
            else
            {
                checkJustDelete = false;

            }
        }

        private void btn_sua(object sender, RoutedEventArgs e)
        {
            var customer = DataProvider.Ins.DB.KhachHangs.Where(x => x.Id == SelectedCustomer.Id).FirstOrDefault();

            customer.TenKH = tb_ten_khach_hang.Text.Trim();
            customer.DiaChi = tb_dia_chi.Text.Trim();
            customer.SDT = tb_so_dien_thoai.Text.Trim();
            customer.Email = tb_email.Text.Trim();
            //customer.MoreInfo = tb_thong_tin_them.Text.Trim();
            DateTime date;

            if (dp_ngay_hop_tac.SelectedDate.HasValue)
            {
                date = DateTime.Parse(dp_ngay_hop_tac.Text.Trim());
                customer.NgayHoptac = date;
                SelectedCustomer.NgayHoptac = date;

            }
            DataProvider.Ins.DB.SaveChanges();

            SelectedCustomer.TenKH = tb_ten_khach_hang.Text.Trim();
            SelectedCustomer.DiaChi = tb_dia_chi.Text.Trim();
            SelectedCustomer.SDT = tb_so_dien_thoai.Text.Trim();
            SelectedCustomer.Email = tb_email.Text.Trim();
            //SelectedCustomer.MoreInfo = tb_thong_tin_them.Text.Trim();

            list_Customer.Items.Refresh();

            tb_ten_khach_hang.Text = "";
            tb_dia_chi.Text = "";
            tb_so_dien_thoai.Text = "";
            tb_email.Text = "";
            //tb_thong_tin_them.Text = "";
            dp_ngay_hop_tac.Text = "";

            nbtn_them.IsEnabled = false;
            nbtn_sua.IsEnabled = false;
            nbtn_xoa.IsEnabled = false;

        }
    }
}
