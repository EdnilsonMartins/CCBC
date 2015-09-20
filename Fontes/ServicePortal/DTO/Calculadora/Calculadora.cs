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
        public string TotalStr { get; set; }
        public string DespesasAdministrativasStr { get; set; }
    }
}
