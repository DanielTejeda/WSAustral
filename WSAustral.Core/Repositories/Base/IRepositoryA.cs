using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSAustral.Core.Repositories.Base
{
    public interface IRepositoryA<T> where T : class
    {
        Task<int> Agregar(T entidad);
        Task<T> Obtener(int id);
        Task<IEnumerable<T>> Listar();
        Task<bool> Eliminar(T entidad);
        Task<bool> Modificar(T entidad);
    }
}