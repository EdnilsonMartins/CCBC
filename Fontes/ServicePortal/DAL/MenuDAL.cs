using DTO.Menu;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class MenuDAL
    {
        public List<Menu> ListarMenu(int SiteId, int MenuTipoId, int IdiomaId, int? PublicacaoId, bool? ExibirTodos = false, int? UsuarioId = null, bool FiltrarPrivacidade = true)
        {
            List<Menu> lista = new List<Menu>();

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            if (ExibirTodos != null)
            {
                tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Menu", SiteId, MenuTipoId, IdiomaId, PublicacaoId, null, ExibirTodos, UsuarioId, FiltrarPrivacidade);
            }
            else
            {
                tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Menu", SiteId, MenuTipoId, IdiomaId, PublicacaoId, null, null, UsuarioId, FiltrarPrivacidade);
            }

            if (tabela.Rows.Count > 0)
            {
                Menu dto;
                foreach (DataRow dr in tabela.Rows)
                {
                    dto = new Menu();
                    CarregarDTO(dto, dr);
                    lista.Add(dto);
                }
            }


            return lista;
        }

        public List<Menu> ListarMenuTree(int IdiomaId, int? PublicacaoId )
        {
            List<Menu> lista = new List<Menu>();

            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Menu_Tree", IdiomaId, PublicacaoId);

            if (tabela.Rows.Count > 0)
            {
                Menu dto;
                foreach (DataRow dr in tabela.Rows)
                {
                    dto = new Menu();
                    CarregarDTO(dto, dr);
                    lista.Add(dto);
                }
            }


            return lista;
        }

        public MenuResponse Carregar(int MenuId, int IdiomaId, bool FiltrarPrivacidade = true)
        {
            MenuResponse resposta = new MenuResponse();
            Menu menu;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@MenuId", SqlDbType.Int, MenuId);
                    objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, IdiomaId);
                    objetoConexao.AdicionarParametro("@FiltrarPrivacidade", SqlDbType.Bit, FiltrarPrivacidade);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Menu"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            menu = new Menu();
                            CarregarDTO(menu, dr);

                            resposta.Menu = menu;
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

        public MenuResponse Gravar(Menu Menu, Menu MenuOld, List<MenuIdiomaExcecao> Extras, List<MenuIdiomaExcecao> ExtrasOld, string ListaUsuarioGrupo, string ListaUsuario)
        {
            MenuResponse resposta = new MenuResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@MenuId", SqlDbType.Int, Menu.MenuId);
                    objetoConexao.AdicionarParametro("@MenuPaiId", SqlDbType.Int, Menu.MenuPaiId);
                    objetoConexao.AdicionarParametro("@SiteId", SqlDbType.Int, Menu.SiteId);
                    objetoConexao.AdicionarParametro("@MenuTipoId", SqlDbType.Int, Menu.MenuTipoId);
                    objetoConexao.AdicionarParametro("@MenuTipoAcaoId", SqlDbType.Int, Menu.MenuTipoAcaoId);
                    objetoConexao.AdicionarParametro("@PublicacaoId", SqlDbType.Int, Menu.PublicacaoId);
                    objetoConexao.AdicionarParametro("@LinkURL", SqlDbType.VarChar, Menu.LinkURL);
                    objetoConexao.AdicionarParametro("@ImageURL", SqlDbType.VarChar, Menu.ImageURL);
                    objetoConexao.AdicionarParametro("@TargetId", SqlDbType.VarChar, Menu.TargetId);
                    objetoConexao.AdicionarParametro("@Privado", SqlDbType.Bit, Menu.Complemento.Privado);
                    objetoConexao.AdicionarParametro("@ListaUsuarioGrupo", SqlDbType.VarChar, ListaUsuarioGrupo);
                    objetoConexao.AdicionarParametro("@ListaUsuario", SqlDbType.VarChar, ListaUsuario);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_Menu"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;// (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = "";// (string)dt.Rows[0]["msgErro"];
                            resposta.Menu = Menu;
                            resposta.Menu.MenuId = (int)dt.Rows[0]["MenuId"];
                        }
                    }
                }

                //using (ConexaoDB objetoConexao = new ConexaoDB())
                //{
                //    objetoConexao.AdicionarParametro("@PublicacaoId", SqlDbType.Int, resposta.Publicacao.PublicacaoId);
                //    objetoConexao.AdicionarParametro("@Privado", SqlDbType.Bit, false);
                //    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_PublicacaoRestricao"))
                //    {
                //        if (dt != null && dt.Rows.Count > 0)
                //        {
                //            resposta.Resposta.Erro = false;// (bool)dt.Rows[0]["indErro"];
                //            resposta.Resposta.Mensagem = "";// (string)dt.Rows[0]["msgErro"];
                //            resposta.Publicacao = Publicacao;
                //            //resposta.Publicacao.PublicacaoId = (int)dt.Rows[0]["PublicacaoRestricaoId"];
                //        }
                //    }
                //}

                using (ConexaoDB objetoConexao = new ConexaoDB())
                {

                    objetoConexao.AdicionarParametro("@MenuIdiomaExcecaoId", SqlDbType.Int, 0);
                    objetoConexao.AdicionarParametro("@MenuId", SqlDbType.Int, resposta.Menu.MenuId);
                    objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, Menu.Detalhe.IdiomaId);
                    objetoConexao.AdicionarParametro("@Rotulo", SqlDbType.VarChar, Menu.Detalhe.Rotulo);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_MenuIdiomaExcecao"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = false;// (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = "";// (string)dt.Rows[0]["msgErro"];
                            resposta.Menu = Menu;
                            //resposta.Publicacao.PublicacaoId = (int)dt.Rows[0]["PublicacaoRestricaoId"];
                        }
                    }
                }

                foreach (var Extra in Extras)
                {
                    using (ConexaoDB objetoConexao = new ConexaoDB())
                    {

                        objetoConexao.AdicionarParametro("@MenuIdiomaExcecaoId", SqlDbType.Int, 0);
                        objetoConexao.AdicionarParametro("@MenuId", SqlDbType.Int, resposta.Menu.MenuId);
                        objetoConexao.AdicionarParametro("@IdiomaId", SqlDbType.Int, Extra.IdiomaId);
                        objetoConexao.AdicionarParametro("@Rotulo", SqlDbType.VarChar, Extra.Rotulo);
                        using (DataTable dt = objetoConexao.RetornarTabela("USP_INS_MenuIdiomaExcecao"))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                resposta.Resposta.Erro = false;// (bool)dt.Rows[0]["indErro"];
                                resposta.Resposta.Mensagem = "";// (string)dt.Rows[0]["msgErro"];
                                resposta.Menu = Menu;
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

        public MenuResponse Excluir(int MenuId)
        {
            MenuResponse resposta = new MenuResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@MenuId", SqlDbType.Int, MenuId);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_DEL_Menu"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Menu = null;
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

        public MenuResponse Reposicionar(Menu Menu)
        {
            MenuResponse resposta = new MenuResponse();
            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@MenuId", SqlDbType.Int, Menu.MenuId);
                    objetoConexao.AdicionarParametro("@MenuPaiId", SqlDbType.Int, Menu.MenuPaiId);
                    objetoConexao.AdicionarParametro("@Posicao", SqlDbType.Int, Menu.Posicao);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_UPD_Menu_Reposicionar"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            resposta.Resposta.Erro = (bool)dt.Rows[0]["indErro"];
                            resposta.Resposta.Mensagem = (string)dt.Rows[0]["msgErro"];
                            resposta.Menu = null;
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

        private void CarregarDTO(Menu dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["MenuId"]))
                dto.MenuId = (int)dr["MenuId"];
            if (Util.GetNonNull(dr["MenuPaiId"]))
                dto.MenuPaiId = (int)dr["MenuPaiId"];
            if (Util.GetNonNull(dr["MenuTipoId"]))
                dto.MenuTipoId = (int)dr["MenuTipoId"];
            if (Util.GetNonNull(dr["MenuTipoAcaoId"]))
                dto.MenuTipoAcaoId = (int)dr["MenuTipoAcaoId"];
            if (Util.GetNonNull(dr["TargetId"]))
                dto.TargetId = (int)dr["TargetId"];
            if (Util.GetNonNull(dr["PublicacaoId"]))
                dto.PublicacaoId = (int)dr["PublicacaoId"];
            if (Util.GetNonNull(dr["Rotulo"]))
                dto.Rotulo = dr["Rotulo"].ToString();
            if (Util.GetNonNull(dr["LinkURL"]))
                dto.LinkURL = dr["LinkURL"].ToString();
            if (Util.GetNonNull(dr["ImageURL"]))
                dto.ImageURL = dr["ImageURL"].ToString();

            if (Util.GetNonNull(dr["Rotulo"]))
                dto.Detalhe.Rotulo = dr["Rotulo"].ToString();

            if (Util.GetNonNull(dr["Privado"]))
                dto.Complemento.Privado = (bool)dr["Privado"];
            if (Util.GetNonNull(dr["ListaUsuarioGrupo"]))
                dto.Complemento.ListaUsuarioGrupo = dr["ListaUsuarioGrupo"].ToString();
            if (Util.GetNonNull(dr["ListaUsuario"]))
                dto.Complemento.ListaUsuario = dr["ListaUsuario"].ToString();
            
        }
    }
}
