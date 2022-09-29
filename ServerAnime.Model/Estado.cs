using System;
using System.Collections.Generic;

namespace ServerAnime.Model
{
    public partial class Estado
    {
        public Estado()
        {
            Catalogos = new HashSet<Catalogo>();
        }

        public int Id { get; set; }
        public string NombreEst { get; set; } = null!;
        public sbyte? StatusEst { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Catalogo> Catalogos { get; set; }
    }
}
