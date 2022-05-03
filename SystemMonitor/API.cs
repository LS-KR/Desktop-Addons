using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SystemMonitor
{
    internal class API
    {
        public static class RAM
        {
            [DllImport("kernel32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GlobalMemoryStatusEx(ref MEMORY_INFO mi);

            //定义内存的信息结构
            [StructLayout(LayoutKind.Sequential)]
            public struct MEMORY_INFO
            {
                public uint dwLength; //当前结构体大小
                public uint dwMemoryLoad; //当前内存使用率
                public ulong ullTotalPhys; //总计物理内存大小
                public ulong ullAvailPhys; //可用物理内存大小
                public ulong ullTotalPageFile; //总计交换文件大小
                public ulong ullAvailPageFile; //总计交换文件大小
                public ulong ullTotalVirtual; //总计虚拟内存大小
                public ulong ullAvailVirtual; //可用虚拟内存大小
                public ulong ullAvailExtendedVirtual; //保留 这个值始终为0
            }
            public static MEMORY_INFO GetMemoryStatus()
            {
                MEMORY_INFO mi = new MEMORY_INFO();
                mi.dwLength = (uint)System.Runtime.InteropServices.Marshal.SizeOf(mi);
                GlobalMemoryStatusEx(ref mi);
                return mi;
            }
            public static ulong GetAvailPhys()
            {
                MEMORY_INFO mi = GetMemoryStatus();
                return mi.ullAvailPhys;
            }
            public static ulong GetUsedPhys()
            {
                MEMORY_INFO mi = GetMemoryStatus();
                return (mi.ullTotalPhys - mi.ullAvailPhys);
            }
            public static ulong GetTotalPhys()
            {
                MEMORY_INFO mi = GetMemoryStatus();
                return mi.ullTotalPhys;
            }
        }
        public static class CPU
        {
            public static float GetCPUUsage()
            {
                PerformanceCounter cpuCounter = new PerformanceCounter();
                cpuCounter.CategoryName = "Processor";
                cpuCounter.CounterName = "% Processor Time";
                cpuCounter.InstanceName = "_Total";
                cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                _ = cpuCounter.NextValue();
                System.Threading.Thread.Sleep(50);
                return cpuCounter.NextValue();
            }
        }
    }
}
