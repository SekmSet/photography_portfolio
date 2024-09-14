using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Model;

public class UserPolicyModel
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    public UserModel User { get; set; }

    [ForeignKey("Policy")]
    public int PolicyId { get; set; }

    public PolicyModel Policy { get; set; }
}