using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLibrary;

namespace UserRepo
{
    public interface IUserRepo
    {
        Task<User> Authenticate(string token);
    }
}
