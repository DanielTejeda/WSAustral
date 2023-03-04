using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSAustral.Core.Repositories.Base;

namespace WSAustral.Core.Dapper.Repositories.Base
{
    public class RepositoryA<T> : IRepositoryA<T> where T : class
    {
        protected string _connectionString; //modificadores de acceso: protected <> public <> private

        public RepositoryA(string connectionString)
        {
            SqlMapperExtensions.TableNameMapper = (type) => { return $"{type.Name}"; }; //Configuración para activar el mapeo de Dapper

            _connectionString = connectionString;
        }

        public async Task<int> Agregar(T entidad)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.InsertAsync<T>(entidad);
            }
        }
        public async Task<T> Obtener(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.GetAsync<T>(id);
            }
        }
        public async Task<IEnumerable<T>> Listar()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.GetAllAsync<T>();
            }
        }
        public async Task<bool> Eliminar(T entidad)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.DeleteAsync<T>(entidad);
            }
        }
        public async Task<bool> Modificar(T entidad)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.UpdateAsync<T>(entidad);
            }
        }
    }
}
