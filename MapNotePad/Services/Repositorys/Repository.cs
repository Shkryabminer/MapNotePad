using MapNotePad.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MapNotePad.Services
{
    public class Repository : IRepository
    {
        private SQLiteAsyncConnection _dataBase;

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
            _dataBase = new SQLiteAsyncConnection(DataPath); // replace to async connection
            _dataBase.CreateTableAsync<User>();
            _dataBase.CreateTableAsync<PinModel>();

        }
        #region --IRepository implement--

        public async Task<int> AddOrrUpdateAsync<T>(T item) where T : class, IEntity, new()
        {
            int i;

            if (item != null)
            {
                if (item.ID == 0)
                {
                    i =await _dataBase.InsertAsync(item);
                }
                else
                {
                    i = await _dataBase.UpdateAsync(item);
                }
            }
            else
            {
                i = 0;
            }

            return i;
        }

        public  async Task<int> DeleteItemAsync<T>(T item) where T : class, IEntity, new()
        {
            int i = -1;
            if (item != null)
            {
             i= await  _dataBase.DeleteAsync<T>(item.ID);
            }
            else
            {
                Debug.WriteLine("Item is null");
            }
            return i;
        }

        public  Task<List<T>> GetItemsAsync<T>() where T : class, IEntity, new()
        {
            return _dataBase.Table<T>().ToListAsync();
        }

        public Task<List<T>> GetItems<T>(Func<T, bool> pred) where T : class, IEntity, new()
        {
            return _dataBase.Table<T>().ToListAsync();
        }

        //make get item

        public Task<T> GetItemAsync<T>(Func<T, bool> pred) where T : class, IEntity, new()
        {
            return _dataBase.GetAsync<T>(pred);
        }

      public  Task<List<T>> FindByAsync<T>(Expression<Func<T, bool>> predicate) where T:class,IEntity,new()
        {
            return _dataBase.Table<T>().Where(predicate).ToListAsync();
        }

        #endregion
    }
}
