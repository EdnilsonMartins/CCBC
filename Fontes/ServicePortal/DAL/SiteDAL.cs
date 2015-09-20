using DTO.Configuracao;
using DTO.Site;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class SiteDAL
    {
        public List<Site> ListarSite(int? SiteId)
        {
            List<Site> lista = new List<Site>();
            Site reg;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Site"))
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            reg = new Site();
                            CarregarDTO_Site(reg, dr);
                            lista.Add(reg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }

            return lista;
        }

        public SiteResponse CarregarSite(int SiteId)
        {
            SiteResponse resposta = new SiteResponse();
            Site site;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Site"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            site = new Site();
                            CarregarDTO_Site(site, dr);


                            ConfiguracaoDAL configuracaoDAL = new ConfiguracaoDAL();
                            Configuracao configuracao = configuracaoDAL.CarregarConfiguracao(SiteId);
                            site.Configuracao = configuracao;

                            resposta.Site = site;
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

        public SiteResponse GravarSite(Site Site, Site SiteOld)
        {
            SiteResponse resposta = new SiteResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, Site.SiteId);
                    objetoConexao.AdicionarParametro("@Descricao", SqlDbType.VarChar, Site.Descricao);
                    objetoConexao.AdicionarParametro("@Tags", SqlDbType.VarChar, Site.Tags);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Site"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.Site = Site;
                            resposta.Site.SiteId = (int)dt.Rows[0]["SiteId"];
                        }
                    }
                }

                if (resposta.Resposta.Erro == false)
                {
                    Site.Configuracao.SiteId = Site.SiteId;
                    using (ConexaoDB objetoConexao = new ConexaoDB())
                    {
                        ConfiguracaoDAL configuracaoDAL = new ConfiguracaoDAL();
                        ConfiguracaoResponse resp = configuracaoDAL.GravarConfiguracao(Site.Configuracao, SiteOld.Configuracao);
                        if (resp.Resposta.Erro)
                        {
                            resposta.Resposta = resp.Resposta;
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

        public SiteResponse ExcluirSite(int SiteId)
        {
            SiteResponse resposta = new SiteResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_Site"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Site = null;
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

        private void CarregarDTO_Site(Site dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["SiteId"]))
                dto.SiteId = (int)dr["SiteId"];
            if (Util.GetNonNull(dr["Descricao"]))
                dto.Descricao = dr["Descricao"].ToString();
            if (Util.GetNonNull(dr["Tags"]))
                dto.Tags = dr["Tags"].ToString();
        }

    }
}
