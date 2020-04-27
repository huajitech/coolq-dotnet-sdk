using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    public interface IRequest
    {
        string Message { get; }

        void Accept();

        Task AcceptAsync();

        void Reject();

        Task RejectAsync();
    }
}