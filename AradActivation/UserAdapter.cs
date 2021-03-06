﻿using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    public class UserAdapter : BaseAdapter<UserBussines>
    {
        private Activity _context;
        private List<UserBussines> _list;

        public UserAdapter(Activity context, List<UserBussines> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.UserItemLayout, null);

            var cust = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblUserName).Text = cust?.Name;
            view.FindViewById<TextView>(Resource.Id.lblUserStatus).Text = cust?.StatusName;

            SetFonts(view);

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override UserBussines this[int position] => _list[position];
        private void SetFonts(View view)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");
            var fontTitr = Typeface.CreateFromAsset(_context.Assets, "B TITR BOLD.TTF");

            view.FindViewById<TextView>(Resource.Id.lblUserStatus).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.UserStatusText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblUserName).Typeface = fontTitr;
        }
    }
}