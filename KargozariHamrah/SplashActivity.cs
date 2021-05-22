using System.Timers;
using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace KargozariHamrah
{
    [Activity(Label = "Arad", Theme = "@style/MyTheme", MainLauncher = true, Icon = "@drawable/Arad_Icon", NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        private Timer timer;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Splash);

            timer = new Timer {Interval = 3000};
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            StartActivity(typeof(MainActivity));
        }
    }
}