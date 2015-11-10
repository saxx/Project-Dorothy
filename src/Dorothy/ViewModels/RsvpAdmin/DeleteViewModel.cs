

namespace Dorothy.ViewModels.RsvpAdmin
{
    public class DeleteViewModel
    {
        public DeleteViewModel Fill(Models.Rsvp rsvp)
        {
            Id = rsvp.Id;
            Name = rsvp.Name;

            return this;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
