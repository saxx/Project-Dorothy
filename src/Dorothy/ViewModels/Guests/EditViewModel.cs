using System.Threading.Tasks;
using Dorothy.Models;


namespace Dorothy.ViewModels.Guests
{
    public class EditViewModel : CreateViewModel
    {
        public async Task<EditViewModel> Fill(Db db, Guest guest)
        {
            await base.Fill(db);

            Names = guest.Names;
            AdultCount = guest.AdultCount;
            ChildCount = guest.ChildCount;
            IsOptional = guest.IsOptional;
            HasInvitation = guest.HasInvitation;
            Notes = guest.Notes;
            Group = guest.Group;

            return this;
        }
    }
}
