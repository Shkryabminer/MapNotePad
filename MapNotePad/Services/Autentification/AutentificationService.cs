using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MapNotePad.Models;
using MapNotePad.Services;

namespace MapNotePad.Services
{
    public  class AutentificationService : IAuthentificationService
    {
        private readonly IRepository _data;
       
        public AutentificationService(IRepository repository)
        {
            _data = repository;
        }
        #region --IAutentification implementation--

        public IUser GetAuthUser(string email, string password)
        {
            IUser user=null;
            if(IsAutenficated(email,password))
            {                
                var c = from u in _data.GetItems<User>()
                        where u.Email.ToLower() == email.ToLower() && u.Password == password
                        select u;
                user = c.First();
            }
            return user;
        }

        public bool IsAutenficated(string login, string password)
        {
            var c = from u in _data.GetItems<User>() 
                    where u.Email == login && u.Password == password
                    select u;
            return (c != null && c.Count() > 0);
        }
        #endregion
    }
}
