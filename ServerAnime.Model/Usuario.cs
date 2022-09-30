using System;
using System.Collections.Generic;

namespace ServerAnime.Model
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
