using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LookUpIp.ViewModel
{
   public class NETViewModel : INotifyPropertyChanged
    {
        public NETViewModel()
        { }

        private string _department;
        public string department
        {
            get { return _department; }
            set { _department = value; GetChanged("department"); }
        }

        private string _banzhu;
        public string banzhu
        {
            get { return _banzhu; }
            set { _banzhu = value; GetChanged("banzhu"); }
        }

        private string _master;
        public string master
        {
            get { return _master; }
            set { _master = value; GetChanged("master"); }
        }

        private string _location;
        public string location
        {
            get { return _location; }
            set { _location = value; GetChanged("location"); }
        }

        private string _assetsno;
        private string _belongs;
        public string assetsno
        {
            get { return _assetsno; }
            set { _assetsno = value; GetChanged("assetsno"); if (value.IndexOf("JT") > -1)  _belongs = "集体企业"; else _belongs = "海宁供电公司"; GetChanged("belongs"); }
        }

        public string belongs
        {
            get { return _belongs; }
            set { _belongs = value; GetChanged("belongs"); }
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
        public string sn
        {
            get { return _sn; }
            set { _sn = value; GetChanged("sn"); }
        }

        private string _ip;
        public string ip
        {
            get { return _ip; }
            set { _ip = value; GetChanged("ip"); }
        }

        private string _mac;
        public string mac
        {
            get { return _mac; }
            set { _mac = value; GetChanged("mac"); }
        }

        private string _network;
        public string network
        {
            get { return _network; }
            set { _network = value; GetChanged("network"); }
        }

        private string _lastupdate;
        public string lastupdate
        {
            get { return _lastupdate; }
            set { _lastupdate = value; }
        }

        private string _switc;
        public string switc
        {
            get { return _switc; }
            set { _switc = value; }
        }

        private string _switchport;
        public string switchport
        {
            get { return _switchport; }
            set { _switchport = value; }
        }

        private string _application;
        public string application
        {
            get { return _application; }
            set { _application = value; GetChanged("application"); }
        }

        private string _arp;
        public string mac_arp
        {
            get { return _arp; }
            set { _arp = value; GetChanged("mac_arp"); }
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
