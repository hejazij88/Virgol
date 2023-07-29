using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virgol.Repository;
using Virgol_Model;
using Virgol_Model.Context;

namespace Virgol.Service
{
    public class UserService : GenericService<User>, IUserService
    {
        private UserRepository _userRepository;
        public UserService(VirgolContext context) : base(context)
        {
            _userRepository = new UserRepository(context);
        }

        public int GetAdminId(string PhoneNumber)
        {
            return _userRepository.GetAll().FirstOrDefault(t => t.PhoneNumber == PhoneNumber).UserId;
        }
    }
}
