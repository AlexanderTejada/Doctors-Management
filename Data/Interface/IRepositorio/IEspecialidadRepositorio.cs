using Models.Entidades;
using System;

namespace Data.Interface.IRepositorio
{
    public interface IEspecialidadRepositorio : IRepositorioGenerico<Especialidad>
    {
        void Actualizar(Especialidad especialidad);
    }
}
