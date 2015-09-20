using DTO.Pasta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PastaDAL
    {
        public List<Pasta> ListarPasta(int SiteId)
        {
            List<Pasta> lista = new List<Pasta>();
            Pasta reg;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PastaId", SqlDbType.Int, DBNull.Value);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Pasta"))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            reg = new Pasta();
                            CarregarDTO_Pasta(reg, r);
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

        public PastaResponse Carregar(int PastaId)
        {
            PastaResponse resposta = new PastaResponse();
            Pasta pasta;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PastaId", SqlDbType.Int, PastaId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, DBNull.Value);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Pasta"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            pasta = new Pasta();
                            CarregarDTO_Pasta(pasta, dr);

                            resposta.Pasta = pasta;
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

        public PastaResponse Gravar(Pasta Pasta, Pasta PastaOld)
        {
            PastaResponse resposta = new PastaResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PastaId", SqlDbType.Int, Pasta.PastaId);
                    objetoConexao.AdicionarParametro("@PastaPaiId", SqlDbType.Int, Pasta.PastaPaiId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, Pasta.SiteId);
                    objetoConexao.AdicionarParametro("@Descricao", SqlDbType.VarChar, Pasta.Descricao);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Pasta"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.Pasta = Pasta;
                            resposta.Pasta.PastaId = (int)dt.Rows[0]["PastaId"];
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

        public PastaResponse ExcluirPasta(int PastaId)
        {
            PastaResponse resposta = new PastaResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PastaId", SqlDbType.Int, PastaId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_Pasta"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Pasta = null;
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

        public PastaResponse Reposicionar(Pasta Pasta)
        {
            PastaResponse resposta = new PastaResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PastaId", SqlDbType.Int, Pasta.PastaId);
                    objetoConexao.AdicionarParametro("@PastaPaiId", SqlDbType.Int, Pasta.PastaPaiId);
                    objetoConexao.AdicionarParametro("@Posicao", SqlDbType.Int, Pasta.Posicao);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_UPD_Pasta_Reposicionar"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Pasta = null;
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
        
        private void CarregarDTO_Pasta(Pasta dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["PastaId"]))
                dto.PastaId = (int)dr["PastaId"];
            if (Util.GetNonNull(dr["PastaPaiId"]))
                dto.PastaPaiId = (int)dr["PastaPaiId"];
            if (Util.GetNonNull(dr["SiteId"]))
                dto.SiteId = (int)dr["SiteId"];
            if (Util.GetNonNull(dr["Descricao"]))
                dto.Descricao = dr["Descricao"].ToString();
            if (Util.GetNonNull(dr["Posicao"]))
                dto.Posicao = (int)dr["Posicao"];
        }
    }
}
