using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class Prestamo
    {
        [Key]
        public int PrestamoId { get; set; }
        public DateTime Fecha { get; set; }
        public int CuentaId { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public int Tiempo { get; set; }
        public virtual Cuenta Cuenta { get; set; }
        public virtual List<CuotaDetalle> Detalle { get; set; }

        public Prestamo()
        {
            PrestamoId = 0;
            Fecha = DateTime.Now;
            CuentaId = 0;
            Capital = 0;
            Interes = 0;
            Tiempo = 0;
            Detalle = new List<CuotaDetalle>();
        }

        public Prestamo(int prestamoId, DateTime fecha, int cuentaId, decimal capital, decimal interes, int tiempo)
        {
            PrestamoId = prestamoId;
            Fecha = fecha;
            CuentaId = cuentaId;
            Capital = capital;
            Interes = interes;
            Tiempo = tiempo;
        }

       
    }
}
