using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSAustral.Modelos;

namespace WSAustral.Core.Repositories.Base
{
    public interface IRepositoryA<T> where T : class
    {
        void Agregar(T entidad);
        T Obtener(int id);
        IEnumerable<T> Listar();
        bool Eliminar(T entidad);
        bool Modificar(T entidad);
    }
}