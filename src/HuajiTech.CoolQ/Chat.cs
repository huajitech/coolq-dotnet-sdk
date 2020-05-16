using HuajiTech.QQ;

namespace HuajiTech.CoolQ
{
    internal abstract class Chat : IChattable
    {
        protected Chat(long number) => Number = number;

        public abstract string Name { get; }

        public long Number { get; }

        public override bool Equals(object? obj) => Equals(obj as IChattable);

        public virtual bool Equals(IChattable? other) => base.Equals(other) || (other is Chat && other?.Number == Number);

        public override int GetHashCode() => (int)Number;

        public abstract IContentfulMessage Send(string message);

        public override string ToString() => $"{GetType().Name}({Number})";
    }
}