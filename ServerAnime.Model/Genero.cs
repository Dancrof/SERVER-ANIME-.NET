using System;
using System.Collections.Generic;

namespace ServerAnime.Model
{
    public partial class Genero
    {
        public int Id { get; set; }
        public string NombreGen { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int CatalogoId { get; set; }
    }
}
