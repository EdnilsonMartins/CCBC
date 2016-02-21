using DTO.Configuracao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ConfiguracaoDAL
    {

        public ConfiguracaoResponse GravarConfiguracao(Configuracao Configuracao, Configuracao ConfiguracaoOld)
        {
            ConfiguracaoResponse resposta = new ConfiguracaoResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, Configuracao.SiteId);
                    objetoConexao.AdicionarParametro("@ContatoTelefonePrincipal", SqlDbType.VarChar, Configuracao.ContatoTelefonePrincipal);
                    objetoConexao.AdicionarParametro("@ContatoEmailPrincipal", SqlDbType.VarChar, Configuracao.ContatoEmailPrincipal);
                    objetoConexao.AdicionarParametro("@Localizacao", SqlDbType.VarChar, Configuracao.Localizacao);
                    objetoConexao.AdicionarParametro("@LocalizacaoComplemento", SqlDbType.VarChar, Configuracao.LocalizacaoComplemento);
                    objetoConexao.AdicionarParametro("@EmailHost", SqlDbType.VarChar, Configuracao.EmailHost);
                    objetoConexao.AdicionarParametro("@EmailUsername", SqlDbType.VarChar, Configuracao.EmailUsername);
                    objetoConexao.AdicionarParametro("@EmailPassword", SqlDbType.VarChar, Configuracao.EmailPassword);
                    objetoConexao.AdicionarParametro("@EmailDisplayName", SqlDbType.VarChar, Configuracao.EmailDisplayName);
                    objetoConexao.AdicionarParametro("@LinkMapa", SqlDbType.VarChar, Configuracao.LinkMapa);
                    objetoConexao.AdicionarParametro("@HostBase", SqlDbType.VarChar, Configuracao.HostBase);
                    objetoConexao.AdicionarParametro("@EmailDestino", SqlDbType.VarChar, Configuracao.EmailDestino);
                    objetoConexao.AdicionarParametro("@EmailPorta", SqlDbType.VarChar, Configuracao.EmailPorta);
                    objetoConexao.AdicionarParametro("@EmailDestinoAdministrativoTI", SqlDbType.VarChar, Configuracao.EmailDestinoAdministrativoTI);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Configuracao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.Configuracao = Configuracao;
                            resposta.Configuracao.ConfiguracaoId = (int)dt.Rows[0]["ConfiguracaoId"];
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

        public Configuracao CarregarConfiguracao(int SiteId)
        {
            Configuracao retorno = new Configuracao();

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Configuracao", SiteId);


            foreach (DataRow r in tabela.Rows)
            {
                CarregarDTO(retorno, r);

            }

            return retorno;
        }

        private void CarregarDTO(Configuracao dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["ConfiguracaoId"]))
                dto.ConfiguracaoId = (int)dr["ConfiguracaoId"];
            if (Util.GetNonNull(dr["SiteId"]))
                dto.SiteId = (int)dr["SiteId"];

            if (Util.GetNonNull(dr["ContatoTelefonePrincipal"]))
                dto.ContatoTelefonePrincipal = dr["ContatoTelefonePrincipal"].ToString();
            if (Util.GetNonNull(dr["ContatoEmailPrincipal"]))
                dto.ContatoEmailPrincipal = dr["ContatoEmailPrincipal"].ToString();
            if (Util.GetNonNull(dr["Localizacao"]))
                dto.Localizacao = dr["Localizacao"].ToString();
            if (Util.GetNonNull(dr["LocalizacaoComplemento"]))
                dto.LocalizacaoComplemento = dr["LocalizacaoComplemento"].ToString();
            if (Util.GetNonNull(dr["EmailHost"]))
                dto.EmailHost = dr["EmailHost"].ToString();
            if (Util.GetNonNull(dr["EmailUsername"]))
                dto.EmailUsername = dr["EmailUsername"].ToString();
            if (Util.GetNonNull(dr["EmailPassword"]))
                dto.EmailPassword = dr["EmailPassword"].ToString();
            if (Util.GetNonNull(dr["EmailDisplayName"]))
                dto.EmailDisplayName = dr["EmailDisplayName"].ToString();
            if (Util.GetNonNull(dr["EmailDestino"]))
                dto.EmailDestino = dr["EmailDestino"].ToString();
            if (Util.GetNonNull(dr["EmailPorta"]))
                dto.EmailPorta = (int)dr["EmailPorta"];
            if (Util.GetNonNull(dr["EmailDestinoAdministrativoTI"]))
                dto.EmailDestinoAdministrativoTI = dr["EmailDestinoAdministrativoTI"].ToString();

            if (Util.GetNonNull(dr["LinkMapa"]))
                dto.LinkMapa = dr["LinkMapa"].ToString();

            if (Util.GetNonNull(dr["HostBase"]))
                dto.HostBase = dr["HostBase"].ToString();

        }
    }
}
