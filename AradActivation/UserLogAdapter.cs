using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    public class UserLogAdapter : BaseAdapter<UserLogBussines>
    {
        private Activity _context;
        private List<UserLogBussines> _list;

        public UserLogAdapter(Activity context, List<UserLogBussines> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.UserLogItem, null);

            var cust = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblUserLogDateSh).Text = cust?.DateSh;
            view.FindViewById<TextView>(Resource.Id.lblUserLogSide).Text = cust?.TypeName;
            view.FindViewById<TextView>(Resource.Id.lblUserLogDesc).Text = cust?.Description;

            SetFonts(view);

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override UserLogBussines this[int position] => _list[position];
        private void SetFonts(View view)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");

            view.FindViewById<TextView>(Resource.Id.lblUserLogDesc).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblUserLogDateSh).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblUserLogSide).Typeface = fontYekan;



            view.FindViewById<TextView>(Resource.Id.UserLogDateSh).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.UserlogDesc).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.UserLogSide).Typeface = fontYekan;
        }
    }
}