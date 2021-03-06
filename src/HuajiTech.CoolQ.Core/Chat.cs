namespace HuajiTech.CoolQ
{
    internal abstract class Chat : IChattable
    {
        protected Chat()
        {
        }

        protected Chat(long number) => Number = number;

        public abstract string DisplayName { get; }

        public virtual long Number { get; }

        public override bool Equals(object? obj) => Equals(obj as IChattable);

        public virtual bool Equals(IChattable? other)
            => base.Equals(other) || (other is Chat && other?.Number == Number);

        public override int GetHashCode() => (int)Number;

        public abstract Message Send(string message);

        public override string ToString() => $"{GetType().Name}({Number})";
    }
}