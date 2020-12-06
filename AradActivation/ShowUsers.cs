using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AradActivation.Utilities;
using DepartmentDal.Classes;
using Services;

namespace AradActivation
{
    [Activity(Label = "نمایش لیست کاربران", Theme = "@style/MyTheme")]
    public class ShowUsers : AppCompatActivity
    {
        private ListView lstCustomers;
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private List<UserBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShowUsersLayout);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.UsersToolbar);
            SetSupportActionBar(myToolbar);
            lstCustomers = FindViewById<ListView>(Resource.Id.UsersShowListView);

            BindList();

            lstCustomers.ItemClick += LstCustomers_ItemClick;
        }
        private void LstCustomers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Position < 0 && list.Count <= 0) return;
            var cus = lstCustomers.GetItemAtPosition(e.Position).Cast<UserBussines>();
            var intent = new Intent(this, typeof(UserMainActivity));
            intent.PutExtra("UserGuid", cus.Guid.ToString());
            StartActivity(intent);
        }
        private void BindList()
        {
            try
            {
                list = UserBussines.GetAll();
                lstCustomers.Adapter = new ShowUsersAdapter(this, list.OrderBy(q => q.Name).ToList());
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
            StartActivity(typeof(UserMainActivity));
            return base.OnOptionsItemSelected(item);
        }
    }
}