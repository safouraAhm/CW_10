using Colors.Net;
using Colors.Net.StringColorExtensions;
using HW.DataBase;
using HW.Entities;
using HW.InterFaces;
using HW.Repositories;

namespace HW.Services;

public class UserService : IUserService
{
    static readonly IUserRepository _userRepo;

    static UserService()
    {
        _userRepo = new UserFileRepository();

    }

    public bool ChangePassword(string oldPass, string newPass)
    {
        if (Ram.CurrentUser == null)
        {
            throw new Exception("Please First Login");
        }

        if (Ram.CurrentUser.ChangePassword(oldPass, newPass))
        {
            _userRepo.Update(Ram.CurrentUser);
            return true;
        }
        return false;
    }
    public bool ChangeStatus(string status)
    {
        if (Ram.CurrentUser == null)
        {
            throw new Exception("Please First Login");
        }
        if (Ram.CurrentUser.ChangeStatus(status))
        {
            _userRepo.Update(Ram.CurrentUser);
            return true;
        }
        return false;
    }
    public int GenerateId()
    {
        return _userRepo.GetAll().Count + 1;
    }
    public bool Login(string username, string password)
    {

        if (Ram.CurrentUser != null)
        {
            throw new Exception("Please Logout First");
        }
        try
        {
            if (_userRepo.Get(username).Password == password)
            {
                Ram.CurrentUser = _userRepo.Get(username);
                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public void LogOut()
    {
        Ram.CurrentUser = null;
    }
    public bool Regester(string username, string password)
    {
        if (Ram.CurrentUser != null)
        {
            throw new Exception("Please Logout First");
        }
        try
        {
            _userRepo.Get(username);
            return false;
        }
        catch (Exception)
        {
            _userRepo.Add(new User() { Id = GenerateId(), UserName = username, Password = password });
            return true;
        }
    }
    public void Search(string username)
    {
        if (Ram.CurrentUser == null)
            throw new Exception("Please First Login");

        var users = _userRepo.Search(username);
        foreach (var user in users)
        {
            ColoredConsole.WriteLine(user.ToString().DarkYellow());
        }
    }
}
