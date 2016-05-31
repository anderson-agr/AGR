using System.Collections.Generic;
using System.IO;
using AGR.Data;
using AGR.Entidade.Base;
using SQLite.Net;
using Xamarin.Forms;

namespace AGR.Repository.Base
{
    public class Repository
    {
        private DataBase _dataBase;

        public Repository()
        {
           
            _dataBase = new DataBase(DependencyService.Get<IConfig>().GetConnection());
        }

        public void Salvar(EntidadeBase entity)
        {
            _dataBase.Salvar(entity);
        }

        public int Deletar(EntidadeBase entity)
        {
            return _dataBase.Deletar(entity);
        }

        public int Alterar(EntidadeBase entity)
        {
            return _dataBase.Alterar(entity);
        }

        public List<T> Todos<T>() where T : class
        {
            return _dataBase.Todos<T>();
        }

        public T GetEntity<T>(int Id) where T : class
        {
            return _dataBase.GetEntity<T>(Id);
        }

    }
}