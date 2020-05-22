namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 定义可请求的实例。
    /// </summary>
    public interface IRequestable
    {
        /// <summary>
        /// 获取一个值，指示当前 <see cref="IRequestable"/> 实例是否已请求。
        /// </summary>
        bool IsRequested { get; }

        /// <summary>
        /// 获取一个值，指示当前 <see cref="IRequestable"/> 实例是否已成功请求。
        /// </summary>
        bool IsRequestedSuccessfully { get; }

        /// <summary>
        /// 请求当前 <see cref="IRequestable"/> 实例。
        /// </summary>
        void Request();
    }
}