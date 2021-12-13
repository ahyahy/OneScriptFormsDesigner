namespace osfDesigner
{
    public interface IDesignSurfaceExt2 : IDesignSurfaceExt
    {
        // Получаем IDesignerHost из .NET DesignSurface
        ToolboxServiceImp GetIToolboxService();
        void EnableDragandDrop();
    }
}
