using TotalModel.Models;
using TotalDTO.Productions;

using TotalCore.Services.Productions;
using TotalSmartCoding.ViewModels.Productions;

namespace TotalSmartCoding.Controllers.Productions
{
    public class BatchController : GenericSimpleController<Batch, BatchDTO, BatchPrimitiveDTO, BatchViewModel>
    {
        IBatchService batchService;
        public BatchController(IBatchService batchService, BatchViewModel batchViewModel)
            : base(batchService, batchViewModel)
        {
            this.batchService = batchService;
        }

        public bool AddLot(int batchID)
        {
            return this.batchService.AddLot(batchID);
        }
    }
}

