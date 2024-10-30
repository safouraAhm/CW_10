using HW.Entities;

namespace HW.InterFaces;

public interface IUserRepository : IGeneralFileRepository<User>
{
    public User Get(string username);
    public void Update(User user);
    public List<User> Search(string username);
}
