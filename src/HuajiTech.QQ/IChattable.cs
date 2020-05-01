using System;

namespace HuajiTech.QQ
{
    public interface IChattable : ISendable, IEquatable<IChattable>
    {
        string DisplayName { get; }

        long Number { get; }
    }
}