using System.Security.Principal;

namespace HW.Entities;

public class User
{
    public required int Id { get; init; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public bool Status { get; set; } = false;
    public bool ChangeStatus(string status)
    {
        if (status == "available")
        {
            Status = true;
            return true;
        }
        if (status == "not available")
        {
            Status = false;
            return true;
        }
        else
            return false;

    }
    public bool ChangePassword(string oldPass, string newPass)
    {
        
        if (oldPass == Password)
        {
            Password = newPass;
            return true;
        }
        else
        {
            return false;
        }
    }
    public override string ToString()
    {
        string status = Status == true ? "available" : "not available";
        return $"{UserName} | status: {status}";
    }
}
