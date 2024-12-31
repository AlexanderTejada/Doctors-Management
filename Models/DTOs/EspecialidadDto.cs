using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class EspecialidadDto
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="El Nombre es Requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "El nombre de la especialidad debe tener entre 1 y 60 caracteres")]
        public string NombreEspecialidad { get; set; }

        [Required (ErrorMessage ="La descripcion es Requerida")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La descripción de la especialidad debe tener entre 1 y 100 caracteres")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El estado es Requerido")]
        public int Estado { get; set; } // para que reciba valores de 1 o 0 y luego haga la conversion por mapper

    }
}
