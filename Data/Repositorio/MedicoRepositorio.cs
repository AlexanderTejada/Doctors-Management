using Data.Interface.IRepositorio;
using Models.Entidades;
using System;
using System.Linq;

namespace Data.Repositorio
{
    public class MedicoRepositorio : Repositorio<Medico>, IMedicoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public MedicoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Medico medico)
        {
            var medicoDb = _db.Medicos.FirstOrDefault(e => e.Id == medico.Id);
            if (medicoDb != null)
            {
                medicoDb.Apellidos = medico.Apellidos;
                medicoDb.Nombres = medico.Nombres;
                medicoDb.Estado = medico.Estado;
                medicoDb.FechaActualizacion = DateTime.Now;
                medicoDb.EspecialidadId = medico.EspecialidadId;
                medicoDb.FechaCreacion = medico.FechaCreacion;
                medicoDb.Genero = medico.Genero;
                medicoDb.Telefono = medico.Telefono;
                medicoDb.Direccion = medico.Direccion;

                _db.SaveChanges();
            }
        }
    }
}
