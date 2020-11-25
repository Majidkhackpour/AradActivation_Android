using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;

namespace AradActivation
{
    [Activity(Label = "مدیریت مشتریان", Theme = "@style/MyTheme")]
    public class CustomerMainActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar myToolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CustomerMainLayout);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.mainCustToolbar);
            SetSupportActionBar(myToolbar);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.addMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //StartActivity(typeof(CustomerMainActivity));
            return base.OnOptionsItemSelected(item);
        }
    }
}