namespace HuajiTech.CoolQ
{
    /// <summary>
    /// 表示文件。
    /// </summary>
    public class File
    {
        internal File(string id, string name, long length, long busId)
        {
            Id = id;
            Name = name;
            Length = length;
            BusId = busId;
        }

        /// <summary>
        /// 获取 BusID。
        /// </summary>
        public long BusId { get; }

        /// <summary>
        /// 获取 ID。
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 获取长度。
        /// </summary>
        public long Length { get; }

        /// <summary>
        /// 获取名称。
        /// </summary>
        public string Name { get; }
    }
}