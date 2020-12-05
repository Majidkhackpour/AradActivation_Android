using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    public class CustomerAccountAdapter : BaseAdapter<CustomerBussines>
    {
        private Activity _context;
        private List<CustomerBussines> _list;

        public CustomerAccountAdapter(Activity context, List<CustomerBussines> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.CustomerAccountItem, null);

            var cust = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblCustAccountName).Text = cust?.Name;
            view.FindViewById<TextView>(Resource.Id.lblCustAccount).Text =
                Math.Abs(cust?.Account ?? 0).ToString("N0") + " ریال";
            view.FindViewById<TextView>(Resource.Id.lblCustAccountStatus).Text = cust?.AccountFlag;

            SetFonts(view);

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override CustomerBussines this[int position] => _list[position];
        private void SetFonts(View view)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");
            var fontTitr = Typeface.CreateFromAsset(_context.Assets, "B TITR BOLD.TTF");

            view.FindViewById<TextView>(Resource.Id.lblCustAccount).Typeface = fontTitr;
            view.FindViewById<TextView>(Resource.Id.lblCustAccountName).Typeface = fontTitr;
            view.FindViewById<TextView>(Resource.Id.lblCustAccountStatus).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.CustAccountStatus).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.CustAccountText).Typeface = fontYekan;
        }
    }
}