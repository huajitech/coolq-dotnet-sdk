using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuajiTech.QQ
{
    public interface IContactRequest : IRequest
    {
        void Accept(string remark);

        Task AcceptAsync(string remark);
    }
}