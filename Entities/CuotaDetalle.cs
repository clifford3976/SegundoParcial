using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class CuotaDetalle
    {
        [Key]
        public int CuotaId { get; set; }
        public int PrestamoId { get; set; }
        public decimal Interes { get; set; }
        public decimal Capital { get; set; }
        public decimal Valor { get; set; }
        public decimal Balance { get; set; }

        public CuotaDetalle()
        {
            CuotaId = 0;
            PrestamoId = 0;
            Interes = 0;
            Capital = 0;
            Valor = 0;
            Balance = 0;
        }

        public CuotaDetalle(int cuotaId, int prestamoId, decimal interes, decimal capital, decimal valor, decimal balance)
        {
            CuotaId = cuotaId;
            PrestamoId = prestamoId;
            Interes = interes;
            Capital = capital;
            Valor = valor;
            Balance = balance;
        }
    }
}
