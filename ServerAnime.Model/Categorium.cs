using System.ComponentModel.DataAnnotations;

namespace ServerAnime.Model
{
    public partial class Categorium
    {           
        public Categorium()
        {
            Catalogos = new HashSet<Catalogo>();
        }
       
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual ICollection<Catalogo> Catalogos { get; set; }
    }
}
