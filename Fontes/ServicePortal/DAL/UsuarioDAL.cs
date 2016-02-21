using DTO;
using DTO.Usuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class UsuarioDAL
    {


        #region --> Usuário

        public List<UsuarioDTO> ListarUsuario(int SiteId)
        {
            List<UsuarioDTO> lista = new List<UsuarioDTO>();
            UsuarioDTO usuario;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@ListarTodos", SqlDbType.Int, 1);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Login"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                usuario = new UsuarioDTO();
                                CarregarRegistro(usuario, dr);
                                lista.Add(usuario);
                            }
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

        public UsuarioResponse Carregar(int UsuarioId)
        {
            UsuarioResponse resposta = new UsuarioResponse();
            UsuarioDTO usuario;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, UsuarioId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Login"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            usuario = new UsuarioDTO();
                            CarregarRegistro(usuario, dr);

                            usuario.Funcionalidades = CarregarUsuarioFuncionalidade(usuario.UsuarioId);
                            resposta.Usuario = usuario;
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

        public UsuarioResponse Carregar(string Email)
        {
            UsuarioResponse resposta = new UsuarioResponse();
            UsuarioDTO usuario;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@Email", SqlDbType.VarChar, Email);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_LoginEmail"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            usuario = new UsuarioDTO();
                            CarregarRegistro(usuario, dr);

                            resposta.Usuario = usuario;
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

        public UsuarioResponse Gravar(UsuarioDTO Usuario, UsuarioDTO UsuarioOld, string ListaUsuarioGrupo, bool ForcarTrocaSenha = false)
        {
            UsuarioResponse resposta = new UsuarioResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    
                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, Usuario.UsuarioId);
                    objetoConexao.AdicionarParametro("@Nome", SqlDbType.VarChar, Usuario.Nome);
                    objetoConexao.AdicionarParametro("@Login", SqlDbType.VarChar, Usuario.Login);
                    objetoConexao.AdicionarParametro("@Email", SqlDbType.VarChar, Usuario.Email);
                    objetoConexao.AdicionarParametro("@Ativo", SqlDbType.Bit, Usuario.Ativo);
                    objetoConexao.AdicionarParametro("@ListaUsuarioGrupo", SqlDbType.VarChar, ListaUsuarioGrupo);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, Usuario.SiteId);
                    objetoConexao.AdicionarParametro("@TedescoUsuario", SqlDbType.VarChar, Usuario.TedescoUsuario);
                    objetoConexao.AdicionarParametro("@TedescoEmail", SqlDbType.VarChar, Usuario.TedescoEmail);
                    objetoConexao.AdicionarParametro("@TedescoStatusId", SqlDbType.VarChar, Usuario.TedescoStatusId);
                    if (Usuario.TedescoDataConfirmacao != null)
                    {
                        objetoConexao.AdicionarParametro("@TedescoDataConfirmacao", SqlDbType.DateTime, Usuario.TedescoDataConfirmacao);
                    }
                    objetoConexao.AdicionarParametro("@ForcarTrocaSenha", SqlDbType.Bit, ForcarTrocaSenha);

                    string Senha = "";
                    if (Usuario.UsuarioId == 0 || ForcarTrocaSenha)
                    {
                        Senha = Util.MixMD5(Usuario.Senha);
                    }
                    else
                    {
                        Senha = Usuario.Senha;
                    }
                    objetoConexao.AdicionarParametro("@Senha", SqlDbType.VarChar, Senha);
                    
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Login"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            Usuario.UsuarioId = (int)dt.Rows[0]["UsuarioId"];
                            resposta.Usuario = Usuario;

                            Resposta respostaFunc = GravarUsuarioFuncionalidade(Usuario);
                            resposta.Resposta = respostaFunc;

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

        public UsuarioResponse AlterarSenha(UsuarioDTO Usuario, UsuarioDTO UsuarioOld)
        {
            UsuarioResponse resposta = new UsuarioResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, Usuario.UsuarioId);
                    objetoConexao.AdicionarParametro("@Senha", SqlDbType.VarChar, Usuario.Senha);

                    using (DataTable dt = objetoConexao.RetornarTabela("USP_UPD_LoginSenha"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;// (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = "";// (string)dt.Rows[0]["msgErro"];
                            resposta.Usuario = Usuario;
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

        public UsuarioResponse ExcluirUsuario(int UsuarioId)
        {
            UsuarioResponse resposta = new UsuarioResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, UsuarioId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_Login"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Usuario = null;
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

        public UsuarioResponse NotificarUsuario(int UsuarioId)
        {
            UsuarioResponse resposta = new UsuarioResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, UsuarioId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Login_Notificar"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Usuario = null;
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

        

        private void CarregarRegistro(UsuarioDTO dto, DataRow dr)
        {
            try
            {
                if (Util.GetNonNull(dr["UsuarioId"]))
                    dto.UsuarioId = (int)dr["UsuarioId"];
                if (dr.Table.Columns["SiteId"] != null)
                    if (Util.GetNonNull(dr["SiteId"]))
                        dto.SiteId = (int)dr["SiteId"];
                if (Util.GetNonNull(dr["Nome"]))
                    dto.Nome = (string)dr["Nome"];
                if (Util.GetNonNull(dr["Login"]))
                    dto.Login = (string)dr["Login"];
                if (Util.GetNonNull(dr["Ativo"]))
                    dto.Ativo = (bool)dr["Ativo"];
                if (Util.GetNonNull(dr["Email"]))
                    dto.Email = (string)dr["Email"];
                if (Util.GetNonNull(dr["ListaUsuarioGrupo"]))
                    dto.ListaUsuarioGrupo = dr["ListaUsuarioGrupo"].ToString();

                if (Util.GetNonNull(dr["TedescoUsuario"]))
                    dto.TedescoUsuario = (string)dr["TedescoUsuario"];
                if (Util.GetNonNull(dr["TedescoEmail"]))
                    dto.TedescoEmail = (string)dr["TedescoEmail"];

                if (Util.GetNonNull(dr["TedescoUltimaNotificacao"]))
                    dto.TedescoUltimaNotificacao = new DateTime(((DateTime)dr["TedescoUltimaNotificacao"]).Year, ((DateTime)dr["TedescoUltimaNotificacao"]).Month, ((DateTime)dr["TedescoUltimaNotificacao"]).Day, ((DateTime)dr["TedescoUltimaNotificacao"]).Hour, ((DateTime)dr["TedescoUltimaNotificacao"]).Minute, ((DateTime)dr["TedescoUltimaNotificacao"]).Second);
                if (Util.GetNonNull(dr["TedescoStatusId"]))
                    dto.TedescoStatusId = (int)dr["TedescoStatusId"];
                if (Util.GetNonNull(dr["TedescoStatus"]))
                    dto.Complemento.TedescoStatus = dr["TedescoStatus"].ToString();
                if (Util.GetNonNull(dr["TedescoDataConfirmacao"]))
                    dto.TedescoDataConfirmacao = new DateTime(((DateTime)dr["TedescoDataConfirmacao"]).Year, ((DateTime)dr["TedescoDataConfirmacao"]).Month, ((DateTime)dr["TedescoDataConfirmacao"]).Day, ((DateTime)dr["TedescoDataConfirmacao"]).Hour, ((DateTime)dr["TedescoDataConfirmacao"]).Minute, ((DateTime)dr["TedescoDataConfirmacao"]).Second);

            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
        }

        public Util.PASSWORD_LEVEL ValidarSenha(UsuarioDTO dto)
        {
            ConfiguracaoDAL config = new ConfiguracaoDAL();
            var c = config.CarregarConfiguracao(dto.SiteId);
            
            Util.PASSWORD_LEVEL nivel = Util.PASSWORD_LEVEL.REJECTED;

            bool hasAlphabetic = false;
            bool hasNumeric = false;
            bool hasSpecial = false;

            string Alphabetic = "abcdefghijklmnopqrstuvxwyz";
            string Numeric = "1234567890";
            string Special = @"!@#$%¨&*()_+`´^~{[}]<>:,.;|\?/°ºª-§";

            string login = dto.Senha.ToLower();

            if (login.Length >= c.TamanhoMinimoSenha)
            {
                for (int i = 0; i < login.Length; i++)
                {
                    string value = login.Substring(i, 1);
                    if (Alphabetic.IndexOf(value) > -1) hasAlphabetic = true;
                    if (Numeric.IndexOf(value) > -1) hasNumeric = true;
                    if (Special.IndexOf(value) > -1) hasSpecial = true;
                }
                if (hasAlphabetic || hasNumeric) nivel = Util.PASSWORD_LEVEL.LOW;
                if (hasAlphabetic && hasNumeric && !hasSpecial) nivel = Util.PASSWORD_LEVEL.MEDIUM;
                if (hasAlphabetic && hasNumeric && hasSpecial) nivel = Util.PASSWORD_LEVEL.HIGH;
            }

            return nivel;
        }

        #endregion


        #region --> Grupo de Usuário

        public List<UsuarioGrupoDTO> ListarUsuarioGrupo(int SiteId)
        {
            List<UsuarioGrupoDTO> lista = new List<UsuarioGrupoDTO>();
            UsuarioGrupoDTO reg;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@UsuarioGrupoId", SqlDbType.Int, DBNull.Value);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, SiteId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_UsuarioGrupo"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                reg = new UsuarioGrupoDTO();
                                CarregarRegistro_UsuarioGrupo(reg, dr);
                                lista.Add(reg);
                            }
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

        public UsuarioGrupoResponse CarregarUsuarioGrupo(int UsuarioGrupoId)
        {
            UsuarioGrupoResponse resposta = new UsuarioGrupoResponse();
            UsuarioGrupoDTO usuarioGrupo;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@UsuarioGrupoId", SqlDbType.Int, UsuarioGrupoId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_UsuarioGrupo"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            usuarioGrupo = new UsuarioGrupoDTO();
                            CarregarRegistro_UsuarioGrupo(usuarioGrupo, dr);

                            resposta.UsuarioGrupo = usuarioGrupo;
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

        public UsuarioResponse ExcluirUsuarioGrupo(int UsuarioGrupoId)
        {
            UsuarioResponse resposta = new UsuarioResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@UsuarioGrupoId", SqlDbType.Int, UsuarioGrupoId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_UsuarioGrupo"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Usuario = null;
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

        public UsuarioGrupoResponse GravarUsuarioGrupo(UsuarioGrupoDTO UsuarioGrupo, UsuarioGrupoDTO UsuarioOld)
        {
            UsuarioGrupoResponse resposta = new UsuarioGrupoResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@UsuarioGrupoId", SqlDbType.Int, UsuarioGrupo.UsuarioGrupoId);
                    objetoConexao.AdicionarParametro("@Nome", SqlDbType.VarChar, UsuarioGrupo.Nome);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, UsuarioGrupo.SiteId);
                    objetoConexao.AdicionarParametro("@UsuarioGrupoPaiId", SqlDbType.Int, UsuarioGrupo.UsuarioGrupoPaiId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_UsuarioGrupo"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;// (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = "";// (string)dt.Rows[0]["msgErro"];
                            resposta.UsuarioGrupo = UsuarioGrupo;
                            resposta.UsuarioGrupo.UsuarioGrupoId = (int)dt.Rows[0]["UsuarioGrupoId"];
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

        private void CarregarRegistro_UsuarioGrupo(UsuarioGrupoDTO dto, DataRow dr)
        {
            try
            {
                if (Util.GetNonNull(dr["UsuarioGrupoId"]))
                    dto.UsuarioGrupoId = (int)dr["UsuarioGrupoId"];
                if (Util.GetNonNull(dr["Nome"]))
                    dto.Nome = (string)dr["Nome"];
                if (Util.GetNonNull(dr["UsuarioGrupoPaiId"]))
                    dto.UsuarioGrupoPaiId = (int)dr["UsuarioGrupoPaiId"];
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
        }

        #endregion

        #region --> Usuário Funcionalidades
        
        public Resposta GravarUsuarioFuncionalidade(UsuarioDTO Usuario)
        {
            Resposta resposta = new Resposta();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {

                    StringBuilder s = new StringBuilder();
                    foreach (Funcionalidade fun in Usuario.Funcionalidades)
                    {
                        //a.Marca = Util.FormataXMLValues(a.Marca);
                        s.Append(String.Format("<Fun><FuncionalidadeId>{0}</FuncionalidadeId><Ativo>{1}</Ativo><Parametro>{2}</Parametro></Fun>",
                            fun.FuncionalidadeId, fun.Ativo, fun.Parametro));
                    }
                    string xml = String.Format("<UsuarioFuncionalidades>{0}</UsuarioFuncionalidades>", s.ToString());

                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, Usuario.UsuarioId);
                    objetoConexao.AdicionarParametro("@XML", SqlDbType.VarChar, xml);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_UsuarioFuncionalidade"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Erro = false;
                            resposta.Mensagem = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resposta.Erro = true;
                resposta.Mensagem = ex.Message;

                //logBLL.Error(ex);
            }
            return resposta;
        }

        public List<Funcionalidade> CarregarUsuarioFuncionalidade(int UsuarioId){
            List<Funcionalidade> lista = new List<Funcionalidade>();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@UsuarioId", SqlDbType.Int, UsuarioId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_UsuarioFuncionalidade"))
                    {
                        
                        foreach (DataRow dr in dt.Rows)
                        {
                            Funcionalidade fun = new Funcionalidade();
                            if (Util.GetNonNull(dr["FuncionalidadeId"]))
                                fun.FuncionalidadeId = (int)dr["FuncionalidadeId"];
                            if (Util.GetNonNull(dr["Ativo"]))
                                fun.Ativo = (bool)dr["Ativo"];
                            if (Util.GetNonNull(dr["Parametro"]))
                                fun.Parametro = dr["Parametro"].ToString();
                            lista.Add(fun);
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

        public List<Funcionalidade> ListarFuncionalidades(int SistemaId)
        {
            List<Funcionalidade> lista = new List<Funcionalidade>();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@SistemaId", SqlDbType.Int, SistemaId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Funcionalidade"))
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            Funcionalidade fun = new Funcionalidade();
                            if (Util.GetNonNull(dr["FuncionalidadeId"]))
                                fun.FuncionalidadeId = (int)dr["FuncionalidadeId"];
                            lista.Add(fun);
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

        #endregion
    }
}
