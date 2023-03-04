using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSAustral.Core.Dapper.Repositories.Base;
using WSAustral.Core.Repositories;
using WSAustral.Modelos;

namespace WSAustral.Core.Dapper.Repositories
{
    public class RepositoryRCNP : RepositoryA<LogRCNP>, IRepositoryRCNP
    {
        public RepositoryRCNP(string connectionString) : base(connectionString)
        {

        }
    }
}
