using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tamir.SharpSsh;

namespace CJComLibrary
{
   public class SshOper
    {
       private string username;
       private string password;
       private string ip;
       public SshStream ssh;
       public string secret = "Hndl2007";

       public SshOper(string ip,string username,string password)
       {
           if (!ssh_con(ip, username, password)) return;
           this.ip = ip;
           this.username = username;
           this.password = password;
       }

       private bool ssh_con(string ip, string username, string password)
       {
           try
           {
               ssh = new SshStream(ip, username, password);
               return true;
           }
           catch (Tamir.SharpSsh.jsch.JSchException)
           {
               return false;
           }
       }

       public bool ssh_con()
       {
           return ssh_con(ip,username,password);
       }

       public bool Arp_7604(string arp_ip,string arp_mac)
       {
           if (!ssh_logon())
           { return false; }
           
           string arp_command = "arp " + arp_ip + " " + arp_mac + " arpa";
           Command(arp_command);
           Command("end");
           arp_command = "sh arp " + arp_ip;
          string arp_query= GetResault(arp_command);
          Command("wr");
          if (arp_query.IndexOf(arp_mac) > 0) return true;
          else return false;
           
       }

       public string GetResault(string command, string prompt="#")
       {
           ssh.Write(command);
           ssh.Flush();
           ssh.Prompt = prompt;
           return ssh.ReadResponse();
       }

       public void Command(string command, string prompt = "#")
       {
           ssh.Write(command);
           ssh.Flush();
           ssh.Prompt = prompt;
           ssh.ReadResponse();
       }

       public void ssh_close()
       {
           ssh.Close();
       }

       public bool ssh_logon()
       {
           if (ssh == null)
           { return false; }
           try
           {
               ssh.Prompt = ">";
               ssh.RemoveTerminalEmulationCharacters = true;
               //writing to the ssh channel
               ssh.ReadResponse();
               ssh.Write("en");
               ssh.Flush();
               ssh.Prompt = ":";
               ssh.ReadResponse();
               //secret
               Command(secret);
               Command("conf t");
               return true;
           }
           catch (Exception)
           { return false; }
       }


    }
}
