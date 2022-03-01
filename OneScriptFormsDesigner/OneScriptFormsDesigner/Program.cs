using System.Drawing;
using System.Reflection;
using System;

namespace osfDesigner
{
    public class Program
    {
        public static IDesignerMainForm pDesignerMainForm1 = null;

        public void Main()
        {
            // Предотвращение появления окна с текстом - Desktop applications are required to opt in to all earlier accessibility improvements to get the later improvements......
            var type = Type.GetType("System.AppContext");
            if (type != null)
            {
                var setSwitch = type.GetMethod("SetSwitch", BindingFlags.Public | BindingFlags.Static);
                setSwitch.Invoke(null, new object[] { "Switch.UseLegacyAccessibilityFeatures", false });
            }

            // Можно получить путь до oscript.exe и таким способом, но нам ещё понадобится путь до OneScriptForms.dll,
            // поэтому воспользуемся osfDesigner.Properties.Settings, а это закомментируем пока.
            ////osfDesigner.Properties.Settings.Default["osPath"] = System.Windows.Forms.Application.ExecutablePath;

            // Создадим Форму.
            pDesignerMainForm f = new pDesignerMainForm();
            pDesignerMainForm1 = f;
            f.Size = new Size(1200, 800);
            (f.pDesignerCore as IpDesigner).AddDesignSurface<Form>(670, 600, AlignmentModeEnum.SnapLines, new Size(1, 1));

            // Запускаем дизайнер.
            f.ShowDialog();
        }

        public void MainPFL()
        {
            // Предотвращение появления окна с текстом - Desktop applications are required to opt in to all earlier accessibility improvements to get the later improvements......
            var type = Type.GetType("System.AppContext");
            if (type != null)
            {
                var setSwitch = type.GetMethod("SetSwitch", BindingFlags.Public | BindingFlags.Static);
                setSwitch.Invoke(null, new object[] { "Switch.UseLegacyAccessibilityFeatures", false });
            }

            // Можно получить путь до oscript.exe и таким способом, но нам ещё понадобится путь до OneScriptForms.dll,
            // поэтому воспользуемся osfDesigner.Properties.Settings, а это закомментируем пока.
            ////osfDesigner.Properties.Settings.Default["osPath"] = System.Windows.Forms.Application.ExecutablePath;

            // Создадим Форму.
            pDesignerMainFormPFL f = new pDesignerMainFormPFL();
            pDesignerMainForm1 = f;
            f.Size = new Size(1200, 800);
            (f.pDesignerCore as IpDesigner).AddDesignSurface<Form>(670, 600, AlignmentModeEnum.SnapLines, new Size(1, 1));

            // Запускаем дизайнер.
            f.ShowDialog();
        }
    }
}
