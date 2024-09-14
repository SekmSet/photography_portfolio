using System.ComponentModel.DataAnnotations;

namespace server.Model;

public class UserModel
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string EmailAddress { get; set; }
    
    [Required]
    public bool Actif { get; set; }
    
    [Required]
    public DateTime Created { get; set; }
    
    public DateTime? Updated { get; set; }
    
    public DateTime? Deleted { get; set; }
    
    public ICollection<UserPolicyModel> UserPolicies { get; set; }
}