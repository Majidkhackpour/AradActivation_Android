using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AradActivation.Utilities;
using DepartmentDal.Classes;
using Services;

namespace AradActivation
{
    [Activity(Label = "انتخاب کاربران", Theme = "@style/MyTheme")]
    public class UserFilterActivity : Activity
    {
        private ListView lstCustomers;
        private List<UserBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserFilterLayout);
            lstCustomers = FindViewById<ListView>(Resource.Id.UsersFilterListView);

            BindList();
            lstCustomers.ItemClick += LstCustomers_ItemClick;
        }
        private void LstCustomers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Position < 0 && list.Count <= 0) return;
            var cus = lstCustomers.GetItemAtPosition(e.Position).Cast<UserBussines>();
            var intent = new Intent(this, typeof(UserLogActivity));
            intent.PutExtra("UserGuid", cus.Guid.ToString());
            StartActivity(intent);
        }
        private void BindList()
        {
            try
            {
                list = UserBussines.GetAll();

                lstCustomers.Adapter = new UserFilterAdapter(this, list.OrderBy(q => q.Name).ToList());
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}