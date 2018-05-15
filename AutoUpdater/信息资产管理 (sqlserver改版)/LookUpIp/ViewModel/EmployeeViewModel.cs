using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LookUpIp.ViewModel
{
   public class EmployeeViewModel : INotifyPropertyChanged
    {
        public EmployeeViewModel()
        { }

        private string _username;
        public string username
        {
            get { return _username; }
            set { _username = value; GetChanged("username"); }
        }

        private string _department;
        public string department
        {
            get { return _department; }
            set { _department = value; GetChanged("department"); }
        }

        private string _gonghao;
        public string gonghao
        {
            get { return _gonghao; }
            set { _gonghao = value; GetChanged("gonghao"); }
        }

        private double _mobile_phone_number;
        public double mobile_phone_number
        {
            get { return _mobile_phone_number; }
            set { _mobile_phone_number = value; GetChanged("mobile_phone_number"); }
        }

        private double _virture_number;
        public double virture_number
        {
            get { return _virture_number; }
            set { _virture_number = value; GetChanged("virture_number"); }
        }

        private double _phone_number;
        public double phone_number
        {
            get { return _phone_number; }
            set { _phone_number = value; }
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
