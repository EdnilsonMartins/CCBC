using DAL;
using DTO.Usuario;
using SitePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace SitePortal.Controllers
{
    public class MinhaContaController : Controller
    {
        
        public ActionResult Index(string ID)
        {
            Portal model = new Portal().CarregarModel();


            //string ID = Request.Form["ID"];
            string Fluxo = Request.Form["Fluxo"];

            if (ID != null && Fluxo != null && Fluxo == "1")
            {

                string nome = Request.Form["nome"];
                string login = Request.Form["login"];
                string email = Request.Form["email"];
                string senha = Request.Form["senha"];

                byte[] data = Convert.FromBase64String(ID);
                string ID2 = Encoding.UTF8.GetString(data);

                UsuarioDTO usuario = new UsuarioDAL().Carregar(Convert.ToInt32(ID2)).Usuario;

                usuario.Nome = nome;
                usuario.Login = login;
                usuario.Email = email;
                usuario.Senha = Util.MixMD5(senha);
                usuario.Ativo = true;

                var func = new List<Funcionalidade>();
                func.Add(new Funcionalidade() { Ativo = true, FuncionalidadeId = 1 });
                usuario.Funcionalidades = func;

                usuario.TedescoStatusId = (int)Util.TEDESCO_STATUS.CONFIRMADO;
                usuario.TedescoDataConfirmacao = DateTime.Now;

                usuario = new UsuarioDAL().Gravar(usuario, null, "", true).Usuario;

                model.Usuario = usuario;
            }
            else
            {

                byte[] data = Convert.FromBase64String(ID);
                string ID2 = Encoding.UTF8.GetString(data);

                UsuarioDTO usuario = new UsuarioDAL().Carregar(Convert.ToInt32(ID2)).Usuario;
                model.Usuario = usuario;

                @ViewBag.ID = ID;
                @ViewBag.Fluxo = 1; //Deixar como parâmetro e mexer na rota.
            }

            return View(model);
        }

        public ActionResult Atualizar(FormCollection form)
        {
            Portal model = new Portal().CarregarModel();


            string ID = Request.Form["ID"];
            string Fluxo = Request.Form["Fluxo"];

            if(ID != null && Fluxo !=null && Fluxo == "1"){

                string nome = Request.Form["nome"];
                string login = Request.Form["login"];
                string email = Request.Form["email"];
                string senha = Request.Form["senha"];

                byte[] data = Convert.FromBase64String(ID);
                string ID2 = Encoding.UTF8.GetString(data);

                UsuarioDTO usuario = new UsuarioDAL().Carregar(Convert.ToInt32(ID2)).Usuario;

                usuario.Nome = nome;
                usuario.Login = login;
                usuario.Email = email;
                usuario.Senha = senha;
                usuario.Ativo = true;

                var func = new List<Funcionalidade>();
                func.Add(new Funcionalidade() { Ativo = true, FuncionalidadeId = 1 });
                usuario.Funcionalidades = func;

                usuario.TedescoStatusId = (int)Util.TEDESCO_STATUS.CONFIRMADO;
                usuario.TedescoDataConfirmacao = DateTime.Now;

                usuario = new UsuarioDAL().Gravar(usuario, null, "").Usuario;

                model.Usuario = usuario;
            }

            return View(model);
        }
        
    }
}
