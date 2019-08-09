using System.Windows.Forms;

namespace TotalSmartCoding.Libraries.StackedHeaders
{
    public interface IStackedHeaderGenerator
    {
        Header GenerateStackedHeader(DataGridView objGridView);
    }
}
