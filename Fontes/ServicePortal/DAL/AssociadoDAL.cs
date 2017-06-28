using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class AssociadoDAL
    {
        public List<Associado> ListarAssociado(int SiteId, int? AssociadoCategoriaId, string LetraInicial, bool ExibirTotos = false)
        {
            List<Associado> lista = new List<Associado>();
            Associado reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Associado", SiteId, AssociadoCategoriaId, LetraInicial, ExibirTotos);


            foreach (DataRow r in tabela.Rows)
            {
                reg = new Associado();
                CarregarDTO_Associado(reg, r);
                lista.Add(reg);
            }

            foreach(var item in lista){
                    
            }

            return lista;
        }

        public AssociadoResponse Carregar(int AssociadoId)
        {
            AssociadoResponse resposta = new AssociadoResponse();
            Associado associado;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, DBNull.Value);
                    objetoConexao.AdicionarParametro("@AssociadoCategoriaId", SqlDbType.Int, DBNull.Value);
                    objetoConexao.AdicionarParametro("@LetraInicial", SqlDbType.Int, DBNull.Value);
                    objetoConexao.AdicionarParametro("@ExibirTodos", SqlDbType.Bit, true);
                    objetoConexao.AdicionarParametro("@AssociadoId", SqlDbType.Int, AssociadoId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Associado"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            associado = new Associado();
                            CarregarDTO_Associado(associado, dr);

                            resposta.Associado = associado;
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

        public AssociadoResponse Gravar(Associado Associado, Associado AssociadoOld)
        {
            AssociadoResponse resposta = new AssociadoResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@AssociadoId", SqlDbType.Int, Associado.AssociadoId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, Associado.SiteId);
                    objetoConexao.AdicionarParametro("@Nome", SqlDbType.VarChar, Associado.Nome);
                    objetoConexao.AdicionarParametro("@Resumo", SqlDbType.VarChar, Associado.Resumo);
                    objetoConexao.AdicionarParametro("@AssociadoCategoriaId", SqlDbType.Int, Associado.AssociadoCategoriaId);
                    objetoConexao.AdicionarParametro("@TipoPessoaId", SqlDbType.Int, Associado.TipoPessoaId);
                    objetoConexao.AdicionarParametro("@PaisId", SqlDbType.Int, Associado.PaisId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Associado"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.Associado = Associado;
                            resposta.Associado.AssociadoId = (int)dt.Rows[0]["AssociadoId"];
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

        public AssociadoResponse ExcluirAssociado(int AssociadoId)
        {
            AssociadoResponse resposta = new AssociadoResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@AssociadoId", SqlDbType.Int, AssociadoId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_Associado"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Associado = null;
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

        private void CarregarDTO_Associado(Associado dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["AssociadoId"]))
                dto.AssociadoId = (int)dr["AssociadoId"];
            if (Util.GetNonNull(dr["SiteId"]))
                dto.SiteId = (int)dr["SiteId"];
            if (Util.GetNonNull(dr["Nome"]))
                dto.Nome = dr["Nome"].ToString();
            if (Util.GetNonNull(dr["Resumo"]))
                dto.Resumo = dr["Resumo"].ToString();
            if (Util.GetNonNull(dr["AssociadoCategoriaId"]))
                dto.AssociadoCategoriaId = (int)dr["AssociadoCategoriaId"];
            if (Util.GetNonNull(dr["TipoPessoaId"]))
                dto.TipoPessoaId = (int)dr["TipoPessoaId"];
            if (Util.GetNonNull(dr["PaisId"]))
                dto.PaisId = (int)dr["PaisId"];

            if (Util.GetNonNull(dr["Pais"]))
                dto.Detalhe.Pais = dr["Pais"].ToString();
            if (Util.GetNonNull(dr["AssociadoCategoria"]))
                dto.Detalhe.AssociadoCategoria = dr["AssociadoCategoria"].ToString();
            if (Util.GetNonNull(dr["TipoPessoa"]))
                dto.Detalhe.TipoPessoa = dr["TipoPessoa"].ToString();

            if (Util.GetNonNull(dr["DataAtualizacao"]))
            {
                dto.DataAtualizacao = (DateTime)dr["DataAtualizacao"];
                dto.Detalhe.DataAtualizacao = ((DateTime)dr["DataAtualizacao"]).ToString();
            }
        }
    }
}
