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
            context.Response.ContentType = "image/jpg";

            //Gravar arquivo:
            byte[] imagemBytes = null;
            string caminhoCompletoImagem = "";// = "c:\\Ed\\mapa.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Foto01.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Foto02.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Foto03.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Foto04.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Foto05.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Foto06.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Banner_Reserve.png";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Banner_Vale.png";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Evento02.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Evento03.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Img_banner_home.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Img_banner_home2.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Img_banner_home3.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Planta_baixa.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Parceiros_06.jpg";
            caminhoCompletoImagem = @"C:\Ed\Projeto CCBC\Portal\imgs\Assos_05.jpg";
            caminhoCompletoImagem = @"C:\Projetos\CCBC\Documentação\Leiaute Portal ASPX\Portal\imgs\Ico_quick_06.png";
            
            
            //FileStream fs = new FileStream(caminhoCompletoImagem, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fs);
            //imagemBytes = br.ReadBytes(1200000);

            //Arquivo arq = new Arquivo();
            //arq.Content = imagemBytes;
            //arq.Legenda = "Quick6";

            //ArquivoDAL up = new ArquivoDAL();
            //up.GravarArquivo(arq);


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
                        
                        //Nova implementação.
                        if (String.IsNullOrEmpty(arquivo.FileName))
                        {
                            arquivo.FileName = "Arquivo";
                            if (arquivo.Tipo == "application/pdf") arquivo.FileName += ".pdf";
                            else if (arquivo.Tipo == "image/gif") arquivo.FileName += ".gif";
                            else if (arquivo.Tipo == "image/jpeg") arquivo.FileName += ".jpg";
                            else if (arquivo.Tipo == "image/png") arquivo.FileName += ".png";
                            else if (arquivo.Tipo.IndexOf("word") > 0) arquivo.FileName += ".docx";
                            else if (arquivo.Tipo.IndexOf("excel") > 0) arquivo.FileName += ".xlsx";

                        }

                        if (arquivo.Tipo.IndexOf("webp") > -1 && !String.IsNullOrEmpty(context.Request.Browser.Browser)
                            && (context.Request.Browser.Browser.Contains("Firefox") || context.Request.Browser.Browser.Contains("Safari") || context.Request.Browser.Browser.Contains("Opera") || context.Request.Browser.Browser.Contains("Explorer") || context.Request.Browser.Browser.Contains("IE") || context.Request.Browser.Browser.Contains("Internet")))
                        {
                            context.Response.ContentType = "image/png";
                            context.Response.WriteFile(context.Server.MapPath("~/Images/evento-default.png"));
                        }
                        else { 
                            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + arquivo.FileName);
                            MemoryStream ms = new MemoryStream(arquivo.Content);
                            ms.WriteTo(context.Response.OutputStream);
                        }
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