using System;
using DracoGame.Shared;
using DracoGame.Shared.Helpers;

namespace DracoGame.Desktop
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var config = new PlatformConfig
            {
                IsFullScreen = false
            };

            using (var game = new GameMain(config))
                game.Run();
        }
    }
}
