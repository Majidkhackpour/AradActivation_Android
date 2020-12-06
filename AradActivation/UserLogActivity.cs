using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using DepartmentDal.Classes;
using Services;

namespace AradActivation
{
    [Activity(Label = "لاگ عملکرد کاربران", Theme = "@style/MyTheme")]
    public class UserLogActivity : Activity
    {
        private ListView lstCustomers;
        private List<UserLogBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserLogLayout);
            lstCustomers = FindViewById<ListView>(Resource.Id.UserLogListView);
            var guid = Intent.GetStringExtra("UserGuid");
            var cusGuid = Guid.Empty;
            if (!string.IsNullOrEmpty(guid))
                cusGuid = Guid.Parse(guid);
            BindList(cusGuid);
        }
        private void BindList(Guid guid)
        {
            try
            {
                list = UserLogBussines.GetAll(guid);
                lstCustomers.Adapter = new UserLogAdapter(this, list.OrderByDescending(q => q.Date).ToList());
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}