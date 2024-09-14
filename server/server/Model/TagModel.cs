using System.ComponentModel.DataAnnotations;

namespace server.Model;

public class TagModel
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Label { get; set; }
    
    [Required]
    public DateTime Created { get; set; }
    
    public DateTime? Updated { get; set; }
    
    public DateTime? Deleted { get; set; }
    
    public ICollection<TagOnPhotographyModel> TagOnPhotographies { get; set; }
}