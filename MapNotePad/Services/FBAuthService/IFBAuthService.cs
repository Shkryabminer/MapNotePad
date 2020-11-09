using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotePad.Services.FBAuthService
{
    public interface IFBAuthService
    {
        Task<string> GetFBAccauntEmail();        
    }
}
