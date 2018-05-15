using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnmpSharpNet;
using System.Net;

namespace CJComLibrary
{
  public  class SnmpOper
    {
      private const string _key = "Hndl314400";
      public int erro_status = 0;

       public string GetSingleString(string agentip,string oid,string key=_key)
        {
            OctetString community = new OctetString(key);
            AgentParameters param = new AgentParameters(community);
            param.Version = SnmpVersion.Ver2;
            IpAddress agent = new IpAddress(agentip);
            UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
            Pdu pdu = new Pdu(PduType.Get);
            pdu.VbList.Add(oid);
            SnmpV2Packet result = (SnmpV2Packet)target.Request(pdu, param);
           
            string info = "";
            if (result != null)
            {
                //ErrorStatus other than 0 is an error returned by the agent -see SnmpConstants for error definitions
                if (result.Pdu.ErrorStatus != 0)
                {
                    erro_status = result.Pdu.ErrorStatus;
                   // erro_index = result.Pdu.ErrorIndex;
                    info = "no such oid";
                }
                else
                {
                    info = result.Pdu.VbList[0].Value.ToString();
                    erro_status = 0;
                }
            }
            else
            {
                info = "no response";
                erro_status = -1;
            }
            target.Dispose();
            return info;
        }

       public string GetBulkString(string agentip, string oid, string key = _key)
       {
           OctetString community = new OctetString(key);
           AgentParameters param = new AgentParameters(community);
           param.Version = SnmpVersion.Ver2;
           IpAddress agent = new IpAddress(agentip);
           UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
           Oid startOid = new Oid(oid);
           Pdu bulkPdu = Pdu.GetBulkPdu();
           bulkPdu.VbList.Add(startOid);
           bulkPdu.NonRepeaters = 0;
           bulkPdu.MaxRepetitions = 100;
           Oid curOid = (Oid)startOid.Clone();
           string info = "";
           SnmpV2Packet result = (SnmpV2Packet)target.Request(bulkPdu, param);
           if (result != null)
           {
               //ErrorStatus other than 0 is an error returned by the agent -see SnmpConstants for error definitions
               if (result.Pdu.ErrorStatus != 0)
               {
                   erro_status = result.Pdu.ErrorStatus;
                   // erro_index = result.Pdu.ErrorIndex;
                   info = "no such oid";
               }
               else
               {
                   info = result.Pdu.VbList[0].Value.ToString();
                   erro_status = 0;
               }
           }
           else
           {
               info = "no response";
               erro_status = -1;
           }
           target.Dispose();
           return info;
       }

       public void SetValue(string agentip, string[] oid, object[]ovalue, string key = _key)
       {
           int i = ovalue.Length;
           OctetString community = new OctetString(key);
           AgentParameters param = new AgentParameters(community);
           param.Version = SnmpVersion.Ver2;
           IpAddress agent = new IpAddress(agentip);
           UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
           Pdu pdu = new Pdu(PduType.Set);
          
           for (int j = 0; j < i;j++ )
           {
               if (ovalue[j] is string)
               { pdu.VbList.Add(new Oid(oid[j]), new OctetString((string)ovalue[j])); }
               else if (ovalue[j] is Int32)
               {
                   pdu.VbList.Add(new Oid(oid[j]), new Integer32((Int32)ovalue[j]));
               }
           }
           SnmpV2Packet response;
           try
           {
               response = target.Request(pdu, param) as SnmpV2Packet;
               erro_status = 0;
           }
           catch (Exception)
           {
               erro_status = 1;

           }
       }

       public void SetOctecString(string agentip, string oid, string setvalue, string key = _key)
       {
           OctetString community = new OctetString(key);
           AgentParameters param = new AgentParameters(community);
           param.Version = SnmpVersion.Ver2;
           IpAddress agent = new IpAddress(agentip);
           UdpTarget target = new UdpTarget((IPAddress)agent,161, 2000, 1);
           Pdu pdu = new Pdu(PduType.Set);
           pdu.VbList.Add(new Oid(oid), new OctetString(setvalue));
           SnmpV2Packet response;
           try
           {
               response = target.Request(pdu, param) as SnmpV2Packet;
               erro_status = 0;
           }
           catch (Exception)
           {
               erro_status = 1;

           }
       
       }

       public void SetInteger(string agentip, string oid,Int32 ivalue, string key = _key)
       {
           OctetString community = new OctetString(key);
           AgentParameters param = new AgentParameters(community);
           param.Version = SnmpVersion.Ver2;
           IpAddress agent = new IpAddress(agentip);
           UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
           Pdu pdu = new Pdu(PduType.Set);
           pdu.VbList.Add(new Oid(oid), new Integer32(ivalue));
           SnmpV2Packet response;
           try
           {
               response = target.Request(pdu, param) as SnmpV2Packet;
               erro_status = 0;
           }
           catch (Exception)
           {
               erro_status = 1;

           }
       
       }


       //public string GetBulkString(string agentip, string oid, string key = _key)
       //{
       //    OctetString community = new OctetString(key);
       //    AgentParameters param = new AgentParameters(community);
       //    param.Version = SnmpVersion.Ver2;
       //    IpAddress agent = new IpAddress(agentip);
       //    UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
       //    Oid startOid = new Oid(oid);
       //    Pdu bulkPdu = Pdu.GetBulkPdu();
       //    bulkPdu.VbList.Add(startOid);
       //    bulkPdu.NonRepeaters = 0;
       //    bulkPdu.MaxRepetitions = 100;
       //    Oid curOid = (Oid)startOid.Clone();
       //    string info = "";
       //    while (startOid.IsRootOf(curOid))
       //    {
       //        SnmpPacket result = null;
       //        try
       //        {
       //            result = target.Request(bulkPdu, param);
       //        }
       //        catch (Exception ex) 
       //        {
       //            target.Close();
       //            info = ex.ToString();
       //            erro_status = -1;
       //            return info;
       //        }
       //        if (result.Pdu.ErrorStatus != 0)
       //        {
       //            erro_status = result.Pdu.ErrorStatus;
       //            // erro_index = result.Pdu.ErrorIndex;
       //            info = "no such oid";
       //        }
       //        foreach (Vb v in result.Pdu.VbList)
       //        {
       //            curOid = (Oid)v.Oid.Clone();
       //            if (startOid.IsRootOf(v.Oid))
       //            {
       //                uint[] childOids = Oid.GetChildIdentifiers(startOid,v.Oid);
       //                uint[] instance = new uint[childOids.Length - 1];
       //                Array.Copy(childOids,1,instance,0,childOids.Length-1);
       //                String strInst = InstanceToString(instance);
       //                uint column = childOids[0];

       //            }
       //        }
           
       //    }
       //}

       public static string InstanceToString(uint[] instance) 
       {
           StringBuilder str = new StringBuilder();
           foreach (uint v in instance)
           {
               if (str.Length == 0)
               {
                   str.Append(v);
               }
                   else
                   str.AppendFormat(".{0}",v);
           }
           return str.ToString();
       
       }
    }
}
