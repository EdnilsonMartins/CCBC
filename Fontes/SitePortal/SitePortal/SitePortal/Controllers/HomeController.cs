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
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace SitePortal.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //var UsuarioId = "";
            //var UsuarioNome = "";
            //if (Session["UsuarioId"] != null)
            //    UsuarioId = Session["UsuarioId"].ToString();
            //if (Session["UsuarioNome"] != null)
            //    UsuarioNome = Session["UsuarioNome"].ToString();
            //Portal model = new Portal().CarregarModel(true, UsuarioId, UsuarioNome);

            Portal model = new Portal().CarregarModel(true);

            model.isHome = true;

            //Implementando Callback na Home:
            //var CallbackPortal = HttpContext.Request.Cookies["CallbackPortal"] != null ? HttpContext.Request.Cookies["CallbackPortal"].Value : "";
            //if (!String.IsNullOrEmpty(CallbackPortal))
            //{
            //    var _callbackPortal = new HttpCookie("CallbackPortal", null) { HttpOnly = true };
            //    Response.AppendCookie(_callbackPortal);
            //    HttpContext.Request.Cookies.Set(_callbackPortal);
            //    Response.RedirectPermanent(CallbackPortal);
            //}
            //else if (model.SiteId == 0)
            //{
            //    Response.RedirectPermanent(Url.Content("~/Portal/Index"));
            //}
            var CallbackPortal = HttpContext.Request.Cookies["CallbackPortal"] != null ? HttpContext.Request.Cookies["CallbackPortal"].Value : "";
            if (model.SiteId == 0)
            {
                Response.RedirectPermanent(Url.Content("~/Portal/Index"));
            }
            else
            {
                
                var _callbackPortal = new HttpCookie("CallbackPortal", null) { HttpOnly = true };
                Response.AppendCookie(_callbackPortal);
                HttpContext.Request.Cookies.Set(_callbackPortal);

                var _callbackPortal_Anterior = new HttpCookie("CallbackPortal_Anterior", null) { HttpOnly = true };
                Response.AppendCookie(_callbackPortal_Anterior);
                HttpContext.Request.Cookies.Set(_callbackPortal_Anterior);
            }

            return View(model);
        }

        public ActionResult GetWeatherExchangeFee(int CidadeId = 0)
        {
            string cidade = "";
            string pais = "Brazil";

            string urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22S%C3%A3o%20Paulo%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

            switch (CidadeId) {
                case 1:
                    cidade = "Quebec, Que";
                    pais = "Canada";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%202344924&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "6325494";
                    break;
                case 2:
                    cidade = "Toronto Pearson Int'L. Ont.";
                    pais = "Canada";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%204118&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "6167865";
                    break;
                case 3:
                    cidade = "Vancouver International Air-Port, B. C.";
                    pais = "Canada";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%209807&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "6173331";
                    break;
                case 4:
                    cidade = "Sao Paulo";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%20455827&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "3448439";
                    break;
                case 5:
                    cidade = "Brasilia Aeroporto";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%20455819&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "3469058";
                    break;
                case 6:
                    cidade = "Rio de Janeiro Aeroporto";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%20455825&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "3451190";
                    break;
                case 7:
                    cidade = "Recife Aeroporto";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%20455824&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "3390760";
                    break;
                //case 8:
                //    cidade = "Joao Pessoa";
                //    break;
                case 9:
                    cidade = "Curitiba";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%20455822&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "3464975";
                    break;
                case 10:
                    cidade = "Salvador Aeroporto";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%20455826&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "6321026";
                    break;
                case 11:
                    cidade = "Belo Horizonte";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%20455821&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "3470127";
                    break;
                case 12:
                    cidade = "Porto Alegre";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%20455823&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "3452925";
                    break;
                case 13:
                    cidade = "Montreal-Est";
                    pais = "Canada";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%203534&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "6077251";
                    break;
                case 14:
                    cidade = "Edmonton Municipal Alta.";
                    pais = "Canada";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%2012511647&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "5946768";
                    break;
                case 15:
                    cidade = "Winnipeg Int. Airportman.";
                    pais = "Canada";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%202972&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "6183235";
                    break;
                case 16:
                    cidade = "Ottawa Int'L. Ont.";
                    pais = "Canada";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%2091982014&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "6094817";
                    break;
                case 17:
                    cidade = "Calgary International, Alta.";
                    pais = "Canada";
                    //urlY = "https://query.yahooapis.com/v1/public/yql?q=select%20item.condition%20from%20weather.forecast%20where%20woeid%20=%208775&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

                    urlY = "5913490";
                    break;
            }

            urlY = String.Format("http://api.openweathermap.org/data/2.5/forecast/city?id={0}&APPID=6398a401b84ef7a5e5e41e04d6abc2ab", urlY);

            string temperatura = "";
            string ceu = "";
            //try
            //{
            //    using (Weather.GlobalWeatherSoapClient w = new Weather.GlobalWeatherSoapClient())
            //    {
            //        string respWeather = w.GetWeather(cidade, pais);
            //        using (XmlTextReader xtr = new XmlTextReader(new System.IO.StringReader(respWeather)))
            //        {
            //            using (DataSet ds = new DataSet())
            //            {
            //                ds.ReadXml(xtr);
            //                if (ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count >= 1)
            //                {
            //                    temperatura = ds.Tables[0].Rows[0]["Temperature"].ToString().Replace("F", "°F").Replace("C", "°C");
            //                    temperatura = temperatura.Substring(temperatura.IndexOf("(") + 1, temperatura.IndexOf(")") - 1 - temperatura.IndexOf("("));
            //                    //ceu = ds.Tables[0].Rows[0]["SkyConditions"].ToString();
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    temperatura = "unavailable";
            //}

            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlY);

            //request.MaximumAutomaticRedirections = 4;
            //request.MaximumResponseHeadersLength = 4;
            //request.Credentials = CredentialCache.DefaultCredentials;
           
            // int i = 0;
            //while (i < 10)
            //{
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream receiveStream = response.GetResponseStream())
                        {
                            using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                            {
                                string resposta = readStream.ReadToEnd();

                                dynamic stuff = JsonConvert.DeserializeObject(resposta);

                                temperatura = stuff.list[0].main.temp;

                                NumberFormatInfo provider = new NumberFormatInfo();
                                provider.NumberGroupSeparator = ",";
                                provider.NumberDecimalSeparator = ".";

                                double t = (double)Convert.ToDecimal(temperatura, provider);
                                //double t = Convert.ToDouble(temperatura.Replace(".",","));

                                t = (t - 273.15); //Kelvin     //C = (F - 32)/1.8
                                temperatura = Math.Round(t, 1).ToString() + " °C";

                                response.Close();
                                readStream.Close();

                                //break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //i++;
                    temperatura = "unavailable";
                }
            //}


            return Json(new { Temperatura = temperatura, Ceu = ceu }, JsonRequestBehavior.AllowGet);
        }
        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
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

                    //Session["UsuarioId"] = resp.UsuarioId;
                    //Session["UsuarioNome"] = resp.Nome;
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

            //Session["UsuarioId"] = null;
            //Session["UsuarioNome"] = null;
            //Session.Clear();
            //Session.Abandon();

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

            Portal model = new Portal().CarregarModel(true,int.Parse(site));

            model.isHome = true;

            //Implementando Callback na Home:
            //var CallbackPortal = HttpContext.Request.Cookies["CallbackPortal"] != null ? HttpContext.Request.Cookies["CallbackPortal"].Value : "";
            //if (!String.IsNullOrEmpty(CallbackPortal))
            //{
            //    var _callbackPortal = new HttpCookie("CallbackPortal", null) { HttpOnly = true };
            //    Response.AppendCookie(_callbackPortal);
            //     Response.RedirectPermanent(CallbackPortal);
            //     return null;
            //     // return RedirectToAction(CallbackPortal);
            //}
            //else 
            if (_site == "0")
            {
                Response.RedirectPermanent(Url.Content("~/Portal/Index"));
                return null;
            }
            else
            {
                var _callbackPortal = new HttpCookie("CallbackPortal", null) { HttpOnly = true };
                Response.AppendCookie(_callbackPortal);
                HttpContext.Request.Cookies.Set(_callbackPortal);

                                var _callbackPortal_Anterior = new HttpCookie("CallbackPortal_Anterior", null) { HttpOnly = true };
                Response.AppendCookie(_callbackPortal_Anterior);
                HttpContext.Request.Cookies.Set(_callbackPortal_Anterior);
            }

            return View("Index", model);

        }

        public ActionResult SessionCulture(string lang)
        {
            var langCookie = new HttpCookie("lang", lang) { HttpOnly = true };
            Response.AppendCookie(langCookie);
            HttpContext.Request.Cookies.Set(langCookie);
            ConfigurarDadosDeCultura(lang);

            var CallbackPortal = HttpContext.Request.Cookies["CallbackPortal_Anterior"] != null ? HttpContext.Request.Cookies["CallbackPortal_Anterior"].Value : "";
            if (!String.IsNullOrEmpty(CallbackPortal))
            {
                var _callbackPortal = new HttpCookie("CallbackPortal_Anterior", null) { HttpOnly = true };
                Response.AppendCookie(_callbackPortal);
                HttpContext.Request.Cookies.Set(_callbackPortal);
                Response.RedirectPermanent(CallbackPortal);
            }
            
            return RedirectToAction("Index", "Home", new { culture = lang });
        }

        private void ConfigurarDadosDeCultura(string lang)
        {
            var currentCulture = HttpContext.Request.Cookies["lang"] != null
                ? HttpContext.Request.Cookies["lang"].Value
                : "en-US";
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
