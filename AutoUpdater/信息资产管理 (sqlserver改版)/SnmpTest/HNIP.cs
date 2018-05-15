using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace SnmpTest
{
   public class HNIP
    {
        public IPAddress ip;
        public int vlan;
        public IPAddress getway;
        public IPAddress getway_A;
        public IPAddress getway_B;
        public string mask = "";
        public bool isinclude=false;
        public HNIP(IPAddress ip)
        {
            string strip = ip.ToString();
            string[] arry = strip.Split('.');
            if (arry.Length != 4 || arry[0] != "10" || arry[1] !="147")
            {
                this.ip = null;
                isinclude = false;
                return;
            }
            if (Convert.ToInt16(arry[2]) < 184 || Convert.ToInt16(arry[2]) > 191)
            {
                this.ip = null;
                isinclude = false;
                return;
            }
            this.ip = ip;
            switch (arry[2])
            { 
                case "184":
                    this.isinclude = true;
                    this.getway = IPAddress.Parse("10.147.184.1");
                    this.getway_A = IPAddress.Parse("10.147.184.251");
                    this.getway_B = IPAddress.Parse("10.147.184.252");
                    this.mask = "255.255.255.0";
                    vlan = 20;
                    break;
                case "185":
                    if (Convert.ToInt16(arry[3]) < 128)
                    {
                        this.getway = IPAddress.Parse("10.147.185.1");
                        this.getway_A = IPAddress.Parse("10.147.185.2");
                        this.getway_B = IPAddress.Parse("10.147.185.3");
                        this.mask = "255.255.255.128";
                        vlan = 3;
                    }
                    else
                    {
                        this.getway = IPAddress.Parse("10.147.185.129");
                        this.getway_A = IPAddress.Parse("10.147.185.251");
                        this.getway_B = IPAddress.Parse("10.147.185.252");
                        this.mask = "255.255.255.128";
                        vlan = 4;
                    
                    }
                    break;
                case "186":
                    this.getway = IPAddress.Parse("10.147.186.254");
                        this.getway_A = IPAddress.Parse("10.147.186.251");
                        this.getway_B = IPAddress.Parse("10.147.186.252");
                        this.mask = "255.255.255.0";
                        vlan = 2;
                    break;
                case "187":
                    this.getway = IPAddress.Parse("10.147.187.1");
                        this.getway_A = IPAddress.Parse("10.147.187.251");
                        this.getway_B = IPAddress.Parse("10.147.187.252");
                        this.mask = "255.255.255.0";
                        vlan = 6;
                    break;
                case "188":
                    this.getway = IPAddress.Parse("10.147.188.1");
                        this.getway_A = IPAddress.Parse("10.147.188.251");
                        this.getway_B = IPAddress.Parse("10.147.188.252");
                        this.mask = "255.255.255.0";
                        vlan = 10;
                    break;
                case "189":
                    this.getway = IPAddress.Parse("10.147.189.254");
                        this.getway_A = IPAddress.Parse("10.147.189.252");
                        this.getway_B = IPAddress.Parse("10.147.189.251");
                        this.mask = "255.255.255.0";
                        vlan = 40;
                    break;
                case "190":
                    this.getway = IPAddress.Parse("10.147.190.254");
                        this.getway_A = IPAddress.Parse("10.147.190.252");
                        this.getway_B = IPAddress.Parse("10.147.190.251");
                        this.mask = "255.255.255.0";
                        vlan = 50;
                    break;
                case "191":
                     this.getway = IPAddress.Parse("10.147.191.254");
                        this.getway_A = IPAddress.Parse("10.147.191.252");
                        this.getway_B = IPAddress.Parse("10.147.191.251");
                        this.mask = "255.255.255.0";
                        vlan = 60;
                    break;

            }
        }


    }
}
