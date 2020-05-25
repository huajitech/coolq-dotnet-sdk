namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供用于禁言的方法。
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