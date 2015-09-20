using DTO.Banner;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class BannerDAL
    {
        public List<Banner> ListarBanner(int SiteId, int? BannerId, int? BannerTipoId, DateTime? DataValidade, int? UsuarioId, int IdiomaId, bool RetornaDefault = true, bool Apenas1 = false, int? PublicacaoId = null, bool FiltrarPrivacidade = true)
        {
            List<Banner> lista = new List<Banner>();
            Banner reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Banner", SiteId, BannerId, BannerTipoId, DataValidade, UsuarioId, IdiomaId, RetornaDefault, Apenas1, PublicacaoId, FiltrarPrivacidade);


            foreach (DataRow r in tabela.Rows)
            {
                reg = new Banner();
                CarregarDTO_Banner(reg, r);
                lista.Add(reg);
            }

            return lista;
        }

        public BannerResponse Carregar(int SiteId, int IdiomaId, int BannerId, int UsuarioId, bool FiltrarPrivacidade = true)
        {
            BannerResponse resposta = new BannerResponse();
            Banner banner;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, IdiomaId);
                    objetoConexao.AdicionarParametro("@BannerId", SqlDbType.Int, BannerId);
                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, UsuarioId);
                    objetoConexao.AdicionarParametro("@FiltrarPrivacidade", SqlDbType.Bit, FiltrarPrivacidade);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Banner"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            banner = new Banner();
                            CarregarDTO_Banner(banner, dr);

                            resposta.Banner = banner;
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

        public BannerResponse Gravar(Banner Banner, Banner BannerOld, List<BannerIdiomaExcecao> Extras, List<BannerIdiomaExcecao> ExtrasOld, string ListaUsuarioGrupo, string ListaUsuario)
        {
            BannerResponse resposta = new BannerResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@BannerId", SqlDbType.Int, Banner.BannerId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, Banner.SiteId);
                    objetoConexao.AdicionarParametro("@BannerTipoId", SqlDbType.Int, Banner.BannerTipoId);
                    objetoConexao.AdicionarParametro("@TargetId", SqlDbType.Int, Banner.TargetId);
                    objetoConexao.AdicionarParametro("@Data", SqlDbType.DateTime, Banner.Data);
                    objetoConexao.AdicionarParametro("@DataValidade", SqlDbType.DateTime, Banner.DataValidade);
                    objetoConexao.AdicionarParametro("@Posicao", SqlDbType.Int, Banner.Posicao);
                    objetoConexao.AdicionarParametro("@LinkURL", SqlDbType.VarChar, Banner.LinkURL);
                    objetoConexao.AdicionarParametro("@Privado", SqlDbType.Bit, Banner.Complemento.Privado);
                    objetoConexao.AdicionarParametro("@Referencia", SqlDbType.VarChar, Banner.Referencia);
                    objetoConexao.AdicionarParametro("@Ativo", SqlDbType.Bit, Banner.Ativo);
                    objetoConexao.AdicionarParametro("@ListaUsuarioGrupo", SqlDbType.VarChar, ListaUsuarioGrupo);
                    objetoConexao.AdicionarParametro("@ListaUsuario", SqlDbType.VarChar, ListaUsuario);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Banner"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.Banner = Banner;
                            resposta.Banner.BannerId = (int)dt.Rows[0]["BannerId"];
                        }
                    }
                }

                using (ConexaoDB objetoConexao = new ConexaoDB())
                {

                    objetoConexao.AdicionarParametro("@BannerIdiomaExcecaoId", SqlDbType.Int, 0);
                    objetoConexao.AdicionarParametro("@BannerId", SqlDbType.Int, resposta.Banner.BannerId);
                    objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, Banner.Detalhe.IdiomaId);
                    objetoConexao.AdicionarParametro("@Titulo", SqlDbType.VarChar, Banner.Detalhe.Titulo);
                    objetoConexao.AdicionarParametro("@Resumo", SqlDbType.VarChar, Banner.Detalhe.Resumo);
                    objetoConexao.AdicionarParametro("@Descricao", SqlDbType.VarChar, Banner.Detalhe.Descricao);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_BannerIdiomaExcecao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.Banner = Banner;
                        }
                    }
                }

                foreach (var Extra in Extras)
                {
                    using (ConexaoDB objetoConexao = new ConexaoDB())
                    {

                        objetoConexao.AdicionarParametro("@BannerIdiomaExcecaoId", SqlDbType.Int, 0);
                        objetoConexao.AdicionarParametro("@BannerId", SqlDbType.Int, resposta.Banner.BannerId);
                        objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, Extra.IdiomaId);
                        objetoConexao.AdicionarParametro("@Titulo", SqlDbType.VarChar, Extra.Titulo);
                        objetoConexao.AdicionarParametro("@Resumo", SqlDbType.VarChar, Extra.Resumo);
                        objetoConexao.AdicionarParametro("@Descricao", SqlDbType.VarChar, Extra.Descricao);

                        using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_BannerIdiomaExcecao"))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                resposta.Resposta.Erro = false;
                                resposta.Resposta.Mensagem = "";
                                resposta.Banner = Banner;
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

        public BannerResponse GravarEvento(Util.BANNER_EVENTO_TIPO BannerEventoTipoId, int BannerId, long ArquivoId)
        {
            BannerResponse resposta = new BannerResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@BannerEventoTipoId", SqlDbType.Int, (int)BannerEventoTipoId);
                    objetoConexao.AdicionarParametro("@BannerId", SqlDbType.Int, BannerId);
                    objetoConexao.AdicionarParametro("@ArquivoId", SqlDbType.BigInt, ArquivoId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_BannerEvento"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
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

        public BannerResponse ExcluirBanner(int BannerId)
        {
            BannerResponse resposta = new BannerResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@BannerId", SqlDbType.Int, BannerId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_Banner"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Banner = null;
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

        private void CarregarDTO_Banner(Banner dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["BannerId"]))
                dto.BannerId = (int)dr["BannerId"];
            if (Util.GetNonNull(dr["BannerTipoId"]))
                dto.BannerTipoId = (int)dr["BannerTipoId"];
            if (Util.GetNonNull(dr["Data"]))
                dto.Data = new DateTime(((DateTime)dr["Data"]).Year, ((DateTime)dr["Data"]).Month, ((DateTime)dr["Data"]).Day);
            if (Util.GetNonNull(dr["DataValidade"]))
                dto.DataValidade = (DateTime)dr["DataValidade"];
            if (Util.GetNonNull(dr["Posicao"]))
                dto.Posicao = (int)dr["Posicao"];
            if (Util.GetNonNull(dr["TargetId"]))
                dto.TargetId = (int)dr["TargetId"];
            if (Util.GetNonNull(dr["LinkURL"]))
                dto.LinkURL = dr["LinkURL"].ToString();
            if (Util.GetNonNull(dr["Ativo"]))
                dto.Ativo = (bool)dr["Ativo"];

            if (Util.GetNonNull(dr["TotalVisualizacao"]))
                dto.TotalVisualizacao = (int)dr["TotalVisualizacao"];
            if (Util.GetNonNull(dr["TotalClique"]))
                dto.TotalClique = (int)dr["TotalClique"];

            if (Util.GetNonNull(dr["Titulo"]))
                dto.Detalhe.Titulo = dr["Titulo"].ToString();
            if (Util.GetNonNull(dr["Resumo"]))
                dto.Detalhe.Resumo = dr["Resumo"].ToString();
            if (Util.GetNonNull(dr["Descricao"]))
                dto.Detalhe.Descricao = dr["Descricao"].ToString();
            if (Util.GetNonNull(dr["Referencia"]))
                dto.Referencia = dr["Referencia"].ToString();

            if (Util.GetNonNull(dr["Privado"]))
                dto.Complemento.Privado = (bool)dr["Privado"];
            if (Util.GetNonNull(dr["BannerTipo"]))
                dto.Complemento.BannerTipo = dr["BannerTipo"].ToString();

            if (Util.GetNonNull(dr["ArquivoId_Primaria"]))
                dto.ArquivoId_Primaria = (long)dr["ArquivoId_Primaria"];
            if (Util.GetNonNull(dr["ArquivoId_Secundaria"]))
                dto.ArquivoId_Secundaria = (long)dr["ArquivoId_Secundaria"];

            if (Util.GetNonNull(dr["ListaUsuarioGrupo"]))
                dto.Complemento.ListaUsuarioGrupo = dr["ListaUsuarioGrupo"].ToString();
            if (Util.GetNonNull(dr["ListaUsuario"]))
                dto.Complemento.ListaUsuario = dr["ListaUsuario"].ToString();
        }
    }
}
