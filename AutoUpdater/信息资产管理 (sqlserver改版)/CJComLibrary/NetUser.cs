using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Collections;

namespace CJComLibrary
{
   public class NetUser 
    {
           //创建用户
        [DllImport("Netapi32.dll")]
        extern static int NetUserAdd([MarshalAs(UnmanagedType.LPWStr)] string sName, int Level, ref USER_INFO_1 buf, int parm_err);
        //修改用户密码
        [DllImport("Netapi32.dll")]
        extern static int NetUserChangePassword([MarshalAs(UnmanagedType.LPWStr)] string sName,[MarshalAs(UnmanagedType.LPWStr)] string UserName,[MarshalAs(UnmanagedType.LPWStr)] string OldPassword,[MarshalAs(UnmanagedType.LPWStr)] string NewPassword);
        //删除用户
        [DllImport("Netapi32.dll")]
        extern static int NetUserDel([MarshalAs(UnmanagedType.LPWStr)] string sName,[MarshalAs(UnmanagedType.LPWStr)] string UserName);
        //枚举全部用户
        [DllImport("Netapi32.dll")]
        extern static int NetUserEnum([MarshalAs(UnmanagedType.LPWStr)] string sName,int Level,int filter,out IntPtr bufPtr,int Prefmaxlen,out int Entriesread,out int Totalentries,out int Resume_Handle);
        //获取用户信息
        [DllImport("Netapi32.dll")]
        extern static int NetUserGetInfo([MarshalAs(UnmanagedType.LPWStr)] string sName,[MarshalAs(UnmanagedType.LPWStr)] string UserName,int Level,out IntPtr intptr);
        //获取用户所在本地组
        [DllImport("Netapi32.dll")]
        extern static int NetUserGetLocalGroups([MarshalAs(UnmanagedType.LPWStr)] string sName,[MarshalAs(UnmanagedType.LPWStr)] string UserName,int Level,int Flags,out IntPtr intptr,int Prefmaxlen,out int Entriesread,out int Totalentries);
        //修改用户信息
        [DllImport("Netapi32.dll")]
        extern static int NetUserSetInfo([MarshalAs(UnmanagedType.LPWStr)] string sName, [MarshalAs(UnmanagedType.LPWStr)] string UserName, int Level, ref USER_INFO_1 bufptr, int parm_err);
        //开释API
        [DllImport("Netapi32.dll")]
        extern static int NetApiBufferFree(IntPtr Buffer);
        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode)]
        public extern static int NetLocalGroupGetMembers([MarshalAs(UnmanagedType.LPWStr)] string servername, [MarshalAs(UnmanagedType.LPWStr)] string localgroupname, int level, out IntPtr bufptr, int prefmaxlen, out int entriesread, out int totalentries, out int resume_handle);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct LOCALGROUP_USERS_INFO_0
        {
            public string GroupName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct USER_INFO_1
        {
            public string sName;    //用户名
            public string sPass;    //用户密码
            public int PasswordAge; //密码级别
            public int sPriv;       //帐户类型 1
            public string sHomeDir; //用户主目录 null
            public string sComment; //用户描写
            public int sFlags;      //用户权限
            public string sScriptPath;  //登陆脚本门路 null
        }

        public struct LOCALGROUP_MEMBERS_INFO_2
        {
            public int lgrmi2_sid;
            public int lgrmi2_sidusage;
            public string lgrmi2_domainaname;
        }
        //枚举全部用户
        public string UserEnum()
        {
            string tempStr = "<?xml version=\"1.0\" encoding=\"gb2312\" ?>\r\n";
            tempStr += "<INFO>\r\n";
            int Entriesread;
            int TotalEntries;
            int Resume_Handle;
            IntPtr bufPtr;

            if (NetUserEnum(null, 1, 0, out bufPtr, -1, out Entriesread, out TotalEntries, out Resume_Handle) != 0)
            {
                throw (new Exception("枚举全部用户失败"));
            }
            if (Entriesread > 0)
            {
                USER_INFO_1[] UserInfo = new USER_INFO_1[Entriesread];
                IntPtr iter = bufPtr;
                for (int i = 0; i < Entriesread; i++)
                {
                    UserInfo[i] = (USER_INFO_1)Marshal.PtrToStructure(iter, typeof(USER_INFO_1));
                    iter = (IntPtr)((int)iter + Marshal.SizeOf(typeof(USER_INFO_1)));
                    tempStr += "<ITEM value=\"" + UserInfo[i].sComment + "\">" + UserInfo[i].sName + "</ITEM>\r\n";
                }
                tempStr += "</INFO>";
            }
            NetApiBufferFree(bufPtr);
            return tempStr;
        }
        //读取用户信息
        public string UserGetInfo(string UserName)
        {
            string tmpStr = "<?xml version=\"1.0\" encoding=\"gb2312\" ?>\r\n";
            tmpStr += "<INFO>\r\n";
            IntPtr bufPtr;
            USER_INFO_1 UserInfo = new USER_INFO_1();
            if (NetUserGetInfo(null, UserName.ToString(), 1,out bufPtr) != 0)
            {
                throw (new Exception("读取用户信息失败"));
            }
            else
            {
                UserInfo = (USER_INFO_1)Marshal.PtrToStructure(bufPtr, typeof(USER_INFO_1));
                tmpStr += "<NAME>" + UserInfo.sName + "</NAME>\r\n";
                tmpStr += "<PASS>" + UserInfo.sPass + "</PASS>\r\n";
                tmpStr += "<DESC>" + UserInfo.sComment + "</DESC>\r\n";
                tmpStr += "</INFO>";
                NetApiBufferFree(bufPtr);
                return tmpStr;
            }
        }
        //删除用户
        public bool UserDelete(string UserName)
        {
            if (NetUserDel(null, UserName.ToString()) != 0)
            {
                throw (new Exception("删除用户失败"));
            }
            else
            {
                return true;
            }
        }

        //修改用户信息
        public bool UserSetInfo(string UserName,string NewUserName, string UserPass, string sDescription)
        {
            USER_INFO_1 UserInfo = new USER_INFO_1();
            UserInfo.sName = NewUserName.ToString();
            UserInfo.sPass = UserPass.ToString();
            UserInfo.PasswordAge = 0;
            UserInfo.sPriv = 1;
            UserInfo.sHomeDir = null;
            UserInfo.sComment = sDescription.ToString();
            UserInfo.sFlags = 0x10040;
            UserInfo.sScriptPath = null;
            if (NetUserSetInfo(null, UserName.ToString(), 1, ref UserInfo, 0) != 0)
            {
                throw (new Exception("用户信息修改失败"));
            }
            else
            {
                return true;
            }
        }
                    
        //创建系统用户
        public bool UserAdd(string UserName, string UserPass,string sDescription)
        {
            USER_INFO_1 UserInfo = new USER_INFO_1();
            UserInfo.sName = UserName.ToString();
            UserInfo.sPass = UserPass.ToString();
            UserInfo.PasswordAge = 0;
            UserInfo.sPriv = 1;
            UserInfo.sHomeDir = null;
            UserInfo.sComment = sDescription.ToString();
            //UserInfo.sFlags = 0x0040;
            UserInfo.sFlags = 0x10040;
            UserInfo.sScriptPath = null;
            if (NetUserAdd(null, 1, ref UserInfo, 0) != 0)
            {
                throw (new Exception("创立系统用户失败"));
            }
            else
            {
                return true;
            }
        }

        //修改用户密码
        public bool UserChangePassword(string UserName, string OldPassword, string NewPassword)
        {
            if (NetUserChangePassword(null, UserName.ToString(), OldPassword.ToString(), NewPassword.ToString()) != 0)
            {
                throw (new Exception("修正体系用户密码失败"));
            }
            else
            {
                return true;
            }
        }
        //获取用户全体所在本地组
        public string UserGetLocalGroups(string UserName)
        {
            int EntriesRead;
            int TotalEntries;
            IntPtr bufPtr;
            string tempStr = "<?xml version=\"1.0\" encoding=\"gb2312\" ?>\r\n";
            tempStr += "<INFO>\r\n";
            if (NetUserGetLocalGroups(null, UserName.ToString(), 0, 0, out bufPtr, 1024, out EntriesRead, out TotalEntries) != 0)
            {
                throw (new Exception("读取用户所在本地组失败"));
            }
            if (EntriesRead > 0)
            {
                LOCALGROUP_USERS_INFO_0[] GroupInfo = new LOCALGROUP_USERS_INFO_0[EntriesRead];
                IntPtr iter = bufPtr;
                for (int i = 0; i < EntriesRead; i++)
                {
                    GroupInfo[i] = (LOCALGROUP_USERS_INFO_0)Marshal.PtrToStructure(iter, typeof(LOCALGROUP_USERS_INFO_0));
                    iter = (IntPtr)((int)iter + Marshal.SizeOf(typeof(LOCALGROUP_USERS_INFO_0)));
                    tempStr += "<GROUP>" + GroupInfo[i].GroupName + "</GROUP>\r\n";
                }
                tempStr += "</INFO>";
                NetApiBufferFree(bufPtr);
            }
            return tempStr;
        }

       //获取用户组成员
       public  ArrayList GetLocalGroupMembers(string servername, string groupname)
        {
            ArrayList mylist = new ArrayList();
            int EntriesRead;
            int TotalEntries;
            int Resume;
            IntPtr bufPtr;
            NetLocalGroupGetMembers(servername, groupname, 2, out bufPtr, -1, out EntriesRead, out TotalEntries, out Resume);
            if (EntriesRead > 0)
            {
                LOCALGROUP_MEMBERS_INFO_2[] Members = new LOCALGROUP_MEMBERS_INFO_2[EntriesRead];
                IntPtr iter = bufPtr;
                for (int i = 0; i < EntriesRead; i++)
                {
                    Members[i] = (LOCALGROUP_MEMBERS_INFO_2)Marshal.PtrToStructure(iter, typeof(LOCALGROUP_MEMBERS_INFO_2));
                    iter = (IntPtr)((int)iter + Marshal.SizeOf(typeof(LOCALGROUP_MEMBERS_INFO_2)));
                    mylist.Add(Members[i].lgrmi2_domainaname + "," + Members[i].lgrmi2_sidusage);
                }
                NetApiBufferFree(bufPtr);
            }
            return mylist;
        }

       public  bool IsGroupMember(string groupname,string username)
       {

           ArrayList RetGroups = new ArrayList();
           RetGroups = GetLocalGroupMembers(null, groupname);
           string delimStr = ",";
           char[] delimiter = delimStr.ToCharArray();
           foreach (string str in RetGroups)
           {
               string[] split = null;
               split = str.Split(delimiter);
               if(split[0].ToUpper()==username.ToUpper())
               {
                   return true;
                
               }
           }
           return false;
       }
    }
}
