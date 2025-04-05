using Android.App;
using Android.Content.PM;
using Android.OS;
using Android;
using Plugin.Fingerprint;

namespace PushInOutProyect
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
        ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity

    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // ✅ Esta línea es la que soluciona tu error
            CrossFingerprint.SetCurrentActivityResolver(() => this);
        }
    }
}
