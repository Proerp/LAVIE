using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using TotalModel.Models;
using TotalDTO.Inventories;
using TotalCore.Repositories.Inventories;
using TotalCore.Services.Inventories;

namespace TotalService.Inventories
{
    public class GoodsIssueService : GenericWithViewDetailService<GoodsIssue, GoodsIssueDetail, GoodsIssueViewDetail, GoodsIssueDTO, GoodsIssuePrimitiveDTO, GoodsIssueDetailDTO>, IGoodsIssueService
    {
        public GoodsIssueService(IGoodsIssueRepository goodsIssueRepository)
            : base(goodsIssueRepository, "GoodsIssuePostSaveValidate", "GoodsIssueSaveRelative", "GoodsIssueToggleApproved", null, null, "GetGoodsIssueViewDetails")
        {
        }

        public override ICollection<GoodsIssueViewDetail> GetViewDetails(int goodsIssueID)
        {
            ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("GoodsIssueID", goodsIssueID) };
            return this.GetViewDetails(parameters);
        }
    }
}
