using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Podcast
{
    public class PodcastIdiomaExcecao
    {
        public int PodcastIdiomaExcecaoId { get; set; }
        public int PodcastId { get; set; }
        public int IdiomaId { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public string TituloEpisodio { get; set; }
        public string Descricao { get; set; }
    }
}
