using System.Threading.Tasks;
using AutoMapper;
using Dorothy.Models;


namespace Dorothy.ViewModels.Guests
{
    public class EditViewModel : CreateViewModel
    {
        public async Task<EditViewModel> Fill(IMapper mapper, Db db, Guest guest)
        {
            await base.Fill(db);

            mapper.Map(guest, this);

            return this;
        }
    }
}
