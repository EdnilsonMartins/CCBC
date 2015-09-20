using DTO.Publicacao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class EditoriaDAL
    {
        public List<Editoria> ListarEditoria(int SiteId, int IdiomaId)
        {
            List<Editoria> lista = new List<Editoria>();
            Editoria reg;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@EditoriaId", SqlDbType.Int, DBNull.Value);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, IdiomaId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Editoria"))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            reg = new Editoria();
                            CarregarDTO_Editoria(reg, r);
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

        public EditoriaResponse Carregar(int EditoriaId, int IdiomaId, bool RetornarDefault)
        {
            EditoriaResponse resposta = new EditoriaResponse();
            Editoria editoria;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@EditoriaId", SqlDbType.Int, EditoriaId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, DBNull.Value);
                    objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, IdiomaId);
                    objetoConexao.AdicionarParametro("@RetornarDefault", SqlDbType.Bit, RetornarDefault);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Editoria"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            editoria = new Editoria();
                            CarregarDTO_Editoria(editoria, dr);

                            resposta.Editoria = editoria;
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

        public EditoriaResponse Gravar(Editoria Editoria, Editoria EditoriaOld, List<EditoriaIdiomaExcecao> Extras)
        {
            EditoriaResponse resposta = new EditoriaResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@EditoriaId", SqlDbType.Int, Editoria.EditoriaId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, Editoria.SiteId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Editoria"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.Editoria = Editoria;
                            resposta.Editoria.EditoriaId = (int)dt.Rows[0]["EditoriaId"];
                        }
                    }
                }

                using (ConexaoDB objetoConexao = new ConexaoDB())
                {

                    objetoConexao.AdicionarParametro("@EditoriaIdiomaExcecaoId", SqlDbType.Int, 0);
                    objetoConexao.AdicionarParametro("@EditoriaId", SqlDbType.Int, resposta.Editoria.EditoriaId);
                    objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, Editoria.Detalhe.IdiomaId);
                    objetoConexao.AdicionarParametro("@Descricao", SqlDbType.VarChar, Editoria.Detalhe.Descricao);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_EditoriaIdiomaExcecao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.Editoria = Editoria;
                        }
                    }
                }

                foreach (var Extra in Extras)
                {
                    using (ConexaoDB objetoConexao = new ConexaoDB())
                    {

                        objetoConexao.AdicionarParametro("@EditoriaIdiomaExcecaoId", SqlDbType.Int, 0);
                        objetoConexao.AdicionarParametro("@EditoriaId", SqlDbType.Int, resposta.Editoria.EditoriaId);
                        objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, Extra.IdiomaId);
                        objetoConexao.AdicionarParametro("@Descricao", SqlDbType.VarChar, Extra.Descricao);
                        using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_EditoriaIdiomaExcecao"))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                resposta.Resposta.Erro = false;
                                resposta.Resposta.Mensagem = "";
                                resposta.Editoria = Editoria;
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

        public EditoriaResponse ExcluirEditoria(int EditoriaId)
        {
            EditoriaResponse resposta = new EditoriaResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@EditoriaId", SqlDbType.Int, EditoriaId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_Editoria"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Editoria = null;
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

        private void CarregarDTO_Editoria(Editoria dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["EditoriaId"]))
                dto.EditoriaId = (int)dr["EditoriaId"];

            if (Util.GetNonNull(dr["Descricao"]))
                dto.Detalhe.Descricao = dr["Descricao"].ToString();

            if (Util.GetNonNull(dr["SiteId"]))
                dto.SiteId = (int)dr["SiteId"];
        }
    }
}
