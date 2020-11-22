using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace AradActivation
{
    [Activity(Label = "AradActivation", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextView lblCustomers;
        private TextView lblProducts;
        private TextView lblOrders;
        private TextView lblReception;
        private TextView lblPardakht;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            lblCustomers = FindViewById<TextView>(Resource.Id.lblCustomer);
            lblProducts = FindViewById<TextView>(Resource.Id.lblProduct);
            lblOrders = FindViewById<TextView>(Resource.Id.lblOrder);
            lblReception = FindViewById<TextView>(Resource.Id.lblReception);
            lblPardakht = FindViewById<TextView>(Resource.Id.lblPardakht);
            lblPardakht.Click += LblPardakht_Click;
        }

        private void LblPardakht_Click(object sender, System.EventArgs e)
        {
            Toast.MakeText(this, "پرداخت", ToastLength.Short).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}