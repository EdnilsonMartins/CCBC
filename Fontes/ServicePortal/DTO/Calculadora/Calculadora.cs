using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Calculadora
{
    public class Calculadora
    {
        public double? ArbitroUnico { get; set; }
        public double? Presidente { get; set; }
        public double? CoArbitros { get; set; }
        public double? Total { get; set; }
        public double? DespesasAdministrativas { get; set; }

        public string ArbitroUnicoStr { get; set; }
        public string PresidenteStr { get; set; }
        public string CoArbitrosStr { get; set; }
        public string CoArbitrosStr2 { get; set; }
        public string TotalStr { get; set; }
        public string DespesasAdministrativasStr { get; set; }



        public double? TaxaRegistro { get; set; }
        public double? HonorarioArbitroUnico { get; set; }
        public double? TotalRequerenteArbitroUnico { get; set; }
        public double? TotalRequeridoArbitroUnico { get; set; }
        public double? Honorario3Arbitros { get; set; }
        public double? TotalRequerente3Arbitros { get; set; }
        public double? TotalRequerido3Arbitros { get; set; }

        public string TaxaRegistroStr { get; set; }
        public string HonorarioArbitroUnicoStr { get; set; }
        public string TotalRequerenteArbitroUnicoStr { get; set; }
        public string TotalRequeridoArbitroUnicoStr { get; set; }
        public string Honorario3ArbitrosStr { get; set; }
        public string TotalRequerente3ArbitrosStr { get; set; }
        public string TotalRequerido3ArbitrosStr { get; set; }

        public double? Segregacao3ArbitrosTaxaAdm { get; set; }
        public double? Segregacao3ArbitrosHonorarios { get; set; }
        public double? Segregacao3ArbitrosTotal { get; set; }
        public string Segregacao3ArbitrosTaxaAdmStr { get; set; }
        public string Segregacao3ArbitrosHonorariosStr { get; set; }
        public string Segregacao3ArbitrosTotalStr { get; set; }

        public double? Segregacao1ArbitroTaxaAdm { get; set; }
        public double? Segregacao1ArbitroHonorarios { get; set; }
        public double? Segregacao1ArbitroTotal { get; set; }
        public string Segregacao1ArbitroTaxaAdmStr { get; set; }
        public string Segregacao1ArbitroHonorariosStr { get; set; }
        public string Segregacao1ArbitroTotalStr { get; set; }

    }
}
