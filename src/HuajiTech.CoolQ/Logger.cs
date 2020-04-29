using HuajiTech.QQ;

namespace HuajiTech.CoolQ
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Performance", "CA1806:不要忽略方法结果", Justification = "<挂起>")]
    internal class Logger : QQ.Logger
    {
        public override void Log(LogLevel level, string type, string message)
        {
            NativeMethods.Log(Bot.Instance.AuthCode, level, type, message);
        }

        public override void LogFatal(string message)
        {
            NativeMethods.LogFatal(Bot.Instance.AuthCode, message);
        }
    }
}