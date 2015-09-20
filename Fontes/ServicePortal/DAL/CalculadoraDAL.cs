using DTO.Calculadora;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public class CalculadoraDAL
    {

        public CalculadoraResponse Calcular(double? Valor)
        {
            CalculadoraResponse resposta = new CalculadoraResponse();
            Calculadora calculadora;

            try
            {
                using (ConexaoDB objetoConexao = new ConexaoDB())
                {
                    objetoConexao.AdicionarParametro("@Valor", SqlDbType.Float, Valor);
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
                dto.ArbitroUnico = (double)dr["ArbitroUnico"];
                dto.ArbitroUnicoStr = ((double)dto.ArbitroUnico).ToString("R$ #,#00.00");
            }
            if (Util.GetNonNull(dr["Presidente"]))
            {
                dto.Presidente = (double)dr["Presidente"];
                dto.PresidenteStr = ((double)dto.Presidente).ToString("R$ #,#00.00");
            }
            if (Util.GetNonNull(dr["CoArbitros"]))
            {
                dto.CoArbitros = (double)dr["CoArbitros"];
                dto.CoArbitrosStr = ((double)dto.CoArbitros).ToString("R$ #,#00.00");
            }
            if (Util.GetNonNull(dr["Total"]))
            {
                dto.Total = (double)dr["Total"];
                dto.TotalStr = ((double)dto.Total).ToString("R$ #,#00.00");
            }
            if (Util.GetNonNull(dr["DespesasAdministrativas"]))
            {
                dto.DespesasAdministrativas = (double)dr["DespesasAdministrativas"];
                dto.DespesasAdministrativasStr = ((double)dto.DespesasAdministrativas).ToString("R$ #,#00.00");
            }
        }

    }
}
