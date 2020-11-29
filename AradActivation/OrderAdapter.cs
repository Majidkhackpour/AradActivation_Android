using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    public class OrderAdapter : BaseAdapter<OrderBussines>
    {
        private Activity _context;
        private List<OrderBussines> _list;

        public OrderAdapter(Activity context, List<OrderBussines> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.OrderItem, null);

            var cust = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblOrderCustName).Text = cust?.CustomerName;
            view.FindViewById<TextView>(Resource.Id.lblOrderDateSh).Text = cust?.DateSh;
            view.FindViewById<TextView>(Resource.Id.lblOrderDiscount).Text = cust?.Discount.ToString("N0") + " ریال";
            view.FindViewById<TextView>(Resource.Id.lblOrderSum).Text = cust?.Sum.ToString("N0") + " ریال";
            view.FindViewById<TextView>(Resource.Id.lblOrderTotal).Text = cust?.Total.ToString("N0") + " ریال";
            view.FindViewById<TextView>(Resource.Id.lblOrderDets).Text =
                string.Join(" , ", cust?.DetList.Select(q => q.ProductName).ToList());

            SetFonts(view);

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override OrderBussines this[int position] => _list[position];
        private void SetFonts(View view)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");
            var fontTitr = Typeface.CreateFromAsset(_context.Assets, "B TITR BOLD.TTF");

            view.FindViewById<TextView>(Resource.Id.lblOrderCustName).Typeface = fontTitr;
            view.FindViewById<TextView>(Resource.Id.lblOrderDateSh).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblOrderDets).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblOrderDiscount).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblOrderTotal).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblOrderSum).Typeface = fontYekan;



            view.FindViewById<TextView>(Resource.Id.dateShText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.sumText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.discountText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.totalText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.orderDetsText).Typeface = fontYekan;
        }
    }
}