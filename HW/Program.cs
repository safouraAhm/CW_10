using Colors.Net;
using Colors.Net.StringColorExtensions;
using HW.Entities;
using HW.InterFaces;
using HW.Services;
using Microsoft.Win32;

IUserService userService = new UserService();
MainMenu();
void MainMenu()
{
    Console.Clear();
    ColoredConsole.WriteLine("##########################################".DarkGray());
    ColoredConsole.WriteLine("regetser --username [username] --password [password]".DarkGray());
    ColoredConsole.WriteLine("login --username [username] --password [password]".DarkGray());
    ColoredConsole.WriteLine("changepassword --old [oldpassword] --new [newpassword]".DarkGray());
    ColoredConsole.WriteLine("change --status [available]".DarkGray());
    ColoredConsole.WriteLine("regetser --username [username] --password [password]".DarkGray());
    ColoredConsole.WriteLine("change --status [not available]".DarkGray());
    ColoredConsole.WriteLine("search --username [username]".DarkGray());
    ColoredConsole.WriteLine("logout".DarkGray());
    ColoredConsole.WriteLine("##########################################".DarkGray());

    ColoredConsole.Write("Command: ".DarkBlue());
    var input = Console.ReadLine()?.ToLower().Split(" ");
    switch (input[0])
    {
        case "regester":
            RegesterFunc(input);
            break;
        case "login":
            LoginFunc(input);
            break;
        case "changepassword":
            ChangePass(input);
            break;
        case "change":
            ChangeStatusFunc(input);
            break;
        case "search":
            SearchFunc(input);
            break;
        case "logout":
            userService.LogOut();
            ColoredConsole.WriteLine("You Are Logout of the System!".Red());
            Console.ReadKey();
            MainMenu();
            break;
        default:
            ColoredConsole.WriteLine("Inavlid Command".DarkRed());
            Console.ReadKey();
            MainMenu();
            break;
    }

}
void RegesterFunc(string[] input)
{
    // Register --username sara --password 12@34
    try
    {
        if (input.Length == 5 && input[1] == "--username" && input[3] == "--password")
        {
            var username = input[2];
            var password = input[4];
            if (userService.Regester(username, password))
            {
                ColoredConsole.WriteLine("Regesteration was Successfully.".DarkGreen());
                Console.ReadKey();
                MainMenu();
            }
            else
            {
                ColoredConsole.WriteLine("Register failed! Username Already Exists.".DarkRed());
                Console.ReadKey();
                MainMenu();
            }
        }
        else
        {
            ColoredConsole.WriteLine("Invalid Command.".DarkRed());
            Console.ReadKey();
            MainMenu();
        }
    }
    catch (Exception ex)
    {
        ColoredConsole.WriteLine(ex.Message.DarkRed());
        Console.ReadKey();
        MainMenu();
    }
}
void LoginFunc(string[] input)
{
    // Login --username sara --password 12@34
    try
    {
        if (input.Length == 5 && input[1] == "--username" && input[3] == "--password")
        {
            var username = input[2];
            var password = input[4];
            if (userService.Login(username, password))
            {
                ColoredConsole.WriteLine("Login was Successfully".DarkGreen());
                Console.ReadKey();
                MainMenu();
            }
            else
            {
                ColoredConsole.WriteLine("Login Failed! Username or Password was Wrong".DarkRed());
                Console.ReadKey();
                MainMenu();
            }
        }
        else
        {
            ColoredConsole.WriteLine("Invalid Command.".DarkRed());
            Console.ReadKey();
            MainMenu();
        }
    }
    catch (Exception ex)
    {
        ColoredConsole.WriteLine(ex.Message.DarkRed());
        Console.ReadKey();
        MainMenu();
    }
}
void ChangePass(string[] input)
{
    //ChangePassword --old [myOldPassword] --new [myNewPassword]
    if (input.Length == 5 && input[1] == "--old" && input[3] == "--new")
    {
        try
        {
            var oldPass = input[2];
            var newPass = input[4];
            if (userService.ChangePassword(oldPass, newPass))
            {
                ColoredConsole.WriteLine("New Password Changed Successfully.".DarkGreen());
                Console.ReadKey();
                MainMenu();
            }

            else
            {
                ColoredConsole.WriteLine("Changing Failed! Old Password was Wrong".Red());
                Console.ReadKey();
                MainMenu();
            }
        }
        catch (Exception ex)
        {
            ColoredConsole.WriteLine(ex.Message.DarkRed());
            Console.ReadKey();
            MainMenu();
        }
    }
    else
    {
        ColoredConsole.WriteLine("Invalid Command.".DarkRed());
        Console.ReadKey();
        MainMenu();
    }
}
void ChangeStatusFunc(string[] input)
{
    try
    {
        // Change --status [available]
        if (input.Length == 3 && input[1] == "--status" && input[2] == "available")
        {
            userService.ChangeStatus("available");
            ColoredConsole.WriteLine("User Set Available".DarkGreen());
            Console.ReadKey();
            MainMenu();
        }
        //Change --status [not available]
        else if (input.Length == 4 && input[1] == "--status" && input[2] == "not" && input[3] == "available")
        {
            userService.ChangeStatus("not available");
            ColoredConsole.WriteLine("User Set not Available".DarkRed());
            Console.ReadKey();
            MainMenu();
        }
        else
        {
            ColoredConsole.WriteLine("Invalid Command.".DarkRed());
            Console.ReadKey();
            MainMenu();
        }
    }

    catch (Exception ex)
    {
        ColoredConsole.WriteLine(ex.Message.DarkRed());
        Console.ReadKey();
        MainMenu();
    }

}
void SearchFunc(string[] input)
{
    //Search --username ma
    try
    {
        if (input.Length == 3 && input[1] == "--username")
        {
            userService.Search(input[2]);
            Console.ReadKey();
            MainMenu();
        }
        else
        {
            ColoredConsole.WriteLine("Invalid Command.".DarkRed());
            Console.ReadKey();
            MainMenu();
        }
    }
    catch (Exception ex)
    {
        ColoredConsole.WriteLine(ex.Message.DarkRed());
        Console.ReadKey();
        MainMenu();
    }

}