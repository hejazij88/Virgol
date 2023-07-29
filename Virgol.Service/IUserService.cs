﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virgol_Model;

namespace Virgol.Service
{
    public interface IUserService:IGenericService<User>
    {
        int GetAdminId(string PhoneNumber);
    }
}
