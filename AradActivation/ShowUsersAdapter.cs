using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    public class ShowUsersAdapter : BaseAdapter<UserBussines>
    {
        private Activity _context;
        private List<UserBussines> _list;

        public ShowUsersAdapter(Activity context, List<UserBussines> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.ShowUsersItem, null);

            var cust = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblShowUserEmail).Text = cust?.Email;
            view.FindViewById<TextView>(Resource.Id.lblShowUserMobile).Text = cust?.Mobile;
            view.FindViewById<TextView>(Resource.Id.lblShowUserName).Text = cust?.Name;
            view.FindViewById<TextView>(Resource.Id.lblShowUserType).Text = cust?.TypeName;
            view.FindViewById<TextView>(Resource.Id.lblShowUser_UserName).Text = cust?.UserName;

            SetFonts(view);

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override UserBussines this[int position] => _list[position];
        private void SetFonts(View view)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");
            var fontTitr = Typeface.CreateFromAsset(_context.Assets, "B TITR BOLD.TTF");

            view.FindViewById<TextView>(Resource.Id.lblShowUserName).Typeface = fontTitr;
            view.FindViewById<TextView>(Resource.Id.lblShowUserEmail).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblShowUserMobile).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblShowUserType).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblShowUser_UserName).Typeface = fontYekan;



            view.FindViewById<TextView>(Resource.Id.ShowUserEmail).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.ShowUserMobile).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.ShowUserType).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.ShowUser_UserName).Typeface = fontYekan;
        }
    }
}