using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using LookUpIp.Classess;
using HNInfoClassLibrary;

namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("请加入一个ip地址参数");
                return;
            }

            try
            {
                IPAddress ip = IPAddress.Parse(args[0]);
                AutoArp.bingding(ip);
                Console.WriteLine(AutoArp.message);
                Console.WriteLine("old_a:\t" + AutoArp.ussha.old_arp);
                Console.WriteLine("old_b:\t" + AutoArp.usshb.old_arp);
                Console.WriteLine("new_a:\t" + AutoArp.ussha.new_arp);
                Console.WriteLine("new_b:\t" + AutoArp.usshb.new_arp);
                Console.WriteLine(AutoArp.message);
            }
            catch (Exception)
            {
                Console.WriteLine("输入参数格式错误！");
                return;
            }
           
        }
    }
}
