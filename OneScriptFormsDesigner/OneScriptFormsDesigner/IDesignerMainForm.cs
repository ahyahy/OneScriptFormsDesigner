using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.ComponentModel.Design.Serialization;

namespace osfDesigner
{
    //!!!!!!!!!!!!!!!!!!!
    public interface IDesignerMainForm
    {
        void ChangeImage(bool change);
        System.Windows.Forms.Control GetmainForm();
    }
}
