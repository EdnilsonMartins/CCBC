﻿using DTO;
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

        public UsuarioResponse Gravar(UsuarioDTO Usuario, UsuarioDTO UsuarioOld, string ListaUsuarioGrupo)
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
                    if (Usuario.UsuarioId == 0)
                    {
                        string Senha = Usuario.Senha;
                        Senha = Util.MixMD5(Senha);
                        objetoConexao.AdicionarParametro("@Senha", SqlDbType.VarChar, Senha);
                    } 
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

        private void CarregarRegistro(UsuarioDTO dto, DataRow dr)
        {
            try
            {
                if (Util.GetNonNull(dr["UsuarioId"]))
                    dto.UsuarioId = (int)dr["UsuarioId"];
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

            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
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
