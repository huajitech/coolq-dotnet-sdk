using System;

namespace HuajiTech.QQ.Events
{
    /// <summary>
    /// 定义管理员添加事件。
    /// </summary>
    public interface INotifyAdministratorAdded
    {
        /// <summary>
        /// 在添加管理员时引发。
        /// </summary>
        event EventHandler<GroupEventArgs> AdministratorAdded;
    }
}