using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class RolUsuariAplicacion : IdentityUserRole<int>
    {
        public virtual UsuarioAplicacion Usuario { get; set; }
        public virtual RolAplicacion RoleAplicacion { get; set; }

    }
}
