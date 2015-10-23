using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dorothy.ViewModels.Home
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
