﻿using System;
using System.Collections.Generic;

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

        public virtual ICollection<Catalogo> Catalogos { get; set; }
    }
}
