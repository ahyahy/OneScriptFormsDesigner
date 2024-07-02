namespace osfDesigner
{
    [System.Flags]
    public enum NotifyFilters
    {
        Атрибуты = 4,
        Безопасность = 256,
        ВремяСоздания = 64,
        ИмяКаталога = 2,
        ИмяФайла = 1,
        ПоследнийДоступ = 32,
        ПоследняяЗапись = 16,
        Размер = 8
    }
}
