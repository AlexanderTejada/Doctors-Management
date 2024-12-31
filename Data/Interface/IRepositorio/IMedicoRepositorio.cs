using Models.Entidades;
using System;

namespace Data.Interface.IRepositorio
{
    public interface IMedicoRepositorio : IRepositorioGenerico<Medico>
    {
        void Actualizar(Medico medico);
    }
}
