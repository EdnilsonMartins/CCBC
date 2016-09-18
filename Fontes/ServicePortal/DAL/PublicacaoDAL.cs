using DTO.Publicacao;
using DTO.Regra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PublicacaoDAL
    {
        public List<Publicacao> ListarPublicacao(int SiteId, int? PublicacaoId, int? PublicacaoTipoId, bool? Destaque, DateTime? DataValidade, int? UsuarioId, int IdiomaId, bool RetornaDefault = true, bool FiltrarPrivacidade = true)
        {
            List<Publicacao> lista = new List<Publicacao>();
            Publicacao reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Publicacao", SiteId, PublicacaoId, PublicacaoTipoId, Destaque, DataValidade, UsuarioId, IdiomaId, RetornaDefault, FiltrarPrivacidade);


            foreach (DataRow r in tabela.Rows)
            {
                reg = new Publicacao();
                CarregarDTO_Publicacao(reg, r);
                lista.Add(reg);
            }

            return lista;
        }

        public PublicacaoResponse Carregar(int SiteId, int IdiomaId, int PublicacaoId, int UsuarioId, bool FiltrarPrivacidade = true)
        {
            PublicacaoResponse resposta = new PublicacaoResponse();
            Publicacao publicacao;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, IdiomaId);
                    objetoConexao.AdicionarParametro("@PublicacaoId", SqlDbType.Int, PublicacaoId);
                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, UsuarioId);
                    objetoConexao.AdicionarParametro("@FiltrarPrivacidade", SqlDbType.Bit, FiltrarPrivacidade);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Publicacao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            publicacao = new Publicacao();
                            CarregarDTO_Publicacao(publicacao, dr);

                            resposta.Publicacao = publicacao;
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

        public PublicacaoResponse Gravar(Publicacao Publicacao, Publicacao PublicacaoOld, List<PublicacaoIdiomaExcecao> Extras, List<PublicacaoIdiomaExcecao> ExtrasOld, string ListaUsuarioGrupo, string ListaUsuario)
        {
            PublicacaoResponse resposta = new PublicacaoResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PublicacaoId", SqlDbType.Int, Publicacao.PublicacaoId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, Publicacao.SiteId);
                    objetoConexao.AdicionarParametro("@PublicacaoTipoId", SqlDbType.Int, Publicacao.PublicacaoTipoId);
                    objetoConexao.AdicionarParametro("@Data", SqlDbType.DateTime, Publicacao.Data);
                    objetoConexao.AdicionarParametro("@DataValidade", SqlDbType.DateTime, Publicacao.DataValidade);
                    objetoConexao.AdicionarParametro("@Posicao", SqlDbType.Int, Publicacao.Posicao);
                    objetoConexao.AdicionarParametro("@Destaque", SqlDbType.Bit, Publicacao.Destaque);
                    objetoConexao.AdicionarParametro("@Privado", SqlDbType.Bit, Publicacao.Complemento.Privado);
                    objetoConexao.AdicionarParametro("@ListaUsuarioGrupo", SqlDbType.VarChar, ListaUsuarioGrupo);
                    objetoConexao.AdicionarParametro("@ListaUsuario", SqlDbType.VarChar, ListaUsuario);
                    objetoConexao.AdicionarParametro("@Ativo", SqlDbType.Bit, Publicacao.Ativo);
                    objetoConexao.AdicionarParametro("@EditoriaId", SqlDbType.Int, Publicacao.EditoriaId);
                    objetoConexao.AdicionarParametro("@Tags", SqlDbType.VarChar, Publicacao.Tags);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Publicacao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;// (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = "";// (string)dt.Rows[0]["msgErro"];
                            resposta.Publicacao = Publicacao;
                            resposta.Publicacao.PublicacaoId = (int)dt.Rows[0]["PublicacaoId"];
                        }
                    }
                }

                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PublicacaoId", SqlDbType.Int, resposta.Publicacao.PublicacaoId);
                    objetoConexao.AdicionarParametro("@Privado", SqlDbType.Bit, false);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_PublicacaoRestricao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;// (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = "";// (string)dt.Rows[0]["msgErro"];
                            resposta.Publicacao = Publicacao;
                            //resposta.Publicacao.PublicacaoId = (int)dt.Rows[0]["PublicacaoRestricaoId"];
                        }
                    }
                }

                using (ConexaoDB objetoConexao = new ConexaoDB())
                {

                    objetoConexao.AdicionarParametro("@PublicacaoIdiomaExcecaoId", SqlDbType.Int, 0);
                    objetoConexao.AdicionarParametro("@PublicacaoId", SqlDbType.Int, resposta.Publicacao.PublicacaoId);
                    objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, Publicacao.Detalhe.IdiomaId);
                    objetoConexao.AdicionarParametro("@Titulo", SqlDbType.VarChar, Publicacao.Detalhe.Titulo);
                    objetoConexao.AdicionarParametro("@Resumo", SqlDbType.VarChar, Publicacao.Detalhe.Resumo);
                    objetoConexao.AdicionarParametro("@Conteudo", SqlDbType.VarChar, Publicacao.Detalhe.Conteudo);
                    //objetoConexao.AdicionarParametro("@Editora", SqlDbType.VarChar, Publicacao.Detalhe.Editora);
                    objetoConexao.AdicionarParametro("@Fonte", SqlDbType.VarChar, Publicacao.Detalhe.Fonte);
                    objetoConexao.AdicionarParametro("@Tags", SqlDbType.VarChar, Publicacao.Detalhe.Tags);
                    objetoConexao.AdicionarParametro("@ArquivoDestaqueId", SqlDbType.BigInt, Publicacao.Detalhe.ArquivoDestaqueId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_PublicacaoIdiomaExcecao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;// (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = "";// (string)dt.Rows[0]["msgErro"];
                            resposta.Publicacao = Publicacao;
                            //resposta.Publicacao.PublicacaoId = (int)dt.Rows[0]["PublicacaoRestricaoId"];
                        }
                    }
                }

                foreach (var Extra in Extras)
                {
                    using (ConexaoDB objetoConexao = new ConexaoDB())
                    {

                        objetoConexao.AdicionarParametro("@PublicacaoIdiomaExcecaoId", SqlDbType.Int, 0);
                        objetoConexao.AdicionarParametro("@PublicacaoId", SqlDbType.Int, resposta.Publicacao.PublicacaoId);
                        objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, Extra.IdiomaId);
                        objetoConexao.AdicionarParametro("@Titulo", SqlDbType.VarChar, Extra.Titulo);
                        objetoConexao.AdicionarParametro("@Resumo", SqlDbType.VarChar, Extra.Resumo);
                        objetoConexao.AdicionarParametro("@Conteudo", SqlDbType.VarChar, Extra.Conteudo);
                        //objetoConexao.AdicionarParametro("@Editora", SqlDbType.VarChar, Extra.Editora);
                        objetoConexao.AdicionarParametro("@Fonte", SqlDbType.VarChar, Extra.Fonte);
                        objetoConexao.AdicionarParametro("@Tags", SqlDbType.VarChar, Extra.Tags);
                        objetoConexao.AdicionarParametro("@ArquivoDestaqueId", SqlDbType.BigInt, Publicacao.Detalhe.ArquivoDestaqueId);
                        using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_PublicacaoIdiomaExcecao"))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                resposta.Resposta.Erro = false;// (bool)dt.Rows[0]["indErro"];
                                resposta.Resposta.Mensagem = "";// (string)dt.Rows[0]["msgErro"];
                                resposta.Publicacao = Publicacao;
                                //resposta.Publicacao.PublicacaoId = (int)dt.Rows[0]["PublicacaoRestricaoId"];
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

        public PublicacaoResponse ExcluirPublicacao(int PublicacaoId)
        {
            PublicacaoResponse resposta = new PublicacaoResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PublicacaoId", SqlDbType.Int, PublicacaoId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_Publicacao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Publicacao = null;
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

        public PublicacaoResponse LiberarPublicacao(int PublicacaoId, int? UsuarioId, bool Liberado)
        {
            PublicacaoResponse resposta = new PublicacaoResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PublicacaoId", SqlDbType.Int, PublicacaoId);
                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, UsuarioId);
                    objetoConexao.AdicionarParametro("@Liberado", SqlDbType.Bit, Liberado);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_PublicacaoAprovacaoItem"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Publicacao = null;
                        }
                    }
                }

                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PublicacaoId", SqlDbType.Int, PublicacaoId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Publicacao_Liberacao"))
                    {
                        //if (dt != null && dt.Rows.Count > 0)
                        //{
                        //    resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                        //    resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                        //    resposta.Publicacao = null;
                        //}
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


        public List<Publicacao.PublicacaoHistoricoItem> ListarPublicacaoAprovacaoHistorico(int PublicacaoId)
        {
            List<Publicacao.PublicacaoHistoricoItem> lista = new List<Publicacao.PublicacaoHistoricoItem>();
            Publicacao.PublicacaoHistoricoItem reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_PublicacaoAprovacaoHistorico", PublicacaoId);


            foreach (DataRow r in tabela.Rows)
            {
                reg = new Publicacao.PublicacaoHistoricoItem();
                CarregarDTO_PublicacaoAprovacaoHistorico(reg, r);
                lista.Add(reg);
            }

            return lista;
        }

        private void CarregarDTO_PublicacaoAprovacaoHistorico(Publicacao.PublicacaoHistoricoItem dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["DataLiberacao"]))
            {
                dto.DataLiberacao = (DateTime)dr["DataLiberacao"];
                dto.Data = ((DateTime)dto.DataLiberacao).ToString("dd-MM-yy");
                dto.Hora = ((DateTime)dto.DataLiberacao).ToString("HH:mm");
            }
            if (Util.GetNonNull(dr["Liberado"]))
                dto.Liberado = (bool)dr["Liberado"];
            if (Util.GetNonNull(dr["Descricao"]))
                dto.Descricao = dr["Descricao"].ToString();

            if (Util.GetNonNull(dr["UsuarioId"]))
                dto.UsuarioId = (int)dr["UsuarioId"];
            if (Util.GetNonNull(dr["Nome"]))
                dto.NomeUsuario = dr["Nome"].ToString();
        }

        private void CarregarDTO_Publicacao(Publicacao dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["PublicacaoId"]))
                dto.PublicacaoId = (int)dr["PublicacaoId"];
            if (Util.GetNonNull(dr["PublicacaoTipoId"]))
                dto.PublicacaoTipoId = (int)dr["PublicacaoTipoId"];
            if (Util.GetNonNull(dr["Destaque"]))
                dto.Destaque = (bool)dr["Destaque"];
            if (Util.GetNonNull(dr["Data"]))
                dto.Data = new DateTime(((DateTime)dr["Data"]).Year, ((DateTime)dr["Data"]).Month, ((DateTime)dr["Data"]).Day);
            if (Util.GetNonNull(dr["DataValidade"]))
                dto.DataValidade = (DateTime)dr["DataValidade"];
            if (Util.GetNonNull(dr["Posicao"]))
                dto.Posicao = (int)dr["Posicao"];
            if (Util.GetNonNull(dr["Ativo"]))
                dto.Ativo = (bool)dr["Ativo"];
            if (Util.GetNonNull(dr["EditoriaId"]))
                dto.EditoriaId = (int)dr["EditoriaId"];

            if (Util.GetNonNull(dr["Titulo"]))
                dto.Detalhe.Titulo = dr["Titulo"].ToString();
            if (Util.GetNonNull(dr["Resumo"]))
                dto.Detalhe.Resumo = dr["Resumo"].ToString();
            if (Util.GetNonNull(dr["Conteudo"]))
                dto.Detalhe.Conteudo = dr["Conteudo"].ToString();
            //if (Util.GetNonNull(dr["Editora"]))
            //    dto.Detalhe.Editora = dr["Editora"].ToString();
            if (Util.GetNonNull(dr["Fonte"]))
                dto.Detalhe.Fonte = dr["Fonte"].ToString();
            if (Util.GetNonNull(dr["Tags"]))
                dto.Detalhe.Tags = dr["Tags"].ToString();
            if (Util.GetNonNull(dr["ArquivoDestaqueId"]))
                dto.ArquivoDestaqueId = (long)dr["ArquivoDestaqueId"];
            if (Util.GetNonNull(dr["ArquivoGaleriaId"]))
                dto.ArquivoGaleriaId = (long)dr["ArquivoGaleriaId"];

            if (Util.GetNonNull(dr["Privado"]))
                dto.Complemento.Privado = (bool)dr["Privado"];
            if (Util.GetNonNull(dr["Liberado"]))
                dto.Complemento.Liberado = (bool)dr["Liberado"];
            if (Util.GetNonNull(dr["DataLiberado"]))
            {
                dto.Complemento.DataLiberado = (DateTime)dr["DataLiberado"];
                dto.Complemento.Data = ((DateTime)dr["DataLiberado"]).ToString("dd/MM/yyyy");
                dto.Complemento.Hora = ((DateTime)dr["DataLiberado"]).ToString("HH:mm");
            }
            if (Util.GetNonNull(dr["PublicacaoTipo"]))
                dto.Complemento.PublicacaoTipo = dr["PublicacaoTipo"].ToString();
            if (Util.GetNonNull(dr["Editoria"]))
                dto.Complemento.Editoria = dr["Editoria"].ToString();

            if (Util.GetNonNull(dr["ListaUsuarioGrupo"]))
                dto.Complemento.ListaUsuarioGrupo = dr["ListaUsuarioGrupo"].ToString();
            if (Util.GetNonNull(dr["ListaUsuario"]))
                dto.Complemento.ListaUsuario = dr["ListaUsuario"].ToString();

            if (Util.GetNonNull(dr["UsuarioElegivel"]))
                dto.UsuarioElegivel = (bool)dr["UsuarioElegivel"];
            if (Util.GetNonNull(dr["DataAprovacao"]))
                dto.DataAprovacao = (DateTime)dr["DataAprovacao"];

            if (Util.GetNonNull(dr["Tags"]))
                dto.Tags = dr["Tags"].ToString();
        }
    }
}
