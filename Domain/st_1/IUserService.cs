using st_1;

namespace Services
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User GetUserById(Guid id);
        public void CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(Guid id);
    }
}