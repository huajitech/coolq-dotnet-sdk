namespace HuajiTech.CoolQ.Messaging
{
    /// <summary>
    /// 定义扩展方法。
    /// </summary>
    public static class Extensions
    {
        public static PlainText ToPlainText(this string? str) => new PlainText(str);
    }
}