using System;
using DracoGame.Shared.Helpers;
using DracoGame.Shared.Menus;
using DracoGame.Shared.Scenes;
using Microsoft.Xna.Framework;
using Nez;

namespace DracoGame.Shared
{
    public class GameMain : Core
    {
        private readonly PlatformConfig _config;

        public GameMain(PlatformConfig config) : base()
        {
            _config = config;

            Window.AllowUserResizing = true;
            IsMouseVisible = true;

            Screen.supportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            Screen.isFullscreen = _config.IsFullScreen;
            Screen.setSize(_config.ScreenWidth, _config.ScreenHeight);
            Screen.applyChanges();

            exitOnEscapeKeypress = false;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Input.touch.enableTouchSupport();
            Scene.setDefaultDesignResolution(640, 360, Scene.SceneResolutionPolicy.None);

            scene = new MainMenu(_config);
        }
    }
}
