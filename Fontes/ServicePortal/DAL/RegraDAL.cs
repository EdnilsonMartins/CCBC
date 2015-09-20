using DTO.Regra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class RegraDAL
    {

        public RegraResponse Gravar(Regra Regra, Regra RegraOld)
        {
            RegraResponse resposta = new RegraResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@RegraId", SqlDbType.Int, Regra.RegraId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, Regra.SiteId);
                    objetoConexao.AdicionarParametro("@Descricao", SqlDbType.VarChar, Regra.Descricao);
                    objetoConexao.AdicionarParametro("@RegraTipoId", SqlDbType.Int, Regra.RegraTipoId);

                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Regra"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.Regra = Regra;
                            resposta.Regra.RegraId = (int)dt.Rows[0]["RegraId"];
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

        public RegraResponse ExcluirRegra(int RegraId)
        {
            RegraResponse resposta = new RegraResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@RegraId", SqlDbType.Int, RegraId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_Regra"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Regra = null;
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

        public List<Regra> ListarRegra(int SiteId)
        {
            List<Regra> lista = new List<Regra>();
            Regra reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Regra", SiteId);


            foreach (DataRow r in tabela.Rows)
            {
                reg = new Regra();
                CarregarDTO_Regra(reg, r);
                lista.Add(reg);
            }

            return lista;
        }
        
        public RegraResponse Carregar(int SiteId, int IdiomaId, int RegraId, int UsuarioId)
        {
            RegraResponse resposta = new RegraResponse();
            Regra regra;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    objetoConexao.AdicionarParametro("@RegraId", SqlDbType.Int, RegraId);
                    //objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, UsuarioId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Regra"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            regra = new Regra();
                            CarregarDTO_Regra(regra, dr);

                            resposta.Regra = regra;
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

        public List<RegraPasso> ListarPublicacaoRegraPasso(int PublicacaoId)
        {
            List<RegraPasso> lista = new List<RegraPasso>();
            RegraPasso reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_PublicacaoRegraPasso", PublicacaoId);


            foreach (DataRow r in tabela.Rows)
            {
                reg = new RegraPasso();
                CarregarDTO_RegraPasso(reg, r);
                lista.Add(reg);
            }

            return lista;
        }

        public RegraUsuarioElegivel VerificarUsuarioElegivel_Publicacao(int PublicacaoId, int? UsuarioId)
        {
            RegraUsuarioElegivel reg = new RegraUsuarioElegivel();

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_PublicacaoRegraPasso_Usuario", PublicacaoId, UsuarioId);


            if (tabela.Rows.Count > 0) {
                DataRow dr = tabela.Rows[0];
                if (Util.GetNonNull(dr["Ok_Usuario"]))
                    reg.Ok_Usuario = (bool)dr["Ok_Usuario"];
                if (Util.GetNonNull(dr["Ok_UsuarioGrupo"]))
                    reg.Ok_UsuarioGrupo = (bool)dr["Ok_UsuarioGrupo"];
                if (Util.GetNonNull(dr["UsuarioElegivel"]))
                    reg.UsuarioElegivel = (bool)dr["UsuarioElegivel"];
                if (Util.GetNonNull(dr["Liberado"]))
                    reg.Liberado = (bool)dr["Liberado"];
                if (Util.GetNonNull(dr["DataAprovacao"]))
                {
                    reg.DataAprovacao = (DateTime)dr["DataAprovacao"];
                    reg.Data = ((DateTime)reg.DataAprovacao).ToString("dd/MM/yyyy");
                    reg.Hora = ((DateTime)reg.DataAprovacao).ToString("HH:mm");

                }
            }

            return reg;
        }
        
        private void CarregarDTO_Regra(Regra dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["RegraId"]))
                dto.RegraId = (int)dr["RegraId"];
            if (Util.GetNonNull(dr["Regra"]))
                dto.Descricao = dr["Regra"].ToString();
            if (Util.GetNonNull(dr["DataCadastro"]))
                dto.DataCadastro = (DateTime)dr["DataCadastro"];
            if (Util.GetNonNull(dr["RegraTipoId"]))
                dto.RegraTipoId = (int)dr["RegraTipoId"];
            if (Util.GetNonNull(dr["RegraTipo"]))
                dto.RegraTipo.Descricao = dr["RegraTipo"].ToString();

        }

        private void CarregarDTO_RegraPasso(RegraPasso dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["RegraPassoId"]))
                dto.RegraPassoId = (int)dr["RegraPassoId"];
            if (Util.GetNonNull(dr["Sequencia"]))
                dto.Sequencia = (int)dr["Sequencia"];
            if (Util.GetNonNull(dr["Descricao"]))
                dto.Descricao = dr["Descricao"].ToString();

            if (Util.GetNonNull(dr["TotalUsuarios"]))
                dto.Detalhes.TotalUsuarios = (int)dr["TotalUsuarios"];
            if (Util.GetNonNull(dr["Resultado"]))
                dto.Detalhes.Resultado = (bool)dr["Resultado"];

        }


        #region --> Regra Passo

        public List<RegraPasso> ListarRegraPasso(int RegraId)
        {
            List<RegraPasso> lista = new List<RegraPasso>();
            RegraPasso reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_RegraPasso", null, RegraId);


            foreach (DataRow r in tabela.Rows)
            {
                reg = new RegraPasso();
                CarregarDTO_RegraPasso_Cadastro(reg, r);
                lista.Add(reg);
            }

            return lista;
        }

        public RegraPassoResponse CarregarRegraPasso(int RegraPassoId, int RegraId)
        {
            RegraPassoResponse resposta = new RegraPassoResponse();
            RegraPasso regraPasso;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@RegraPassoId", SqlDbType.Int, RegraPassoId);
                    objetoConexao.AdicionarParametro("@RegraId", SqlDbType.Int, RegraId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_RegraPasso"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            regraPasso = new RegraPasso();
                            CarregarDTO_RegraPasso_Cadastro(regraPasso, dr);

                            resposta.RegraPasso = regraPasso;
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

        public RegraPassoResponse Gravar(RegraPasso RegraPasso, RegraPasso RegraOld)
        {
            RegraPassoResponse resposta = new RegraPassoResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@RegraPassoId", SqlDbType.Int, RegraPasso.RegraPassoId);
                    objetoConexao.AdicionarParametro("@RegraId", SqlDbType.Int, RegraPasso.RegraId);
                    objetoConexao.AdicionarParametro("@Sequencia", SqlDbType.Int, RegraPasso.Sequencia);
                    objetoConexao.AdicionarParametro("@Descricao", SqlDbType.VarChar, RegraPasso.Descricao);
                    objetoConexao.AdicionarParametro("@QuantidadeMinimaUsuariosDoGrupo", SqlDbType.VarChar, RegraPasso.QuantidadeMinimaUsuariosDoGrupo);

                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_RegraPasso"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.RegraPasso = RegraPasso;
                            resposta.RegraPasso.RegraPassoId = (int)dt.Rows[0]["RegraPassoId"];
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

        public RegraPassoResponse ExcluirRegraPasso(int RegraPassoId)
        {
            RegraPassoResponse resposta = new RegraPassoResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@RegraPassoId", SqlDbType.Int, RegraPassoId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_RegraPasso"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.RegraPasso = null;
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

        private void CarregarDTO_RegraPasso_Cadastro(RegraPasso dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["RegraPassoId"]))
                dto.RegraPassoId = (int)dr["RegraPassoId"];
            if (Util.GetNonNull(dr["RegraId"]))
                dto.RegraId = (int)dr["RegraId"];
            if (Util.GetNonNull(dr["Sequencia"]))
                dto.Sequencia = (int)dr["Sequencia"];
            if (Util.GetNonNull(dr["Descricao"]))
                dto.Descricao = dr["Descricao"].ToString();
            if (Util.GetNonNull(dr["QuantidadeMinimaUsuariosDoGrupo"]))
                dto.QuantidadeMinimaUsuariosDoGrupo = (int)dr["QuantidadeMinimaUsuariosDoGrupo"];
        }

        #endregion


        #region --> Condições do Passo

        public List<RegraPassoCondicao> ListarRegraPassoCondicao(int RegraPassoId)
        {
            List<RegraPassoCondicao> lista = new List<RegraPassoCondicao>();
            RegraPassoCondicao reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_RegraPassoCondicao", null, RegraPassoId);


            foreach (DataRow r in tabela.Rows)
            {
                reg = new RegraPassoCondicao();
                CarregarDTO_RegraPassoCondicao_Cadastro(reg, r);
                lista.Add(reg);
            }

            return lista;
        }

        public RegraPassoCondicaoResponse CarregarRegraPassoCondicao(int RegraPassoCondicaoId, int RegraPassoId)
        {
            RegraPassoCondicaoResponse resposta = new RegraPassoCondicaoResponse();
            RegraPassoCondicao regraPassoCondicao;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@RegraPassoCondicaoId", SqlDbType.Int, RegraPassoCondicaoId);
                    objetoConexao.AdicionarParametro("@RegraPassoId", SqlDbType.Int, RegraPassoId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_RegraPassoCondicao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            regraPassoCondicao = new RegraPassoCondicao();
                            CarregarDTO_RegraPassoCondicao_Cadastro(regraPassoCondicao, dr);

                            resposta.RegraPassoCondicao = regraPassoCondicao;
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

        public RegraPassoCondicaoResponse Gravar(RegraPassoCondicao RegraPassoCondicao, RegraPassoCondicao RegraOld)
        {
            RegraPassoCondicaoResponse resposta = new RegraPassoCondicaoResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@RegraPassoCondicaoId", SqlDbType.Int, RegraPassoCondicao.RegraPassoCondicaoId);
                    objetoConexao.AdicionarParametro("@RegraPassoId", SqlDbType.Int, RegraPassoCondicao.RegraPassoId);
                    objetoConexao.AdicionarParametro("@UsuarioGrupoId", SqlDbType.Int, RegraPassoCondicao.UsuarioGrupoId);
                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.VarChar, RegraPassoCondicao.UsuarioId);
                    objetoConexao.AdicionarParametro("@QuantidadeMinimaUsuarios", SqlDbType.VarChar, RegraPassoCondicao.QuantidadeMinimaUsuarios);

                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_RegraPassoCondicao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.RegraPassoCondicao = RegraPassoCondicao;
                            resposta.RegraPassoCondicao.RegraPassoCondicaoId = (int)dt.Rows[0]["RegraPassoCondicaoId"];
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

        public RegraPassoCondicaoResponse ExcluirRegraPassoCondicao(int RegraPassoCondicaoId)
        {
            RegraPassoCondicaoResponse resposta = new RegraPassoCondicaoResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@RegraPassoCondicaoId", SqlDbType.Int, RegraPassoCondicaoId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_RegraPassoCondicao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.RegraPassoCondicao = null;
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

        private void CarregarDTO_RegraPassoCondicao_Cadastro(RegraPassoCondicao dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["RegraPassoCondicaoId"]))
                dto.RegraPassoCondicaoId = (int)dr["RegraPassoCondicaoId"];
            if (Util.GetNonNull(dr["RegraPassoId"]))
                dto.RegraPassoId = (int)dr["RegraPassoId"];
            if (Util.GetNonNull(dr["UsuarioGrupoId"]))
                dto.UsuarioGrupoId = (int)dr["UsuarioGrupoId"];
            if (Util.GetNonNull(dr["UsuarioId"]))
                dto.UsuarioId = (int)dr["UsuarioId"];
            if (Util.GetNonNull(dr["QuantidadeMinimaUsuarios"]))
                dto.QuantidadeMinimaUsuarios = (int)dr["QuantidadeMinimaUsuarios"];

            if (Util.GetNonNull(dr["Usuario"]))
                dto.Detalhes.Usuario = dr["Usuario"].ToString();
            if (Util.GetNonNull(dr["UsuarioGrupo"]))
                dto.Detalhes.UsuarioGrupo = dr["UsuarioGrupo"].ToString();
        }

        #endregion


        #region --> PublicacaoTipo x Regra

        public List<PublicacaoTipoRegra> ListarPublicacaoTipoRegra(int? PublicacaoTipoRegraId, int SiteId)
        {
            List<PublicacaoTipoRegra> lista = new List<PublicacaoTipoRegra>();
            PublicacaoTipoRegra reg;

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_PublicacaoTipoRegra", PublicacaoTipoRegraId, SiteId);


            foreach (DataRow r in tabela.Rows)
            {
                reg = new PublicacaoTipoRegra();
                CarregarDTO_PublicacaoTipoRegra(reg, r);
                lista.Add(reg);
            }

            return lista;
        }

        public PublicacaoTipoRegraResponse CarregarPublicacaoTipoRegra(int PublicacaoTipoRegraId, int SiteId)
        {
            PublicacaoTipoRegraResponse resposta = new PublicacaoTipoRegraResponse();
            PublicacaoTipoRegra publicacaoTipoRegra;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PublicacaoTipoRegraId", SqlDbType.Int, PublicacaoTipoRegraId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_PublicacaoTipoRegra"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            publicacaoTipoRegra = new PublicacaoTipoRegra();
                            CarregarDTO_PublicacaoTipoRegra(publicacaoTipoRegra, dr);

                            resposta.PublicacaoTipoRegra = publicacaoTipoRegra;
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

        public PublicacaoTipoRegraResponse Gravar(PublicacaoTipoRegra PublicacaoTipoRegra, PublicacaoTipoRegra PublicacaoTipoRegraOld)
        {
            PublicacaoTipoRegraResponse resposta = new PublicacaoTipoRegraResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PublicacaoTipoRegraId", SqlDbType.Int, PublicacaoTipoRegra.PublicacaoTipoRegraId);
                    objetoConexao.AdicionarParametro("@PublicacaoTipoId", SqlDbType.Int, PublicacaoTipoRegra.PublicacaoTipoId);
                    objetoConexao.AdicionarParametro("@RegraId", SqlDbType.Int, PublicacaoTipoRegra.RegraId);
                    objetoConexao.AdicionarParametro("@Privado", SqlDbType.VarChar, PublicacaoTipoRegra.Privado);
                    
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_PublicacaoTipoRegra"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;
                            resposta.Resposta.Mensagem = "";
                            resposta.PublicacaoTipoRegra = PublicacaoTipoRegra;
                            resposta.PublicacaoTipoRegra.PublicacaoTipoRegraId = (int)dt.Rows[0]["PublicacaoTipoRegraId"];
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

        public PublicacaoTipoRegraResponse ExcluirPublicacaoTipoRegra(int PublicacaoTipoRegraId)
        {
            PublicacaoTipoRegraResponse resposta = new PublicacaoTipoRegraResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@PublicacaoTipoRegraId", SqlDbType.Int, PublicacaoTipoRegraId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_PublicacaoTipoRegra"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.PublicacaoTipoRegra = null;
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

        private void CarregarDTO_PublicacaoTipoRegra(PublicacaoTipoRegra dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["PublicacaoTipoRegraId"]))
                dto.PublicacaoTipoRegraId = (int)dr["PublicacaoTipoRegraId"];
            if (Util.GetNonNull(dr["PublicacaoTipoId"]))
                dto.PublicacaoTipoId = (int)dr["PublicacaoTipoId"];
            if (Util.GetNonNull(dr["RegraId"]))
                dto.RegraId = (int)dr["RegraId"];
            if (Util.GetNonNull(dr["Privado"]))
                dto.Privado = (bool)dr["Privado"];

            if (Util.GetNonNull(dr["PublicacaoTipo"]))
                dto.Detalhes.PublicacaoTipo = dr["PublicacaoTipo"].ToString();
            if (Util.GetNonNull(dr["Regra"]))
                dto.Detalhes.Regra = dr["Regra"].ToString();
        }


        #endregion
    }
}
