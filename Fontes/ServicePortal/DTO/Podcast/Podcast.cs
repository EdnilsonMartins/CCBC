using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Podcast
{
    public class Podcast
    {
        public int PodcastId { get; set; }
        public int PodcastGrupoId { get; set; }
        public int SiteId { get; set; }
        public string Autor { get; set; }
        //public string Titulo { get; set; }
        public string LinkURL { get; set; }
        public string LinkURLImagem { get; set; }
        public string LinkURLAudio { get; set; }
        public string Keywords { get; set; }
        //public string Descricao { get; set; }
        //public string TituloEpisodio { get; set; }
        public string DireitosAutorais { get; set; }
        public string ProprietarioNome { get; set; }
        public string ProprietarioEmail { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public int? Duracao { get; set; }
        //public string SubTitulo { get; set; }
        public string Tamanho { get; set; }
        public int? PodcastPaiId { get; set; }

        public PodcastIdiomaExcecao Detalhe { get; set; }

        public PodcastComplemento Complemento { get; set; }

        public class PodcastComplemento
        {
            public bool Privado { get; set; }
            public bool Liberado { get; set; }
            public string ListaUsuarioGrupo { get; set; }
            public string ListaUsuario { get; set; }
            public string PodcastGrupo { get; set; }
        }

        public Podcast()
        {
            Detalhe = new PodcastIdiomaExcecao();
            Complemento = new PodcastComplemento();
        }

    }
}
