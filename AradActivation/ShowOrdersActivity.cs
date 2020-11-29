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
    [Activity(Label = "نمایش لیست قراردادها", Theme = "@style/MyTheme")]
    public class ShowOrdersActivity : AppCompatActivity
    {
        private ListView lstOrders;
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private List<OrderBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ShowOrders);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myOrderToolbar);
            SetSupportActionBar(myToolbar);
            lstOrders = FindViewById<ListView>(Resource.Id.OrderListView);

            BindList();

            lstOrders.ItemClick += LstOrders_ItemClick;
        }

        private void LstOrders_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //if (e.Position < 0 && list.Count <= 0) return;
            //var cus = lstCustomers.GetItemAtPosition(e.Position).Cast<CustomerBussines>();
            //var intent = new Intent(this, typeof(CustomerMainActivity));
            //intent.PutExtra("CusGuid", cus.Guid.ToString());
            //StartActivity(intent);
        }

        private void BindList()
        {
            try
            {
                list = OrderBussines.GetAll();
                if (CurrentUser.User.Type != EnUserType.Manager)
                    list = list.Where(q => q.UserGuid == CurrentUser.User.Guid).ToList();
                lstOrders.Adapter = new OrderAdapter(this, list.OrderByDescending(q => q.Date).ToList());
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
            StartActivity(typeof(OrderMainActivity));
            return base.OnOptionsItemSelected(item);
        }
    }
}