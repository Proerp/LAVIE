using System.Collections.Generic;

using TotalModel.Models;

namespace TotalCore.Repositories.Commons
{
    public interface ILavieRepository : IGenericRepository<Lavie>
    {
    }

    public interface ILavieAPIRepository : IGenericAPIRepository
    {
        void LavieUpdate(int lavieID);
        void LavieDoEmpty();
    }
}
