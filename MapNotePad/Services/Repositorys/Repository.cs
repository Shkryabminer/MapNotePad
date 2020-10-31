using MapNotePad.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MapNotePad.Services
{
    public class Repository : IRepository
    {
        SQLiteConnection dataBase;
        private string _dataPath;
        private string DataPath
        {
            get
            {
                if (_dataPath == null)
                    _dataPath = Constants.dataBasePath;
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), _dataPath);
            }
            set { _dataPath = value; }
        }
        public Repository()
        {
            dataBase = new SQLiteConnection(DataPath);
            dataBase.CreateTable<User>();
            dataBase.CreateTable<PinModel>(CreateFlags.AutoIncPK);       

        }
      public int AddOrrUpdate<T>(T item) where T: class,IEntity,new ()
        {
            int i = 0;
            if (item != null)
            {
                if (item.ID == 0)
                  i=  dataBase.Insert(item);

                else
                    i=dataBase.Update(item);
            }

            return i;
        }

        public void DeleteItem<T>(T item) where T : class, IEntity, new()
        {
            if (item != null)
            {

                dataBase.Delete<T>(item.ID);
            }
        }

      public  IEnumerable<T> GetItems<T>() where T : class, IEntity, new()
        {
            return dataBase.Table<T>();
        }
    }
}
