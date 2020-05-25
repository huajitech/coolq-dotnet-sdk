namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 提供用于请求的方法。
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
        /// <param name="refresh">
        /// 如果要求刷新当前 <see cref="IRequestable"/> 实例而不允许使用缓存，则为 <see langword="true"/>；否则为 <see langword="false"/>。
        /// </param>
        void Request(bool refresh = false);
    }
}