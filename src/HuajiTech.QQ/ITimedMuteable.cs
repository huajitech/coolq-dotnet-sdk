using System;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    public interface ITimedMuteable : IMuteable
    {
        void Mute(TimeSpan duration);

        Task MuteAsync(TimeSpan duration);
    }
}