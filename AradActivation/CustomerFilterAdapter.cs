using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    public class CustomerFilterAdapter : BaseAdapter<CustomerBussines>
    {
        private Activity _context;
        private List<CustomerBussines> _list;

        public CustomerFilterAdapter(Activity context, List<CustomerBussines> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.CustomerFilterItem, null);

            var cust = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblCustomerFilterName).Text = cust?.Name;

            SetFonts(view);

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override CustomerBussines this[int position] => _list[position];
        private void SetFonts(View view)
        {
            var fontTitr = Typeface.CreateFromAsset(_context.Assets, "B TITR BOLD.TTF");

            view.FindViewById<TextView>(Resource.Id.lblCustomerFilterName).Typeface = fontTitr;
        }
    }
}