
namespace EMP.Common.Configuration
{
    public class EmployeeAPIConfig
    {
        public string ConnectionString { get; set; } = string.Empty;
        public int TokenExpires {  get; set; }
        public string? SecretKey { get; set; }= string.Empty;
        public float BonusRate { get; set; }
    }
}
