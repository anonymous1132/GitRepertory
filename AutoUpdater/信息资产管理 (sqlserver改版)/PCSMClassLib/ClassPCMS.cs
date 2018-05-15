using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace PCSMClassLib
{
    public enum DeviceKind { 台式机,打印机,投影仪,扫描仪,传真机,复印机,显示器,笔记本,其他终端 }
   
    /// <summary>
    /// 员工类
    /// </summary>
    public class Employee
    {
        public string department { get; set; }
        public string gonghao { get; set; }
        public string name { get; set; }
        public string banzhu { get; set; }
        public string room { get; set; }
        public bool getdated { get; set; }
        private accessOper aco = new accessOper();
        public  Employee(string gonghao)
        {
        this.gonghao=gonghao;
        GetProperty();
        }

        //更换部门
        public void ChangeDepartment(string newdepartment) 
        { 
            if (this.gonghao == "")return; 
            GetProperty(); 
            this.department = newdepartment; 
            Save(); 
        }
       
        //更换班组
        public void ChangeBanzhu(string newbanzhu)
        {
            if (this.gonghao == "") return;
            GetProperty();
            this.banzhu = newbanzhu;
            Save(); 
        }
       
        //将人员信息上传或更新至数据库
        public void Save()
        {
            if (this.getdated == true)
            {
                string sql = "update employee set username ='" + this.name + "',department ='"+this.department+"',banzhu ='"+this.banzhu+"',room ='"+this.room+"' where gonghao='"+this.gonghao+"'";
                aco.getSomeData(sql);
            }
            if (this.getdated == false)
            {
                string sql = " insert into employee (gonghao,username,department,banzhu,room) values ('"+this.gonghao+"','"+ this.name + "','" + this.department + "','" + this.banzhu + "','" + this.room + "')";
                aco.getSomeData(sql);
            }
        }

        //获取数据库中的人员信息
        public void GetProperty()
        {
            string sql = "select * from employee where gonghao='"+this.gonghao+"'";
            aco.getSomeData(sql);
            if (aco.dt.Rows.Count == 0)
            {
                this.getdated = false;
                return;
            }
            this.name = aco.dt.Rows[0]["username"].ToString();
            this.department = aco.dt.Rows[0]["department"].ToString();
            this.banzhu = aco.dt.Rows[0]["banzhu"].ToString();
            this.room = aco.dt.Rows[0]["room"].ToString();
            this.getdated = true;
        }
    }

    /// <summary>
    /// 终端类
    /// </summary>
    public class EndingDevice
    {
        public string xinghao { get; set; }
        public string pinpai { get; set; }
        public string made_company { get; set; }
        public string SN { get; set; }
        public string made_date { get; set; }
        public bool getdated { get; set; }
        private accessOper aco = new accessOper();
       /// <summary>
       /// 资产编号
        /// </summary>
        public string assets_NO { get; set; }
        public Employee user = new Employee("");
        public DeviceKind devicekind { get; set; }
        public EndingDevice()
        { }
        public EndingDevice(string assets_NO)
        {
            this.assets_NO = assets_NO;
            GetProperty();
        }
        public EndingDevice(string assets_NO, Employee user)
        { 
            this.assets_NO = assets_NO;
            GetProperty();
            this.user = user;
            Save();
        }

        public void Save() 
        {
            if (this.getdated == true)
            {
                string sql = "update endingdevice set xinghao ='" + this.xinghao + "',pinpai ='" + this.pinpai + "',made_company ='" + this.made_company +"',SN ='"+this.SN+"',made_date ='"+this.made_date+"',user_gonghao ='"+this.user.gonghao+"',devicekind ='"+this.devicekind.ToString() + "' where assets_NO='" + this.assets_NO + "'";
                aco.getSomeData(sql);
            }
            if (this.getdated == false)
            {
                string sql = " insert into endingdevice (assets_NO,xinghao,pinpai,made_company,SN,made_date,user_gonghao,devicekind) values ('" + this.assets_NO + "','" + this.xinghao + "','" + this.pinpai + "','" + this.made_company + "','" + this.SN + "','" + this.made_date + "','" + this.user.gonghao + "','" + this.devicekind.ToString() + "')";
                aco.getSomeData(sql);
            }
        }

        public virtual void GetProperty()
        {
            string sql = "select * from endingdevice where assets_NO ='"+this.assets_NO+"'";
            aco.getSomeData(sql);
            if (aco.dt.Rows.Count == 0)
            {
                this.getdated = false;
                return;
            }
            this.devicekind = getDevicekind(aco.dt.Rows[0]["devicekind"].ToString());
            this.xinghao = aco.dt.Rows[0]["xinghao"].ToString();
            this.pinpai = aco.dt.Rows[0]["pinpai"].ToString();
            this.made_company = aco.dt.Rows[0]["made_company"].ToString();
            this.SN = aco.dt.Rows[0]["SN"].ToString();
            this.made_date = aco.dt.Rows[0]["made_date"].ToString();
            this.user = new Employee(aco.dt.Rows[0]["user_gonghao"].ToString());
            this.getdated = true;
        }

        private DeviceKind getDevicekind(string strdevice)
        {
            switch (strdevice)
            {
                case "台式机":
                    return DeviceKind.台式机;
                case "显示器":
                    return DeviceKind.显示器;
                case "笔记本":
                    return DeviceKind.笔记本;
                case "打印机":
                    return DeviceKind.打印机;
                case "复印机":
                    return DeviceKind.复印机;
                case "扫描仪":
                    return DeviceKind.扫描仪;
                case "投影仪":
                    return DeviceKind.投影仪;
                case "传真机":
                    return DeviceKind.传真机;
                default:
                    return DeviceKind.其他终端;
            }
        }

       
    }

    /// <summary>
    /// 台式机类：继承终端类
    /// </summary>
    public class Computer:EndingDevice
    {
        private bool getdated_self { get; set; }
        public string menmery { get; set; }
        public string hard { get; set; }
        public IPAddress ip { get; set; }
        public string mac { get; set; }
        public string cpu { get; set; }
        public string f_cpu { get; set; }
        private accessOper aco = new accessOper();

        public Computer(string assets_NO)
        {
            this.assets_NO = assets_NO;
            this.devicekind = DeviceKind.台式机;
            GetProperty();
        }
        //赋予用户
        public Computer(string assets_NO, Employee user):base(assets_NO,user)
        { }

        public new void Save()
        {
            base.Save();
            if (this.getdated_self == true)
            {
                string sql = "update computer set menmery ='" + this.menmery + "',hard ='" + this.hard + "',ip ='" + this.ip.ToString() + "',mac ='" + this.mac + "',cpu ='" + this.cpu + "',f_cpu ='" + this.f_cpu +  "' where assets_NO='" + this.assets_NO + "'";
                aco.getSomeData(sql);
            }
            if (this.getdated_self == false)
            {
                string sql = " insert into computer (assets_NO,menmery,hard,ip,mac,cpu,f_cpu) values ('" + this.assets_NO + "','" + this.menmery + "','" + this.hard + "','" + this.ip.ToString() + "','" + this.mac + "','" + this.cpu + "','" + this.f_cpu  + "')";
                aco.getSomeData(sql);
            }
        }
        public new void GetProperty()
        {
            base.GetProperty();
            string sql = "select * from computer where assets_NO ='" + this.assets_NO + "'";
            aco.getSomeData(sql);
            if (aco.dt.Rows.Count == 0)
            {
                this.getdated_self = false;
                return;
            }
            this.menmery = aco.dt.Rows[0]["menmery"].ToString();
            this.hard = aco.dt.Rows[0]["hard"].ToString();
            this.ip =IPAddress.Parse(aco.dt.Rows[0]["ip"].ToString());
            this.mac = aco.dt.Rows[0]["mac"].ToString();
            this.cpu = aco.dt.Rows[0]["cpu"].ToString();
            this.f_cpu = aco.dt.Rows[0]["f_cpu"].ToString();
            this.getdated_self = true;
        }
    }

    /// <summary>
    /// 笔记本电脑：继承终端类
    /// </summary>
    public class Notebook : EndingDevice
    {
        private bool getdated_self { get; set; }
        public string menmery { get; set; }
        public string hard { get; set; }
        public IPAddress ip { get; set; }
        public string mac { get; set; }
        public string cpu { get; set; }
        public string f_cpu { get; set; }
        private accessOper aco = new accessOper();

        public Notebook(string assets_NO)
        {
            this.assets_NO = assets_NO;
            this.devicekind = DeviceKind.笔记本;
            GetProperty();
        }

        public Notebook(string assets_NO, Employee user):base(assets_NO,user)
        { }

        public new void Save()
        {
            base.Save();
            if (this.getdated_self == true)
            {
                string sql = "update notebook set menmery ='" + this.menmery + "',hard ='" + this.hard + "',ip ='" + this.ip.ToString() + "',mac ='" + this.mac + "',cpu ='" + this.cpu + "',f_cpu ='" + this.f_cpu + "' where assets_NO='" + this.assets_NO + "'";
                aco.getSomeData(sql);
            }
            if (this.getdated_self == false)
            {
                string sql = " insert into notebook (assets_NO,menmery,hard,ip,mac,cpu,f_cpu) values ('" + this.assets_NO + "','" + this.menmery + "','" + this.hard + "','" + this.ip.ToString() + "','" + this.mac + "','" + this.cpu + "','" + this.f_cpu + "')";
                aco.getSomeData(sql);
            }
        }
        public new void GetProperty()
        {
            base.GetProperty();
            string sql = "select * from notebook where assets_NO ='" + this.assets_NO + "'";
            aco.getSomeData(sql);
            if (aco.dt.Rows.Count == 0)
            {
                this.getdated_self = false;
                return;
            }
            this.menmery = aco.dt.Rows[0]["menmery"].ToString();
            this.hard = aco.dt.Rows[0]["hard"].ToString();
            this.ip = IPAddress.Parse(aco.dt.Rows[0]["ip"].ToString());
            this.mac = aco.dt.Rows[0]["mac"].ToString();
            this.cpu = aco.dt.Rows[0]["cpu"].ToString();
            this.f_cpu = aco.dt.Rows[0]["f_cpu"].ToString();
            this.getdated_self = true;
        }
    }

    /// <summary>
    /// 打印机类：继承终端类
    /// </summary>
    public class Printer : EndingDevice
    {
        private bool getdated_self { get; set; }
        public IPAddress ip { get; set; }
        public string papersize { get; set; }
        public string printer_kind { get; set; }
        public string useage { get; set; }
        public bool iscolorful { get; set; }
        public string function { get; set; }
        private accessOper aco = new accessOper();

        public Printer(string assets_NO)
        {
            this.assets_NO = assets_NO;
            this.devicekind = DeviceKind.打印机;
            GetProperty();
        }

        public Printer(string assets_NO, Employee user)
            : base(assets_NO, user)
        { }

        public new void Save() 
        {
            base.Save();
            if (this.getdated_self == true)
            {
                string sql = "update printer set ip ='" + this.ip.ToString() + "',papersize ='" + this.papersize + "',printer_kind ='" + this.printer_kind + "',useage ='" + this.useage + "',iscolorful ='" + this.iscolorful + "',function ='" + this.function + "' where assets_NO='" + this.assets_NO + "'";
                aco.getSomeData(sql);
            }
            if (this.getdated_self == false)
            {
                string sql = " insert into printer (assets_NO,ip,papersize,printer_kind,useage,iscolorful,function) values ('" + this.assets_NO + "','" + this.ip.ToString() + "','" + this.papersize + "','" + this.printer_kind + "','" + this.useage + "','" + this.iscolorful + "','" + this.function + "')";
                aco.getSomeData(sql);
            }
        }

        public new void GetProperty() 
        {
            base.GetProperty();
            string sql = "select * from printer where assets_NO ='" + this.assets_NO + "'";
            aco.getSomeData(sql);
            if (aco.dt.Rows.Count == 0)
            {
                this.getdated_self = false;
                return;
            }
            this.ip = IPAddress.Parse(aco.dt.Rows[0]["ip"].ToString());
            this.papersize = aco.dt.Rows[0]["papersize"].ToString();
            this.printer_kind = aco.dt.Rows[0]["printer_kind"].ToString();
            this.useage = aco.dt.Rows[0]["useage"].ToString();
            this.iscolorful = Convert.ToBoolean(aco.dt.Rows[0]["iscolorful"].ToString());
            this.function = aco.dt.Rows[0]["function"].ToString();
            this.getdated_self = true;
        }
    }

    /// <summary>
    /// 显示器类：继承终端类
    /// </summary>
    public class Mornitor:EndingDevice
    {
        public Computer pc = new Computer("");
        private bool getdated_self { get; set; }
        private accessOper aco = new accessOper();

        public Mornitor(string assets_NO)
        {
            this.assets_NO = assets_NO;
            this.devicekind = DeviceKind.显示器;
            GetProperty();
        }

        public Mornitor(string assets_NO, Computer pc)
        {
            this.assets_NO = assets_NO;
            GetProperty();
            this.pc = pc;
            Save();
        }

        public new void Save()
        {
            if (this.pc != null)
            {
                pc.GetProperty();
                if (pc.getdated)
                { this.user = pc.user; }
            }
            base.Save();
            if (this.getdated_self == true)
            {
                string sql = "update mornitor set pc ='" + this.pc.assets_NO + "' where assets_NO='" + this.assets_NO + "'";
                aco.getSomeData(sql);
            }
            if (this.getdated_self == false)
            {
                string sql = " insert into mornitor (assets_NO,pc) values ('" + this.assets_NO + "','" + this.pc.assets_NO + "')";
                aco.getSomeData(sql);
            }
        }

        public new void GetProperty()
        {
            base.GetProperty();
            string sql = "select * from mornitor where assets_NO ='" + this.assets_NO + "'";
            aco.getSomeData(sql);
            if (aco.dt.Rows.Count == 0)
            {
                this.getdated_self = false;
                return;
            }
            this.pc = new Computer(aco.dt.Rows[0]["pc"].ToString());
            this.getdated_self = true;
        }
    }

}
