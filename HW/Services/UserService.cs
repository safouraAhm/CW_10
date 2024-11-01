using Colors.Net;
using Colors.Net.StringColorExtensions;
using HW.DataBase;
using HW.Entities;
using HW.InterFaces;
using HW.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace HW.Services;

public class UserService : IUserService
{
    static readonly IUserRepository _userRepo;

    static UserService()
    {
        _userRepo = new UserDapperRepository();

    }
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
    public bool ChangePassword(string oldPass, string newPass)
    {
        if (Ram.CurrentUser == null)
        {
            throw new Exception("Please First Login");
        }

        else
        {
            string hashedOldPass = HashPassword(oldPass);
            string hasheNewPass = HashPassword(newPass);
            if (Ram.CurrentUser.ChangePassword(hashedOldPass, hasheNewPass))
            {
                _userRepo.Update(Ram.CurrentUser);
                return true;
            }
            return false;
        }
        
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
            string hashedPassword = HashPassword(password);
            if (_userRepo.Get(username).Password == hashedPassword)
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
            string hashedPassword = HashPassword(password);
            _userRepo.Add(new User() { Id = GenerateId(), UserName = username, Password = hashedPassword });
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
