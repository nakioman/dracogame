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

        public GameMain(PlatformConfig config)
        {
            _config = config;

            Window.AllowUserResizing = true;
            IsMouseVisible = true;

            Screen.supportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            Screen.isFullscreen = _config.IsFullScreen;
            Screen.applyChanges();

            exitOnEscapeKeypress = false;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Scene.setDefaultDesignResolution(1280, 720, Scene.SceneResolutionPolicy.ShowAllPixelPerfect);
            scene = new MainMenu(_config);
        }
    }
}
