using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MapNotePad.Models;
using MapNotePad.Services;
using System.Diagnostics;

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
            IUser user;

            if(IsAutenficated(email,password))
            {                
                var c = from u in _data.GetItems<User>()
                        where u.Email.ToLower() == email.ToLower() && u.Password == password
                        select u;
                //duplicate
                user = c.First();
            }
            else
            {
                user = null;
                Debug.WriteLine("User is not authenticated");
            }

            return user;
        }

        public bool IsAutenficated(string login, string password)
        {           
            var c = from u in _data.GetItems<User>() // wtf is c, wtf is u
                    where u.Email.ToUpper() == login.ToUpper() && u.Password == password
                    select u;
            //duplicate
            return (c != null && c.Any());
        }
        #endregion
    }
}
