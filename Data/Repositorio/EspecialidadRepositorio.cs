﻿using Data.Interface.IRepositorio;
using Models.Entidades;
using System;
using System.Linq;

namespace Data.Repositorio
{
    public class EspecialidadRepositorio : Repositorio<Especialidad>, IEspecialidadRepositorio
    {
        private readonly ApplicationDbContext _db;

        public EspecialidadRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Especialidad especialidad)
        {
            var especialidadDb = _db.Especialidades.FirstOrDefault(e => e.Id == especialidad.Id);
            if (especialidadDb != null)
            {
                especialidadDb.NombreEspecialidad = especialidad.NombreEspecialidad;
                especialidadDb.Descripcion = especialidad.Descripcion;
                especialidadDb.Estado = especialidad.Estado;
                especialidadDb.FechaActualizacion = DateTime.Now;

                _db.SaveChanges();
            }
        }
    }
}
