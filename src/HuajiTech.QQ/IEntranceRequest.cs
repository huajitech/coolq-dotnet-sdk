namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义加入请求。
    /// </summary>
    public interface IEntranceRequest : IRequest
    {
        /// <summary>
        /// 拒绝当前请求，并指定拒绝的原因。
        /// </summary>
        /// <param name="reason">原因。</param>
        void Reject(string reason);
    }
}