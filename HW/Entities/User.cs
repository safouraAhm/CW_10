using HW.Enums;
using System.Security.Principal;

namespace HW.Entities;

public class User
{
    public required int Id { get; init; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public StatusEnum? Status { get; set; } = StatusEnum.NotAvialable;
    public bool ChangeStatus(string status)
    {
        if (status == "available")
        {
            Status = StatusEnum.Available;
            return true;
        }
        if (status == "not available")
        {
            Status = StatusEnum.NotAvialable;
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
        string status = Status == StatusEnum.Available ? "available" : "not available";
        return $"{UserName} | status: {status}";
    }
}
