using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using WSAustral.Core.Dapper.Repositories.Base;
using WSAustral.Core.Repositories;
using WSAustral.Modelos;

namespace WSAustral.Core.Dapper.Repositories
{
    public class RepositoryRCNP : RepositoryA<TBL_LogWS>, IRepositoryRCNP
    {
        public RepositoryRCNP(string connectionString) : base(connectionString)
        {

        }

        public async Task<string> BuscarPEP(string pep)
        {
            // "Server=10.5.0.52;Initial Catalog=Almacenes;Persist Security Info=False;User ID=UsrPortales;Password=$#ewo2001.2d;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;"
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ISCOD_PEP", pep);
                var result = await connection.QueryAsync<string>("Almacen.PR_BUSCA_PEP", parameters,
                                            commandType: System.Data.CommandType.StoredProcedure);
                return result.Single().ToString();
            }
        }
    }
}
