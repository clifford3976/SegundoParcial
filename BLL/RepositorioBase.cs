using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioBase<T> : IDisposable, IRepository<T> where T : class
    {
        internal Contexto contexto;

        public RepositorioBase()
        {
            contexto = new Contexto();
        }

        public virtual T Buscar(int id)
        {
            T entity;
            try
            {
                entity = contexto.Set<T>().Find(id);
            }
            catch (Exception)
            {
                throw;
            }

            return entity;
        }

        public virtual bool Eliminar(int id)
        {
            bool paso = false;
            try
            {
                T entity = contexto.Set<T>().Find(id);
                contexto.Set<T>().Remove(entity);

                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }

                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            return paso;
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> expression)
        {
            List<T> lista = new List<T>();
            try
            {
                lista = contexto.Set<T>().Where(expression).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return lista;
        }

        public virtual bool Guardar(T entity)
        {
            bool paso = false;
            try
            {
                if (contexto.Set<T>().Add(entity) != null)
                {
                    contexto.SaveChanges();
                    paso = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return paso;
        }

        public virtual bool Modificar(T entity)
        {
            bool paso = false;
            try
            {
                contexto.Entry(entity).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return paso;
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
