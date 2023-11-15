﻿using Model.Dto.User;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface ICustomJWTService
    {
        Task<string> GetToken(User user);
    }
}
