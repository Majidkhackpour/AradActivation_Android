using Android.App;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using KargozariHamrah.Adapters;
using Services;
using Services.AndroidViewModels;
using System;
using System.Collections.Generic;
using WebHesabBussines;

namespace KargozariHamrah
{
    [Activity(Label = "Arad", Theme = "@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private DrawerLayout myDrawer;
        private bool doubleBackToExitPressedOnce = false;
        private RecyclerView lstBuildings;
        private List<BuildingListViewModel> list;
        RecyclerView.LayoutManager mLayoutManager;
        private SwipeRefreshLayout refreshLayout;
        private ActionBarDrawerToggle _toggle;
        private Button btnRahn;
        private Button btnForoush;
        private Button btnPishForoush;
        private Button btnMoaveze;
        private Button btnMosharekat;
        private EnRequestType _type = EnRequestType.None;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            myDrawer = FindViewById<DrawerLayout>(Resource.Id.myDrawer);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myToolbar);
            _toggle = new ActionBarDrawerToggle(this, myDrawer, myToolbar, 0, 1);
            lstBuildings = FindViewById<RecyclerView>(Resource.Id.BuildingMainListView);
            refreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            refreshLayout.SetColorSchemeColors(Color.Red, Color.Green, Color.Blue, Color.Yellow);

            btnRahn = FindViewById<Button>(Resource.Id.btnRahn);
            btnForoush = FindViewById<Button>(Resource.Id.btnForoush);
            btnMoaveze = FindViewById<Button>(Resource.Id.btnMoaveze);
            btnMosharekat = FindViewById<Button>(Resource.Id.btnMosharekat);
            btnPishForoush = FindViewById<Button>(Resource.Id.btnPishForoush);

            SetSupportActionBar(myToolbar);
            SetUi();

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            if (savedInstanceState != null)
            {
                SupportActionBar.SetTitle(savedInstanceState.GetString("DrawerState") == "Opened"
                    ? Resource.String.OpenDrawer
                    : Resource.String.CloseDrawer);
            }
            else SupportActionBar.SetTitle(Resource.String.CloseDrawer);
            BindList();

            refreshLayout.Refresh += SwipeRefreshLayoutMain_Refresh;
            btnRahn.Click += BtnRahnOnClick;
            btnForoush.Click += BtnForoushOnClick;
            btnMoaveze.Click += BtnMoavezeOnClick;
            btnMosharekat.Click += BtnMosharekatOnClick;
            btnPishForoush.Click += BtnPishForoushOnClick;

            btnRahn.PerformClick();
        }

        private void BtnPishForoushOnClick(object sender, EventArgs e)
        {
            if (_type == EnRequestType.PishForush) return;
            _type = EnRequestType.PishForush;
            RefreshButtons();
            btnPishForoush.SetTextColor(Color.White);
            btnPishForoush.SetBackgroundColor(new Color(GetColor(Resource.Color.Blue)));
        }
        private void BtnMosharekatOnClick(object sender, EventArgs e)
        {
            if (_type == EnRequestType.Mosharekat) return;
            _type = EnRequestType.Mosharekat;
            RefreshButtons();
            btnMosharekat.SetTextColor(Color.White);
            btnMosharekat.SetBackgroundColor(new Color(GetColor(Resource.Color.Blue)));
        }
        private void BtnMoavezeOnClick(object sender, EventArgs e)
        {
            if (_type == EnRequestType.Moavezeh) return;
            _type = EnRequestType.Moavezeh;
            RefreshButtons();
            btnMoaveze.SetTextColor(Color.White);
            btnMoaveze.SetBackgroundColor(new Color(GetColor(Resource.Color.Blue)));
        }
        private void BtnForoushOnClick(object sender, EventArgs e)
        {
            if (_type == EnRequestType.Forush) return;
            _type = EnRequestType.Forush;
            RefreshButtons();
            btnForoush.SetTextColor(Color.White);
            btnForoush.SetBackgroundColor(new Color(GetColor(Resource.Color.Blue)));
        }
        private void BtnRahnOnClick(object sender, EventArgs e)
        {
            if (_type == EnRequestType.Rahn) return;
            _type = EnRequestType.Rahn;
            RefreshButtons();
            btnRahn.SetTextColor(Color.White);
            btnRahn.SetBackgroundColor(new Color(GetColor(Resource.Color.Blue)));
        }
        private void RefreshButtons()
        {
            try
            {
                btnRahn.SetTextColor(Color.Black);
                btnForoush.SetTextColor(Color.Black);
                btnMoaveze.SetTextColor(Color.Black);
                btnMosharekat.SetTextColor(Color.Black);
                btnPishForoush.SetTextColor(Color.Black);

                btnRahn.Background = GetDrawable(Resource.Drawable.Button_Border);
                btnForoush.Background = GetDrawable(Resource.Drawable.Button_Border);
                btnMoaveze.Background = GetDrawable(Resource.Drawable.Button_Border);
                btnMosharekat.Background = GetDrawable(Resource.Drawable.Button_Border);
                btnPishForoush.Background = GetDrawable(Resource.Drawable.Button_Border);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        private void BindList()
        {
            try
            {
                list = WebBuilding.GetList(WebCustomer.Customer?.HardSerial ?? "");
                mLayoutManager = new LinearLayoutManager(this);
                lstBuildings.SetLayoutManager(mLayoutManager);
                var mAdapter = new ShowBuildingListAdapter(list, this);
                lstBuildings.SetAdapter(mAdapter);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void SetUi()
        {
            var font = Typeface.CreateFromAsset(Assets, "B Yekan.TTF");
            btnRahn.Typeface = font;
            btnForoush.Typeface = font;
            btnMosharekat.Typeface = font;
            btnMoaveze.Typeface = font;
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
            if (_toggle.OnOptionsItemSelected(item)) return true;
            return base.OnOptionsItemSelected(item);
        }
        private void SwipeRefreshLayoutMain_Refresh(object sender, System.EventArgs e)
        {
            BindList();
            refreshLayout.Refreshing = false;
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString("DrawerState", myDrawer.IsDrawerOpen((int)GravityFlags.Left) ? "Opened" : "Closed");
            base.OnSaveInstanceState(outState);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            _toggle.SyncState();
            base.OnPostCreate(savedInstanceState);
        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            _toggle.OnConfigurationChanged(newConfig);
            base.OnConfigurationChanged(newConfig);
        }
    }
}