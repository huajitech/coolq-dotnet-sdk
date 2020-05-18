using System;

namespace HuajiTech.CoolQ.DataExchange
{
    internal class MemberInfo
    {
        public static readonly MemberInfo Empty = new MemberInfo();

        public int Age { get; set; }

        public string? Alias { get; set; }

        public bool CanEditAlias { get; set; }

        public CustomTitle? CustomTitle { get; set; }

        public DateTime TimeEntered { get; set; }

        public Gender Gender { get; set; }

        public Group? Group { get; set; }

        public bool HasBadRecord { get; set; }

        public DateTime LastSpeakTime { get; set; }

        public string? Level { get; set; }

        public string? Location { get; set; }

        public string? Nickname { get; set; }

        public long Number { get; set; }

        public MemberRole Role { get; set; }
    }
}