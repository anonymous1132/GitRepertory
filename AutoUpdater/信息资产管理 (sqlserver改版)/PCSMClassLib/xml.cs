using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
//using System.Windows.Forms;

namespace PCSMClassLib
{
   public class xml
    {
        public static string DataSource;
        public static string InitialCatalog;
        public static string UserID;
        public static string Password;
        public static int verision_1;
        public static int verision_2;
        public static int verision_3;
        
         public static void createXml()
        {
            string path = Directory.GetCurrentDirectory() + @"\xml\data.xml";
             XmlTextWriter writer = new XmlTextWriter(path, null);
            //使用自动缩进便于阅读
            writer.Formatting = Formatting.Indented;
            //写入根元素
            writer.WriteStartElement("connections");
            writer.WriteStartElement("sqlconnection");
            //写入属性及属性的名字
            writer.WriteAttributeString("IntegratedSecurity", "false");
            //加入子元素
            writer.WriteElementString("DataSource", "caojin.zicp.net,20590");
            writer.WriteElementString("InitialCatalog", "caojin");
            writer.WriteElementString("UserID", "caojin_logon");
            writer.WriteElementString("Password", DESjiami.EncryptDES("Black5408*","black5408"));
            //关闭根元素，并书写结束标签
            writer.WriteEndElement();
            writer.WriteEndElement();
            //将XML写入文件并且关闭XmlTextWriter
            writer.Close();
        }
     
        public static void getxmlcontent()
        {
            string path = Directory.GetCurrentDirectory() + @"\xml\data.xml";
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                XmlNode xns = xmlDoc.SelectSingleNode("connections");

                XmlNodeList xnl = xns.ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;
                    XmlNodeList xnl2 = xe.ChildNodes;
                    foreach (XmlNode xn2 in xnl2)
                    {
                        XmlElement xe2 = (XmlElement)xn2;
                        if (xe2.Name == "DataSource")
                        { 
                            DataSource = xe2.InnerText;
                            continue;
                        }
                        if (xe2.Name == "InitialCatalog")
                        { 
                            InitialCatalog = xe2.InnerText;
                            continue;
                        }
                        if (xe2.Name == "UserID")
                        { 
                            UserID = xe2.InnerText;
                            continue;
                        }
                        if (xe2.Name == "Password")
                        { 
                            Password = DESjiami.DecryptDES(xe2.InnerText, "black5408");
                            continue;
                        }
                    }

                }
            }
            catch (Exception)
            {
               // MessageBox.Show("未找到‘" + path + "’数据文件！");
                return;
            }

            
        }
        public static void getversion()
        {
            string path = Directory.GetCurrentDirectory() + @"\xml\version.xml";
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                XmlNode xns = xmlDoc.SelectSingleNode("version");
                XmlNodeList xnl = xns.ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    if (xn.Name == "num1")
                    {
                        verision_1 = Convert.ToInt32(xn.InnerText);
                        continue;
                    }
                    if (xn.Name == "num2")
                    {
                        verision_2 = Convert.ToInt32(xn.InnerText);
                        continue;
                    }
                    if (xn.Name == "num3")
                    {
                        verision_3 = Convert.ToInt32(xn.InnerText);
                        continue;
                    }
                }
            }
            catch (Exception)
            {
              //  MessageBox.Show("未找到‘" + path + "’数据文件！");
            }
             
            
        }

        public static void updatexml(string datasource,string initialcatalog,string uid,string pwd)
        {
           //暂不考虑windows身份验证连接
            string path = Directory.GetCurrentDirectory() + @"\xml\data.xml";

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                XmlNode xns = xmlDoc.SelectSingleNode("connections");
                XmlNodeList xnl = xns.ChildNodes;
                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;
                    XmlNodeList xnl2 = xe.ChildNodes;
                    foreach (XmlNode xn2 in xnl2)
                    {
                        XmlElement xe2 = (XmlElement)xn2;
                        if (xe2.Name == "DataSource")
                        { xe2.InnerText = datasource; }
                        if (xe2.Name == "InitialCatalog")
                        { xe2.InnerText = initialcatalog; }
                        if (xe2.Name == "UserID")
                        { xe2.InnerText = uid; }
                        if (xe2.Name == "Password")
                        { xe2.InnerText = DESjiami.EncryptDES(pwd, "black5408"); }
                    }
                }
                xmlDoc.Save(path);
            }
            catch (Exception)
            {
               // MessageBox.Show("未找到‘" + path + "’数据文件！");
            }
        }
       
   
    }
}
