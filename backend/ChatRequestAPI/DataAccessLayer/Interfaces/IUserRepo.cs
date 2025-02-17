﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> Insert(UserEntity user);
    }
}
