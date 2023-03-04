using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSAustral.Core.Dapper.Repositories;
using WSAustral.Core.Repositories;

namespace WSAustral.UnitOfWork
{
    public class WSUnitOfWork : IUnitOfWork
    {
        public WSUnitOfWork(string connectionString)
        {
            ReportesCNP = new RepositoryRCNP(connectionString);
        }

        public IRepositoryRCNP ReportesCNP
        {
            get;
            private set;
        }
    }
}
