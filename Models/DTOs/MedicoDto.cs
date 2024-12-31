using Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class MedicoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Apellidos es Requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Apellidos debe ser Minimo 1 y Maximo 60 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Nombres es Requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nombres debe ser Minimo 1 y Maximo 60 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Direccion es Requerido")]

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Direccion debe ser Minimo 1 y Maximo 100 caracteres")]
        public string Direccion { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "Telefono debe ser Minimo 1 y Maximo 40 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Genero es Requerido")]
        [StringLength(1, ErrorMessage = "Genero debe ser Minimo 1 y Maximo 1 caracteres")]
        public char Genero { get; set; }

        [Required(ErrorMessage = "Especialidad es Requerido")]
        public int EspecialidadId { get; set; }

        public string NombreEspecialidad { get; set; }
        public int Estado { get; set; } // 1: Activo, 0: Inactivo

    }
}
