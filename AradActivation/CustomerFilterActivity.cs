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
    [Activity(Label = "انتخاب مشتری", Theme = "@style/MyTheme")]
    public class CustomerFilterActivity : Activity
    {
        private ListView lstCustomers;
        private List<CustomerBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CustomerFilterLayout);
            lstCustomers = FindViewById<ListView>(Resource.Id.CustomerFilterListView);

            BindList();
            lstCustomers.ItemClick += LstCustomers_ItemClick;
        }
        private void LstCustomers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Position < 0 && list.Count <= 0) return;
            var cus = lstCustomers.GetItemAtPosition(e.Position).Cast<CustomerBussines>();
            var intent = new Intent(this, typeof(CustomerLogActivity));
            intent.PutExtra("CusGuid", cus.Guid.ToString());
            StartActivity(intent);
        }
        private void BindList()
        {
            try
            {
                list = CustomerBussines.GetAll();

                lstCustomers.Adapter = new CustomerFilterAdapter(this, list.OrderBy(q => q.Name).ToList());
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}