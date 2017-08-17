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

        public CalculadoraResponse Calcular(string _Valor, int CalculadoraId = 1)
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
                    objetoConexao.AdicionarParametro("@CalculadoraId", SqlDbType.Int, CalculadoraId);
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
            if (Util.GetNonNull(dr["CoArbitros"]))
            {
                dto.CoArbitros = Convert.ToDouble(dr["CoArbitros"]);
                dto.CoArbitrosStr2 = ((double)dto.CoArbitros/2).ToString("R$ #,#00.00");
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



            if (dr.Table.Columns["TaxaRegistro"] != null)
            {
                if (Util.GetNonNull(dr["TaxaRegistro"]))
                {
                    dto.TaxaRegistro = Convert.ToDouble(dr["TaxaRegistro"]);
                    dto.TaxaRegistroStr = ((double)dto.TaxaRegistro).ToString("R$ #,#00.00");
                }
            }
            if (dr.Table.Columns["HonorarioArbitroUnico"] != null)
            {
                if (Util.GetNonNull(dr["HonorarioArbitroUnico"]))
                {
                    dto.HonorarioArbitroUnico = Convert.ToDouble(dr["HonorarioArbitroUnico"]);
                    dto.HonorarioArbitroUnicoStr = ((double)dto.HonorarioArbitroUnico).ToString("R$ #,#00.00");
                }
            }
            if (dr.Table.Columns["TotalRequerenteArbitroUnico"] != null)
            {
                if (Util.GetNonNull(dr["TotalRequerenteArbitroUnico"]))
                {
                    dto.TotalRequerenteArbitroUnico = Convert.ToDouble(dr["TotalRequerenteArbitroUnico"]);
                    dto.TotalRequerenteArbitroUnicoStr = ((double)dto.TotalRequerenteArbitroUnico).ToString("R$ #,#00.00");
                }
            }
            if (dr.Table.Columns["TotalRequeridoArbitroUnico"] != null)
            {
                if (Util.GetNonNull(dr["TotalRequeridoArbitroUnico"]))
                {
                    dto.TotalRequeridoArbitroUnico = Convert.ToDouble(dr["TotalRequeridoArbitroUnico"]);
                    dto.TotalRequeridoArbitroUnicoStr = ((double)dto.TotalRequeridoArbitroUnico).ToString("R$ #,#00.00");
                }
            }
            if (dr.Table.Columns["Honorario3Arbitros"] != null)
            {
                if (Util.GetNonNull(dr["Honorario3Arbitros"]))
                {
                    dto.Honorario3Arbitros = Convert.ToDouble(dr["Honorario3Arbitros"]);
                    dto.Honorario3ArbitrosStr = ((double)dto.Honorario3Arbitros).ToString("R$ #,#00.00");
                }
            }
            if (dr.Table.Columns["TotalRequerente3Arbitros"] != null)
            {
                if (Util.GetNonNull(dr["TotalRequerente3Arbitros"]))
                {
                    dto.TotalRequerente3Arbitros = Convert.ToDouble(dr["TotalRequerente3Arbitros"]);
                    dto.TotalRequerente3ArbitrosStr = ((double)dto.TotalRequerente3Arbitros).ToString("R$ #,#00.00");
                }
            }
            if (dr.Table.Columns["TotalRequerido3Arbitros"] != null)
            {
                if (Util.GetNonNull(dr["TotalRequerido3Arbitros"]))
                {
                    dto.TotalRequerido3Arbitros = Convert.ToDouble(dr["TotalRequerido3Arbitros"]);
                    dto.TotalRequerido3ArbitrosStr = ((double)dto.TotalRequerido3Arbitros).ToString("R$ #,#00.00");
                }
            }


            if (dto.DespesasAdministrativas != null)
                dto.Segregacao3ArbitrosTaxaAdm = dto.DespesasAdministrativas;
            if (dto.Honorario3Arbitros!=null && dto.Honorario3Arbitros!=null)
                dto.Segregacao3ArbitrosHonorarios = dto.Honorario3Arbitros + dto.Honorario3Arbitros;
            if (dto.TotalRequerente3Arbitros !=null && dto.TotalRequerido3Arbitros !=null && dto.TaxaRegistro != null && dto.DespesasAdministrativas != null)
                dto.Segregacao3ArbitrosTotal = dto.TotalRequerente3Arbitros + dto.TotalRequerido3Arbitros - dto.TaxaRegistro - dto.DespesasAdministrativas;
            if (dto.DespesasAdministrativasStr != null)
                dto.Segregacao3ArbitrosTaxaAdmStr = dto.DespesasAdministrativasStr;
            if (dto.Segregacao3ArbitrosHonorarios != null)
                dto.Segregacao3ArbitrosHonorariosStr = ((double)dto.Segregacao3ArbitrosHonorarios).ToString("R$ #,#00.00");
            if (dto.Segregacao3ArbitrosTotal != null)
                dto.Segregacao3ArbitrosTotalStr = ((double)dto.Segregacao3ArbitrosTotal).ToString("R$ #,#00.00");


            if (dto.DespesasAdministrativas != null)
                dto.Segregacao1ArbitroTaxaAdm = dto.DespesasAdministrativas;
            if (dto.HonorarioArbitroUnico != null && dto.HonorarioArbitroUnico != null)
                dto.Segregacao1ArbitroHonorarios = dto.HonorarioArbitroUnico + dto.HonorarioArbitroUnico;
            if (dto.TotalRequerenteArbitroUnico != null && dto.TotalRequeridoArbitroUnico != null && dto.TaxaRegistro != null && dto.DespesasAdministrativas != null)
                dto.Segregacao1ArbitroTotal = dto.TotalRequerenteArbitroUnico + dto.TotalRequeridoArbitroUnico - dto.TaxaRegistro - dto.DespesasAdministrativas;
            if (dto.DespesasAdministrativasStr != null)
                dto.Segregacao1ArbitroTaxaAdmStr = dto.DespesasAdministrativasStr;
            if (dto.Segregacao1ArbitroHonorarios != null)
                dto.Segregacao1ArbitroHonorariosStr = ((double)dto.Segregacao1ArbitroHonorarios).ToString("R$ #,#00.00");
            if (dto.Segregacao1ArbitroTotal != null)
                dto.Segregacao1ArbitroTotalStr = ((double)dto.Segregacao1ArbitroTotal).ToString("R$ #,#00.00");

        }

    }
}
