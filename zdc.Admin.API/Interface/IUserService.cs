using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(User user);

        Task<bool> Edit(User req);

        Task<bool> Del(int uid);

        Task<bool> Add(User req);
    }
}
