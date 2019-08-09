using TotalModel.Models;
using TotalDTO.Commons;
using TotalCore.Repositories.Commons;
using TotalCore.Services.Commons;

namespace TotalService.Commons
{
    public class LavieService : GenericService<Lavie, LavieDTO, LaviePrimitiveDTO>, ILavieService
    {
        public LavieService(ILavieRepository lavieRepository)
            : base(lavieRepository, null, "LavieSaveRelative")
        {
        }
    }
}
