using DAL;
using DTO.Arquivo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace SitePortal.WebForm
{
    /// <summary>
    /// Summary description for HandlerFile
    /// </summary>
    public class HandlerFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/pdf";

            try
            {
                var ArquivoId = context.Request.QueryString["id"];

                if (!String.IsNullOrEmpty(ArquivoId) && ArquivoId != "0")
                {
                    ArquivoDAL model = new ArquivoDAL();
                    var arquivo = model.CarregarArquivo(Convert.ToInt32(ArquivoId));

                    if (arquivo.Content != null)
                    {
                        context.Response.ContentType = arquivo.Tipo;
                        MemoryStream ms = new MemoryStream(arquivo.Content);
                        ms.WriteTo(context.Response.OutputStream);
                    }
                }
                //context.Response.ContentType = "image/jpg";
                //context.Response.WriteFile(context.Server.MapPath("~/Images/Portal/Evento02.jpg"));

            }
            catch (Exception ex)
            {
                var s = ex.Message;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}