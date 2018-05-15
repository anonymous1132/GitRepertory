using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LookUpIp.ViewModel
{
   public class XSQViewModel : INotifyPropertyChanged
    {
        public XSQViewModel()
        { }

        private string _assetsno;
        public string assets_NO
        {
            get { return _assetsno; }
            set { _assetsno = value; GetChanged("assets_NO"); }
        }

        private string _pinpai;
        public string pinpai
        {
            get { return _pinpai; }
            set { _pinpai = value; GetChanged("pinpai"); }
        }

        private string _xinghao;
        public string xinghao
        {
            get { return _xinghao; }
            set { _xinghao = value; GetChanged("xinghao"); }
        }

        private string _made_date;
        public string made_date
        {
            get { return _made_date; }
            set { _made_date = value; GetChanged("made_date"); }
        }

        private string _sn;
        public string SN
        {
            get { return _sn; }
            set { _sn = value; GetChanged("SN"); }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void GetChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }
    }
}
