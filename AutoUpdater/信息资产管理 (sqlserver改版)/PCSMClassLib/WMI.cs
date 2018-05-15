using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace PCSMClassLib
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

        private void chushihua()
        {
            co = new ConnectionOptions();
            //co.Username = "jxep\\caojin";
            //co.Password = "Cj671132";
            //co.Username = "jxep\\wufanglin";
            //co.Password = "hn.123456";

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
        private bool lianjie(string wql,ManagementScope manscop)
        {
            oq = new ObjectQuery(wql);
            try
            {
                query = new ManagementObjectSearcher(manscop, oq);
                return true;
            }
            catch { return false; }
        }

      //获取C盘空间信息
        public bool GetdiskC(ref string size,ref string freespace,string drive )
        {
            wql = "Select * From Win32_LogicalDisk where deviceid=\""+drive+"\"";
            if (lianjie(wql))
            {
                try
                {
                    query.Get();
                }
                catch (Exception) { return false; }
                foreach (ManagementBaseObject disk in query.Get())
                {
                    UInt64 allpm1 = 0;
                    UInt64 allpm2 = 0;
                    try
                    {
                        allpm1 = (UInt64)disk["size"];
                        size = (allpm1 / 1024.0 / 1024.0 / 1024.0).ToString("f3") + "G";
                    }
                    catch (Exception) { }
                    try
                    {
                        allpm2 = (UInt64)disk["freespace"];
                        freespace = (allpm2 / 1024.0 / 1024.0 / 1024.0).ToString("f3") + "G";
                    }
                    catch (Exception) { }
                }
                
                return true;
            }
            else { return false; }
        }
      //获取内存大小信息
        public bool GetMemory(ref string memsize)
        {
            wql = "SELECT * FROM Win32_PhysicalMemory";
            if (lianjie(wql))
            {
                try
                {
                    query.Get();
                }
                catch (Exception) { return false; }
                UInt64 allpm = 0;
               
                    foreach (ManagementObject mem in query.Get())
                    {
                        try
                        {
                            allpm += (UInt64)mem["Capacity"];
                        }
                        catch (Exception) { }
                    }
                    memsize = (allpm / 1024.0 / 1024.0 / 1024.0).ToString("f3") + "G";
                    return true;
            } 
            return false;
        }
      //获取硬盘总容量
        public bool GetdiskAll(ref string harddisksize)
        {
            wql = "SELECT * FROM Win32_DiskDrive";
            if (lianjie(wql))
            {
                try
                {
                    query.Get();
                }
                catch (Exception) { return false; }
                foreach (ManagementObject cj in query.Get())
                {
                    try
                    {
                        UInt64 hdlN = (UInt64)cj["Size"];
                        if (harddisksize == "")
                        {
                            harddisksize = (hdlN / 1024.0 / 1024.0 / 1024.0).ToString("f3") + "G";
                        }
                        else { harddisksize = harddisksize + " +" + (hdlN / 1024.0 / 1024.0 / 1024.0).ToString("f3") + "G"; }
                    }
                    catch { }
                }




                return true;
            }
            return false;
        }

      //获取序列号、主板出厂日期（bios）
        public bool GetBIOS(ref string SN,ref string releasedate)
        {
            wql = "Select * from Win32_BIOS";
            if(lianjie(wql))
            {
                try { query.Get(); }
                catch (Exception)
                { return false; }

                    foreach (ManagementBaseObject mo in query.Get())
                    {
                        try
                        {
                            SN = mo["SerialNumber"].ToString();
                        }
                        catch (Exception) { }
                        try
                        {
                            releasedate = mo["ReleaseDate"].ToString();
                        }
                        catch (Exception) { }
                    }
                    return true;

                }
            return false;


        }
      //获取主板型号、制造商（baseboard）
        public bool GetBaseboard(ref string product, ref string productor)
        {
            wql = "SELECT * FROM Win32_BaseBoard";
            if(lianjie(wql))
            {
                try { query.Get(); }
                catch (Exception) { return false; }
        
                foreach (ManagementBaseObject mbo in query.Get())
                {
                    try
                    {
                        product = mbo["product"].ToString();
                    }
                    catch (Exception) { }
                    try
                    {
                        productor = mbo["Manufacturer"].ToString();
                    }
                    catch (Exception) { }
                }
                return true;
             }
            return false;

        }
      //获取系统管控是否正常(service)
        public bool GetServiceWatchClient(ref bool isWCInstalled)
        {
            wql = "SELECT * FROM Win32_Service";
            if (lianjie(wql))
            {
                try { query.Get(); }
                catch (Exception) { return false; }
                foreach (ManagementBaseObject mbo in query.Get())
                {
                    try
                    {
                        if (mbo["name"].ToString() == "WatchClient") 
                        { 
                            isWCInstalled = true;
                            return true; 
                        }
                    }
                    catch (Exception) { }
                }
                isWCInstalled = false;
                return true;
            }
            return false;
        }
      //获取MAC地址、ip地址(network)
        public bool GetMac(ref string mac,string ip)
        {
            wql = "SELECT * FROM Win32_NetworkAdapterConfiguration where ipenabled=true";
            if (lianjie(wql))
            {
                try { query.Get(); }
                catch (Exception) { return false; }
                foreach (ManagementBaseObject mbo in query.Get())
                {
                    try
                    {
                        string[] addresses = (string[])mbo["IPAddress"];
                        if (addresses[0].IndexOf(ip) != -1)
                            mac = mac + mbo["macaddress"].ToString();
                    }
                    catch (Exception) { }
                }

                return true;
            }
            return false;
            
           
        }


      //获取主机名、操作系统(operationsystem)
        public bool GetOperSys(ref string opersys,ref string compname,ref string operversion,ref string sysdriver,ref string sysdir)
        {
            wql = "SELECT * FROM Win32_OperatingSystem";
            if (lianjie(wql))
            {
                try { query.Get(); }
                catch (Exception) { return false; }
                foreach (ManagementBaseObject mbo in query.Get())
                {
                    try
                    {
                        opersys = mbo["caption"].ToString();
                    }
                    catch (Exception) { }
                    try
                    {
                        compname = mbo["csname"].ToString();
                    }
                    catch (Exception) { };
                    try
                    {
                        operversion = mbo["version"].ToString();
                    }
                    catch (Exception) { };
                    try
                    {
                        sysdriver = mbo["systemdrive"].ToString();
                    }
                    catch (Exception) { };
                    try
                    {
                        sysdir = mbo["systemdirectory"].ToString();
                    }
                    catch (Exception) { };
                }

                return true;
            }
            return false;
        
          
        }

      //获取当前用户名、型号(computersystem)
        public bool GetComSys(ref string username,ref string model)
        {
            wql = "SELECT * FROM Win32_ComputerSystem";
            if (lianjie(wql))
            {
                try { query.Get(); }
                catch (Exception) { return false; }
                
                foreach (ManagementBaseObject mbo in query.Get())
                {
                    try
                    {
                        username = mbo["username"].ToString();
                    }
                    catch (Exception) { }
                    try
                    {
                        model = mbo["Model"].ToString();
                    }
                    catch (Exception) { };
                }

                return true;
            }
            return false;

                
            

        }

        //显示器（WmiMonitorID）
        public bool GetwmiMonitorID(ref string[] SerialNumberID, ref string[] ManufacturerName, ref string[] UserFriendlyName, ref string[] Manufacture)
        {
            ManagementScope ms_wmi = new ManagementScope("\\\\" + this.ip + "\\root\\wmi", co);
            wql = "select * from WmiMonitorID";
            int i = 0;
            if(lianjie(wql, ms_wmi))
            {
                try
                {
                    if (query.Get().Count == 0) return true;
                }
                catch (Exception) { return true; }

                
                foreach (ManagementObject mbo in query.Get())
                {
                    try
                    {
                        for (int j = 0; j < ((UInt16[])mbo["SerialNumberID"]).Length; j++)
                        {
                            SerialNumberID[i] = SerialNumberID[i] + Convert.ToChar((BitConverter.GetBytes(((UInt16[])mbo["SerialNumberID"])[j]))[0]);
                        }

                    }
                    catch (Exception) { }
                    try
                    {
                        for (int j = 0; j < ((UInt16[])mbo["ManufacturerName"]).Length; j++)
                        {
                            ManufacturerName[i] = ManufacturerName[i] + Convert.ToChar((BitConverter.GetBytes(((UInt16[])mbo["ManufacturerName"])[j]))[0]);  
                        }
                    }
                    catch (Exception) { }
                    try
                    {
                        for (int j = 0; j < ((UInt16[])mbo["UserFriendlyName"]).Length; j++)
                        {
                            UserFriendlyName[i] = UserFriendlyName[i] + Convert.ToChar((BitConverter.GetBytes(((UInt16[])mbo["UserFriendlyName"])[j]))[0]);
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        string year = mbo["YearOfManufacture"].ToString();
                        double moth = Convert.ToDouble(mbo["WeekOfManufacture"]) / 4.2;
                        Manufacture[i] =year+"."+ Math.Round(moth).ToString();
                    }
                    catch (Exception) { }
                    i++;
                }
                return true;
            }
            return false;

        }
     //cpu信息
        public bool GetProcessor(ref string cpu_name,ref string cpu_hz)
        {
            wql = "select * from win32_processor";
            if (lianjie(wql))
            {
                try { query.Get(); }
                catch (Exception) { return false; }
                foreach (ManagementBaseObject mbo in query.Get())
                {
                    try 
                    {
                        if (cpu_name == "")
                        {
                            cpu_name = mbo["Name"].ToString();
                        }
                        else { cpu_name =cpu_name+ ";\n" + mbo["Name"].ToString(); }
                    }
                    catch (Exception) { }
                    UInt32 allpm = 0;
                    try
                    {
                        allpm = (UInt32)mbo["MaxClockSpeed"];
                        if (cpu_hz == "")
                        {
                            cpu_hz = (allpm / 1000.0).ToString() + "GHz";
                        }
                        else { cpu_hz =cpu_hz+";\n"+ (allpm / 1000.0).ToString() + "GHz"; }
                    }
                    catch (Exception) { }
                    return true;
                
                }
            }
            return false;
        }
    }
}
