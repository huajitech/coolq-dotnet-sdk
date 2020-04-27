using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    public interface IMuteable
    {
        void Mute();

        Task MuteAsync();

        void Unmute();

        Task UnmuteAsync();
    }
}