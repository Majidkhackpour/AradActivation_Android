using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;

namespace AradActivation
{
    public class ManageDrawer : ActionBarDrawerToggle
    {
        private AppCompatActivity _activity;
        private int openRes, closeRes;
        public ManageDrawer(AppCompatActivity activity, DrawerLayout drawerLayout, int openDrawerContentDescRes, int closeDrawerContentDescRes) : base(activity, drawerLayout, openDrawerContentDescRes, closeDrawerContentDescRes)
        {
            _activity = activity;
            openRes = openDrawerContentDescRes;
            closeRes = closeDrawerContentDescRes;
        }

        public override void OnDrawerClosed(View drawerView)
        {
            var type = (int)drawerView.Tag;
            if (type != 0) return;
            _activity.SupportActionBar.SetTitle(closeRes);
            base.OnDrawerClosed(drawerView);
        }

        public override void OnDrawerOpened(View drawerView)
        {
            var type = (int)drawerView.Tag;
            if (type != 0) return;
            _activity.SupportActionBar.SetTitle(openRes);
            base.OnDrawerOpened(drawerView);
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            var type = (int)drawerView.Tag;
            if (type != 0) return;
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}