using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    public interface IRequestable
    {
        bool HasRequested { get; }

        void Request();

        Task RequestAsync();
    }
}