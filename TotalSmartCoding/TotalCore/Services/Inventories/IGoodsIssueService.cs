using TotalModel.Models;

using TotalDTO.Inventories;

namespace TotalCore.Services.Inventories
{
    public interface IGoodsIssueService : IGenericWithViewDetailService<GoodsIssue, GoodsIssueDetail, GoodsIssueViewDetail, GoodsIssueDTO, GoodsIssuePrimitiveDTO, GoodsIssueDetailDTO>
    {
    }
}

