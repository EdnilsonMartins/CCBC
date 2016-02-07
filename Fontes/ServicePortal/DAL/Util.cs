using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;


namespace DAL
{
    public static class Util
    {
        public enum IDIOMA
        {
            PORTUGUES = 1,
            ESPANHOL = 2,
            ENGLISH = 3,
            FRANCES = 4
        }

        public enum TIPOPUBLICACAO
        {
            EVENTO = 1,
            NOTICIA = 2,
            MATERIA = 3,
            ARTIGO = 4,
            PAGINA = 5,
            HOME = 6
        }

        public enum ARQUIVO_CATEGORIA_TIPO
        {
            PUBLICACAO = 1,
            MENU = 2,
            BANNER = 3,
            MEDIA_GLOBAL = 4
        }

        public enum BANNER_EVENTO_TIPO
        {
            VISUALIZACAO = 1,
            CLIQUE = 2
        }

        public enum FUNCIONALIDADES
        {
            ACESSO_PORTAL = 1,
            CMS_CCBC = 10,
            CMS_CAM = 20,

            DASH_GERAL_VISUALIZARTOTALIZADORES = 30,
            DASH_GERAL_PERMITIRAPROVACAO = 40,

            SITE_LISTAR = 50,
            SITE_ADICIONAR = 51,
            SITE_EDITAR = 52,
            SITE_EXCLUIR = 53,

            MENU_LISTAR = 60,
            MENU_ADICIONAR = 70,
            MENU_EDITAR = 80,
            MENU_EXCLUIR = 90,

            BANNER_LISTAR = 100,
            BANNER_ADICIONAR = 110,
            BANNER_EDITAR = 120,
            BANNER_EXCLUIR = 130,

            PUBLICACAO_LISTAR = 140,
            PUBLICACAO_ADICIONAR = 150,
            PUBLICACAO_EDITAR = 160,
            PUBLICACAO_EXCLUIR = 170,

            MEDIACENTER_EXPLORAR = 175,
            MEDIACENTER_CRIARPASTA = 180,
            MEDIACENTER_EXCLUIRPASTA = 190,
            MEDIACENTER_RENOMEARPASTA = 200,
            MEDIACENTER_ADICIONARARQUIVO = 210,
            MEDIACENTER_EXCLUIRARQUIVO = 211,
            MEDIACENTER_EDITARARQUIVO = 212,

            ATENDIMENTO_LISTARFILA = 220,

            EMARKETING_MAILING = 230,
            EMARKETING_TEMPLATES = 240,
            EMARKETING_CAMPANHAS = 250,
            EMARKETING_ENVIOS = 260,
            EMARKETING_RELATORIOS = 270,

            USUARIO_LISTAR = 280,
            USUARIO_ADICIONAR = 290,
            USUARIO_ADICIONAR_PRECADASTRO_WEBFULL = 295,
            USUARIO_EDITAR = 300,
            USUARIO_EXCLUIR = 310,

            USUARIOGRUPO_LISTAR = 320,
            USUARIOGRUPO_ADICIONAR = 330,
            USUARIOGRUPO_EDITAR = 340,
            USUARIOGRUPO_EXCLUIR = 350,

            CONFIGURACAO_SETUP = 360,

            CONFIGURACAO_WORKFLOW = 370,

            PESSOA_LISTAR = 380,
            PESSOA_ADICIONAR = 390,
            PESSOA_EDITAR = 400,
            PESSOA_EXCLUIR = 410,

            TEMPLATEEMAILADMIN_LISTAR = 420,
            TEMPLATEEMAILADMIN_EDITAR = 430

        }

        public static bool GetNonNull(Object obj)
        {
            bool retorno = false;
            if (obj != DBNull.Value)
            {
                retorno = true;
            }
            return retorno;
        }

        public static int GetIdiomaId(string lang)
        {
            int IdiomaId = 0;

            if (lang == "pt-BR") IdiomaId = (int)IDIOMA.PORTUGUES;
            else if (lang == "es-ES") IdiomaId = (int)IDIOMA.ESPANHOL;
            else if (lang == "en-US") IdiomaId = (int)IDIOMA.ENGLISH;
            else if (lang == "fr-CA") IdiomaId = (int)IDIOMA.FRANCES;
            return IdiomaId;
        }

        public static Object GetValue<T>(JObject Form, string Key, int index = 0)
        {
            try
            {
                Type tipo = typeof(T);


                Object retorno = null;

                if (Form[Key] != null)
                {
                    if (!String.IsNullOrEmpty(Form[Key][index].Value<string>().ToString()))
                        retorno = Form[Key][index].Value<T>();
                }
                if (retorno == null)
                {
                    if (tipo == typeof(int)) retorno = 0;
                }


                return retorno;
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
        }

        public static string MixMD5(string Senha)
        {
            //202CB962A C59075B964B07152D234B70 <= MD5 Gerado (123a)
            //C59075B964B07152D234B70 202CB962A <= MD5 Mixed ;)

            if (String.IsNullOrEmpty(Senha)) Senha = "";
            Senha = Util.CalculateMD5Hash(Senha);
            Senha = Senha.Substring(9) + Senha.Substring(0, 9);

            return Senha;
        }

        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string GerarURLAmigavel(string valor)
        {
            valor = GetString(valor);
            string resp = valor.ToLower();
            return resp;
        }

        private static string GetString(string Valor)
        {
            string resposta = null;
            if (Valor != null)
            {
                resposta = Valor;

                /** Troca os caracteres acentuados por não acentuados **/
                string[] acentos = new string[] { "ç", "á", "é", "í", "ó", "ú", "ý", "à", "è", "ì", "ò", "ù", "ä", "ë", "ï", "ö", "ü", "ã", "õ", "ñ", "â", "ê", "î", "ô", "û", "Ç", "Á", "É", "Í", "Ó", "Ú", "Ý", "À", "È", "Ì", "Ò", "Ù", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "Â", "Ê", "Î", "Ô", "Û", " " };
                string[] semAcento = new string[] { "c", "a", "e", "i", "o", "u", "y", "a", "e", "i", "o", "u", "a", "e", "i", "o", "u", "a", "o", "n", "a", "e", "i", "o", "u", "C", "A", "E", "I", "O", "U", "Y", "A", "E", "I", "O", "U", "A", "E", "I", "O", "U", "A", "O", "N", "A", "E", "I", "O", "U", "-" };

                for (int i = 0; i < acentos.Length; i++)
                {
                    resposta = resposta.Replace(acentos[i], semAcento[i]);
                }

                /** Troca os caracteres acentuados por não acentuados **/
                string[] remover = new string[] { "'", "~", "`", "!", "#", "?", "'", "\"", "(", ")", "^", "*", "=", ".", ",", "|", "/",":","%","[","]" };

                for (int i = 0; i < remover.Length; i++)
                {
                    resposta = resposta.Replace(remover[i], "");
                }

            }
            return resposta;
        }

        public static string ScrubHtml(string value)
        {
            if (value == null) return "";
            var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ");
            return step2;
        }
    }
}
