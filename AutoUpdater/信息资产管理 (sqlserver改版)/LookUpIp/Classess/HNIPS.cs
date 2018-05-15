using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Xml;

namespace LookUpIp.Classess
{
    public class HNIP_InNet_Common:HNIP
    {
        public bool isinclude = false;
        public IPAddress getway_A;
        public IPAddress getway_B;
        public HNIP_InNet_Common(IPAddress ip)
        {
            checkip(ip,ref isinclude);
        }

        private void checkip(IPAddress ip, ref bool isinclude)
        {
            string filepath =System.IO.Directory.GetCurrentDirectory() + @"\xml\config.xml"; ;
            XmlHelper xml = new XmlHelper(filepath);
            XmlNodeList xnl = xml.GetXmlNodeList("/IP/HNInnerFirstIP/Vlan");
            string[][] arry = new string[2][] { new string[6], new string[6] };
            arry[0][0] = "getway";
            arry[0][1] = "getway_A";
            arry[0][2] = "getway_B";
            arry[0][3] = "startIP";
            arry[0][4] = "endIP";
            arry[0][5] = "mask";
            foreach (XmlNode xn in xnl)
            {
                if (xml.GetValueByNames(xn, ref arry))
                {
                   if (IP2long(ip) <= IP2long(IPAddress.Parse(arry[1][4])) && IP2long(ip) >= IP2long(IPAddress.Parse(arry[1][3])))
                    {
                        isinclude = true;
                        this.ip = ip;
                        this.getway_A = IPAddress.Parse(arry[1][1]);
                        this.getway_B = IPAddress.Parse(arry[1][2]);
                        this.getway = IPAddress.Parse(arry[1][0]);
                        this.mask = arry[1][5];
                        this.vlan =Convert.ToInt16(xn.Attributes[0].Value);
                        break;
                    }

                }
            }
        }
        private long IP2long(IPAddress ip)
        {
            string[] ipBytes;
            double num = 0;
            if (!string.IsNullOrEmpty(ip.ToString()))
            {
                ipBytes = ip.ToString().Split('.');
                for (int i = ipBytes.Length - 1; i >= 0; i--)
                {
                    num += ((int.Parse(ipBytes[i]) % 256) * Math.Pow(256, (3 - i)));
                }
            }
            return (long)num;
        }
    }

    public class HNIP_InNet_Sub : HNIP
    {
        public bool isinclude = false;
        public IPAddress getway_A;
        public IPAddress getway_B;
        public HNIP_InNet_Sub(IPAddress ip)
        {
            checkip(ip, ref isinclude);
        }
        private void checkip(IPAddress ip, ref bool isinclude)
        {
            string filepath = System.IO.Directory.GetCurrentDirectory() + @"\xml\config.xml"; ;
            XmlHelper xml = new XmlHelper(filepath);
            XmlNodeList xnl = xml.GetXmlNodeList("/IP/HNInnerSecondIP/Vlan");
            string[][] arry = new string[2][] { new string[6], new string[6] };
            arry[0][0] = "getway";
            arry[0][1] = "getway_A";
            arry[0][2] = "getway_B";
            arry[0][3] = "startIP";
            arry[0][4] = "endIP";
            arry[0][5] = "mask";
            foreach (XmlNode xn in xnl)
            {
                if (xml.GetValueByNames(xn, ref arry))
                {
                    if (IP2long(ip) <= IP2long(IPAddress.Parse(arry[1][4])) && IP2long(ip) >= IP2long(IPAddress.Parse(arry[1][3])))
                    {
                        isinclude = true;
                        this.ip = ip;
                        this.getway_A = IPAddress.Parse(arry[1][1]);
                        this.getway_B = IPAddress.Parse(arry[1][2]);
                        this.getway = IPAddress.Parse(arry[1][0]);
                        this.mask = arry[1][5];
                        this.vlan = Convert.ToInt16(xn.Attributes[0].Value);
                        break;
                    }

                }
            }
        }

        private long IP2long(IPAddress ip)
        {
            string[] ipBytes;
            double num = 0;
            if (!string.IsNullOrEmpty(ip.ToString()))
            {
                ipBytes = ip.ToString().Split('.');
                for (int i = ipBytes.Length - 1; i >= 0; i--)
                {
                    num += ((int.Parse(ipBytes[i]) % 256) * Math.Pow(256, (3 - i)));
                }
            }
            return (long)num;
        }

    }

    public class HNIP_InNet_Business : HNIP
    {
        public bool isinclude = false;
        public bool iscommon = false;
        public IPAddress getway_A;
        public IPAddress getway_B;
        public HNIP_InNet_Business(IPAddress ip)
        {
            checkip(ip, ref isinclude);

        }

        private void checkip(IPAddress ip, ref bool isinclude)
        {
            HNIP_InNet_Common hnip = new HNIP_InNet_Common(ip);
            if (hnip.isinclude)
            {
                this.ip = hnip.ip;
                isinclude = true;
                iscommon = true;
                getway = hnip.getway;
                getway_A = hnip.getway_A;
                getway_B = hnip.getway_B;
                mask = hnip.mask;
                vlan = hnip.vlan;
                return;
            }

            HNIP_InNet_Sub hnip_sub = new HNIP_InNet_Sub(ip);
            if (hnip_sub.isinclude)
            {
                this.ip = hnip_sub.ip;
                isinclude = true;
                iscommon = false;
                getway = hnip_sub.getway;
                getway_A = hnip_sub.getway_A;
                getway_B = hnip_sub.getway_B;
                mask = hnip_sub.mask;
                vlan = hnip_sub.vlan;
                return;
            }

            isinclude = false;
            
        }

    }
}
