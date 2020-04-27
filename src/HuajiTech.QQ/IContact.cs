namespace HuajiTech.QQ
{
    /// <summary>
    /// 定义联系人。
    /// </summary>
    public interface IContact : IUser
    {
        string Alias { get; }
    }
}