using DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class LoginDAL
    {
        public ResponseLogin EfetuarLogin(string Login, string Senha)
        {
            ResponseLogin resp = new ResponseLogin();


            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();
            
            Senha = Util.MixMD5(Senha);
            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Login", Login, Senha);

            if (tabela.Rows.Count > 0)
            {
                CarregarDTO(resp, tabela.Rows[0]);
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



        private void CarregarDTO(ResponseLogin dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["UsuarioId"]))
                dto.UsuarioId = (int)dr["UsuarioId"];
            if (Util.GetNonNull(dr["Nome"]))
                dto.Nome = dr["Nome"].ToString();
        }
    }
}
