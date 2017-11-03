using System;
using DracoGame.Shared.Helpers;
using DracoGame.Shared.Scenes;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tweens;
using Nez.UI;

namespace DracoGame.Shared.Menus
{
    public class MainMenu : Scene
    {
        private readonly PlatformConfig _config;

        //private const int ScreenSpaceRenderLayer = 999;
        private UICanvas _canvas;
        private Table _table;

        public MainMenu(PlatformConfig config)
        {
            _config = config;

            _canvas = createEntity(Constants.Entities.MainMenu).addComponent<UICanvas>();
            _canvas.isFullScreen = _config.IsFullScreen;

            SetupMainMenu();
        }

        public override void initialize()
        {
            addRenderer(new DefaultRenderer { renderTargetClearColor = Color.TransparentBlack });
        }

        private void SetupMainMenu()
        {
            _table = _canvas.stage.addElement(new Table());
            _table.setFillParent(true).center();

            var buttonStyle = new TextButtonStyle(new PrimitiveDrawable(new Color(78, 91, 98), 10f),
                new PrimitiveDrawable(new Color(244, 23, 135)),
                new PrimitiveDrawable(new Color(168, 207, 115)))
            {
                downFontColor = Color.Black
            };

            var newGameButton = _table.add(new TextButton(Content.Messages.Menus.NewGame, buttonStyle))
                .setFillX().setMinHeight(30).getElement<TextButton>();
            newGameButton.onClicked += button =>
            {
                TweenManager.stopAllTweens();
                Core.startSceneTransition(new FadeTransition(Activator.CreateInstance<TestMap>));
            };
            _table.row().setPadTop(10);

            var exitGameButton = _table.add(new TextButton(Content.Messages.Menus.ExitGame, buttonStyle))
                .setFillX().setMinHeight(30).getElement<TextButton>();
            exitGameButton.onClicked += button =>
            {
                Core.exit();
            };
        }
    }
}