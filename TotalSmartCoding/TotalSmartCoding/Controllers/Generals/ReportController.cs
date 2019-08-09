using TotalModel.Models;
using TotalDTO.Generals;

using TotalCore.Services.Generals;
using TotalSmartCoding.ViewModels.Generals;

namespace TotalSmartCoding.Controllers.Generals
{
    public class ReportController : GenericSimpleController<Report, ReportDTO, ReportPrimitiveDTO, ReportViewModel>
    {
        public ReportController(IReportService reportService, ReportViewModel reportViewModel)
            : base(reportService, reportViewModel)
        {
        }
    }
}
