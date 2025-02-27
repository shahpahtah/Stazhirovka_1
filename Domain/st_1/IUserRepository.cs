using st_1;

public interface IUserRepository
{
    public List<User> GetAll();
    public User GetById(Guid id);
    public void Add(User user);
    public void Update(User user);
    public void Delete(Guid id);
}