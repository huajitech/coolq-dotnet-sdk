namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义成员请求。
    /// </summary>
    public interface IMembershipRequest : IRequest
    {
        /// <summary>
        /// 拒绝当前请求，并指定拒绝的原因。
        /// </summary>
        /// <param name="reason">原因。</param>
        void Reject(string reason);
    }
}