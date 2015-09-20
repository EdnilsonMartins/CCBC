using DTO.Mailing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class MailingDAL
    {
        public List<MailingDTO> ListarMailing(int? MailingId, int? SiteId)
        {
            List<MailingDTO> lista = new List<MailingDTO>();
            MailingDTO reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Mailing", MailingId, SiteId);

            foreach (DataRow r in tabela.Rows)
            {
                reg = new MailingDTO();
                CarregarDTO(reg, r);
                lista.Add(reg);
            }

            return lista;
        }

        public MailingResponse RegistrarMailing(MailingDTO Mailing)
        {
            MailingResponse resp = new MailingResponse();
            

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_INS_Mailing",    Mailing.MailingId, 
                                                                                    Mailing.SiteId,
                                                                                    Mailing.Nome,
                                                                                    Mailing.Email,
                                                                                    Mailing.Segmento,
                                                                                    Mailing.Ativo
                                                                                    );
            if (tabela.Rows.Count > 0)
            {
                //CarregarDTO(resp.Mailing, tabela.Rows[0]);
                resp.Resposta.Erro = false;
                resp.Resposta.Mensagem = "";
            }
            else
            {
                resp.Resposta.Erro = true;
                resp.Resposta.Mensagem = "";
            }

            return resp;
        }



        private void CarregarDTO(MailingDTO dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["MailingId"]))
                dto.MailingId = (int)dr["MailingId"];
            if (Util.GetNonNull(dr["SiteId"]))
                dto.SiteId = (int)dr["SiteId"];
            if (Util.GetNonNull(dr["Nome"]))
                dto.Nome = dr["Nome"].ToString();
            if (Util.GetNonNull(dr["Email"]))
                dto.Email = dr["Email"].ToString();
            if (Util.GetNonNull(dr["Segmento"]))
                dto.Segmento = dr["Segmento"].ToString();
            if (Util.GetNonNull(dr["DataInclusao"]))
                dto.DataInclusao = (DateTime)dr["DataInclusao"];
            if (Util.GetNonNull(dr["Ativo"]))
                dto.Ativo = (bool)dr["Ativo"];
        }
    }
}
