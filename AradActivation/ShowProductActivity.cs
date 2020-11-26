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
    [Activity(Label = "نمایش لیست کالاها", Theme = "@style/MyTheme")]
    public class ShowProductActivity : AppCompatActivity
    {
        private ListView lstProducts;
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private List<ProductBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShowProduct);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myPrdToolbar);
            SetSupportActionBar(myToolbar);
            lstProducts = FindViewById<ListView>(Resource.Id.ProductListView);

            BindList();

            lstProducts.ItemClick += LstProducts_ItemClick;
        }

        private void LstProducts_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Position < 0 && list.Count <= 0) return;
            var cus = lstProducts.GetItemAtPosition(e.Position).Cast<ProductBussines>();
            var intent = new Intent(this, typeof(ProductMainActivity));
            intent.PutExtra("PrdGuid", cus.Guid.ToString());
            StartActivity(intent);
        }

        private void BindList()
        {
            try
            {
                list = ProductBussines.GetAll();
                lstProducts.Adapter = new ProductAdapter(this, list.OrderByDescending(q => q.Price).ToList());
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
            StartActivity(typeof(ProductMainActivity));
            return base.OnOptionsItemSelected(item);
        }
    }
}