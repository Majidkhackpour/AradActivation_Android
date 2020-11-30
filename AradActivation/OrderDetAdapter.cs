using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    public class OrderDetAdapter : BaseAdapter<ProductBussines>
    {
        private Activity _context;
        private List<ProductBussines> _list;
        private ProductBussines prd;

        public OrderDetAdapter(Activity context, List<ProductBussines> list)
        {
            _context = context;
            _list = list;
        }


        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.OrderDetsItem, null);
            var cust = _list[position];
            prd = _list[position];
            view.FindViewById<TextView>(Resource.Id.lblOrderDetPrdName).Text = cust?.Name;
            view.FindViewById<TextView>(Resource.Id.lblOrderDetPrdBackUp).Text = cust?.BckUpPrice.ToString("N0") + " ریال";
            view.FindViewById<TextView>(Resource.Id.lblOrderDetPrdPrice).Text = cust?.Price.ToString("N0") + " ریال";
            SetFonts(view);
            view.FindViewById<CheckBox>(Resource.Id.checkboxOrderDet).CheckedChange += OrderDetAdapter_CheckedChange;


            return view;
        }

        private void OrderDetAdapter_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var a = new OrderDetailBussines()
            {
                Guid = Guid.NewGuid(),
                Price = prd.Price,
                Discount = 0,
                Total = prd.Price,
                Modified = DateTime.Now,
                Status = true,
                OrderGuid = Guid.Empty,
                PrdGuid = prd.Guid
            };

            OrderBussines.RaiseEvent(e.IsChecked, a);
        }

        public override int Count => _list?.Count ?? 0;
        public override ProductBussines this[int position] => _list[position];
        private void SetFonts(View view)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");

            view.FindViewById<TextView>(Resource.Id.lblOrderDetPrdName).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblOrderDetPrdBackUp).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblOrderDetPrdPrice).Typeface = fontYekan;



            view.FindViewById<TextView>(Resource.Id.orderDetBackUpText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.orderDetPriceText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblOrderDetChecker).Typeface = fontYekan;
        }
    }
}