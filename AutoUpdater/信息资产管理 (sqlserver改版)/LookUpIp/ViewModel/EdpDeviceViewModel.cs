using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LookUpIp.ViewModel
{
    public class EdpDeviceViewModel : INotifyPropertyChanged
    {
        public EdpDeviceViewModel()
        { }
        public event PropertyChangedEventHandler PropertyChanged;
        private void GetChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }

        private decimal _deviceid;
        public decimal DeviceID
        {
            get { return _deviceid; }
            set { _deviceid = value; GetChanged("DeviceID"); }
        }

        //private string _classid;
        //public string ClassID
        //{
        //    get { return _classid; }
        //    set { _classid = value; GetChanged("ClassID"); }
        //}

        //公司名称
        private string _deptname;
        public string DeptName
        {
            get { return _deptname; }
            set { _deptname = value; GetChanged("DeptName"); }
        }

        //部门名称
        private string _officename;
        public string OfficeName
        {
            get { return _officename; }
            set { _officename = value; GetChanged("OfficeName"); }
        }

        //地点
        private string _roomnumber;
        public string RoomNumber
        {
            get { return _roomnumber; }
            set { _roomnumber = value; GetChanged("RoomNumber"); }
        }

        //用户名
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; GetChanged("UserName"); }
        }

        //主机名
        private string _devicename;
        public string DeviceName
        {
            get { return _devicename; }
            set { _devicename = value; GetChanged("DeviceName"); }
        }

        //操作系统
        private string _ostype;
        public string OSType
        {
            get { return _ostype; }
            set { _ostype = value; GetChanged("OSType"); }
        }

        //操作系统SP号
        private string _spnumber;
        public string SpNumber
        {
            get { return _spnumber; }
            set { _spnumber = value; GetChanged("SpNumber"); }
        }

        //ie版本
        private string _ieversion;
        public string IEVersion
        {
            get { return _ieversion; }
            set { _ieversion = value; GetChanged("IEVersion"); }
        }

        //cpu
        private string _cputype;
        public string CpuType
        {
            get { return _cputype; }
            set { _cputype = value; GetChanged("CpuType"); }
        }

        //IP
        private string _ipaddres;
        public string IPAddres
        {
            get { return _ipaddres; }
            set { _ipaddres = value; GetChanged("IPAddres"); }
        }

        //MAC
        private string _macaddress;
        public string MacAddress
        {
            get { return _macaddress; }
            set { _macaddress = value; GetChanged("MacAddress"); }
        }

        //网关
        private string _routeip;
        public string RouteIPAddress
        {
            get { return _routeip; }
            set { _routeip = value; GetChanged("RouteIPAddress"); }
        }

        //网络配置
        private string _network;
        public string NetWork
        {
            get { return _network; }
            set { _network = value; GetChanged("NetWork"); }
        }
        //内存
        private int _memory;
        public int Memory
        {
            get { return _memory; }
            set { _memory = value; GetChanged("Memory"); }
        }
        //硬盘容量
        private int _disksize;
        public int DiskSize
        {
            get { return _disksize; }
            set { _disksize = value; GetChanged("DiskSize"); }
        }
        //联系电话
        private string _tel;
        public string Tel
        {
            get { return _tel; }
            set { _tel = value; GetChanged("Tel"); }
        }

        //登录用户
        private string _logonusername;
        public string LogonUserName
        {
            get { return _logonusername; }
            set { _logonusername = value; GetChanged("LogonUserName"); }
        }

        //注册时间
        private DateTime? _registertime = null;
        public DateTime? RegisterTime
        {
            get { return _registertime; }
            set { _registertime = value; GetChanged("RegisterTime"); }
        }

        //上一次客户端汇报时间
        private DateTime? _lasttime = null;
        public DateTime? LastTime
        {
            get { return _lasttime; }
            set { _lasttime = value; GetChanged("LastTime"); }
        }

        //上次卸载管控时间
        private DateTime? _uninstalltime =null;
        public DateTime? UnInstallTime
        {
            get { return _uninstalltime; }
            set { _uninstalltime = value; GetChanged("UnInstallTime"); }
        }

        //在线时长
        private decimal _onlinetime;
        public decimal OnlineTime
        {
            get { return _onlinetime; }
            set { _onlinetime = value; GetChanged("OnlineTime"); }
        }

        //总时长
        private decimal _totaltime;
        public decimal TotalTime
        {
            get { return _totaltime; }
            set { _totaltime = value; GetChanged("TotalTime"); }
        }

        //是否开机
        private byte _runstatus;
        public byte RunStatus
        {
            get { return _runstatus; }
            set { _runstatus = value; GetChanged("RunStatus"); }
        }

        //是否注册
        private byte _registered;
        public byte Registered
        {
            get { return _registered; }
            set { _registered = value; GetChanged("Registered"); }
        }

        //管控版本号
        private string _agentversion;
        public string AgentVersion
        {
            get { return _agentversion; }
            set { _agentversion = value; GetChanged("AgentVersion"); }
        }

        //杀毒软件
        private string _kvscompany;
        public string KvsCompany
        {
            get { return _kvscompany; }
            set { _kvscompany = value; GetChanged("KvsCompany"); }
        }

        //sn
        private string _diskserial;
        public string DiskSerial
        {
            get { return _diskserial; }
            set { _diskserial = value; GetChanged("DiskSerial"); }
        }

        //域名
        private string _domainname;
        public string DomainName
        {
            get { return _domainname; }
            set { _domainname = value; GetChanged("DomainName"); }
        }

        //登录用户名
        private string _exfiled1;
        public string ExField1
        {
            get { return _exfiled1; }
            set { _exfiled1 = value; GetChanged("ExField1"); }
        }
    }
}
