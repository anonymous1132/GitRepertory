using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Net;

namespace CJComLibrary
{
  public  class WMI
    {
      private string wql { get; set; }
      private string ip = "";
      private ManagementScope ms;
      private ManagementObjectSearcher query;
      private ObjectQuery oq;
      private ConnectionOptions co;
        public WMI(string ip)
        {
            this.ip = ip;
            chushihua();
        }

        public WMI()
        { }

        public string GetIPAddress()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostByName(hostName);
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }

        private void chushihua()
        {
            co = new ConnectionOptions();
            //co.Username = "jxep\\caojin";
            //co.Password = "Cj671132";
            ms = new ManagementScope("\\\\" + this.ip + "\\root\\cimv2", co);
        }

        private bool lianjie( string wql)
        {
            oq = new ObjectQuery(wql);
            try
            {
                query = new ManagementObjectSearcher(ms, oq);
                return true;
            }
            catch { return false; }
        }
      //获取C盘空间信息
        public bool GetdiskC(ref double size,ref double freespace,string drive )
        {
            wql = "Select * From Win32_LogicalDisk where deviceid=\""+drive+"\"";
            if (lianjie(wql))
            {
                foreach (ManagementBaseObject disk in query.Get())
                {
                    size = Convert.ToDouble(disk["size"]);
                    size = size / 10;
                    size = Math.Round(size / (1024 * 1024 * 1024),2)*10;
                    freespace = Convert.ToDouble(disk["freespace"]);
                    freespace = freespace / 10;
                    freespace = Math.Round(freespace/ (1024 * 1024 * 1024),2)*10;
                }
                return true;
            }
            else { return false; }
        }
      //获取序列号
        public bool GetSN(ref string SN)
        {
            wql = "Select * from Win32_BIOS";
            if (lianjie(wql))
            {
                foreach (ManagementBaseObject sn in query.Get())
                {
                    SN = sn["SerialNumber"].ToString();
                }
                return true;
            }
            else
            { return false; }
        }
      //获取主板型号、制造商
        public bool GetZhuBan(ref string product, ref string productor)
        {
            wql = "SELECT * FROM Win32_BaseBoard";
            if (lianjie(wql))
            {
                foreach (ManagementBaseObject mbo in query.Get())
                {
                    product = mbo["product"].ToString();
                    productor = mbo["Manufacturer"].ToString();
                }
                return true;
            }
            else
            { return false; }
        }
      //获取系统管控是否正常
        public bool GetServiceWatchClient(ref bool isWCInstalled)
        {
            wql = "SELECT * FROM Win32_Service";
            if (lianjie(wql))
            {
                foreach (ManagementBaseObject mbo in query.Get())
                {
                    if (mbo["name"].ToString() == "WatchClient") { isWCInstalled = true; return true; }
                }
                isWCInstalled = false;
                return true;
            }
            else
            { return false; }
        }
      //获取MAC地址、ip地址
        public bool GetMac(ref string mac,string ip)
        {
            wql = "SELECT * FROM Win32_NetworkAdapterConfiguration where ipenabled=true";
            if (lianjie(wql))
            {
                foreach (ManagementBaseObject mbo in query.Get())
                {
                        string[] addresses = (string[])mbo["IPAddress"];
                        if(addresses[0].IndexOf(ip)!=-1)
                        mac =mac+ mbo["macaddress"].ToString();
                }
             
                return true;
            }
            else
            { return false; }
        }
      //获取主机名、操作系统、
        public bool GetOperSys(ref string opersys,ref string compname,ref string operversion,ref string sysdriver,ref string sysdir)
        {
            wql = "SELECT * FROM Win32_OperatingSystem";
            if (lianjie(wql))
            {
                foreach (ManagementBaseObject mbo in query.Get())
                {
                    opersys = mbo["caption"].ToString();
                    compname=mbo["csname"].ToString();
                    operversion=mbo["version"].ToString();
                    sysdriver=mbo["systemdrive"].ToString();
                    sysdir=mbo["systemdirectory"].ToString();
                }

                return true;
            }
            else
            { return false; }
        }


    }
}
