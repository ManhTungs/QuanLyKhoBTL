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
        private ObservableCollection<Inventory> _inventories;
        //public ObservableCollection<Inventory> inventories { get => _inventories; set { _inventories = value;OnPropertyChanged();  } }
        public MainWindow2()
        {
            InitializeComponent();
            loadInventory();
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

         void loadInventory()
        {
            //_inventories = new ObservableCollection<Inventory>();
            //var objectList=DataProvider.Ins.DB.Objects;
            //int i = 1;
            //foreach (var item in objectList)
            //{
            //    var InputList = DataProvider.Ins.DB.InputInfoes.Where(p => p.IdObject == item.Id);
            //    var OutputList=DataProvider.Ins.DB.OutputInfoes.Where(p=>p.IdObject == item.Id);

            //    int sumInput=0;
            //    int sumOutput=0;

            //    if (InputList != null)
            //    {
            //        sumInput = (int)InputList.Sum(p => p.Count);

            //    }

            //    if (OutputList != null)
            //    {
            //        sumOutput = (int)OutputList.Sum(p => p.Count);
            //    }

            //    Inventory inventory = new Inventory();
            //    inventory.STT = i;
            //    inventory.Count = sumInput-sumOutput;
            //    inventory.Object=item;

            //    _inventories.Add(inventory);
            //}

            //inventory_list.ItemsSource = _inventories;
           
        }
    }
}
