using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace EmaloteContta.Data.Padrao
{
    public abstract class RepositorioPadrao<T>
       where T : class
    {
        public virtual void Cadastre(T objeto)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Insert(objeto);
            }
        }

        public virtual void Atualize(T objeto)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Update(objeto);
            }
        }

        public virtual void Exclua(T objeto)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Delete(objeto);
            }
        }

        public virtual void ExcluaTodos()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.DeleteAll<T>();
            }
        }

        public virtual IList<T> ConsulteLista()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.GetAll<T>().ToList();
            }
        }

        protected abstract string LoadConnectionString();
    }
}
