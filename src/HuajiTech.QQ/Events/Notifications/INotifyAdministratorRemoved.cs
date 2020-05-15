using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义管理员移除事件。
    /// </summary>
    public interface INotifyAdministratorRemoved
    {
        /// <summary>
        /// 在移除管理员时引发。
        /// </summary>
        event EventHandler<GroupEventArgs> AdministratorRemoved;
    }
}