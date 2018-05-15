using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace LookUpIp.Classess
{
    public class XmlHelper
    {
        //xml文件路径
        public  string filepath;

        //xmltestwriter写入xml对象
        public XmlTextWriter writer;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filepath">xml文件路径</param>
        public XmlHelper(string filepath)
        {
            this.filepath = filepath;
            //writer = new XmlTextWriter(filepath,null);
        }

        public void startWriteXml()
        {
            writer = new XmlTextWriter(filepath, null);
            //使用自动缩进便于阅读
            writer.Formatting = Formatting.Indented;
        }

        //写入根元素
        public void WriteStartElement(string startelementname)
        {
            writer.WriteStartElement(startelementname);
        }
        //写入属性
        public void WriteAttributeString(string attributename,string attributevalue)
        {
            writer.WriteAttributeString(attributename,attributevalue);
        }
        //写入子元素
        public void WriteElementString(string elementname,string elementvalue)
        {
            writer.WriteElementString(elementname,elementvalue);
        }
        //关闭根元素，并书写结束标签
        public void WriteEndElement()
        {
            writer.WriteEndElement();
        }

        //将XML写入文件并且关闭XmlTextWriter
        public void endXmlWriter()
        {
            writer.Close();
        }
        
        //获取满足条件的所有节点,nodetitle格式"/first/second"
        public XmlNodeList GetXmlNodeList(string nodetitle)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            return xmlDoc.SelectNodes(nodetitle);
        }

        public XmlNodeList GetXmlNodeList(XmlNode xn, string nodetitle)
        {
            return xn.SelectNodes(nodetitle);
        }
        //获取满足条件的第一个节点
        public XmlNode GetSingleXmlNode(string nodetitle)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            return xmlDoc.SelectSingleNode(nodetitle);
        }

        public XmlNode GetSingleXmlNode(XmlNode xn,string nodetitle)
        {
            return xn.SelectSingleNode(nodetitle);

        }

        //根据节点名获取节点值
        public bool GetValueByNames(XmlNode xn,ref string [][] arrystr )
        {
            try
            {
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xnc in xnl)
                {
                    XmlElement xe = (XmlElement)xnc;
                    for (int i = 0; i < arrystr[0].Length; i++)
                    {
                        if (xe.Name == arrystr[0][i])
                        {
                            arrystr[1][i] = xe.InnerText;
                            continue;
                        }
                    }

                }

                return true;
            }
            catch (Exception) { return false; }
        }

        //根据节点名设置节点值
        public bool SetValueByNames(XmlNode xn,string [][] arrystr)
        {
            try
            {
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xnc in xnl)
                {
                    XmlElement xe = (XmlElement)xnc;
                    for (int i = 0; i < arrystr[0].Length; i++)
                    {
                        if (xe.Name == arrystr[0][i])
                        {
                            xe.InnerText=arrystr[1][i];
                            continue;
                        }
                    }

                }

                return true;
            }
            catch (Exception) { return false; }
        }

    }
}
