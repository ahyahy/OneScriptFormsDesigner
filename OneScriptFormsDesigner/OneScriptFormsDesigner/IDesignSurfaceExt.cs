using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.ComponentModel.Design.Serialization;

namespace osfDesigner
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public interface IDesignSurfaceExt
    {
        // выполнение Cut/Copy/Paste/Delete команд
        void DoAction( string command );

        // активация/деактивация возможности TabOrder
        void SwitchTabOrder();

        // выбор режима выравнивания элементов управления
        void UseSnapLines();
        void UseGrid ( System.Drawing.Size gridSize );
        void UseGridWithoutSnapping ( System.Drawing.Size gridSize );
        void UseNoGuides();

        // Методы используются для создания элемента управления без помощи ToolBox
        IComponent CreateRootComponent ( Type controlType, Size controlSize );
        IComponent CreateRootComponent( DesignerLoader loader, Size controlSize );
        Control CreateControl ( Type controlType, Size controlSize, Point controlLocation );

        // Получение UndoEngineExtended объекта
        UndoEngineExt GetUndoEngineExt();

        // Получение IDesignerHost
        IDesignerHost GetIDesignerHost();

        // HostControl поверхности дизайнера .NET - это просто элемент управления
        // вы можете управлять этим элементом управления так же, как и любым другим элементом управления WinForms
        // (вы можете закрепить его и добавить в другой элемент управления, чтобы просто отобразить его)
        // Получить элемент управления HostControl
        Control GetView();

        //* 17.12.2021 perfolenta
        bool Dirty { get; }
        //***

    }
}
