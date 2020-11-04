using MapNotePad.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MapNotePad.Services
{
    public class Repository : IRepository
    {
        private SQLiteConnection _dataBase;

        private string _dataPath;
        public string DataPath
        {
            get
            {
                if (_dataPath == null) 
                { 
                    _dataPath = Constants.dataBasePath;
                }

                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _dataPath);
            }
            set => _dataPath = value;
            
        }
       
        public Repository()
        {
            _dataBase = new SQLiteConnection(DataPath); // replace to async connection
            _dataBase.CreateTable<User>();
            _dataBase.CreateTable<PinModel>();

        }
        #region --IRepository implement--

        public int AddOrrUpdate<T>(T item) where T : class, IEntity, new()
        {
            int i;

            if (item != null)
            {
                if (item.ID == 0)
                {
                    i = _dataBase.Insert(item);
                }
                else
                {
                    i = _dataBase.Update(item);
                }
            }
            else
            {
                i = 0;
            }

            return i;
        }

        public void DeleteItem<T>(T item) where T : class, IEntity, new()
        {
            if (item != null)
            {
                _dataBase.Delete<T>(item.ID);
            }
            else
            {
                Debug.WriteLine("Item is null");
            }
        }

        public IEnumerable<T> GetItems<T>() where T : class, IEntity, new()
        {
            return _dataBase.Table<T>();
        }

        public IEnumerable<T> GetItems<T>(Func<T, bool> pred) where T : class, IEntity, new()
        {
            return _dataBase.Table<T>();
        }

        //make get item

        public Task<T> GetItemAsync<T>(Func<T, bool> pred) where T : class, IEntity, new()
        {
            return Task.FromResult(_dataBase.Get<T>(pred));
        }

        #endregion
    }
}
