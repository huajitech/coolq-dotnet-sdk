using System;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义匿名成员。
    /// </summary>
    public interface IAnonymousMember : ITimedMuteable, IEquatable<IAnonymousMember>
    {
        IGroup Group { get; }

        long Id { get; }

        string Name { get; }
    }
}