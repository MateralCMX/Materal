using System;
using System.IO;
using System.Linq;

namespace Wix.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] allDeviceNos = File.ReadAllText("D://AllDeviceNo.txt").Split(',');
            string[] onlineDeviceNos = File.ReadAllText("D://OnlineDeviceNo.txt").Split(',');
            string[] offlineDeviceNos = (from m in allDeviceNos
                                         where !onlineDeviceNos.Contains(m)
                                         select m).ToArray();
            string offlineDeviceNo = offlineDeviceNos.Aggregate("", (current, item) => current + "'" + item + "',");
            offlineDeviceNo = offlineDeviceNo.Substring(0, offlineDeviceNo.Length - 1);

            string[] noEntryDeviceNos = (from m in onlineDeviceNos
                                        where !allDeviceNos.Contains(m)
                                        select m).ToArray();
            string noEntryDeviceNo = noEntryDeviceNos.Aggregate("", (current, item) => current + item + "\n");
            Console.ReadKey();
        }
    }
}
