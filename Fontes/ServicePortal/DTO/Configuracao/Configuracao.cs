using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Configuracao
{
    public class Configuracao
    {
        public int ConfiguracaoId { get; set; }
        public int SiteId { get; set; }
        public string ContatoTelefonePrincipal { get; set; }
        public string ContatoEmailPrincipal { get; set; }
        public string Localizacao { get; set; }
        public string LocalizacaoComplemento { get; set; }
        public string EmailHost { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }
        public string EmailDisplayName { get; set; }
        public string LinkMapa { get; set; }
        public string HostBase { get; set; }
        public string EmailDestino { get; set; }
        public int EmailPorta { get; set; }
        public string EmailDestinoAdministrativoTI { get; set; }
        public int TamanhoMinimoSenha { get; set; }
    }
}
