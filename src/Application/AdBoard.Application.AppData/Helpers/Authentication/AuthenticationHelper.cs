using System.Security.Cryptography;
using System.Text;

namespace AdBoard.Application.AppData.Helpers.Authentication;

public static class AuthenticationHelper
{
    public static string EncryptPassword(string password)
    {
        var md5 = MD5.Create();
        var passBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = md5.ComputeHash(passBytes);
        return Convert.ToHexString(hashBytes);
    }
}