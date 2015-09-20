using DAL;
using DTO;
using DTO.Menu;
using DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Globalization;
using SitePortal.Models;
using DTO.Mailing;
using System.Xml;
using System.Data;

namespace SitePortal.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Portal model = new Portal().CarregarModel(true);

            model.isHome = true;

            //Implementando Callback na Home:
            var CallbackPortal = HttpContext.Request.Cookies["CallbackPortal"] != null ? HttpContext.Request.Cookies["CallbackPortal"].Value : "";
            if (!String.IsNullOrEmpty(CallbackPortal))
            {
                var _callbackPortal = new HttpCookie("CallbackPortal", null) { HttpOnly = true };
                Response.AppendCookie(_callbackPortal);
                HttpContext.Request.Cookies.Set(_callbackPortal);
                Response.RedirectPermanent(CallbackPortal);
            }
            else if (model.SiteId == 0)
            {
                Response.RedirectPermanent(Url.Content("~/Portal/Index"));
            }


            return View(model);
        }

        public ActionResult GetWeatherExchangeFee(int CidadeId = 0)
        {
            string cidade = "";
            string pais = "Brazil";
            switch (CidadeId) {
                case 1:
                    cidade = "Quebec, Que";
                    pais = "Canada";
                    break;
                case 2:
                    cidade = "Toronto Pearson Int'L. Ont.";
                    pais = "Canada";
                    break;
                case 3:
                    cidade = "Vancouver International Air-Port, B. C.";
                    pais = "Canada";
                    break;
                case 4:
                    cidade = "Sao Paulo";
                    break;
                case 5:
                    cidade = "Brasilia Aeroporto";
                    break;
                case 6:
                    cidade = "Rio de Janeiro Aeroporto";
                    break;
                case 7:
                    cidade = "Recife Aeroporto";
                    break;
                //case 8:
                //    cidade = "Joao Pessoa";
                //    break;
                case 9:
                    cidade = "Curitiba";
                    break;
                case 10:
                    cidade = "Salvador Aeroporto";
                    break;


                case 11:
                    cidade = "Belo Horizonte";
                    break;
                case 12:
                    cidade = "Porto Alegre";
                    break;
                case 13:
                    cidade = "Montreal-Est";
                    pais = "Canada";
                    break;
                case 14:
                    cidade = "Edmonton Municipal Alta.";
                    pais = "Canada";
                    break;
                case 15:
                    cidade = "Winnipeg Int. Airportman.";
                    pais = "Canada";
                    break;
                case 16:
                    cidade = "Ottawa Int'L. Ont.";
                    pais = "Canada";
                    break;
                case 17:
                    cidade = "Calgary International, Alta.";
                    pais = "Canada";
                    break;
            }

            string temperatura = "";
            string ceu = "";
            try
            {
                using (Weather.GlobalWeatherSoapClient w = new Weather.GlobalWeatherSoapClient())
                {
                    string respWeather = w.GetWeather(cidade, pais);
                    using (XmlTextReader xtr = new XmlTextReader(new System.IO.StringReader(respWeather)))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            ds.ReadXml(xtr);
                            if (ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count >= 1)
                            {
                                temperatura = ds.Tables[0].Rows[0]["Temperature"].ToString().Replace("F", "°F").Replace("C", "°C");
                                temperatura = temperatura.Substring(temperatura.IndexOf("(") + 1, temperatura.IndexOf(")") - 1 - temperatura.IndexOf("("));
                                //ceu = ds.Tables[0].Rows[0]["SkyConditions"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                temperatura = "unavailable";
            }

            return Json(new { Temperatura = temperatura, Ceu = ceu }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EfetuarLogin(string Login, string Senha)
        {
            //Resposta resposta = new Resposta();
            //resposta.Erro = false;
            //resposta.Mensagem = "Login inválido. Acesso Negado!";

            ResponseLogin resp = new ResponseLogin();
            //resp.Nome = "Ednilson Martins";
            //resp.Resposta = resposta;

            resp = new LoginDAL().EfetuarLogin(Login, Senha);
            if (resp.Resposta.Erro)
            {
                resp.Resposta.Mensagem = Resources.Portal.Login_Mensagem_Invalido;//"Login inválido. Acesso Negado!";
            }
            else
            {
                List<Funcionalidade> listaFuncionalidades = new UsuarioDAL().CarregarUsuarioFuncionalidade(resp.UsuarioId);
                if (listaFuncionalidades.Any(x => x.FuncionalidadeId == 1))
                {
                    var usuarioCookie = new HttpCookie("UsuarioId", resp.UsuarioId.ToString()) { HttpOnly = true };
                    Response.AppendCookie(usuarioCookie);
                    var usuarioNomeCookie = new HttpCookie("UsuarioNome", resp.Nome.ToString()) { HttpOnly = true };
                    Response.AppendCookie(usuarioNomeCookie);
                }
                else
                {
                    resp.Resposta.Erro = true;
                    resp.Resposta.Mensagem = Resources.Portal.Login_Mensagem_Invalido;
                }
            }

            return Json(resp, JsonRequestBehavior.DenyGet);
        }

        public ActionResult EfetuarLogoff()
        {
            var usuarioCookie = new HttpCookie("UsuarioId", null) { HttpOnly = true };
            Response.AppendCookie(usuarioCookie);
            var usuarioNomeCookie = new HttpCookie("UsuarioNome", null) { HttpOnly = true };
            Response.AppendCookie(usuarioNomeCookie);

            return Json("", JsonRequestBehavior.DenyGet);
        }

        //public ActionResult SessionSite(string site)
        //{
        //    var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
        //    Response.AppendCookie(siteCookie);
        //    //RedirectToAction("Index", "Home")
        //    return RedirectPermanent(Url.Content("~/Home/Index"));
        //}

        public ActionResult SessionSite(string _site = "0")
        {
            
            string site = _site.ToString();
            var siteCookie = new HttpCookie("site", site) { HttpOnly = true };
            Response.AppendCookie(siteCookie);

            HttpContext.Request.Cookies.Set(siteCookie);

            //return Index();
            // return RedirectToActionPermanent("Index", "Home");
             //return RedirectPermanent(Url.Content("~/Home/Index"));


            Portal model = new Portal().CarregarModel(true);

            model.isHome = true;

            //Implementando Callback na Home:
            var CallbackPortal = HttpContext.Request.Cookies["CallbackPortal"] != null ? HttpContext.Request.Cookies["CallbackPortal"].Value : "";
            if (!String.IsNullOrEmpty(CallbackPortal))
            {
                var _callbackPortal = new HttpCookie("CallbackPortal", null) { HttpOnly = true };
                Response.AppendCookie(_callbackPortal);
                 Response.RedirectPermanent(CallbackPortal);
                 return null;
                 // return RedirectToAction(CallbackPortal);
            }
            else if (model.SiteId == 0)
            {
                Response.RedirectPermanent(Url.Content("~/Portal/Index"));
                return null;
            }

            return View("Index", model);

        }

        public ActionResult SessionCulture(string lang)
        {
            var langCookie = new HttpCookie("lang", lang) { HttpOnly = true };
            Response.AppendCookie(langCookie);

            ConfigurarDadosDeCultura(lang);

            //RedirectToAction("Index", "Home", new { culture = lang });
            return RedirectToAction("Index", "Home", new { culture = lang });
        }

        private void ConfigurarDadosDeCultura(string lang)
        {
            var currentCulture = HttpContext.Request.Cookies["lang"] != null
                ? HttpContext.Request.Cookies["lang"].Value
                : "en-US";

            //if (currentCulture != lang)
            //{
            //    ConfigurarUrlDeConsulta();
            //}
        }


        public ActionResult RegistrarMailing(string Nome, string Email, bool Cadastrar)
        {
            var currentSite = HttpContext.Request.Cookies["site"] != null ? HttpContext.Request.Cookies["site"].Value : "2";
            if (string.IsNullOrEmpty(currentSite)) currentSite = "0";
            int _siteId = Convert.ToInt32(currentSite);

            MailingDAL dal = new MailingDAL();
            MailingDTO mailing = new MailingDTO()
            {
                Nome = Nome,
                Email = Email,
                SiteId = _siteId,
                Ativo = Cadastrar
            };
            var resp = dal.RegistrarMailing(mailing);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}
