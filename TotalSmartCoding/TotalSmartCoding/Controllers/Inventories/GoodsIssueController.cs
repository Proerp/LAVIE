using TotalModel.Models;
using TotalDTO.Inventories;

using TotalCore.Services.Inventories;
using TotalSmartCoding.ViewModels.Inventories;

namespace TotalSmartCoding.Controllers.Inventories
{
    public class GoodsIssueController : GenericViewDetailController<GoodsIssue, GoodsIssueDetail, GoodsIssueViewDetail, GoodsIssueDTO, GoodsIssuePrimitiveDTO, GoodsIssueDetailDTO, GoodsIssueViewModel>
    {
        public GoodsIssueController(IGoodsIssueService goodsIssueService, GoodsIssueViewModel goodsIssueViewModel)
            : base(goodsIssueService, goodsIssueViewModel)
        {
        }
    }
}
