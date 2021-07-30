using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using log4net;

namespace L.Util
{
    public class DelegateTraceListener : TraceListener
    {
        public Action<string, bool> OnAppend { get; set; }

        public override void Write(string message)
        {
            if (this.OnAppend != null)
            {
                this.OnAppend.Invoke(message, false);
            }
        }

        public override void WriteLine(string message)
        {
            if (this.OnAppend != null)
            {
                this.OnAppend.Invoke(message, true);
            }
        }

        public DelegateTraceListener()
        {
            Trace.Listeners.Add(this);
        }

        ~DelegateTraceListener()
        {
            Trace.Listeners.Remove(this);
        }
    }
    public class Loger
    {
        public static ILog Log { get; } = LogManager.GetLogger(Assembly.GetExecutingAssembly(),Assembly.GetExecutingAssembly().FullName);

        static Loger()
        {
            //log4net.Config.XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log4.config")));
            log4net.Config.XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetCallingAssembly()),new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log4.config")));
        }

        public static MethodBase GetCurrentMethodName()
        {
            return new StackFrame(1).GetMethod();
        }
    }
}
