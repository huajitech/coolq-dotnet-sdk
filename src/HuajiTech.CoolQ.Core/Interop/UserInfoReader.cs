namespace HuajiTech.CoolQ.Interop
{
    internal sealed class UserInfoReader : Reader<UserInfo>
    {
        public UserInfoReader(string base64String)
            : base(base64String)
        {
        }

        public override UserInfo Read()
            => new UserInfo
            {
                Number = ReadInt64(),
                Nickname = ReadString(),
                Gender = (Gender)ReadInt32(),
                Age = ReadInt32()
            };
    }
}