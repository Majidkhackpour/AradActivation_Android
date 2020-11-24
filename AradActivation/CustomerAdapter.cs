using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;
using Java.Lang;
using Services;

namespace AradActivation
{
    public class CustomerAdapter : BaseAdapter<CustomerBussines>
    {
        private Activity _context;
        private List<CustomerBussines> _list;

        public CustomerAdapter(Activity context, List<CustomerBussines> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.CustomerItem, null);

            var cust = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblCustName).Text = cust?.Name;
            view.FindViewById<TextView>(Resource.Id.lblCompanyName).Text = cust?.CompanyName;
            view.FindViewById<TextView>(Resource.Id.lblSerial).Text = cust?.AppSerial;
            view.FindViewById<TextView>(Resource.Id.lblFanni).Text = cust?.HardSerial;
            view.FindViewById<TextView>(Resource.Id.lblTell1).Text = cust?.Tell1;
            view.FindViewById<TextView>(Resource.Id.lblTell2).Text = cust?.Tell2;

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override CustomerBussines this[int position] => _list[position];
    }
}