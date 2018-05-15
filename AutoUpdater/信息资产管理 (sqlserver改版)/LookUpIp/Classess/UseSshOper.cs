using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJComLibrary;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace LookUpIp.Classess
{
     public  class UseSshOper:SshOper
    {
         public UseSshOper(string ip,string username,string pass):base(ip,username,pass)
         { }

         public string old_arp;
         public string new_arp;
       
         public void get_old_arp(IPAddress ip)
         {
             string request = GetResault("do sh arp " + ip.ToString());
             string[] arry = Regex.Split(GetArpFromResquest(request), "\\s+", RegexOptions.IgnoreCase);
             if (arry.Length >= 4)
             { old_arp = arry[3]; }
             else
             { old_arp = string.Empty; }
         }

         public void get_new_arp(IPAddress ip)
         {
             string request = GetResault("do sh arp " + ip.ToString());
             string[] arry = Regex.Split(GetArpFromResquest(request), "\\s+", RegexOptions.IgnoreCase);
             if (arry.Length >= 4)
             { new_arp = arry[3]; }
             else
             { new_arp = string.Empty; }
         }

         public bool GetClose()
         {
             ssh_close();
             return false;
         }

        public string GetArpFromResquest(string request)
         {
             try
             {
                 string str1 = "Internet";
                 string str2 = "  ARPA";
                 string tempstr = request.Substring(request.IndexOf(str1), request.IndexOf(str2) - request.IndexOf(str1));
                 return tempstr;
             }
             catch (Exception)
             {
                 return string.Empty;
             }
         }
    }

    public class AutoArp
    {
        public static HNIP hnip;
        public static UseSshOper ussha;
        public static UseSshOper usshb;
        public static string message;
        public static ManualResetEvent Adone = new ManualResetEvent(false);
        public static ManualResetEvent Bdone = new ManualResetEvent(false);
        public static ManualResetEvent Pingdone = new ManualResetEvent(false);
        public static ManualResetEvent Alldone = new ManualResetEvent(false);
        public static bool bingding(IPAddress ip)
        {
            hnip = new HNIP_InNet_Common(ip);
            if (!((HNIP_InNet_Common)hnip).isinclude)
            {
                hnip = new HNIP_InNet_Sub(ip);
                if (!((HNIP_InNet_Sub)hnip).isinclude)
                {
                    message = "非海宁内网IP";
                    return false;
                }
            }
            Thread a = new Thread(conna);
            a.Start();
            Thread b = new Thread(connb);
            b.Start();

            Adone.WaitOne();
            Bdone.WaitOne();
            for (int i = 0; i < 15; i++)
            {
                hnip.check_ip_ping();
                Console.Write(i);
                if (hnip.isused) break;
            }
            Adone.Reset();
            Bdone.Reset();
            Pingdone.Set();
            Adone.WaitOne();
            Bdone.WaitOne();
            Alldone.Set();
          
            return true;
        
        }

        public static void backip(IPAddress ip)
        {
            hnip = new HNIP_InNet_Common(ip);
            if (!((HNIP_InNet_Common)hnip).isinclude)
            {
                hnip = new HNIP_InNet_Sub(ip);
                if (!((HNIP_InNet_Sub)hnip).isinclude)
                {
                    throw new Exception("非海宁公司内网IP");
                }
            }
            try
            {
                Thread a = new Thread(arpshutdowna);
                a.Start();
                Thread b = new Thread(arpshutdownb);
                b.Start();
            }
            catch (Exception e)
            { throw new Exception(e.Message); }
        }

        private static void arpshutdowna()
        {
            IPAddress ip = hnip.ip;
            ussha = new UseSshOper("172.30.221.251", "hnep", "itxps");
            if (!ussha.ssh_logon()) { throw new Exception("7604A连接失败");}
            ussha.Command("arp " + ip.ToString()+" aaaa.aaaa.aaaa arpa");
            ussha.ssh_close();
        }
        private static void arpshutdownb()
        {
            IPAddress ip = hnip.ip;
            usshb = new UseSshOper("172.30.221.252", "hnep", "itxps");
            if (!usshb.ssh_logon()) { throw new Exception("7604B连接失败"); }
            usshb.Command("arp " + ip.ToString() + " aaaa.aaaa.aaaa arpa");
            usshb.ssh_close();
        }

        private static void conna()
        {
            IPAddress ip = hnip.ip;
            ussha = new UseSshOper("172.30.221.251", "hnep", "itxps");
            if (!ussha.ssh_logon()) return;
            ussha.get_old_arp(ip);
            ussha.Command("no arp " + ip.ToString());
            Adone.Set();
            Pingdone.WaitOne();
            ussha.get_new_arp(ip);
            Adone.Set();
            Alldone.WaitOne();
            if (string.IsNullOrEmpty(ussha.new_arp + usshb.new_arp) || (ussha.new_arp.IndexOf(".") + usshb.new_arp.IndexOf(".")) == -2)
            {
                message = message + "未获取IP对应MAC，请检查设备网络连接是否正常\n";
                ussha.Command("arp " + ip.ToString() + " " + ussha.old_arp + " arpa");
            }
            else
            {
                string new_arp = string.IsNullOrEmpty(ussha.new_arp) ? usshb.new_arp : ussha.new_arp;
                ussha.Command("arp " + ip.ToString() + " " + new_arp + " arpa");
                ussha.Command("do wr");
                message = message + "绑定成功";
            }
            ussha.ssh_close();
        }

        private static void connb()
        {
            IPAddress ip = hnip.ip;
            usshb = new UseSshOper("172.30.221.252", "hnep", "itxps");
            if (!usshb.ssh_logon()) return;
            usshb.get_old_arp(ip);
            usshb.Command("no arp " + ip.ToString());
            Bdone.Set();
            Pingdone.WaitOne();
            usshb.get_new_arp(ip);
            Bdone.Set();
            Alldone.WaitOne();
            if (string.IsNullOrEmpty(ussha.new_arp + usshb.new_arp) || (ussha.new_arp.IndexOf(".") + usshb.new_arp.IndexOf(".")) == -2)
            {
                usshb.Command("arp " + ip.ToString() + " " + usshb.old_arp + " arpa");
            }
            else
            {
                string new_arp = string.IsNullOrEmpty(ussha.new_arp) ? usshb.new_arp : ussha.new_arp;
                usshb.Command("arp " + ip.ToString() + " " + new_arp + " arpa");
                usshb.Command("do wr");
            }
            usshb.ssh_close();
        }

    }




}
