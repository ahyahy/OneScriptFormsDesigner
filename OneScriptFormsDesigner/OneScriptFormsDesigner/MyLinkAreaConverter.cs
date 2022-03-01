using System.ComponentModel;
using System.Windows.Forms;

namespace osfDesigner
{
    public class MyLinkAreaConverter : LinkArea.LinkAreaConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
