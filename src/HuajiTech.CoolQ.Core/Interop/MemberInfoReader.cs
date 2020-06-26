using HuajiTech.CoolQ.Utilities;

namespace HuajiTech.CoolQ.Interop
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
                GroupNumber = ReadInt64(),
                Number = ReadInt64(),
                Nickname = ReadString(),
                Alias = ReadString(),
                Gender = (Gender)ReadInt32(),
                Age = ReadInt32(),
                Location = ReadString(),
                TimeEntered = ReadDateTime(),
                LastSpeakTime = ReadDateTime(),
                Level = ReadString(),
                Role = (MemberRole)ReadInt32(),
                HasBadRecord = ReadBoolean()
            };

            var titleText = ReadString();
            var titleExpirationTime = ReadDateTime();
            info.CanEditAlias = ReadBoolean();

            if (!(titleText is null))
            {
                info.CustomTitle = titleExpirationTime <= Timestamp.Zero ?
                    new CustomTitle(titleText) :
                    new CustomTitle(titleText, titleExpirationTime);
            }

            return info;
        }
    }
}