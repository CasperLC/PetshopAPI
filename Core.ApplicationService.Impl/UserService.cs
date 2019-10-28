using System.Collections.Generic;
using Core.DomainService;
using Core.Entities;

namespace Core.ApplicationService.Impl
{
    public class UserService: IUserService
    {
        private IUserRepository userRepo;

        public UserService(IUserRepository userRepository)
        {
            userRepo = userRepository;
        }

        public List<User> GetUsers()
        {
            return userRepo.GetUsers();
        }

        public User ReadUser(int id)
        {
            return userRepo.ReadUser(id);
        }

        public User CreateUser(User user)
        {
            return userRepo.CreateUser(user);
        }

        public User UpdateUser(User user)
        {
            return userRepo.UpdateUser(user);
        }

        public User DeleteUser(int id)
        {
            return userRepo.DeleteUser(id);
        }
    }
}