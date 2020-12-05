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
    [Activity(Label = "ضعیت مانده حساب مشتریان", Theme = "@style/MyTheme")]
    public class CustomerAccountActivity : Activity
    {
        private ListView lstCustomers;
        private List<CustomerBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CustomerAccountALayout);
            lstCustomers = FindViewById<ListView>(Resource.Id.CustomerAccountListView);

            BindList();
        }
        private void BindList()
        {
            try
            {
                list = CustomerBussines.GetAll();
                
                lstCustomers.Adapter = new CustomerAccountAdapter(this, list.OrderBy(q => q.Name).ToList());
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}