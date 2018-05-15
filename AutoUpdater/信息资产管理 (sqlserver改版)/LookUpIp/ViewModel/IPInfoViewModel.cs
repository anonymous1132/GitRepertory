using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LookUpIp.ViewModel
{
    class IPInfoViewModel : INotifyPropertyChanged
    {
        public IPInfoViewModel()
        { }
        private string _ipaddress;
        public string IPAddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; GetChanged("IPAddress"); }
        }
        private bool _isused = false;
        public bool isused
        {
            get { return _isused; }
            set { _isused = value; GetChanged("isused"); }
        }
        private DateTime? _lastusedtime=null;
        public DateTime? LastUsedTime
        {
            get { return _lastusedtime; }
            set { _lastusedtime = value; GetChanged("LastUsedTime"); }
            
        }
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; GetChanged("ID"); }
        }

        private string _user;
        public string user
        { 
            get { return _user; }
            set { _user = value; GetChanged("user"); }
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
