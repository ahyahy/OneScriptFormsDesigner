using System.ComponentModel;
using System.Windows.Forms; 

namespace osfDesigner
{
    public class MySelectionRangeConverter : SelectionRangeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
