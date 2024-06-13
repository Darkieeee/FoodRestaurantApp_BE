using Microsoft.IdentityModel.Tokens;

namespace FoodRestaurantApp_BE.Models.DTOs
{
    public class SignUpDto : RequestModel
    {
        public required string Username { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public override void Validate()
        {
            ErrorMessages.AddRange(ValidateInputs());
        }

        private IEnumerable<string> ValidateInputs() 
        {
            if(Username.IsNullOrEmpty())
            {
                yield return "Tên đăng nhập không được bỏ trống";
            }
            else if(FullName.IsNullOrEmpty())
            {
                yield return "Họ tên không được bỏ trống";
            }
            else if (Email.IsNullOrEmpty())
            {
                yield return "Email không được bỏ trống";
            }
            else if (Password.IsNullOrEmpty())
            {
                yield return "Mật khẩu không được bỏ trống";
            }
        }
    }
}
