using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    public interface IRefreshable
    {
        void Refresh();

        Task RefreshAsync();
    }
}