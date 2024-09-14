using server.Model;

namespace server.DTO
{
    public class UserDto
    {
        public string? Username { get; set; }
        public string? EmailAddress { get; set; }

        public string? Password { get; set; }

        public bool? Actif { get; set; }
        
        public string? UsernameOrEmail { get; set; }
    }

    public static class UserModelExtensions 
    {
        public static UserDto ToDto(this UserModel user)
        {
            return new UserDto
            {
                Username = user.Username,
                EmailAddress = user.EmailAddress,
                Actif = user.Actif
            };
        }
    
        public static UserModel ToModel(this UserDto dto)
        {
            return new UserModel
            {
                Username = dto.Username,
                EmailAddress = dto.EmailAddress,
                Actif = true
            };
        }
    }
}