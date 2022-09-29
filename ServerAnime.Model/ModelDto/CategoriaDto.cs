using ServerAnime.Model;
using System.ComponentModel.DataAnnotations;

namespace ServerAnime.Model.ModelDto
{
    public class CategoriaDto
    {
        [Required]
        public string Nombre { get; set; }
        public Categorium categorium { get; set; }
    }
}
