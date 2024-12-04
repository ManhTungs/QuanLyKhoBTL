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

namespace QuanLyKhoDemo.View.UnitWindow
{
    /// <summary>
    /// Interaction logic for MainUnitWindow.xaml
    /// </summary>
    public partial class MainUnitWindow : Window
    {
        private ObservableCollection<DonViDo> units;
        private DonViDo SelectedItem { get; set; }
        public MainUnitWindow()
        {
            InitializeComponent();

            loadUnit();
        }

        private void loadUnit()
        {
            units = new ObservableCollection<DonViDo>(DataProvider.Ins.DB.DonViDoes);

            unit_list.ItemsSource = units;
            btn_edit1.IsEnabled = false;
            btn_add1.IsEnabled = false;
        }

        private void unit_list_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {

            if (unit_list != null)
            {
                SelectedItem = unit_list.SelectedItem as DonViDo;
                name_unit.Text = SelectedItem.TenDonViDo;


            }


        }

        private void btn_add(object sender, RoutedEventArgs e)
        {

            var newUnit = new DonViDo();
            newUnit.TenDonViDo = name_unit.Text;
            DataProvider.Ins.DB.DonViDoes.Add(newUnit);
            DataProvider.Ins.DB.SaveChanges();
            units.Add(newUnit);


        }

        private void btn_edit(object sender, RoutedEventArgs e)
        {
            var unit = DataProvider.Ins.DB.DonViDoes.Where(x => x.Id == SelectedItem.Id).FirstOrDefault();
            unit.TenDonViDo = name_unit.Text.Trim();
            DataProvider.Ins.DB.SaveChanges();

            SelectedItem.TenDonViDo = name_unit.Text.Trim();

            unit_list.Items.Refresh();
            name_unit.Text = "";
            btn_add1.IsEnabled = false;
            btn_edit1.IsEnabled = false;

        }

        private void text_change(object sender, TextChangedEventArgs e)
        {
            var DisplayName = DataProvider.Ins.DB.DonViDoes.Where(x => x.TenDonViDo == name_unit.Text);
            if (DisplayName == null || DisplayName.Count() != 0 || name_unit.Text.Equals(""))
            {
                btn_add1.IsEnabled = false;
                btn_edit1.IsEnabled = false;
            }
            else
            {
                btn_add1.IsEnabled = true;
                if (SelectedItem != null)
                {
                    btn_edit1.IsEnabled = true;

                }
            }
        }
    }
}
