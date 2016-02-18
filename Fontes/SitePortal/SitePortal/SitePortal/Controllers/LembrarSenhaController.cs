using DAL;
using DTO;
using DTO.Usuario;
using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitePortal.Controllers
{
    public class LembrarSenhaController : Controller
    {

        public ActionResult Index(string Email)
        {
            Portal model = new Portal().CarregarModel();

            if (!String.IsNullOrEmpty(Email))
            {
                UsuarioResponse resp = new UsuarioDAL().Carregar(Email);

                if (resp.Resposta.Erro)
                {
                    model.NrProtocoloContato = "Não foi possível localizar uma conta de usuário através do endereço de e-mail informado.";
                }
                else
                {
                    if (resp.Usuario == null || resp.Usuario.UsuarioId == 0)
                    {
                        model.NrProtocoloContato = "Não foi possível localizar uma conta de usuário através do endereço de e-mail informado.";
                    }
                    else
                    {
                        ExecutaNotificarUsuario(resp.Usuario.UsuarioId, 2, 3);

                        model.NrProtocoloContato = "Foi enviado um e-mail com as instruções de resgate de senha!";

                    }
                }

            }

            return View(model);
        }

        private Resposta ExecutaNotificarUsuario(int UsuarioId, int SiteId, int EmailTemplateId)
        {
            Email email = new Email();

            Resposta resposta = new Resposta();

            bool enviado = email.Enviar_NotificacaoPreCadastro_WebFull(UsuarioId, SiteId, EmailTemplateId);
            resposta.Erro = !enviado;

            return resposta;
        }

    }
}
