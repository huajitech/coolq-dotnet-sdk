namespace HuajiTech.QQ
{
    /// <summary>
    /// 表示文件。
    /// </summary>
    public class File
    {
        /// <summary>
        /// 以指定的 ID、名称、长度和 BusID 初始化一个 <see cref="File"/> 类的新实例。
        /// </summary>
        /// <param name="id">ID。</param>
        /// <param name="name">名称。</param>
        /// <param name="length">长度。</param>
        /// <param name="busId">BusID。</param>
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