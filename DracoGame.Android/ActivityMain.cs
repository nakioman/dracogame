using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using DracoGame.Shared;
using DracoGame.Shared.Helpers;

namespace DracoGame.Android
{
    [Activity(Label = "DracoGame.Android"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class ActivityMain : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var config = new PlatformConfig
            {
                IsFullScreen = true,
                ScreenHeight = Resources.DisplayMetrics.HeightPixels,
                ScreenWidth = Resources.DisplayMetrics.WidthPixels
            };
            var g = new GameMain(config);
            SetContentView((View)g.Services.GetService(typeof(View)));
            g.Run();
        }
    }
}

