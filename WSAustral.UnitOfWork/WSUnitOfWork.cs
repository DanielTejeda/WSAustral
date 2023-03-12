using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WSAustral.Core.Dapper.Repositories;
using WSAustral.Core.Repositories;
using Microsoft.Extensions.Configuration;

namespace WSAustral.UnitOfWork
{
    public class WSUnitOfWork : IUnitOfWork
    {
        public IConfiguration _configuration { get; }

        public IRepositoryRCNP ReportesCNP { get; set; }

        public WSUnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;

            ReportesCNP = new RepositoryRCNP(_configuration.GetConnectionString("ServidorAlmacenes"));
            
            /*ReportesCNP = new RepositoryRCNP(_configuration["ConnectionStrings:ServidorAlmacenes"]);*/
        }

    }
}
