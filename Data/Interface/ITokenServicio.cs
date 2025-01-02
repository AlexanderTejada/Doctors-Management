using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface ITokenServicio
    {
        string CrearToken(UsuarioAplicacion usuario); 
    }
}
