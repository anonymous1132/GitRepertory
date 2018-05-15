using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace LookUpIp.Classess
{

    public interface IHNIP
    {
         IPAddress ip
        {
            get;
            set;
        }

         int vlan
        {
            get;
            set;
        }

         IPAddress getway
        {
            get;
            set;
        }

          string mask
        {
            get;
            set;
        }

          bool isused
         {
             get;
             set;
         }

         void check_ip_ping();

    }

    public class HNIP:IHNIP
    {
        public IPAddress ip
        {
            get;
            set;
        }

        public int vlan
        {
            get;
            set;
        }

        public IPAddress getway
        {
            get;
            set;
        }

        public string mask
        {
            get;
            set;
        }

        public bool isused
        {
            get;
            set;
        }

        public HNIP()
        { }

        public void check_ip_ping()
        {
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(this.ip, 120);
            if (reply.Status == IPStatus.Success)
            {
                this.isused = true;
            }
            else
            {
                this.isused = false;
            }
        }
    
    }
    
}
