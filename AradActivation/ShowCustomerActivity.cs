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
    [Activity(Label = "نمایش لیست مشتریان", Theme = "@style/MyTheme")]
    public class ShowCustomerActivity : AppCompatActivity
    {
        private ListView lstCustomers;
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private List<CustomerBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ShowCustomers);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myCustToolbar);
            SetSupportActionBar(myToolbar);
            lstCustomers = FindViewById<ListView>(Resource.Id.CustomerListView);

            BindList();

            lstCustomers.ItemClick += LstCustomers_ItemClick;
        }

        private void LstCustomers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Position < 0 && list.Count <= 0) return;
            var cus = lstCustomers.GetItemAtPosition(e.Position).Cast<CustomerBussines>();
            var intent = new Intent(this, typeof(CustomerMainActivity));
            intent.PutExtra("CusGuid", cus.Guid.ToString());
            StartActivity(intent);
        }

        private void BindList()
        {
            try
            {
                list = CustomerBussines.GetAll();
                lstCustomers.Adapter = new CustomerAdapter(this, list.OrderBy(q => q.Name).ToList());
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
            StartActivity(typeof(CustomerMainActivity));
            return base.OnOptionsItemSelected(item);
        }
    }
}