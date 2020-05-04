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
        /// 将当前 <see cref="IMuteable"/> 对象解除禁言。
        /// </summary>
        void Unmute();
    }
}