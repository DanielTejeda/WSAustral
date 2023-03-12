using Dapper.Contrib.Extensions;
using Dapper.Contrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using WSAustral.Core.Repositories.Base;
using WSAustral.Modelos;

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

        public void Agregar(T entidad)
        {
            var connection = new SqlConnection(_connectionString);
            connection.Insert<T>(entidad);
        }

        public T Obtener(int id)
        {
            var connection = new SqlConnection(_connectionString);
            return connection.Get<T>(id);
            
        }
        public IEnumerable<T> Listar()
        {
            var connection = new SqlConnection(_connectionString);
            return connection.GetAll<T>();
        }
        public bool Eliminar(T entidad)
        {
            var connection = new SqlConnection(_connectionString);
            
            return connection.Delete<T>(entidad);
            
        }
        public bool Modificar(T entidad)
        {
            var connection = new SqlConnection(_connectionString);
            
            return connection.Update<T>(entidad);
        }
    }
}
