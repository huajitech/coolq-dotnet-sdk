using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示加入请求。
    /// </summary>
    public abstract class EntranceRequest : Request
    {
        /// <summary>
        /// 拒绝当前请求，并指定拒绝的原因。
        /// </summary>
        /// <param name="reason">原因。</param>
        public abstract void Reject(string reason);

        /// <summary>
        /// 以异步操作拒绝当前请求，并指定拒绝的原因。
        /// </summary>
        /// <param name="reason">原因。</param>
        public virtual Task RejectAsync(string reason) => Task.Run(() => Reject(reason));

        public override void Reject() => Reject(null);

        public override Task RejectAsync() => RejectAsync(null);
    }
}