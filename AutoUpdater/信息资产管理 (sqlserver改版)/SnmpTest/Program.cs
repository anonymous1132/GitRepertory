using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnmpSharpNet;
using System.Net;
using CJComLibrary;

namespace SnmpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SNMPGetInfo sng = new SNMPGetInfo();
            sng.SetDic();
            HNIP ip = new HNIP(IPAddress.Parse("10.147.190.6"));
            //string result;
            //List<string[]> iplist = new List<string[]>();
            //IPAddress swichip = IPAddress.Parse("172.30.221.252");
            //sng.GetEndPort(swichip, ip);
            //iplist = sng.portiplist;
            //for (int i = 0; i < iplist.Count; i++)
            //{
            //    Console.WriteLine(string.Format("第{0}个网络节点：{1}，端口：{2}", i + 1, (iplist[i])[0], (iplist[i])[1]));
            //}

            //第二个例子
           // string mac = sng.GetMac(ip);
           // Console.WriteLine(mac);
           // result = sng.SetArpMAC(ip, "00 26 cb 50 72 40");
           //// result = sng.SetArpType(ip,2);
           //Console.WriteLine(result);
           // mac = sng.GetCheckMac(ip);
           // Console.WriteLine(mac);

            //临时测试
         //   StringOper sto = new StringOper();
        
         //string  str=sto.MACFormat("00 26 CB 50 72 40");
         //char[] a = new char[6];
         //for (int i = 10; i >= 0; i = i - 2)
         //{
         //     a[i / 2] = Convert.ToChar(str.Substring(i, 2)); 
             
         //}
         //Console.WriteLine(a[1]); 
               Console.ReadLine();
        }
    }
}
