using HW.Entities;
using HW.InterFaces;
using Newtonsoft.Json;

namespace HW.Repositories;

public class UserFileRepository : GeneralFileRepository<User>, IUserRepository
{

    public User Get(string username)
    {
        var users = GetAll();
        var user = users.First(u => u.UserName == username);
        return user;
    }

    public List<User> Search(string username)
    {
        var users = GetAll().Where(u => u.UserName.StartsWith(username)).ToList();
        return users;
    }

    public void Update(User user)
    {
        var users = GetAll();
        var fileUser = users.First(u => u.Id == user.Id);
        fileUser.Password = user.Password;
        fileUser.Status = user.Status;
        UpdateList(users);

    }
}
