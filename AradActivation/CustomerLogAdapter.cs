using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    public class CustomerLogAdapter : BaseAdapter<CustomerLogBussines>
    {
        private Activity _context;
        private List<CustomerLogBussines> _list;

        public CustomerLogAdapter(Activity context, List<CustomerLogBussines> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.CustomerLogItem, null);

            var cust = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblCustomerLogDateSh).Text = cust?.DateSh;
            view.FindViewById<TextView>(Resource.Id.lblCustomerLogTime).Text = cust?.Time;
            view.FindViewById<TextView>(Resource.Id.lblCustomerLogSide).Text = cust?.SideName;
            view.FindViewById<TextView>(Resource.Id.lblCustomerLogDesc).Text = cust?.Description;
           
            SetFonts(view);

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override CustomerLogBussines this[int position] => _list[position];
        private void SetFonts(View view)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");

            view.FindViewById<TextView>(Resource.Id.lblCustomerLogDesc).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblCustomerLogDateSh).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblCustomerLogSide).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblCustomerLogTime).Typeface = fontYekan;



            view.FindViewById<TextView>(Resource.Id.CustomerLogDateSh).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.CustomerlogDesc).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.CustomerLogTime).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.CustomerLogSide).Typeface = fontYekan;
        }
    }
}