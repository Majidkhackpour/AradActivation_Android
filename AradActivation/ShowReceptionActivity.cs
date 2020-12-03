using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;
using Services;

namespace AradActivation
{
    [Activity(Label = "نمایش لیست وجوه دریافتی", Theme = "@style/MyTheme")]
    public class ShowReceptionActivity : AppCompatActivity
    {
        private ListView lstReception;
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private List<ReceptionBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShowReception);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myReceptionToolbar);
            SetSupportActionBar(myToolbar);
            lstReception = FindViewById<ListView>(Resource.Id.ReceptionListView);
            BindList();
        }
        private void BindList()
        {
            try
            {
                list = ReceptionBussines.GetAll();
                if (CurrentUser.User.Type != EnUserType.Manager)
                    list = list.Where(q => q.UserGuid == CurrentUser.User.Guid).ToList();
                lstReception.Adapter = new ReceptionAdapter(this, list.OrderByDescending(q => q.CreateDate).ToList());
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        protected override void OnResume()
        {
            BindList();
            base.OnResume();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ListViewMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            StartActivity(typeof(ReceptionMainActivity));
            return base.OnOptionsItemSelected(item);
        }
    }
}