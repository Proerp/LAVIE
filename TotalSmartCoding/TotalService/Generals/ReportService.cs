using TotalModel.Models;
using TotalDTO.Generals;
using TotalCore.Repositories.Generals;
using TotalCore.Services.Generals;

namespace TotalService.Generals
{
    public class ReportService : GenericService<Report, ReportDTO, ReportPrimitiveDTO>, IReportService
    {
        public ReportService(IReportRepository reportRepository)
            : base(reportRepository)
        {
        }
    }
}
