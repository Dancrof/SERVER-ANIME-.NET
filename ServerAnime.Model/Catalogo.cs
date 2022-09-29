using System;
using System.Collections.Generic;

namespace ServerAnime.Model
{
    public partial class Catalogo
    {
        public int Id { get; set; }
        public string UrlPortadaCat { get; set; } = null!;
        public string NombreCat { get; set; } = null!;
        public string DescipcionCat { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int CategoriaId { get; set; }
        public int EstadoId { get; set; }

        public virtual Categorium Categoria { get; set; } = null!;
        public virtual Estado Estado { get; set; } = null!;
    }
}
