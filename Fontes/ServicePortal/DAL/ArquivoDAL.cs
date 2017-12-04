using DTO.Arquivo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ArquivoDAL
    {
        public ArquivoResponse GravarArquivo(Arquivo arquivo, bool NovoContent)
        {
            ArquivoResponse resposta = new ArquivoResponse();
            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            if (arquivo.ListaCategoria == "null") arquivo.ListaCategoria = "";
            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_INS_Arquivo", arquivo.ArquivoId, arquivo.Content, arquivo.Legenda, arquivo.ListaCategoria, NovoContent, arquivo.Tipo, arquivo.PastaId, arquivo.FileName, arquivo.IdiomaId);

            if (tabela.Rows.Count > 0)
            {
                DataRow dr = tabela.Rows[0];
                if (Util.GetNonNull(dr["ArquivoId"]))
                {
                    resposta.Resposta.Erro = false;
                    resposta.Resposta.Mensagem = "";
                    resposta.Arquivo = arquivo;
                    resposta.Arquivo.ArquivoId = Convert.ToInt64(dr["ArquivoId"].ToString());

                }
            }

            return resposta;
        }

        public ArquivoResponse GravarArquivoGaleria(int OwnerId, long ArquivoId, int _ArquivoCategoriaTipoId)
        {
            ArquivoResponse resposta = new ArquivoResponse();
            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            if (_ArquivoCategoriaTipoId == (int)Util.ARQUIVO_CATEGORIA_TIPO.PUBLICACAO)
            {
                tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_INS_PublicacaoArquivo", OwnerId, ArquivoId);
                if (tabela.Rows.Count > 0)
                {
                    DataRow dr = tabela.Rows[0];
                    if (Util.GetNonNull(dr["PublicacaoArquivoId"]))
                    {
                        resposta.Resposta.Erro = false;
                        resposta.Resposta.Mensagem = "";
                    }
                }
            }
            else if (_ArquivoCategoriaTipoId == (int)Util.ARQUIVO_CATEGORIA_TIPO.MENU)
            {
                tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_INS_MenuArquivo", OwnerId, ArquivoId);
                if (tabela.Rows.Count > 0)
                {
                    DataRow dr = tabela.Rows[0];
                    if (Util.GetNonNull(dr["MenuArquivoId"]))
                    {
                        resposta.Resposta.Erro = false;
                        resposta.Resposta.Mensagem = "";
                    }
                }
            }
            else if (_ArquivoCategoriaTipoId == (int)Util.ARQUIVO_CATEGORIA_TIPO.BANNER)
            {
                tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_INS_BannerArquivo", OwnerId, ArquivoId);
                if (tabela.Rows.Count > 0)
                {
                    DataRow dr = tabela.Rows[0];
                    if (Util.GetNonNull(dr["BannerArquivoId"]))
                    {
                        resposta.Resposta.Erro = false;
                        resposta.Resposta.Mensagem = "";
                    }
                }
            }
            else if (_ArquivoCategoriaTipoId == (int)Util.ARQUIVO_CATEGORIA_TIPO.PODCAST)
            {
                tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_INS_PodcastArquivo", OwnerId, ArquivoId);
                if (tabela.Rows.Count > 0)
                {
                    DataRow dr = tabela.Rows[0];
                    if (Util.GetNonNull(dr["PodcastArquivoId"]))
                    {
                        resposta.Resposta.Erro = false;
                        resposta.Resposta.Mensagem = "";
                    }
                }
            }
            else if (_ArquivoCategoriaTipoId == (int)Util.ARQUIVO_CATEGORIA_TIPO.MEDIA_GLOBAL)
            {

            }


            return resposta;
        }

        public ArquivoResponse ExcluirArquivo(long ArquivoId)
        {
            ArquivoResponse resposta = new ArquivoResponse();
            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_DEL_Arquivo", ArquivoId);

            if (tabela.Rows.Count > 0)
            {
                DataRow dr = tabela.Rows[0];
                resposta.Resposta.Erro = false;
                resposta.Resposta.Mensagem = "";
            }
            return resposta;
        }

        public List<Arquivo> ListarArquivos(int? ArquivoId)
        {
            List<Arquivo> lista = new List<Arquivo>();


            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Arquivo", ArquivoId);

            if (tabela.Rows.Count > 0)
            {
                Arquivo dto;
                foreach (DataRow dr in tabela.Rows)
                {
                    dto = new Arquivo();
                    CarregarDTO(dto, dr, false);
                    lista.Add(dto);
                }
            }

            return lista;
        }

        public List<Arquivo> ListarArquivosGaleria(int? OwnerId, int? ArquivoCategoriaId = null, int _ArquivoCategoriaTipoId = 0, int PastaId = 0)
        {
            List<Arquivo> lista = new List<Arquivo>();


            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            if (_ArquivoCategoriaTipoId == (int)Util.ARQUIVO_CATEGORIA_TIPO.PUBLICACAO)
            {
                tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_PublicacaoArquivo", OwnerId, ArquivoCategoriaId);
            }
            else if (_ArquivoCategoriaTipoId == (int)Util.ARQUIVO_CATEGORIA_TIPO.MENU)
            {
                tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_MenuArquivo", OwnerId, ArquivoCategoriaId);
            }
            else if (_ArquivoCategoriaTipoId == (int)Util.ARQUIVO_CATEGORIA_TIPO.BANNER)
            {
                tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_BannerArquivo", OwnerId, ArquivoCategoriaId);
            }
            else if (_ArquivoCategoriaTipoId == (int)Util.ARQUIVO_CATEGORIA_TIPO.MEDIA_GLOBAL)
            {
                tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_GeralArquivo", PastaId);
            }
            else if (_ArquivoCategoriaTipoId == (int)Util.ARQUIVO_CATEGORIA_TIPO.PODCAST)
            {
                tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_PodcastArquivo", OwnerId, ArquivoCategoriaId);
            }
            if (tabela.Rows.Count > 0)
            {
                Arquivo dto;
                foreach (DataRow dr in tabela.Rows)
                {
                    dto = new Arquivo();
                    CarregarDTO(dto, dr, false);
                    lista.Add(dto);
                }
            }

            return lista;
        }

        public Arquivo CarregarArquivo(long ArquivoId)
        {
            Arquivo cont = new Arquivo();


            AcessoDados acesso = new AcessoDados();

            DataTable tabela = new DataTable();

            tabela = acesso.CarregarDadosParametros("dbCCBC", "USP_SEL_Arquivo", ArquivoId);

            if (tabela.Rows.Count > 0)
            {
                CarregarDTO(cont, tabela.Rows[0], true);
            }

            return cont;
        }

        private void CarregarDTO(Arquivo dto, DataRow dr, bool ComFoto)
        {
            if (Util.GetNonNull(dr["ArquivoId"]))
                dto.ArquivoId = (Int64)dr["ArquivoId"];
            if (ComFoto && Util.GetNonNull(dr["Content"]))
            {
                dto.Content = (byte[])dr["Content"];
            }
            //if (Util.GetNonNull(dr["Content64"]))
            //    dto.Base64 = dr["Content64"].ToString();
            if (Util.GetNonNull(dr["Tipo"]))
                dto.Tipo = dr["Tipo"].ToString();
            if (Util.GetNonNull(dr["Legenda"]))
                dto.Legenda = dr["Legenda"].ToString();
            if(dr.Table.Columns["IdiomaId"] != null)
                if (Util.GetNonNull(dr["IdiomaId"]))
                    dto.IdiomaId = Convert.ToInt32(dr["IdiomaId"]);
            if (Util.GetNonNull(dr["ListaCategoria"]))
                dto.ListaCategoria = dr["ListaCategoria"].ToString();

            if (Util.GetNonNull(dr["NomeArquivo"]))
                dto.FileName = dr["NomeArquivo"].ToString();
        }


    }
}
