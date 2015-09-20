using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using log4net;
using System.Reflection;
using DAL;

namespace DAL
{
    public class ConexaoDB : IDisposable
    {
        //public static readonly ILog logBLL = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public List<SqlParameter> Parametros = null;
        public SqlConnection conn = null;
        //SqlTransaction transaction

        public ConexaoDB()
        {
            Parametros = new List<SqlParameter>();
        }

        public void AdicionarParametro(string Nome, SqlDbType Tipo, object Valor)
        {
            try
            {
                if (Valor != null)
                    Parametros.Add(new SqlParameter { ParameterName = Nome, SqlDbType = Tipo, SqlValue = Valor });
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
        }

        public void LimparParametro()
        {
            try
            {
                Parametros.Clear();
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
        }

        public DataTable RetornarTabela(string query, string database = "dbCCBC", CommandType tipoComando = CommandType.StoredProcedure)
        {
            DataTable dados = new DataTable();
            try
            {
                AbrirConexao(database);

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandText = query;
                cmd.CommandType = tipoComando;
                foreach (SqlParameter p in Parametros)
                    cmd.Parameters.Add(p);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(dados);
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
            finally
            {
                FecharConexao();
            }
            return dados;
        }

        public string RetornarColunaString(string query, string database = "dbCCBC", CommandType tipoComando = CommandType.StoredProcedure)
        {
            SqlCommand com = null;
            try
            {
                AbrirConexao(database);

                com = new SqlCommand(query, conn);
                com.CommandType = tipoComando;
                foreach (SqlParameter p in Parametros)
                    com.Parameters.Add(p);
                return Convert.ToString(com.ExecuteScalar());
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public int RetornarColunaInt(string query, string database = "dbCCBC", CommandType tipoComando = CommandType.StoredProcedure)
        {
            SqlCommand com = null;
            try
            {
                AbrirConexao(database);

                com = new SqlCommand(query, conn);
                com.CommandType = tipoComando;
                foreach (SqlParameter p in Parametros)
                    com.Parameters.Add(p);
                return Convert.ToInt32(com.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public long RetornarColunaLong(string query, string database = "dbCCBC", CommandType tipoComando = CommandType.StoredProcedure)
        {
            SqlCommand com = null;
            try
            {
                AbrirConexao(database);

                com = new SqlCommand(query, conn);
                com.CommandType = tipoComando;
                foreach (SqlParameter p in Parametros)
                    com.Parameters.Add(p);
                return Convert.ToInt64(com.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }

        public int ExecutarComando(string query, string database = "dbCCBC", CommandType tipoComando = CommandType.StoredProcedure)
        {
            SqlCommand com = null;
            try
            {
                AbrirConexao(database);

                com = new SqlCommand(query, conn);
                com.CommandType = tipoComando;
                foreach (SqlParameter p in Parametros)
                    com.Parameters.Add(p);
                return com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
            finally
            {
                FecharConexao();
            }
        }


        public SqlDataReader ExecutarReader(string query, string database = "dbCCBC", CommandType tipoComando = CommandType.StoredProcedure)
        {
            SqlCommand com = null;
            SqlDataReader reader = null;
            try
            {
                AbrirConexao(database);

                com = new SqlCommand(query, conn);
                com.CommandType = tipoComando;
                foreach (SqlParameter p in Parametros)
                    com.Parameters.Add(p);
                reader = com.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
            finally
            {
                LimparParametro();
            }
            return reader;
        }

        public void AbrirConexao(string database, string ConnectionString = "")
        {
            string conexao = string.Empty;

            try
            {
                if (ConnectionString == "")
                {
                    ConnectionString = Config.getConnectionStringValue("Conexao");
                }

                conexao = String.Format(ConnectionString, database);

                if (conn == null)
                    conn = new SqlConnection(conexao);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
        }
        public void FecharConexao()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
                LimparParametro();
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
        }

        public void Dispose()
        {
            FecharConexao();
        }
    }
}