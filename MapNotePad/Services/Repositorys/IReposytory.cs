using MapNotePad.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotePad.Services
{
   public interface IRepository
    {
        IEnumerable<T> GetItems<T>() where T : class, IEntity, new();
        void DeleteItem<T>(T item) where T : class, IEntity, new();
        int AddOrrUpdate<T>(T item) where T : class, IEntity, new();


    }
}
