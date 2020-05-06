namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义机器人信息。
    /// </summary>
    public interface IBotInfo
    {
        /// <summary>
        /// 获取一个值，指示当前 <see cref="IBotInfo"/> 对象是否可以发送图片。
        /// </summary>
        bool CanSendImage { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IBotInfo"/> 对象是否可以发送录音。
        /// </summary>
        bool CanSendRecord { get; }
    }
}