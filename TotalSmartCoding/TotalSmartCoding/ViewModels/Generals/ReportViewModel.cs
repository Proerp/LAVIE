using TotalDTO.Generals;
using TotalSmartCoding.ViewModels.Helpers;

namespace TotalSmartCoding.ViewModels.Generals
{
    public class ReportViewModel : ReportDTO, ISimpleViewModel
    {
        public override bool AllowDataInput { get { return false; } }

        public override bool Printable { get { return true; } }
        public override bool PrintVisible { get { return true; } }
    }
}
