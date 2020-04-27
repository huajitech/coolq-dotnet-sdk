using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义群管理员事件。
    /// </summary>
    public interface IGroupAdministratorEventSource
    {
        /// <summary>
        /// 在添加管理员时引发。
        /// </summary>
        event EventHandler<GroupEventArgs> AdministratorAdded;

        /// <summary>
        /// 在移除管理员时引发。
        /// </summary>
        event EventHandler<GroupEventArgs> AdministratorRemoved;
    }
}