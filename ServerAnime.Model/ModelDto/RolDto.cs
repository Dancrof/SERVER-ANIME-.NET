using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAnime.Model.ModelDto
{
    public class RolDto
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Rol { get; set; } = null!;
    }
}
