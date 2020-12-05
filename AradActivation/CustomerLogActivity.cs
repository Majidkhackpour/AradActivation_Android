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
    [Activity(Label = "لاگ عملکرد مشتریان", Theme = "@style/MyTheme")]
    public class CustomerLogActivity : Activity
    {
        private ListView lstCustomers;
        private List<CustomerLogBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CustomerLogLayout);
            lstCustomers = FindViewById<ListView>(Resource.Id.CustomerLogListView);
            var guid = Intent.GetStringExtra("CusGuid");
            var cusGuid = Guid.Empty;
            if (!string.IsNullOrEmpty(guid)) 
                cusGuid = Guid.Parse(guid);
            BindList(cusGuid);
        }
        private void BindList(Guid guid)
        {
            try
            {
                list = CustomerLogBussines.GetAll();
                list = list.Where(q => q.CustomerGuid == guid).ToList();
                lstCustomers.Adapter = new CustomerLogAdapter(this, list.OrderByDescending(q => q.Date).ToList());
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}