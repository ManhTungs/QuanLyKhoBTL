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

namespace QuanLyKhoDemo.View.ObjectWindow
{
    /// <summary>
    /// Interaction logic for MainObjectWindow.xaml
    /// </summary>
    public partial class MainObjectWindow : Window
    {
        private ObservableCollection<Inventory2> inventories;

        private Inventory2 SelectedHangHoa { get; set; }
        private int positionSelect = -1;
        private bool checkJustDelete = false;
        public MainObjectWindow()
        {
            InitializeComponent();
            loadObject();
        }



        private void btn_them_click(object sender, RoutedEventArgs e)
        {

            var checkIdHangHoa = DataProvider.Ins.DB.HangHoas.Where(p => p.IDHangHoa.Equals(tb_ma_vat_tu.Text.Trim())).FirstOrDefault() as HangHoa;

            if (checkFieldValid())
            {
                if (checkIdHangHoa != null)
                {
                    MessageBox.Show("Mã hàng hóa đã tồn tại, Thêm hàng hóa thất bại");

                }
                else
                {
                    var newHangHoa = new HangHoa();
                    newHangHoa.IDHangHoa = tb_ma_vat_tu.Text.Trim();
                    newHangHoa.TenHangHoa = tb_ten_vat_tu.Text.Trim();
                    newHangHoa.IdDonViDo = (cbb_don_vi_do.SelectedItem as DonViDo).Id;
                    newHangHoa.GiaThanh = decimal.Parse(tb_gia_thanh.Text.Trim());
                    newHangHoa.MoTa = tb_mo_ta.Text.Trim();
                    newHangHoa.SoLuong = 0;

                    DataProvider.Ins.DB.HangHoas.Add(newHangHoa);
                    try
                    {
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    catch (Exception ex)
                    {


                    }

                    Inventory2 inventory2 = new Inventory2();
                    inventory2.hangHoa = newHangHoa;
                    inventory2.TenDonViDo = cbb_don_vi_do.Text.Trim();
                    inventories.Add(inventory2);

                    clearView();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }



        }

        private void clearView()
        {
            tb_ma_vat_tu.Clear();
            tb_ten_vat_tu.Clear();
            cbb_don_vi_do.SelectedItem = null;
            tb_gia_thanh.Clear();
            tb_mo_ta.Clear();


        }

        private bool checkFieldValid()
        {
            if (tb_ma_vat_tu.Text == null || tb_ma_vat_tu.Text.Equals("") || tb_ten_vat_tu == null || tb_ten_vat_tu.Text.Equals("") || cbb_don_vi_do.SelectedItem == null || tb_gia_thanh.Text == null || tb_gia_thanh.Text.Equals("") || tb_mo_ta.Text == null || tb_mo_ta.Text.Equals(""))
            {
                return false;
            }
            return true;
        }

        private void btn_sua_click(object sender, RoutedEventArgs e)
        {
            var hangHoa = DataProvider.Ins.DB.HangHoas.Where(x => x.IDHangHoa == SelectedHangHoa.hangHoa.IDHangHoa).FirstOrDefault();

            hangHoa.IDHangHoa = tb_ma_vat_tu.Text.Trim();
            hangHoa.TenHangHoa = tb_ten_vat_tu.Text.Trim();
            hangHoa.IdDonViDo = (cbb_don_vi_do.SelectedItem as DonViDo).Id;
            hangHoa.GiaThanh = decimal.Parse(tb_gia_thanh.Text.Trim());
            hangHoa.MoTa = tb_mo_ta.Text.Trim();



            DataProvider.Ins.DB.SaveChanges();

            SelectedHangHoa.hangHoa.IDHangHoa = tb_ma_vat_tu.Text.Trim();
            SelectedHangHoa.hangHoa.TenHangHoa = tb_ten_vat_tu.Text.Trim();
            SelectedHangHoa.TenDonViDo = (cbb_don_vi_do.SelectedItem as DonViDo).TenDonViDo;
            SelectedHangHoa.hangHoa.GiaThanh = decimal.Parse(tb_gia_thanh.Text.Trim());
            SelectedHangHoa.hangHoa.MoTa = tb_mo_ta.Text.Trim();

            list_Object.Items.Refresh();

            tb_ma_vat_tu.Text = "";
            tb_ten_vat_tu.Text = "";
            cbb_don_vi_do.SelectedIndex = -1;
            tb_gia_thanh.Text = "";
            tb_mo_ta.Text = "";

            nbtn_them.IsEnabled = false;
            nbtn_xoa.IsEnabled = false;

            list_Object.SelectedItem = -1;

            clearView();

            nbtn_xoa.IsEnabled = false;
            nbtn_sua.IsEnabled = false;
        }

        private void btn_xoa_click(object sender, RoutedEventArgs e)
        {
            var hangHoa = DataProvider.Ins.DB.HangHoas.Where(x => x.IDHangHoa == SelectedHangHoa.hangHoa.IDHangHoa).FirstOrDefault() as HangHoa;

            DataProvider.Ins.DB.HangHoas.Remove(hangHoa);
            DataProvider.Ins.DB.SaveChanges();

            checkJustDelete = true;
            inventories.RemoveAt(positionSelect);
            positionSelect = -1;
            clearView();
            nbtn_xoa.IsEnabled = false;
            nbtn_sua.IsEnabled = false;
        }

        private void list_object_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nbtn_xoa.IsEnabled = true;
            nbtn_sua.IsEnabled = true;
            if (list_Object != null && !checkJustDelete)
            {
                positionSelect = list_Object.SelectedIndex;
                SelectedHangHoa = list_Object.SelectedItem as Inventory2;
                tb_ma_vat_tu.Text = SelectedHangHoa.hangHoa.IDHangHoa;
                tb_ten_vat_tu.Text = SelectedHangHoa.hangHoa.TenHangHoa;
                tb_gia_thanh.Text = SelectedHangHoa.hangHoa.GiaThanh.ToString();
                tb_mo_ta.Text = SelectedHangHoa.hangHoa.MoTa;


                foreach (var item in cbb_don_vi_do.Items)
                {
                    if (((DonViDo)item).Id == SelectedHangHoa.hangHoa.IdDonViDo)
                    {
                        cbb_don_vi_do.SelectedItem = item; // Chọn mục có ID là 123
                        break;
                    }
                }


            }
            else
            {
                checkJustDelete = false;

            }
        }

        private void loadObject()
        {
            inventories = new ObservableCollection<Inventory2>();
            var listObject = DataProvider.Ins.DB.HangHoas;

            foreach (var item in listObject)
            {
                var donViDo = DataProvider.Ins.DB.DonViDoes.Where(p => p.Id == item.IdDonViDo).FirstOrDefault() as DonViDo;
                Inventory2 inventory = new Inventory2();
                inventory.TenDonViDo = donViDo.TenDonViDo;

                inventory.hangHoa = item;
                inventories.Add(inventory);

            }


            list_Object.ItemsSource = inventories;


            var listDonViDo = DataProvider.Ins.DB.DonViDoes;
            foreach (var item in listDonViDo)
            {
                cbb_don_vi_do.Items.Add(new DonViDo { Id = item.Id, TenDonViDo = item.TenDonViDo });

            }

            nbtn_xoa.IsEnabled = false;
            nbtn_sua.IsEnabled = false;


        }

        private void cbb_don_vi_do_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DonViDo newDonViDo= cbb_don_vi_do.SelectedItem as DonViDo;

        }
    }
}
