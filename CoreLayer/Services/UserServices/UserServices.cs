using CoreLayer.DTOs.AccountDTO;
using CoreLayer.DTOs.UserDTO;
using CoreLayer.Entities;
using CoreLayer.Utilities;
using DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly AppDB _Context;

        public UserServices(AppDB Context)
        {
            _Context = Context;
        }


        public resultOperation CreateUser(CreateUserDTO item)
        {
            var userExist = _Context.Users
                .FirstOrDefault(c => c.Name == item.Name && !c.IsDeleted);


            if (userExist != null)
            {
                return resultOperation.Error("نام کاربری از قبل موجود است");
            }


            var user = new User
            {
                Name = item.Name,
                Password = item.Password,
                CraetedTime = DateTime.Now,
                IsDeleted = false
            };


            _Context.Users.Add(user);

            _Context.SaveChanges();


            return resultOperation.Success("کاربر با موفقیت ساخته شد");
        }



        public resultOperation DeleteUser(int id)
        {
            var userFound = GetById(id);


            if (userFound != null)
            {
                userFound.IsDeleted = true;
                userFound.DeletedTime = DateTime.Now;


                _Context.SaveChanges();


                return resultOperation.Success("کاربر با موفقیت حذف شد");
            }


            return resultOperation.Error("کاربر یافت نشد");
        }



        public List<UserDTO> GetAllUsers(int userId)
        {
            return _Context.Users
                .Where(c => !c.IsDeleted&&c.Id==userId)
                .Select(c => new UserDTO
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }



        public User GetById(int id)
        {
            return _Context.Users
                .FirstOrDefault(c => c.Id == id && !c.IsDeleted);
        }

        public ProfileDTO GetProfile(int userId)
        {
            var user = _Context.Users
                    .FirstOrDefault(x => x.Id == userId && !x.IsDeleted);

            if (user == null)
            {
                return null;
            }

            var totalTasks = _Context.TodoItems
                .Count(x => x.UserId == userId && !x.IsDeleted);

            var completedTasks = _Context.TodoItems
                .Count(x => x.UserId == userId &&
                            x.isCompleted &&
                            !x.IsDeleted);

            return new ProfileDTO
            {
                Name = user.Name,
                CreatedTime = user.CraetedTime,
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                PendingTasks = totalTasks - completedTasks
            };
        }

        public resultOperation UpdateUser(UpdateUserDTO item)
        {
            var user = _Context.Users
                .FirstOrDefault(i => i.Id == item.Id && !i.IsDeleted);


            if (user == null)
            {
                return resultOperation.Error("کاربر مورد نظر پیدا نشد");
            }


            var foundUser = _Context.Users
                .FirstOrDefault(i =>
                    i.Name == item.Name &&
                    i.Id != item.Id &&
                    !i.IsDeleted);


            if (foundUser != null)
            {
                return resultOperation.Error("نام کاربری وارد شده از قبل موجود است");
            }


            user.Name = item.Name;


            if (!string.IsNullOrEmpty(item.Password))
            {
                user.Password = item.Password;
            }


           


            _Context.SaveChanges();


            return resultOperation.Success("کاربر با موفقیت بروزرسانی شد");
        }

        public User userLogin(LoginDTO login)
        {
            return _Context.Users.FirstOrDefault(c => c.Name == login.Name && c.Password == login.Password && !c.IsDeleted);
        }
    }
}
