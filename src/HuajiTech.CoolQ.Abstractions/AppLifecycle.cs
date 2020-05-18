namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 指定应用生命周期。
    /// </summary>
    public enum AppLifecycle
    {
        /// <summary>
        /// 应用未被加载。
        /// </summary>
        NotLoaded,

        /// <summary>
        /// 应用已被加载。
        /// </summary>
        Loaded,

        /// <summary>
        /// 应用正在初始化。
        /// </summary>
        Initializing,

        /// <summary>
        /// 机器人已启动。
        /// </summary>
        BotStarted,

        /// <summary>
        /// 应用已被启用。
        /// </summary>
        Enabled,

        /// <summary>
        /// 应用正在停用。
        /// </summary>
        Disabling,

        /// <summary>
        /// 机器人正在关闭。
        /// </summary>
        BotStopping
    }
}