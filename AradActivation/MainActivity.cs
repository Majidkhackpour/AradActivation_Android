using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Services;

namespace AradActivation
{
    [Activity(Label = "AradActivation", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        private TextView lblCustomers;
        private TextView lblProducts;
        private TextView lblOrders;
        private TextView lblReception;
        private TextView lblPardakht;
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private ManageDrawer manager;
        private DrawerLayout myDrawer;
        private ListView myListView;
        private List<string> _list;
        private LinearLayout cusLayout;
        private LinearLayout prdLayout;
        private LinearLayout orderLayout;
        private LinearLayout receptionLayout;
        private LinearLayout pardakhtLayout;
        private bool doubleBackToExitPressedOnce = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            FindElements();
            SetFonts();
            SetSupportActionBar(myToolbar);
            SetDrawer();

            myListView.Tag = 0;
            manager = new ManageDrawer(this, myDrawer, Resource.String.OpenDrawer, Resource.String.CloseDrawer);
            myDrawer.SetDrawerListener(manager);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            manager.SyncState();
            if (savedInstanceState != null)
            {
                SupportActionBar.SetTitle(savedInstanceState.GetString("DrawerState") == "Opened"
                    ? Resource.String.OpenDrawer
                    : Resource.String.CloseDrawer);
            }
            else SupportActionBar.SetTitle(Resource.String.CloseDrawer);

            HandleEvents();
        }
        private void SetFonts()
        {
            var font = Typeface.CreateFromAsset(Assets, "B Yekan.TTF");

            lblCustomers.Typeface = font;
            lblOrders.Typeface = font;
            lblProducts.Typeface = font;
            lblReception.Typeface = font;
            lblPardakht.Typeface = font;
        }
        public override void OnBackPressed()
        {
            if (doubleBackToExitPressedOnce)
            {
                base.OnBackPressed();
                Java.Lang.JavaSystem.Exit(0);
                return;
            }


            doubleBackToExitPressedOnce = true;
            Toast.MakeText(this, "برای خروج، مجددا کلید بازگشت را بفشارید", ToastLength.Short).Show();

            new Handler().PostDelayed(() =>
            {
                doubleBackToExitPressedOnce = false;
            }, 2000);
        }
        private void HandleEvents()
        {
            cusLayout.Click += CusLayout_Click;
            prdLayout.Click += PrdLayout_Click;
            orderLayout.Click += OrderLayout_Click;
            receptionLayout.Click += ReceptionLayout_Click;
            pardakhtLayout.Click += PardakhtLayout_Click;
            myListView.ItemClick += MyListView_ItemClick;
        }

        private void MyListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (CurrentUser.User.Type != EnUserType.Manager)
            {
                Toast.MakeText(this, "شما مجوز دسترسی به این بخش را ندارید", ToastLength.Short).Show();
                return;
            }
            var name = _list[e.Position];
            switch (name)
            {
                case "کد فعالسازی": break;
                case "مدیریت کاربران": break;
                case "وضعیت مانده حساب مشتریان": StartActivity(typeof(CustomerAccountActivity)); break;
                case "مسدودسازی مشتری": break;
                case "لاگ عملکرد مشتری": break;
                case "لاگ عملکرد کاربر": break;
                case "مسدودسازی کاربر": StartActivity(typeof(UserBlockActivity)); break;
            }
        }

        private void PardakhtLayout_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ShowPardakhtActivity));
        }

        private void ReceptionLayout_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ShowReceptionActivity));
        }

        private void OrderLayout_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ShowOrdersActivity));
        }

        private void PrdLayout_Click(object sender, EventArgs e)
        {
            if (CurrentUser.User.Type != EnUserType.Manager)
            {
                Toast.MakeText(this, "شما مجوز دسترسی به این بخش را ندارید", ToastLength.Short).Show();
                return;
            }
            StartActivity(typeof(ShowProductActivity));
        }

        private void SetDrawer()
        {
            var font = Typeface.CreateFromAsset(Assets, "B Yekan.TTF");
            _list = new List<string>
            {
                $"کاربر جاری: {CurrentUser.User.Name}",
                $"سطح دسترسی: {CurrentUser.User.TypeName}",
                $"ساعت ورود: {DateTime.Now.ToShortTimeString()}",
                $"{Calendar.GetFullCalendar()}",
                $"نسخه: {Application.Context.ApplicationContext.PackageManager.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName}",
                "",
                "کد فعالسازی",
                "مدیریت کاربران",
                "وضعیت مانده حساب مشتریان",
                "لاگ عملکرد مشتری",
                "لاگ عملکرد کاربر",
                "مسدودسازی کاربر",
                "مسدودسازی مشتری"
            };
            myListView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, _list);
        }
        private void CusLayout_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ShowCustomerActivity));
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    myDrawer.CloseDrawer(myListView);
                    manager.OnOptionsItemSelected(item);
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("DrawerState", myDrawer.IsDrawerOpen((int)GravityFlags.Left) ? "Opened" : "Closed");
            base.OnSaveInstanceState(outState);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            manager.SyncState();
            base.OnPostCreate(savedInstanceState);
        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            manager.OnConfigurationChanged(newConfig);
            base.OnConfigurationChanged(newConfig);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void FindElements()
        {
            lblCustomers = FindViewById<TextView>(Resource.Id.lblCustomer);
            lblProducts = FindViewById<TextView>(Resource.Id.lblProduct);
            lblOrders = FindViewById<TextView>(Resource.Id.lblOrder);
            lblReception = FindViewById<TextView>(Resource.Id.lblReception);
            lblPardakht = FindViewById<TextView>(Resource.Id.lblPardakht);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myToolbar);
            myListView = FindViewById<ListView>(Resource.Id.MyListView);
            cusLayout = FindViewById<LinearLayout>(Resource.Id.cusLayout);
            prdLayout = FindViewById<LinearLayout>(Resource.Id.productLayout);
            orderLayout = FindViewById<LinearLayout>(Resource.Id.orderLayout);
            receptionLayout = FindViewById<LinearLayout>(Resource.Id.daryaftLayout);
            pardakhtLayout = FindViewById<LinearLayout>(Resource.Id.pardakhtLayout);
            myDrawer = FindViewById<DrawerLayout>(Resource.Id.myDrawer);
        }
    }
}