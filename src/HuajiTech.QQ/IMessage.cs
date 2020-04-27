using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    public interface IMessage
    {
        string Content { get; }

        long Id { get; }

        void Recall();

        Task RecallAsync();
    }
}