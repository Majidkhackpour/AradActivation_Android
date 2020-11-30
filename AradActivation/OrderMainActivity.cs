using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using AradActivation.Utilities;
using DepartmentDal.Classes;
using Services;

namespace AradActivation
{
    [Activity(Label = "مدیریت قراردادها", Theme = "@style/MyTheme")]
    public class OrderMainActivity : Activity
    {
        private ListView lstProducts;
        private List<ProductBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.OrderMainLayout);

            lstProducts = FindViewById<ListView>(Resource.Id.OrderDetListView);
            BindList();

            OrderBussines.OnDetailsChanged += OrderBussines_OnDetailsChanged;
        }

        private EventArgs OrderBussines_OnDetailsChanged(bool isAdd, OrderDetailBussines e)
        {
            return EventArgs.Empty;
            //throw new NotImplementedException();

        }

        private void BindList()
        {
            try
            {
                list = ProductBussines.GetAll();
                lstProducts.Adapter = new OrderDetAdapter(this, list.OrderByDescending(q => q.Price).ToList());
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}