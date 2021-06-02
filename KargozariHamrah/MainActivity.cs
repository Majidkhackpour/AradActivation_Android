using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using KargozariHamrah.Drawers;
using Services;
using System;
using System.Collections.Generic;

namespace KargozariHamrah
{
    [Activity(Label = "Arad", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private ManageDrawer manager;
        private DrawerLayout myDrawer;
        private ListView myListView;
        private bool doubleBackToExitPressedOnce = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            myListView = FindViewById<ListView>(Resource.Id.MyListView);
            myDrawer = FindViewById<DrawerLayout>(Resource.Id.myDrawer);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myToolbar);

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
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void SetDrawer()
        {
            var font = Typeface.CreateFromAsset(Assets, "B Yekan.TTF");
            var _list = new List<string>
            {
                $"کاربر جاری: ",
                $"سطح دسترسی: ",
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
    }
}