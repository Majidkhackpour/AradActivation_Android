using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    [Activity(Label = "نمایش لیست مشتریان", Theme = "@style/MyTheme")]
    public class ShowCustomerActivity : AppCompatActivity
    {
        private ListView lstCustomers;
        private Android.Support.V7.Widget.Toolbar myToolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ShowCustomers);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myCustToolbar);
            SetSupportActionBar(myToolbar);
            lstCustomers = FindViewById<ListView>(Resource.Id.CustomerListView);

            var list = CustomerBussines.GetAll();
            lstCustomers.Adapter = new CustomerAdapter(this, list.OrderBy(q => q.Name).ToList());
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