using System;
using System.Collections.Generic;
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
        
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; } = null!;
        
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    
        public virtual ICollection<Catalogo> Catalogos { get; set; }
    }
}
