namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示文件。
    /// </summary>
    public class File
    {
        public File(string id, string name, long length, long busId)
        {
            Id = id;
            Name = name;
            Length = length;
            BusId = busId;
        }

        /// <summary>
        /// 获取当前 <see cref="File"/> 对象的 BusID。
        /// </summary>
        public long BusId { get; }

        /// <summary>
        /// 获取当前 <see cref="File"/> 对象的 ID。
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 获取当前 <see cref="File"/> 对象的长度。
        /// </summary>
        public long Length { get; }

        /// <summary>
        /// 获取当前 <see cref="File"/> 对象的名称。
        /// </summary>
        public string Name { get; }
    }
}