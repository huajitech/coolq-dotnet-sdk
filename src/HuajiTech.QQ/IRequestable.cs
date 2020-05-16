namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义可请求的对象。
    /// </summary>
    public interface IRequestable
    {
        /// <summary>
        /// 获取一个值，指示当前 <see cref="IRequestable"/> 对象是否已请求。
        /// </summary>
        bool IsRequested { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IRequestable"/> 对象是否已成功请求。
        /// </summary>
        bool IsRequestedSuccessfully { get; }

        /// <summary>
        /// 请求当前 <see cref="IRequestable"/> 对象。
        /// </summary>
        void Request();
    }
}