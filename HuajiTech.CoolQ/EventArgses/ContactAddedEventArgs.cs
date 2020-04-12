using System;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 为 <see cref="CurrentUser.ContactAdded"/> 事件提供数据。
    /// </summary>
    public class ContactAddedEventArgs : RoutedEventArgs
    {
        public ContactAddedEventArgs(DateTime time, Contact affectee)
        {
            Time = time;
            Affectee = affectee;
        }

        /// <summary>
        /// 获取添加的联系人。
        /// </summary>
        public Contact Affectee { get; }

        /// <summary>
        /// 获取时间。
        /// </summary>
        public DateTime Time { get; }
    }
}