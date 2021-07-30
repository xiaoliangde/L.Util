using System;
using System.Diagnostics;
using System.IO;

namespace L.Util
{
    class ProcessEx
    {
        public const string ProxyExe = "LWinProxy.exe";
        public static Process StartProxyProcess(string processNickName, string args, bool isAdmin=true)
        {
            return StartProcessOnParams(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ProxyExe), processNickName, args, isAdmin);
        }

        static object __Locker = new object();
        public static Process StartProcessOnParams(string processFilePath,string processNickName, string args,bool isAdmin=true)
        {
            //lock (__Locker)
            //{
            //}
            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                ErrorDialog = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = processFilePath,
                Verb = isAdmin?"runas":string.Empty,
                Arguments = args,
            };
            var process = Process.Start(startInfo);
            return process;
        }

        public static void Msg(string msg)
        {
            var startInfo = new ProcessStartInfo
            {
                ErrorDialog = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = "LWinProxy.exe",
                Verb = "runas",

            };
            //Process.Start("LWinProxy.exe")
        }
    }
}
