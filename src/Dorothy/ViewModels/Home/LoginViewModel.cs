using System.ComponentModel.DataAnnotations;

namespace Dorothy.ViewModels.Home
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bitte das Passwort angeben.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
