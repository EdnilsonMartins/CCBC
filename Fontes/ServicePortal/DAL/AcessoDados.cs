using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace DAL
{
    public class AcessoDados
    {

        public DataTable CarregarDadosParametros(SqlConnection conn, SqlTransaction transaction, string database, string nomeProcedure, params object[] parametros)
        {
            DataTable dados = new DataTable();

            try
            {

                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = nomeProcedure;
                comando.Connection = conn;
                comando.CommandTimeout = 1200;

                if (transaction != null) comando.Transaction = transaction;

                //Obtém todos os parâmetros da stored procedure.
                SqlCommandBuilder.DeriveParameters(comando);

                //Valoriza os parâmetros da stored procedure.
                for (int i = 0; i < parametros.Length; i++)
                {
                    comando.Parameters[i + 1].Value = parametros[i];
                }

                //Relaciona os parâmetros que não serão utilizados.
                List<SqlParameter> lista = new List<SqlParameter>();
                foreach (SqlParameter p in comando.Parameters)
                {
                    if (p.Value == null) lista.Add(p);
                }

                //Remove os parâmetros relacionados que não serão utilizados.
                foreach (SqlParameter p in lista)
                {
                    comando.Parameters.Remove(p);
                }

                SqlDataReader drReader;
                if (transaction == null)
                    drReader = ExecutarComandoSqLcomCulturaPtBr(comando, CommandBehavior.CloseConnection);
                else
                {
                    //drReader = comando.ExecuteReader();
                    drReader = ExecutarComandoSqLcomCulturaPtBr(comando);
                }

                dados.Load(drReader);


            }
            catch (Exception Err)
            {
                //LogErroDAL.GravarErro("ERRO", this.GetType().ToString() + ".CarregarDadosParametros()", "Stored Procedure:" + nomeProcedure + ": " + Err.Message, Err.StackTrace);
                throw Err;
            }

            return dados;
        }

        public DataTable CarregarDadosParametros(string database, string nomeProcedure, params object[] parametros)
        {
            string conexao = System.Configuration.ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;
            conexao = String.Format(conexao, database);

            DataTable dados = new DataTable();
            SqlConnection conn = new SqlConnection(conexao);
            conn.Open();
            dados = CarregarDadosParametros(conn, null, database, nomeProcedure, parametros);
            conn.Close();
            return dados;
        }

        private SqlDataReader ExecutarComandoSqLcomCulturaPtBr(SqlCommand comando, CommandBehavior? behavior = null)
        {
            var currentCulture = System.Globalization.CultureInfo.CurrentCulture;

            var cultureInfo = CultureInfo.GetCultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);

            var result = behavior.HasValue ? comando.ExecuteReader(behavior.Value) : comando.ExecuteReader();

            Thread.CurrentThread.CurrentCulture = currentCulture;

            return result;
        }


    }
}
