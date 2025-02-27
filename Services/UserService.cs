using st_1;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
  
    
        public class UserService:IUserService
        {
            private readonly IUserRepository _userRepository;

            public UserService(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public  List<User> GetAllUsers()
            {
            return _userRepository.GetAll();
            }

            public User GetUserById(Guid id)
            {
                return _userRepository.GetById(id);
            }

            public void CreateUser(User user)
            {
                _userRepository.Add(user);
            }

            public void UpdateUser(User user)
            {
                 _userRepository.Update(user);
            }

            public void DeleteUser(Guid id)
            {
                 _userRepository.Delete(id);
            }
        }
    }

