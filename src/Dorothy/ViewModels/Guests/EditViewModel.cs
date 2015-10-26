using System.Threading.Tasks;
using Dorothy.Models;


namespace Dorothy.ViewModels.Guests
{
    public class EditViewModel : CreateViewModel
    {
        public async Task<EditViewModel> Fill(Db db, Guest guest)
        {
            await base.Fill(db);

            AutoMapper.Mapper.Map(guest, this);

            return this;
        }
    }
}
