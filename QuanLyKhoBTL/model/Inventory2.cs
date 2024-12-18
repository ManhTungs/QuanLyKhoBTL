using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhoBTL.model;

namespace QuanLyKhoBTL.model
{
    internal class Inventory2
    {
        public HangHoa hangHoa { get; set; }
        public string TenDonViDo { get; set; }
    

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
