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
    [Activity(Label = "AradActivation", Theme = "@style/MyTheme", MainLauncher = true)]
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

        private void HandleEvents()
        {
            cusLayout.Click += CusLayout_Click;
            prdLayout.Click += PrdLayout_Click;
        }

        private void PrdLayout_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ShowProductActivity));
        }

        private void SetDrawer()
        {
            _list = new List<string>();
            _list.Add($"کاربر جاری: Admin");
            _list.Add($"ساعت ورود: {DateTime.Now.ToShortTimeString()}");
            _list.Add($"{Calendar.GetFullCalendar()}");
            _list.Add($"نسخه: 1.0.0.1");
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