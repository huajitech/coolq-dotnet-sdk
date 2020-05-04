namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义请求。
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// 获取当前 <see cref="IRequest"/> 对象的消息。
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 同意当前请求。
        /// </summary>
        void Accept();

        /// <summary>
        /// 拒绝当前请求。
        /// </summary>
        void Reject();
    }
}