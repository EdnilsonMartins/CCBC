using DTO.Conteudo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ConteudoDAL
    {
        public Conteudo CarregarConteudo(int ConteudoId)
        {
            Conteudo cont = new Conteudo();


            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Conteudo", ConteudoId);

            if (tabela.Rows.Count > 0)
            {
                CarregarDTO(cont, tabela.Rows[0]);
            }

            return cont;
        }

        private void CarregarDTO(Conteudo dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["Titulo"]))
                dto.Titulo = dr["Titulo"].ToString();
            if (Util.GetNonNull(dr["Conteudo"]))
                dto.Descricao = dr["Conteudo"].ToString();
        }
    }
}
