using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义可禁言对象。
    /// </summary>
    public interface IMuteable
    {
        /// <summary>
        /// 将当前 <see cref="IMuteable"/> 对象禁言。
        /// </summary>
        void Mute();

        /// <summary>
        /// 以异步操作将当前 <see cref="IMuteable"/> 对象禁言。
        /// </summary>
        Task MuteAsync();

        /// <summary>
        /// 将当前 <see cref="IMuteable"/> 对象解除禁言。
        /// </summary>
        void Unmute();

        /// <summary>
        /// 以异步操作将当前 <see cref="IMuteable"/> 对象解除禁言。
        /// </summary>
        Task UnmuteAsync();
    }
}