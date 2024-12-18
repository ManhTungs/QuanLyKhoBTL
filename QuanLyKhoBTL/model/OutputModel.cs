using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoBTL.model
{
    internal class OutputModel
    {
        public HoaDonXuat hoaDonXuat{ get; set; }
        public ChiTietHoaDonXuat chiTietHoaDonXuat{ get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
