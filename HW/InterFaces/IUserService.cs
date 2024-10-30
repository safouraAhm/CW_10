using HW.Entities;

namespace HW.InterFaces;
public interface IUserService
{
    int GenerateId();
    bool Login(string username, string password);
    bool Regester(string username, string password);
    public bool ChangeStatus(string status);
    public bool ChangePassword(string oldPass, string newPass);
    public void Search(string username);
    void LogOut();
}
