
namespace EMP.Services.Auth.Interfaces
{
    public interface IHashService
    {
        string Generate(string password);
        bool Verify(string password, string hashPassword);
        //string ConvertBytesToHexString(byte[] bytes);
        //byte[] ComputeSha256Hash(string input);
    }
}
