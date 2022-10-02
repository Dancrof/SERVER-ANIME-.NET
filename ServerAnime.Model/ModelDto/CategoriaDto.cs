using ServerAnime.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerAnime.Model.ModelDto
{
    public class CategoriaDto
    {          
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }       
    }
}
