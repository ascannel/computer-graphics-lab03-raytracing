using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace computer_graphics_lab03
{
    public static class Program
    {
        private static void Main()
        {
            //Установка настроек окна программы
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(900, 800),
                Title = "CG Lab03",
                Flags = ContextFlags.ForwardCompatible
            };

            //Запуск окна программы
            using var window = new Window(GameWindowSettings.Default, nativeWindowSettings);
            window.Run();
        }
    }
}