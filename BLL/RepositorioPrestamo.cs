using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioPrestamo : RepositorioBase<Prestamo>
    {
        public RepositorioPrestamo() : base()
        {

        }

        public override Prestamo Buscar(int id)
        {
            Prestamo prestamo = contexto.Prestamos.Include(x => x.Detalle).Where(z => z.PrestamoId == id).FirstOrDefault();

            return prestamo;
        }

        public override bool Guardar(Prestamo entity)
        {
            var cuenta = contexto.Cuenta.Find(entity.CuentaId);
            cuenta.Balance += entity.Capital;
            contexto.Entry(cuenta).State = EntityState.Modified;
            contexto.SaveChanges();

            return base.Guardar(entity);
        }

        public override bool Modificar(Prestamo entity)
        {
            var prestamoAnterior = contexto.Prestamos.Include(x => x.Detalle).Where(z => z.PrestamoId == entity.PrestamoId).AsNoTracking().FirstOrDefault();

            var prestamo = prestamoAnterior;
            var cuenta = contexto.Cuenta.Find(entity.CuentaId);
            cuenta.Balance -= prestamoAnterior.Capital;
           contexto.Entry(cuenta).State = EntityState.Modified;

            foreach (var item in prestamoAnterior.Detalle)
                contexto.Entry(item).State = EntityState.Deleted;

            foreach (var item in entity.Detalle)
                contexto.Entry(item).State = (item.CuotaId == 0) ? EntityState.Added : EntityState.Modified;


            cuenta.Balance += entity.Capital;
            contexto.Entry(cuenta).State = EntityState.Modified;

            return base.Modificar(entity);
        }

        public override bool Eliminar(int id)
        {

            var prestamo = Buscar(id);
            Cuenta cuenta = contexto.Cuenta.Find(prestamo.CuentaId);
            cuenta.Balance -= prestamo.Capital;
            contexto.Entry(cuenta).State = EntityState.Modified;

            return base.Eliminar(id);
        }

        public override List<Prestamo> GetList(Expression<Func<Prestamo, bool>> expression)
        {
            var lista = contexto.Prestamos.Include(x => x.Detalle).Where(expression).ToList();

            return lista;
        }
    }
}
