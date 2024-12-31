using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.IRepositorio
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task <IEnumerable<T>> ObtenerTodos(
            Expression<Func<T,bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = "" // include
            );
        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = "" //Include
            );
        Task Agregar (T entidad);
        void Remover(T entidad);
    }
}
