
namespace EMP.DataAccess.Models
{
    public class User
    {   
        public int Id { get; set; }
        public int? ExternalId { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public bool IsActive { get; set; }  
        public DateTime CreatedAt { get; set; }    
    }
}
