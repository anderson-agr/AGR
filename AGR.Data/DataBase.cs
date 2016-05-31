using System;
using System.Collections.Generic;
using System.Linq;
using AGR.Entidade;
using AGR.Entidade.Base;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;

namespace AGR.Data
{
    public  class DataBase : IDisposable
    {
        private  SQLiteConnection _connection;
        
        public DataBase(SQLiteConnection connection)
        {
            _connection = connection;
            _connection.CreateTable<Usuario>();
        }

        public void Salvar(EntidadeBase entity)
        {
            _connection.Insert(entity);
        }

        public int Deletar(EntidadeBase entity)
        {
            return _connection.Delete(entity);
        }

        public int Alterar(EntidadeBase entity)
        {
            return _connection.Update(entity);
        }

        public List<T> Todos<T>() where T : class
        {
            return _connection.Table<T>().ToList();
        }

        public T GetEntity<T>(int id) where T : class
        {
            return _connection.GetWithChildren<T>(id);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

    }
}
