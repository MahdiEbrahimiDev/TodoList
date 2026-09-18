using CoreLayer.DTOs.AccountDTO;
using CoreLayer.DTOs.UserDTO;
using CoreLayer.Entities;
using CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.UserServices
{
    public interface IUserServices
    {
       public ProfileDTO GetProfile(int userId);
        public User userLogin(LoginDTO login);
        public List<UserDTO> GetAllUsers(int userid);
        public User GetById(int id);
        public resultOperation CreateUser(CreateUserDTO DTO);
        public resultOperation UpdateUser(UpdateUserDTO DTO);
        public resultOperation DeleteUser(int id);
    }
}
