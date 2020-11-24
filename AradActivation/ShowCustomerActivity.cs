using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    [Activity(Label = "نمایش لیست مشتریان", Theme = "@style/MyTheme")]
    public class ShowCustomerActivity : Activity
    {
        private ListView lstCustomers;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ShowCustomers);
            lstCustomers = FindViewById<ListView>(Resource.Id.CustomerListView);

            var list = CustomerBussines.GetAll();
            
            lstCustomers.Adapter = new CustomerAdapter(this, list);
        }
    }
}