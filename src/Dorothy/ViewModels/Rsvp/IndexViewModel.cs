using System.ComponentModel.DataAnnotations;
using Dorothy.Models;

namespace Dorothy.ViewModels.Rsvp
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "Bitte gib deinen Namen an. Ohne würde bei uns die große Verwirrung ausbrechen.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bitte die Anzahl deiner erwachsenen Begleiterinnen oder Begleiter an.")]
        public int AdultsCount { get; set; }

        [Required(ErrorMessage = "Bitte gib die Anzahl der Kinder an, die dich begleiten werden.")]
        public int ChildCount { get; set; }

        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        public RsvpType Type { get; set; }

        public bool DisplaySuccessMessage { get; set; }
        public bool DisplayOverwriteMessage { get; set; }
    }
}
