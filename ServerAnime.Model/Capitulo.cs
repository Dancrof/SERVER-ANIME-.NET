using System;
using System.Collections.Generic;

namespace ServerAnime.Model
{
    public partial class Capitulo
    {
        public int Id { get; set; }
        public string NombreCap { get; set; } = null!;
        public string UrlCapituloCap { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int CatalogoId { get; set; }
    }
}
