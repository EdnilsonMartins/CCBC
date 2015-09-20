using DTO.Configuracao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DAL
{
    public static class Config
    {
        //public static readonly ILog logBLL = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static Type tipo;

        public static string getAppConfigValue(string key)
        {
            try
            {
                if (tipo == null)
                    tipo = MethodBase.GetCurrentMethod().DeclaringType;

                if (tipo == typeof(System.Web.HttpApplication))
                {
                    if (ConfigurationManager.AppSettings[key] != null)
                    {
                        return ConfigurationManager.AppSettings[key];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    Assembly service = Assembly.GetAssembly(tipo);
                    Configuration config = ConfigurationManager.OpenExeConfiguration(service.Location);
                    if (config.AppSettings.Settings[key] != null)
                    {
                        return config.AppSettings.Settings[key].Value;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
        }

        public static string getConnectionStringValue(string key)
        {
            try
            {
                if (tipo == null)
                    tipo = MethodBase.GetCurrentMethod().DeclaringType;

                if (tipo == typeof(System.Web.HttpApplication))
                {
                    if (ConfigurationManager.ConnectionStrings[key].ConnectionString != null)
                    {
                        string valor = ConfigurationManager.ConnectionStrings[key].ConnectionString;
                        return valor;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    Assembly service = Assembly.GetAssembly(tipo);
                    Configuration config = ConfigurationManager.OpenExeConfiguration(service.Location);
                    if (config.ConnectionStrings.ConnectionStrings[key].ConnectionString != null)
                    {
                        return config.ConnectionStrings.ConnectionStrings[key].ConnectionString;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                //logBLL.Error(ex);
                throw;
            }
        }

        
    
    
    
    
    }
}
