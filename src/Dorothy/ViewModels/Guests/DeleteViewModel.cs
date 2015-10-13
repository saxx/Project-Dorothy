using System.ComponentModel.DataAnnotations;
using Dorothy.Models;


namespace Dorothy.ViewModels.Guests
{
    public class DeleteViewModel
    {

        public DeleteViewModel Fill(Guest guest)
        {
            Names = guest.Names;
            return this;
        }

        public string Names { get; set; }
    }
}
