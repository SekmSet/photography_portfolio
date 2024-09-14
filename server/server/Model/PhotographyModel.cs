using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Model;

public class PhotographyModel
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Path { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    public UserModel User { get; set; }
    
    [Required]
    public DateTime Created { get; set; }
    
    public DateTime? Updated { get; set; }
    
    public DateTime? Deleted { get; set; }
    
    public ICollection<TagOnPhotographyModel> TagOnPhotographies { get; set; }
}