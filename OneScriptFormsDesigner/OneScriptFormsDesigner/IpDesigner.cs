using System.Drawing;
using System.Windows.Forms;

namespace osfDesigner
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public enum AlignmentModeEnum : int { SnapLines = 0, Grid, GridWithoutSnapping, NoGuides };

    // Интерфейс, используется для
    //     * hosts: Toolbox;DesignSurfaces;PropertyGrid
    //     * add/remove DesignSurfaces
    //     * perform editing actions on active DesignSurface
    //
    public interface IpDesigner
    {
        // controls accessing section  -----------------------------------------------------------
        //     +-------------+-----------------------------+-----------+
        //     |toolboxItem1 | ____ ____ ____              |           |
        //     |toolboxItem2 ||____|____|____|___________  +-----------+
        //     |toolboxItem3 ||                          | |     |     |
        //     |             ||                          | |     |     |
        //     |  TOOLBOX    ||      DESIGNSURFACES      | | PROPERTY  |
        //     |             ||                          | |   GRID    |
        //     |             ||__________________________| |     |     |
        //     +-------------+-----------------------------+-----------+
        System.Windows.Forms.ListBox Toolbox { get; set; }                       // TOOLBOX
        System.Windows.Forms.TabControl TabControlHostingDesignSurfaces { get; } // DESIGNSURFACES HOST
        PropertyGridHost PropertyGridHost { get; }                               // PROPERTYGRID

        // DesignSurfaces management section -----------------------------------------------------
        DesignSurfaceExt2 ActiveDesignSurface { get; }
        // Создайте DesignSurface и rootComponent (элемент управления .NET) используя IDesignSurfaceExt.CreateRootComponent()
        // если режим выравнивания не использует СЕТКУ, то параметр размера сетки игнорируется
        // Note:
        //     общие параметры используются для определения типа элемента управления, который следует использовать в качестве корневого компонента
        //     TT запрашивается как производное от Control класса .NET

        DesignSurfaceExt2 AddDesignSurface<TT>(
            int startingFormWidth, int startingFormHeight,
            AlignmentModeEnum alignmentMode, Size gridSize,
            string formName = null
            ) where TT : Control;
        void RemoveDesignSurface(DesignSurfaceExt2 activeSurface);

        // Раздел редактирования
        void UndoOnDesignSurface();
        void RedoOnDesignSurface();
        void CutOnDesignSurface();
        void CopyOnDesignSurface();
        void PasteOnDesignSurface();
        void DeleteOnDesignSurface();
        void SwitchTabOrder();

        //* 17.12.2021 perfolenta
        bool Dirty { get; }
        //***

    }
}
