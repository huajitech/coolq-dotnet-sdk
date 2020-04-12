using HuajiTech.CoolQ.Utilities;

namespace HuajiTech.CoolQ.DataExchange
{
    internal sealed class MemberInfoReader : Reader<MemberInfo>
    {
        public MemberInfoReader(string base64String)
            : base(base64String)
        {
        }

        public override MemberInfo Read()
        {
            var info = new MemberInfo
            {
                Group = new Group(ReadInt64()),
                Number = ReadInt64(),
                Nickname = ReadString(),
                Alias = ReadString(),
                Gender = (Gender)ReadInt32(),
                Age = ReadInt32(),
                Location = ReadString(),
                EntranceTime = ReadDateTime(),
                LastSpeakTime = ReadDateTime(),
                Level = ReadString(),
                Type = (MemberType)ReadInt32(),
                HasBadRecord = ReadBoolean(),
                CustomTitle = new CustomTitle(
                    text: ReadString(),
                    expirationTime: ReadDateTime()),
                CanEditAlias = ReadBoolean()
            };

            if (info.CustomTitle.ExpirationTime <= Timestamp.Base)
            {
                info.CustomTitle = null;
            }

            return info;
        }
    }
}