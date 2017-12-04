using DAL;
using DTO.Arquivo;
using DTO.Podcast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BackofficeCMS
{
    /// <summary>
    /// Summary description for HandlerXML
    /// </summary>
    public class HandlerPodcast : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            context.Response.ContentType = "text/xml";
            //context.Response.Write("Hello World");


            //string Autor = "Tendenza User";
            //string Titulo = "Canal Podcast 5";
            //string Categoria = "Technology";
            //string Link = "http://www.tendenza.com.br";
            //string LinkImagem = "http://www.ccbc.org.br/podcastCMS/arte2.png";
            //string LinkAudio = "http://www.ccbc.org.br/podcastCMS/hello2.mp3";
            //string Keywords = "Palavras chaves";
            //string Descricao = "Detalhes sobre o segundo episódio da série você encontra aqui.";
            //string TituloEpisodio = "Episódio 5";
            //string DireitosAutorais = "2017 - Tendenza - Podcast Testing";
            //string ProprietarioNome = "Tendenza";
            //string ProprietarioEmail = "suporte@tendenza.com.br";
            //string DataPublicacao = DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss ") + "GMT"; //"Fri, 30 Nov 2017 11:06:42 GMT";
            //string Duracao = "94"; //em Segundos
            //string SubTitulo = "Este é o segundo episódio da série.";
            //string Tamanho = "3612672";

            string Autor = "";
            string Titulo = "";
            string Categoria = "";
            string Link = "";
            string LinkImagem = "";
            string LinkAudio = "";
            string Keywords = "";
            string Descricao = "";
            string TituloEpisodio = "";
            string DireitosAutorais = "";
            string ProprietarioNome = "";
            string ProprietarioEmail = "";
            string DataPublicacao = "";
            string Duracao = ""; 
            string SubTitulo = "";
            string Tamanho = "";

            string Titulo1 = "";
            string Descricao1 = "";
            

            PodcastDAL podcastDAL = new PodcastDAL();
            
            string PodcastId = context.Request.QueryString["id"];

            List<Podcast> Episodios = new List<Podcast>();


            if (!String.IsNullOrEmpty(PodcastId) && PodcastId != "0")
            {

                PodcastResponse resp = new PodcastResponse();
                resp = podcastDAL.Carregar(1, 1, Convert.ToInt32(PodcastId), 0, false);



                if (resp.Resposta.Erro == false)
                {

                    Episodios = podcastDAL.ListarPodcast(1, Convert.ToInt32(PodcastId), 0, 1, false, false, resp.Podcast.PodcastId);

                    ConfiguracaoDAL configDAL = new ConfiguracaoDAL();
                    var config = configDAL.CarregarConfiguracao(1);

                    var baseURL = config.HostBase;
                    var retorno = "";
                    if (!String.IsNullOrEmpty(PodcastId) && PodcastId != "" && PodcastId != "0") retorno = baseURL + "admin/podcastCMS/" ;

                    ArquivoDAL arquivoDAL = new ArquivoDAL();

                    //Link da Capa
                    List<Arquivo> listaArquivos = arquivoDAL.ListarArquivosGaleria(resp.Podcast.PodcastId, 7, (int)Util.ARQUIVO_CATEGORIA_TIPO.PODCAST, 0);
                    if (listaArquivos.Count > 0)
                    {
                        LinkImagem = resp.Podcast.PodcastId.ToString() + "_5_" + listaArquivos[0].FileName;
                        LinkImagem = retorno + LinkImagem;
                    }

                    


                    Podcast p = resp.Podcast;

                    Autor = p.Autor;
                    Titulo = p.Detalhe.Titulo;
                    Categoria = p.Complemento.PodcastGrupo;
                    Link = p.LinkURLImagem;
                    //LinkImagem = p.LinkURLImagem;
                    //LinkAudio = p.LinkURLAudio;
                    Keywords = p.Keywords;
                    Descricao = p.Detalhe.Descricao;
                    TituloEpisodio = p.Detalhe.TituloEpisodio;
                    DireitosAutorais = p.DireitosAutorais;
                    ProprietarioNome = p.ProprietarioNome;
                    ProprietarioEmail = p.ProprietarioEmail;
                    if (p.DataPublicacao != null)
                        DataPublicacao = ((DateTime)p.DataPublicacao).ToString("ddd, dd MMM yyyy HH:mm:ss ") + "GMT"; //"Fri, 30 Nov 2017 11:06:42 GMT";
                    if (p.Duracao != null)
                        Duracao = ((int)p.Duracao).ToString();
                    SubTitulo = p.Detalhe.SubTitulo;
                    Tamanho = p.Tamanho;

                    Titulo1 = p.Detalhe.Titulo;
                    Descricao1 = p.Detalhe.Descricao;
                }

            }


            DataPublicacao = DataPublicacao.Replace("dom", "Sun");
            DataPublicacao = DataPublicacao.Replace("seg", "Mon");
            DataPublicacao = DataPublicacao.Replace("ter", "Tue");
            DataPublicacao = DataPublicacao.Replace("qua", "Wed");
            DataPublicacao = DataPublicacao.Replace("qui", "Thu");
            DataPublicacao = DataPublicacao.Replace("sex", "Fri");
            DataPublicacao = DataPublicacao.Replace("sab", "Sat");

            DataPublicacao = DataPublicacao.Replace("dez", "Dec");
            DataPublicacao = DataPublicacao.Replace("jan", "Jan");
            DataPublicacao = DataPublicacao.Replace("fev", "Feb");
            DataPublicacao = DataPublicacao.Replace("mar", "Mar");
            DataPublicacao = DataPublicacao.Replace("abr", "Apr");
            DataPublicacao = DataPublicacao.Replace("mai", "May");
            DataPublicacao = DataPublicacao.Replace("jun", "Jun");
            DataPublicacao = DataPublicacao.Replace("jul", "Jul");
            DataPublicacao = DataPublicacao.Replace("ago", "Aug");
            DataPublicacao = DataPublicacao.Replace("set", "Sep");
            DataPublicacao = DataPublicacao.Replace("out", "Oct");
            DataPublicacao = DataPublicacao.Replace("nov", "Nov");

            
            string XMLiTunres = @"<?xml version=""1.0"" encoding=""utf-8""?>
<rss xmlns:content=""http://purl.org/rss/1.0/modules/content/""
xmlns:itunes=""http://www.itunes.com/dtds/podcast-1.0.dtd""
xmlns:media=""http://search.yahoo.com/mrss/"" version=""2.0"">
  <channel>
    <itunes:category text=""{2}""/>
    <itunes:explicit>NO</itunes:explicit>
    <itunes:keywords>{6}</itunes:keywords>
    <itunes:summary>{7}</itunes:summary>
    <itunes:subtitle>{1}</itunes:subtitle>
    <itunes:image href=""{4}""/>
    <itunes:author>{0}</itunes:author>
    <webMaster>{11}</webMaster>
    <copyright>{9}</copyright>
    <language>pt-br</language>
    <title>{1}</title>
    <link>{3}</link>
    <description>{7}</description>
    <pubDate>{12}</pubDate>
    <itunes:owner>
      <itunes:name>{10}</itunes:name>
      <itunes:email>{11}</itunes:email>
    </itunes:owner>
    <image>
      <title>{1}</title>
      <link>{3}</link>
      <url>{4}</url>
    </image>
    {16}
  </channel>
</rss>";

            string XMLiTunesFinal = "";
            string XMLiTunesItem = @"<item>
      <author>{0}</author>
      <title>{8}</title>
      <description>{14}</description>
      <link>{3}</link>
      <pubDate>{12}</pubDate>
      <itunes:duration>{13}</itunes:duration>
      <itunes:keywords>{6}</itunes:keywords>
      <itunes:summary>{14}</itunes:summary>
      <itunes:subtitle>{7}</itunes:subtitle>
      <itunes:author>{0}</itunes:author>
      <itunes:explicit>NO</itunes:explicit>
      <itunes:block>no</itunes:block>
      <enclosure url=""{5}"" length=""{15}"" type=""audio/mpeg""/>
    </item>";

            foreach (var e in Episodios)
            {
                TituloEpisodio = e.Detalhe.Titulo;
                SubTitulo = e.Detalhe.SubTitulo;
                Descricao = e.Detalhe.Descricao;
                if(e.Duracao != null)
                    Duracao = ((int)e.Duracao).ToString();

                ConfiguracaoDAL configDAL2 = new ConfiguracaoDAL();
                var config2 = configDAL2.CarregarConfiguracao(1);

                var baseURL2 = config2.HostBase;
                var retorno2 = "";
                if (!String.IsNullOrEmpty(e.PodcastId.ToString()) && e.PodcastId.ToString() != "" && e.PodcastId.ToString() != "0") retorno2 = baseURL2 + "admin/podcastCMS/";

                ArquivoDAL arquivoDAL2 = new ArquivoDAL();
                //Link do Áudio
                LinkAudio = "";
                var listaArquivos2 = arquivoDAL2.ListarArquivosGaleria(e.PodcastId, 8, (int)Util.ARQUIVO_CATEGORIA_TIPO.PODCAST, 0);
                if (listaArquivos2.Count > 0)
                {
                    LinkAudio = e.PodcastId.ToString() + "_5_" + listaArquivos2[0].FileName;
                    LinkAudio = retorno2 + LinkAudio;
                }

                XMLiTunesFinal += String.Format(XMLiTunesItem,
                    Autor,
                    Titulo,
                    Categoria,
                    Link,
                    LinkImagem,
                    LinkAudio,
                    Keywords,
                    SubTitulo,
                    TituloEpisodio,
                    DireitosAutorais,
                    ProprietarioNome,
                    ProprietarioEmail,
                    DataPublicacao,
                    Duracao,
                    Descricao,
                    Tamanho
                    );
            }


            if (!String.IsNullOrEmpty(Categoria))
                Categoria = Categoria.Replace("&", "&amp;");

            XMLiTunres = string.Format(XMLiTunres,
                Autor,
                Titulo1,
                Categoria,
                Link,
                LinkImagem,
                LinkAudio,
                Keywords,
                Descricao1,
                TituloEpisodio,
                DireitosAutorais,
                ProprietarioNome,
                ProprietarioEmail,
                DataPublicacao,
                Duracao,
                Descricao,
                Tamanho,
                XMLiTunesFinal
                );


            context.Response.Write(XMLiTunres);

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