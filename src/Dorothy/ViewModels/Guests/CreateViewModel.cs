using System.ComponentModel.DataAnnotations;


namespace Dorothy.ViewModels.Guests
{
    public class CreateViewModel
    {

        [Required]
        public string Names { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public bool IsOptional { get; set; }
        public bool HasInvitation { get; set; }
        public string Notes { get; set; }
    }
}
