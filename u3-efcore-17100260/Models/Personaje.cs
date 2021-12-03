using System;
using System.Collections.Generic;

#nullable disable

namespace u3_efcore_17100260.Models
{
    public partial class Personaje
    {
        public int IdPersonaje { get; set; }
        public string Nombre { get; set; }
        public int IdAnime { get; set; }

        public virtual Anime IdAnimeNavigation { get; set; }
    }
}
