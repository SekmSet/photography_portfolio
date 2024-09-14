using Microsoft.AspNetCore.Identity;
using server.DTO;
using server.Model;
using server.Repository;

namespace server.Service
{
    public class UserService
    {
        private readonly DatabaseContext _context;
        private readonly UserRepository _userRepository;
        private readonly IPasswordHasher<UserModel> _passwordHasher;
        
        public UserService(
            DatabaseContext context, 
            IPasswordHasher<UserModel> passwordHasher,
            UserRepository userRepository
            )
        {
            _context = context;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
    
        public UserModel Authenticate(UserDto userLogin)
        {
            var usernameOrEmail = userLogin.UsernameOrEmail;
            var password = userLogin.Password;
            
            if (usernameOrEmail == null && password == null)
            {
                return null;
            }

            if (usernameOrEmail != null)
            {
                var currentUser = _userRepository.UserByNameOrEmail(usernameOrEmail);

                if (currentUser.Result == null)
                {
                    return null;
                }
                
                bool isPasswordValid = 
                    _passwordHasher.VerifyHashedPassword(currentUser.Result, currentUser.Result.Password, password) == PasswordVerificationResult.Success;

                if (!isPasswordValid)
                {
                    return null;
                }
            
                return currentUser.Result;
            }

            return null;
        }

        public async Task<UserModel> Sign(UserDto userDto)
        {
            var username = userDto.Username;
            var email = userDto.EmailAddress;
            var password = userDto.Password;

            if (username == null && email == null && password == null)
            {
                return null;
            }
            
            var existingUser =  _userRepository.UserByNameAndEmail(username, email);
            
            if (existingUser.Result != null)
            {
                return null; 
            }
            
            var user = new UserModel
            {
                Username = username,
                EmailAddress = email,
                Password = _passwordHasher.HashPassword(null, password), 
                Created = DateTime.Now,
                Actif = true
            };

            _context.UserModels.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public UserDto GetInformation(string username, string email)
        {
            var user = _userRepository.UserByNameAndEmail(username, email);
            return user.Result == null ? null : user.Result.ToDto();
        }
        
        public async Task<UserDto> UpdateInformation(string username, string email, UserDto user)
        {
            if (user != null)
            {

                var userExist = _userRepository.UserByNameAndEmail(username, email);
                
                if (userExist.Result != null)
                {
                    userExist.Result.Updated = DateTime.Now;

                    if (user.ToModel().EmailAddress != userExist.Result.EmailAddress)
                    {
                        userExist.Result.EmailAddress = user.EmailAddress;
                    }
                    
                    if (user.Username != userExist.Result.Username)
                    {
                        userExist.Result.Username = user.Username;
                    }
                        
                    _context.UserModels.Update(userExist.Result);
                    await _context.SaveChangesAsync();
                    return userExist.Result.ToDto();
                }
            }

            return null;
        }
        
        public void DeleteUser(string username, string email)
        {
            if (username != null && email!= null )
            {
                
                var userExist = _userRepository.UserByNameAndEmail(username, email);

                if (userExist.Result != null)
                {
                    _context.UserModels.Remove(userExist.Result);
                    _context.SaveChangesAsync();
                }
            }
            else
            {
                throw new KeyNotFoundException("User not found.");
            }
        }
    }   
}