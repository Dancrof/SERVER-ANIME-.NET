using System.ComponentModel.DataAnnotations;

namespace ServerAnime.Model.ModelDto
{
    public class CategoriaDto
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }
    }
}
