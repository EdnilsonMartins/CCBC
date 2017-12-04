using DAL;
using DTO.Arquivo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackofficeCMS.Controllers
{
    public class ArquivoController : Controller
    {
        [HttpPost]
        public ContentResult UploadFiles()
        {
            var r = new List<Arquivo>();

            int _OwnerId = Request.Form["OwnerId"] == null ? 0 : Convert.ToInt32(Request.Form["OwnerId"]);
            int _ArquivoCategoriaTipoId = Request.Form["ArquivoCategoriaTipoId"] == null ? 0 : Convert.ToInt32(Request.Form["ArquivoCategoriaTipoId"]);
            long _ArquivoId = String.IsNullOrEmpty(Request.Form["ArquivoId"]) ? 0 : Convert.ToInt64(Request.Form["ArquivoId"]);
            string _Legenda = Request.Form["Legenda"];
            int? _IdiomaId = Request.Form["Idioma"] != "" ? Convert.ToInt32(Request.Form["Idioma"]) : new Nullable<int>();
            string _ListaCategoria = Request.Form["Categoria"] == null ? "" : Request.Form["Categoria"].ToString();
            bool _NovoContent = false;
            int? _pastaId = Request.Form["PastaId"] == null ? new Nullable<int>() : Convert.ToInt32(Request.Form["PastaId"]);

            if (Request.Files.Count > 0)
            {
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;

                    if (hpf.ContentLength == 0)
                        continue;

                    if (_ArquivoCategoriaTipoId == (int)Util.ARQUIVO_CATEGORIA_TIPO.PODCAST)
                    {
                        string savedFileName = Path.Combine(Server.MapPath("~/podcastCMS"), _OwnerId.ToString() + "_" + _ArquivoCategoriaTipoId + "_" + Path.GetFileName(hpf.FileName));
                        hpf.SaveAs(savedFileName);
                    }

                    byte[] image = new byte[hpf.ContentLength];
                    hpf.InputStream.Read(image, 0, image.Length);

                    r.Add(new Arquivo()
                    {
                        ArquivoId = _ArquivoId,
                        FileName = hpf.FileName,
                        Tamanho = hpf.ContentLength,
                        Tipo = hpf.ContentType,
                        Content = image,
                        Legenda = _Legenda,
                        ListaCategoria = _ListaCategoria,
                        PastaId = _pastaId,
                        IdiomaId = _IdiomaId
                    });

                    //Possui arquivo selecionado -> atualiza campo no banco.
                    _NovoContent = true;
                }
            }
            else
            {
                r.Add(new Arquivo()
                {
                    ArquivoId = _ArquivoId,
                    Legenda = _Legenda,
                    ListaCategoria = _ListaCategoria,
                    IdiomaId = _IdiomaId
                });

                //Não possui arquivo -> não atualiza o campo no banco.
                _NovoContent = false;
            }

            long ArquivoId = 0;
            //Gravação no banco de dados.
            foreach (var a in r)
            {
                ArquivoDAL dal = new ArquivoDAL();
                var arquivo = dal.GravarArquivo(a, _NovoContent);
                ArquivoId = arquivo.Arquivo.ArquivoId;
                dal.GravarArquivoGaleria(_OwnerId, ArquivoId, _ArquivoCategoriaTipoId);
            }

            return Content("{\"name\":\"" + r[0].FileName + "\",\"type\":\"" + r[0].Tipo + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Tamanho) + "\",\"ArquivoId\":\"" + ArquivoId + "\"}", "application/json");
        }

        public ActionResult GravarArquivo(int _OwnerId, long _ArquivoId, string _Legenda, string _ListaCategoria, int _ArquivoCategoriaTipoId, int? _IdiomaId)
        {
            Arquivo a = new Arquivo();
            ArquivoDAL dal = new ArquivoDAL();

            a.ArquivoId = _ArquivoId;
            a.Legenda = _Legenda;
            a.IdiomaId = _IdiomaId;
            a.ListaCategoria = _ListaCategoria;

            var arquivo = dal.GravarArquivo(a, false);
            _ArquivoId = arquivo.Arquivo.ArquivoId;
            dal.GravarArquivoGaleria(_OwnerId, _ArquivoId, _ArquivoCategoriaTipoId);

            return Json(true, JsonRequestBehavior.DenyGet);
        }

        public ActionResult ListarArquivos(int OwnerId, int ArquivoCategoriaTipoId, int PastaId = 0)
        {
            ArquivoDAL model = new ArquivoDAL();
            return Json(model.ListarArquivosGaleria(OwnerId, null, ArquivoCategoriaTipoId, PastaId), JsonRequestBehavior.DenyGet);
        }

        public ActionResult CarregarArquivo(long ArquivoId, bool RetornarContent)
        {
            ArquivoDAL model = new ArquivoDAL();
            Arquivo arquivo = model.CarregarArquivo(ArquivoId);
            if (!RetornarContent)
            {
                arquivo.Content = null;
            }
            return Json(arquivo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ExcluirArquivo(long ArquivoId)
        {
            ArquivoDAL model = new ArquivoDAL();
            return Json(model.ExcluirArquivo(ArquivoId), JsonRequestBehavior.DenyGet);
        }
    }
}
