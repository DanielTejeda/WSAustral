using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSAustral.Core.Repositories.Base;
using WSAustral.Modelos;

namespace WSAustral.Core.Repositories
{
    public interface IRepositoryRCNP : IRepositoryA<LogRCNP>
    {
        Task<string> BuscarPEP(string pep);
    }
}
