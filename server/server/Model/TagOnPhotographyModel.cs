using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Model;

public class TagOnPhotographyModel
{
    [Key]
    public int PhotographyId { get; set; }

    [Key]
    public int TagId { get; set; }
    
    [ForeignKey("PhotographyId")]
    public PhotographyModel Photography { get; set; }

    [ForeignKey("TagId")]
    public TagModel Tag { get; set; }
}