using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    public interface IEntranceRequest : IRequest
    {
        void Reject(string reason);

        Task RejectAsync(string reason);
    }
}