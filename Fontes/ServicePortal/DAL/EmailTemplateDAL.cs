using DTO.EmailTemplate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class EmailTemplateDAL
    {
        public List<EmailTemplate> ListarEmailTemplate(int SiteId, int? EmailGrupoTemplateId)
        {
            List<EmailTemplate> lista = new List<EmailTemplate>();
            EmailTemplate reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_EmailTemplate", SiteId, EmailGrupoTemplateId, null);

            foreach (DataRow r in tabela.Rows)
            {
                reg = new EmailTemplate();
                CarregarDTO_EmailTemplate(reg, r);
                lista.Add(reg);
            }

            return lista;
        }

        public EmailTemplateResponse Carregar(int SiteId, int EmailTemplateId)
        {
            EmailTemplateResponse resposta = new EmailTemplateResponse();
            EmailTemplate emailTemplate;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    objetoConexao.AdicionarParametro("@EmailGrupoTemplateId", SqlDbType.Int, DBNull.Value);
                    objetoConexao.AdicionarParametro("@EmailTemplateId", SqlDbType.Int, EmailTemplateId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_EmailTemplate"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            emailTemplate = new EmailTemplate();
                            CarregarDTO_EmailTemplate(emailTemplate, dr);

                            resposta.EmailTemplate = emailTemplate;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return resposta;
        }

        public EmailTemplateResponse Gravar(EmailTemplate EmailTemplate, EmailTemplate EmailTemplateOld)
        {
            EmailTemplateResponse resposta = new EmailTemplateResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@EmailTemplateId", SqlDbType.BigInt, EmailTemplate.EmailTemplateId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, EmailTemplate.SiteId);
                    objetoConexao.AdicionarParametro("@EmailGrupoTemplateId", SqlDbType.Int, EmailTemplate.EmailGrupoTemplateId);
                    objetoConexao.AdicionarParametro("@Comentario", SqlDbType.VarChar, EmailTemplate.Comentario);
                    objetoConexao.AdicionarParametro("@Assunto", SqlDbType.VarChar, EmailTemplate.Assunto);
                    objetoConexao.AdicionarParametro("@Corpo", SqlDbType.VarChar, EmailTemplate.Corpo);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_EmailTemplate"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.EmailTemplate = EmailTemplate;
                            resposta.EmailTemplate.EmailTemplateId = (long)dt.Rows[0]["EmailTemplateId"];
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                resposta.Resposta.Erro = true;
                resposta.Resposta.Mensagem = ex.Message;
            }
            return resposta;
        }

        private void CarregarDTO_EmailTemplate(EmailTemplate dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["EmailTemplateId"]))
                dto.EmailTemplateId = (long)dr["EmailTemplateId"];
            if (Util.GetNonNull(dr["EmailGrupoTemplateId"]))
                dto.EmailGrupoTemplateId = (int)dr["EmailGrupoTemplateId"];
            if (Util.GetNonNull(dr["Comentario"]))
                dto.Comentario = dr["Comentario"].ToString();
            if (Util.GetNonNull(dr["Assunto"]))
                dto.Assunto = dr["Assunto"].ToString();
            if (Util.GetNonNull(dr["Corpo"]))
                dto.Corpo = dr["Corpo"].ToString();
        }
    }
}
