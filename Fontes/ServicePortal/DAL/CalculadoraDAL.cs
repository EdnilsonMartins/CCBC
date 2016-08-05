using DTO.Calculadora;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DAL
{
    public class CalculadoraDAL
    {

        public CalculadoraResponse Calcular(string _Valor)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
                    provider.NumberGroupSeparator = ",";
                    provider.NumberDecimalSeparator = ".";

            double Valor = (double)Convert.ToDouble(_Valor, provider);
            CalculadoraResponse resposta = new CalculadoraResponse();
            Calculadora calculadora;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@Valor", SqlDbType.Decimal, Valor);
                    using (DataTable dt = objetoConexao.RetornarTabela("USP_SEL_Calculadora"))
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            calculadora = new Calculadora();
                            CarregarDTO_Calculadora(calculadora, dr);

                            resposta.Calculadora = calculadora;
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

        private void CarregarDTO_Calculadora(Calculadora dto, DataRow dr)
        {
            if (Util.GetNonNull(dr["ArbitroUnico"]))
            {
                dto.ArbitroUnico = Convert.ToDouble(dr["ArbitroUnico"]);
                dto.ArbitroUnicoStr = ((double)dto.ArbitroUnico).ToString("R$ #,#00.00");
            }
            if (Util.GetNonNull(dr["Presidente"]))
            {
                dto.Presidente = Convert.ToDouble(dr["Presidente"]);
                dto.PresidenteStr = ((double)dto.Presidente).ToString("R$ #,#00.00");
            }
            if (Util.GetNonNull(dr["CoArbitros"]))
            {
                dto.CoArbitros = Convert.ToDouble(dr["CoArbitros"]);
                dto.CoArbitrosStr = ((double)dto.CoArbitros).ToString("R$ #,#00.00");
            }
            if (Util.GetNonNull(dr["Total"]))
            {
                dto.Total = Convert.ToDouble(dr["Total"]);
                dto.TotalStr = ((double)dto.Total).ToString("R$ #,#00.00");
            }
            if (Util.GetNonNull(dr["DespesasAdministrativas"]))
            {
                dto.DespesasAdministrativas = Convert.ToDouble(dr["DespesasAdministrativas"]);
                dto.DespesasAdministrativasStr = ((double)dto.DespesasAdministrativas).ToString("R$ #,#00.00");
            }
        }

    }
}
