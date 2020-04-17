using HuajiTech.CoolQ.DataExchange;

namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示联系人。
    /// </summary>
    public class Contact : User
    {
        internal Contact(long number)
            : base(number)
        {
        }

        internal Contact(ContactInfo info)
            : base(info.Number, info.Nickname)
        {
            Alias = info.Alias;
        }

        /// <summary>
        /// 获取备注名。
        /// </summary>
        public string Alias { get; }
    }
}