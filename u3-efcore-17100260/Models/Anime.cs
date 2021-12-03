using System;
using System.Collections.Generic;

#nullable disable

namespace u3_efcore_17100260.Models
{
    public partial class Anime
    {
        public Anime()
        {
            Personajes = new HashSet<Personaje>();
        }

        public int IdAnime { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Personaje> Personajes { get; set; }
    }
}
