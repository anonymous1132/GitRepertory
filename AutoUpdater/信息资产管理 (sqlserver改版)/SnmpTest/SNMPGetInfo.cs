using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using CJComLibrary;

namespace SnmpTest
{
  public  class SNMPGetInfo
    {
        const string top_netremark = "1.3.6.1.2.1.4.20.1.2";
        const string top_macoid = "1.3.6.1.2.1.3.1.1.2";
        const string top_portmark = "1.3.6.1.2.1.17.4.3.1.2";
        const string h3c_portmark = "1.3.6.1.2.1.17.7.1.2.2.1.2";
        const string top_portid = "1.3.6.1.2.1.17.1.4.1.2";
        const string top_portname = "1.3.6.1.2.1.31.1.1.1.1";
        const string top_neib = "1.3.6.1.4.1.9.9.23.1.2.1.1.4";
        const string ciscovlan = "1.3.6.1.4.1.9.9.68.1.2.2.1.2";
        const string h3cvlan = "1.3.6.1.2.1.17.7.1.4.5.1.1";
        const string top_setmac = "1.3.6.1.2.1.4.22.1.2";
        const string top_setarptype = "1.3.6.1.2.1.4.22.1.4";
        SnmpOper sno = new SnmpOper();
       Dictionary<string, string> dicliwai=new Dictionary<string,string>();
       List<string> h3clist=new List<string>();
       public List<string[]> portiplist = new List<string[]>();

        string GetNetMark(HNIP IP,string centreswitch="172.30.221.251")
        {
            string oid = top_netremark + "." + IP.getway_A.ToString();
            string mark = sno.GetSingleString(centreswitch, oid);
            if (sno.erro_status != 0) { return null; }
            return mark;
        }

       public string GetMac(HNIP IP)
        {
            if (IP.ip == null)
                return null;
           string oid = top_macoid + "." +GetNetMark(IP) + ".1." + IP.ip.ToString();
            string mac = sno.GetSingleString("172.30.221.251",oid);
            if (sno.erro_status != 0) { return null; }
            return mac;
        }

       public string GetCheckMac(HNIP IP)
       {
           if (IP.ip == null)
               return null;
           string oid = top_setmac + "." + GetNetMark(IP) + "." + IP.ip.ToString();
           string mac = sno.GetSingleString("172.30.221.251", oid);
           if (sno.erro_status != 0) { return null; }
           return mac;
       }

       //public string SetArpMAC(HNIP IP,string MAC,string centreswitch="172.30.221.251")
       //{
       //    if (IP.ip == null)
       //        return "IP error";
       //   // StringOper sto=new StringOper();
       //    string oid = top_setmac + "." + GetNetMark(IP,centreswitch) + "." + IP.ip.ToString();
       //    sno.SetOctecString(centreswitch,oid,MAC);
       //    if (sno.erro_status == 0) return "done";
       //    else return "something wrong";
       //}

      //ivalue=4 静态绑定，3动态
       public string SetArpType(HNIP IP, Int32 ivalue=4)
       {
           if (IP.ip == null)
               return "IP error";
           string oid = top_setarptype + "." + GetNetMark(IP)+"." + IP.ip.ToString();
           sno.SetInteger("172.30.221.251", oid, ivalue);
           if (sno.erro_status == 0) return "done";
           else return "something wrong";
       }

       string GetPortMark(IPAddress switchip, HNIP IP)
       {
           if (IP.ip == null)
               return null;
           StringOper sto = new StringOper();
           string mac_ten = GetMac(IP);
           mac_ten = sto.MACFormat_ten(mac_ten);
           string oid = top_portmark + "." + mac_ten;
           string key = "Hndl314400@" + IP.vlan.ToString();
           string portmark = sno.GetSingleString(switchip.ToString(),oid,key);
           return portmark;
       }

       string GetPortMark_H3C(IPAddress switchip, HNIP IP)
       {
           if (IP.ip == null)
               return null;
           StringOper sto = new StringOper();
           string mac_ten = GetMac(IP);
           mac_ten = sto.MACFormat_ten(mac_ten);
           string oid = h3c_portmark + "." +IP.vlan.ToString()+"."+ mac_ten;
           string portmark = sno.GetSingleString(switchip.ToString(), oid);
           return portmark;
       }

    public  string GetPortID(IPAddress switchip,HNIP IP)
       {
           if (IP.ip == null)
               return null;
           string oid = top_portid + "." + GetPortMark(switchip, IP);
           string key = "Hndl314400@" + IP.vlan.ToString();
           string portid = sno.GetSingleString(switchip.ToString(),oid,key);
           return portid;
      }
    public string GetPortID_H3C(IPAddress switchip,HNIP IP)
    {
        if (IP.ip == null)
            return null;
        string oid = top_portid + "." + GetPortMark_H3C(switchip, IP);
        string portid = sno.GetSingleString(switchip.ToString(), oid);
        return portid;
    }

    public string GetPortName(IPAddress switchip, HNIP IP)
      {
          if (IP.ip == null)
              return null;
          string oid = top_portname + "." + GetPortID(switchip, IP);
          string portname = sno.GetSingleString(switchip.ToString(),oid);
          return portname;
      }

    public string GetPortName_H3C(IPAddress switchip, HNIP IP)
    {
        if (IP.ip == null)
            return null;
        string oid = top_portname + "." + GetPortID_H3C(switchip, IP);
        string portname = sno.GetSingleString(switchip.ToString(), oid);
        return portname;
    }

    public string GetPortVlan(IPAddress switchip, HNIP IP)
    {
        if (IP.ip == null)
            return null;
        string oid = ciscovlan + "." + GetPortID(switchip, IP);
        string portvlan = sno.GetSingleString(switchip.ToString(), oid);
        return portvlan;
    }

    public string GetPortVlan_H3C(IPAddress switchip, HNIP IP)
    {
        if (IP.ip == null)
            return null;
        string oid = h3cvlan + "." + GetPortID_H3C(switchip, IP);
        string portvlan = sno.GetSingleString(switchip.ToString(), oid);
        return portvlan;
    }

    private string getNeibor(IPAddress switchip, HNIP IP)
    {
        if (IP.ip == null)
            return null;
        string oid = top_neib + "." + GetPortID(switchip, IP);
        string nei = sno.GetBulkString(switchip.ToString(), oid);
        StringOper sto = new StringOper();
        nei = sto.IP_ten(nei);
        return nei;
    }

      public string GetNextHop(IPAddress switchip,HNIP IP)
      {

          if (IP.ip == null)
              return null;
        
         string  portname="";
         string portvlan = "";


         if (h3clist.Contains(switchip.ToString()))
         {
             portvlan = GetPortVlan_H3C(switchip, IP);
             portname = GetPortName_H3C(switchip, IP);
         }
         else { portvlan = GetPortVlan(switchip, IP);
            portname = GetPortName(switchip, IP);}


         if (portvlan == IP.vlan.ToString())
         {
             portiplist.Add(new string[]{switchip.ToString(),portname});
             return null;
         }
   
         string check = switchip.ToString()+portname;
         string nexthop="";
         if (dicliwai.ContainsKey(check))
         { 
         nexthop=dicliwai[check];
         }
           else
          {
              nexthop = getNeibor(switchip,IP);
          }
         // Console.WriteLine(check+nexthop);
          portiplist.Add(new string[]{switchip.ToString(),portname});
          return nexthop;
      }

      public string[] GetEndPort(IPAddress switchip, HNIP IP)
      {
          portiplist = new List<string[]>();
          if (IP.ip == null)
              return null ;
          string nexthop = GetNextHop(switchip,IP);
          int i=0;
          while(nexthop !=null && i<=15)
          {
              nexthop = GetNextHop(IPAddress.Parse(nexthop),IP);
              i++;
          }
          
          string[] arry = portiplist[portiplist.Count-1];
          return arry;
      }


       public  void SetDic()
      {
          dicliwai.Add("172.30.221.252Gi2/7","172.30.221.249");
          dicliwai.Add("172.30.221.110Gi1/0/3","172.30.221.226");
          dicliwai.Add("172.30.221.225Gi1/0/1","172.30.221.223");
          dicliwai.Add( "172.30.221.41Gi1/0/1" , "172.30.221.142");
          dicliwai.Add("172.30.221.249Gi1/1" , "172.30.221.249");
          dicliwai.Add("172.30.221.249Fa2/3" , "172.30.221.9");
          dicliwai.Add("172.30.221.249Gi3/1" , "172.30.221.171");
          dicliwai.Add( "172.30.221.9Fa0/24", "172.30.221.249");
          dicliwai.Add( "172.30.221.171Gi0/1" , "172.30.221.249");
          dicliwai.Add( "172.30.221.226GigabitEthernet1/0/25" , "172.30.221.110");
          dicliwai.Add( "172.30.221.226GigabitEthernet1/0/26" , "172.30.221.26");
          dicliwai.Add( "172.30.221.26Gi0/4" , "172.30.221.226");
          dicliwai.Add("172.30.221.223GigabitEthernet1/0/26" , "172.30.221.225");
          dicliwai.Add( "172.30.221.142GigabitEthernet1/0/25" , "172.30.221.41");
          dicliwai.Add( "172.30.221.142GigabitEthernet1/0/26" , "172.30.221.218");
          dicliwai.Add("172.30.221.218Gi1/0/2" , "172.30.221.142");
          h3clist.Add("172.30.221.226");
          h3clist.Add("172.30.221.142");
          h3clist.Add("172.30.221.223");
      }
    }
}
