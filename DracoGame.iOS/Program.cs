using System;
using DracoGame.Shared;
using DracoGame.Shared.Helpers;
using Foundation;
using UIKit;
using WatchKit;

namespace DracoGame.iOS
{
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static GameMain game;

        internal static void RunGame()
        {
            var config = new PlatformConfig
            {
                IsFullScreen = true,
                ScreenWidth = (int) WKInterfaceDevice.CurrentDevice.ScreenBounds.Size.Width,
                ScreenHeight = (int) WKInterfaceDevice.CurrentDevice.ScreenBounds.Size.Height
            };
            game = new GameMain(config);
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
    }
}
