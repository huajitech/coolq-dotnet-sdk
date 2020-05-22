namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义可禁言实例。
    /// </summary>
    public interface IMuteable
    {
        /// <summary>
        /// 将当前 <see cref="IMuteable"/> 实例禁言。
        /// </summary>
        void Mute();

        /// <summary>
        /// 将当前 <see cref="IMuteable"/> 实例解除禁言。
        /// </summary>
        void Unmute();
    }
}