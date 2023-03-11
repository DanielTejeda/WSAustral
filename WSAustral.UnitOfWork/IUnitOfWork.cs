using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSAustral.Core.Repositories;

namespace WSAustral.UnitOfWork
{
    public interface IUnitOfWork
    {
        IConfiguration _configuration { get; }
        IRepositoryRCNP ReportesCNP { get; set; } 
    }
}
