using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Banner
{
    public class BannerIdiomaExcecao
    {
        public int BannerIdiomaExcecaoId { get; set; }
        public int BannerId { get; set; }
        public int IdiomaId { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string Descricao { get; set; }

    }
}
