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
    [Activity(Label = "نمایش لیست وجوه پرداختی", Theme = "@style/MyTheme")]
    public class ShowPardakhtActivity : AppCompatActivity
    {
        private ListView lstPardakht;
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private List<PardakhtBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShowPardakht);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myPardakhtToolbar);
            SetSupportActionBar(myToolbar);
            lstPardakht = FindViewById<ListView>(Resource.Id.PardakhtListView);
            BindList();
        }
        private void BindList()
        {
            try
            {
                list = PardakhtBussines.GetAll();
                if (CurrentUser.User.Type != EnUserType.Manager)
                    list = list.Where(q => q.UserGuid == CurrentUser.User.Guid).ToList();
                lstPardakht.Adapter = new PardakhtAdapter(this, list.OrderByDescending(q => q.CreateDate).ToList());
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
            StartActivity(typeof(ProductMainActivity));
            return base.OnOptionsItemSelected(item);
        }
    }
}