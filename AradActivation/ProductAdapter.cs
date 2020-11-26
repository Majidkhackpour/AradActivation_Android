using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    public class ProductAdapter : BaseAdapter<ProductBussines>
    {
        private Activity _context;
        private List<ProductBussines> _list;

        public ProductAdapter(Activity context, List<ProductBussines> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.ProductItem, null);

            var cust = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblPrdName).Text = cust?.Name;
            view.FindViewById<TextView>(Resource.Id.lblPrdBackUp).Text = cust?.BckUpPrice.ToString("N0") + " ریال";
            view.FindViewById<TextView>(Resource.Id.lblPrdCode).Text = cust?.Code;
            view.FindViewById<TextView>(Resource.Id.lblPrdPrice).Text = cust?.Price.ToString("N0")+" ریال";

            SetFonts(view);

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override ProductBussines this[int position] => _list[position];
        private void SetFonts(View view)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");
            var fontTitr = Typeface.CreateFromAsset(_context.Assets, "B TITR BOLD.TTF");

            view.FindViewById<TextView>(Resource.Id.lblPrdPrice).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblPrdBackUp).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblPrdCode).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblPrdName).Typeface = fontTitr;



            view.FindViewById<TextView>(Resource.Id.CodeText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.PriceText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.BackUpText).Typeface = fontYekan;
        }
    }
}