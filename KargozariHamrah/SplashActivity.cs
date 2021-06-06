using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;
using KargozariHamrah.Utils;
using Services;
using System.Timers;
using Exception = System.Exception;

namespace KargozariHamrah
{
    [Activity(Label = "Arad", Theme = "@style/MyTheme", MainLauncher = true, Icon = "@drawable/Arad_Icon", NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        private Timer timer;
        private TextView lblImie;
        private TextView lblImieDesc;
        private TextView lblImieError;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Splash);

            timer = new Timer { Interval = 7000 };
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
            lblImie = FindViewById<TextView>(Resource.Id.lblImei);
            lblImieDesc = FindViewById<TextView>(Resource.Id.lblImeiDesc);
            lblImieError = FindViewById<TextView>(Resource.Id.lblImeiError);

            lblImie.Typeface = Typeface.CreateFromAsset(Assets, "B Yekan.TTF");
            lblImieDesc.Typeface = Typeface.CreateFromAsset(Assets, "B Yekan.TTF");
            lblImieError.Typeface = Typeface.CreateFromAsset(Assets, "B Yekan.TTF");
            lblImieError.Visibility = ViewStates.Invisible;

            lblImie.Text = CurrentUser.Imei;
            SetCustomer(CurrentUser.Imei);
        }
        void workerThread()
        {
            RunOnUiThread(() =>
            {
                Toast.MakeText(this,
                    "متاسفانه سرور قادر به پاسخوگیی به شما نمی باشد. لطفا پس از اطمینان از اینترنت دستگاه خود، با تیم پشتیبانی تماس بگیرید",
                    ToastLength.Long).Show();
                lblImieError.Visibility = ViewStates.Visible;
            });
        }
        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            if (CurrentUser.Customer == null)
                workerThread();
            else
                StartActivity(typeof(MainActivity));
        }
        private void SetCustomer(string imei)
        {
            try
            {
                CurrentUser.Customer = CustomerBussines.GetByImei(imei);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                CurrentUser.Customer = null;
            }
        }
    }
}