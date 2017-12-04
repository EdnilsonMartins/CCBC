using DTO.Podcast;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PodcastDAL
    {
        public List<Podcast> ListarPodcast(int SiteId, int? PodcastId, int? UsuarioId, int IdiomaId, bool RetornaDefault = true, bool FiltrarPrivacidade = true, int PodcastPaiId = 0)
        {
            List<Podcast> lista = new List<Podcast>();
            Podcast reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Podcast", SiteId, PodcastId, UsuarioId, IdiomaId, RetornaDefault, FiltrarPrivacidade, PodcastPaiId);

            foreach (DataRow r in tabela.Rows)
            {
                reg = new Podcast();
                CarregarDTO(reg, r);
                lista.Add(reg);
            }

            return lista;
        }

        public PodcastResponse Carregar(int SiteId, int IdiomaId, int PodcastId, int UsuarioId, bool FiltrarPrivacidade = true)
        {
            PodcastResponse resposta = new PodcastResponse();
            Podcast podcast;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, IdiomaId);
                    objetoConexao.AdicionarParametro("@PodcastId", SqlDbType.Int, PodcastId);
                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, UsuarioId);
                    objetoConexao.AdicionarParametro("@FiltrarPrivacidade", SqlDbType.Bit, FiltrarPrivacidade);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Podcast"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            podcast = new Podcast();
                            CarregarDTO(podcast, dr);

                            resposta.Podcast = podcast;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }

            return resposta;
        }

        public PodcastResponse Gravar(Podcast Podcast, Podcast PodcastOld, List<PodcastIdiomaExcecao> Extras, List<PodcastIdiomaExcecao> ExtrasOld, string ListaUsuarioGrupo, string ListaUsuario)
        {
            PodcastResponse resposta = new PodcastResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PodcastId", SqlDbType.Int, Podcast.PodcastId);
                    objetoConexao.AdicionarParametro("@PodcastGrupoId", SqlDbType.Int, Podcast.PodcastGrupoId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, Podcast.SiteId);
                    objetoConexao.AdicionarParametro("@Autor", SqlDbType.VarChar, Podcast.Autor);
                    objetoConexao.AdicionarParametro("@LinkURL", SqlDbType.VarChar, Podcast.LinkURL);
                    objetoConexao.AdicionarParametro("@LinkURLImagem", SqlDbType.VarChar, Podcast.LinkURLImagem);
                    objetoConexao.AdicionarParametro("@LinkURLAudio", SqlDbType.VarChar, Podcast.LinkURLAudio);
                    objetoConexao.AdicionarParametro("@Keywords", SqlDbType.VarChar, Podcast.Keywords);
                    objetoConexao.AdicionarParametro("@DireitosAutorais", SqlDbType.VarChar, Podcast.DireitosAutorais);
                    objetoConexao.AdicionarParametro("@ProprietarioNome", SqlDbType.VarChar, Podcast.ProprietarioNome);
                    objetoConexao.AdicionarParametro("@ProprietarioEmail", SqlDbType.VarChar, Podcast.ProprietarioEmail);

                    objetoConexao.AdicionarParametro("@DataPublicacao", SqlDbType.DateTime, Podcast.DataPublicacao);
                    objetoConexao.AdicionarParametro("@Duracao", SqlDbType.VarChar, Podcast.Duracao);
                    objetoConexao.AdicionarParametro("@Tamanho", SqlDbType.VarChar, Podcast.Tamanho);

                    objetoConexao.AdicionarParametro("@PodcastPaiId", SqlDbType.Int, Podcast.PodcastPaiId);

                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Podcast"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.Podcast = Podcast;
                            resposta.Podcast.PodcastId = (int)dt.Rows[0]["PodcastId"];
                        }
                    }
                }

                using (ConexaoDB objetoConexao = new ConexaoDB())
                {

                    objetoConexao.AdicionarParametro("@PodcastIdiomaExcecaoId", SqlDbType.Int, 0);
                    objetoConexao.AdicionarParametro("@PodcastId", SqlDbType.Int, resposta.Podcast.PodcastId);
                    objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, Podcast.Detalhe.IdiomaId);
                    objetoConexao.AdicionarParametro("@Titulo", SqlDbType.VarChar, Podcast.Detalhe.Titulo);
                    objetoConexao.AdicionarParametro("@SubTitulo", SqlDbType.VarChar, Podcast.Detalhe.SubTitulo);
                    objetoConexao.AdicionarParametro("@TituloEpisodio", SqlDbType.VarChar, Podcast.Detalhe.TituloEpisodio);
                    objetoConexao.AdicionarParametro("@Descricao", SqlDbType.VarChar, Podcast.Detalhe.Descricao);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_PodcastIdiomaExcecao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.Podcast = Podcast;
                        }
                    }
                }

                foreach (var Extra in Extras)
                {
                    using (ConexaoDB objetoConexao = new ConexaoDB())
                    {

                        objetoConexao.AdicionarParametro("@PodcastIdiomaExcecaoId", SqlDbType.Int, 0);
                        objetoConexao.AdicionarParametro("@PodcastId", SqlDbType.Int, resposta.Podcast.PodcastId);
                        objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, Extra.IdiomaId);
                        objetoConexao.AdicionarParametro("@Titulo", SqlDbType.VarChar, Extra.Titulo);
                        objetoConexao.AdicionarParametro("@SubTitulo", SqlDbType.VarChar, Extra.SubTitulo);
                        objetoConexao.AdicionarParametro("@TituloEpisodio", SqlDbType.VarChar, Extra.TituloEpisodio);
                        objetoConexao.AdicionarParametro("@Descricao", SqlDbType.VarChar, Extra.Descricao);

                        using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_PodcastIdiomaExcecao"))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                resposta.Resposta.Erro = false;
                                resposta.Resposta.Mensagem = "";
                                resposta.Podcast = Podcast;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposta.Resposta.Erro = true;
                resposta.Resposta.Mensagem = ex.Message;
                //logBLL.Error(ex);
            }
            return resposta;
        }

        public PodcastResponse ExcluirPodcast(int PodcastId)
        {
            PodcastResponse resposta = new PodcastResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PodcastId", SqlDbType.Int, PodcastId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_Podcast"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Podcast = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resposta.Resposta.Erro = true;
                resposta.Resposta.Mensagem = ex.Message;

                //logBLL.Error(ex);
            }
            return resposta;
        }

        private void CarregarDTO(Podcast dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["PodcastId"]))
                dto.PodcastId = (int)dr["PodcastId"];
            if (Util.GetNonNull(dr["PodcastGrupoId"]))
                dto.PodcastGrupoId = (int)dr["PodcastGrupoId"];
            if (Util.GetNonNull(dr["PodcastGrupo"]))
                dto.Complemento.PodcastGrupo = dr["PodcastGrupo"].ToString();
            if (Util.GetNonNull(dr["Autor"]))
                dto.Autor = dr["Autor"].ToString();
            if (Util.GetNonNull(dr["Titulo"]))
                dto.Detalhe.Titulo = dr["Titulo"].ToString();
            if (Util.GetNonNull(dr["LinkURL"]))
                dto.LinkURL = dr["LinkURL"].ToString();
            if (Util.GetNonNull(dr["LinkURLImagem"]))
                dto.LinkURLImagem = dr["LinkURLImagem"].ToString();
            if (Util.GetNonNull(dr["LinkURLAudio"]))
                dto.LinkURLAudio = dr["LinkURLAudio"].ToString();
            if (Util.GetNonNull(dr["Keywords"]))
                dto.Keywords = dr["Keywords"].ToString();
            if (Util.GetNonNull(dr["Descricao"]))
                dto.Detalhe.Descricao = dr["Descricao"].ToString();
            if (Util.GetNonNull(dr["TituloEpisodio"]))
                dto.Detalhe.TituloEpisodio = dr["TituloEpisodio"].ToString();
            if (Util.GetNonNull(dr["DireitosAutorais"]))
                dto.DireitosAutorais = dr["DireitosAutorais"].ToString();
            if (Util.GetNonNull(dr["ProprietarioNome"]))
                dto.ProprietarioNome = dr["ProprietarioNome"].ToString();
            if (Util.GetNonNull(dr["ProprietarioEmail"]))
                dto.ProprietarioEmail = dr["ProprietarioEmail"].ToString();
            if (Util.GetNonNull(dr["DataPublicacao"]))
                dto.DataPublicacao = (DateTime)dr["DataPublicacao"];
            if (Util.GetNonNull(dr["Duracao"]))
            {
                int duracao;
                Int32.TryParse(dr["Duracao"].ToString(), out duracao);
                dto.Duracao = duracao;
            }
            if (Util.GetNonNull(dr["SubTitulo"]))
                dto.Detalhe.SubTitulo = dr["SubTitulo"].ToString();
            if (Util.GetNonNull(dr["Tamanho"]))
                dto.Tamanho = dr["Tamanho"].ToString();

            if (Util.GetNonNull(dr["PodcastPaiId"]))
            {
                int podcastPaiId;
                Int32.TryParse(dr["PodcastPaiId"].ToString(), out podcastPaiId);
                dto.PodcastPaiId = podcastPaiId;
            }
        }
    }
}
