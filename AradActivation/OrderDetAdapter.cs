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
        private class MyViewHolder : Java.Lang.Object
        {
            public TextView txtName;
            public TextView txtPrice;
            public TextView txtBackUp;
            public CheckBox chkItem;

        }
        public class MyWrapper<T> : Java.Lang.Object
        {
            private T _value;
            public MyWrapper(T managedValue)
            {
                _value = managedValue;
            }
            public T value { get { return _value; } }
        }
       
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
            var item = _list[position];
            var view = convertView;
            MyViewHolder holder;
            if (convertView == null)
            {
                holder = new MyViewHolder();
                view = _context.LayoutInflater.Inflate(Resource.Layout.OrderDetsItem, null);
                holder.chkItem = view.FindViewById<CheckBox>(Resource.Id.checkboxOrderDet);
                holder.txtName = view.FindViewById<TextView>(Resource.Id.lblOrderDetPrdName);
                holder.txtPrice = view.FindViewById<TextView>(Resource.Id.lblOrderDetPrdPrice);
                holder.txtBackUp = view.FindViewById<TextView>(Resource.Id.lblOrderDetPrdBackUp);
                view.Tag = holder;
            }
            else holder = view.Tag as MyViewHolder;

            holder.chkItem.Checked = item.IsChecked;
            
            holder.chkItem.Tag = new MyWrapper<int>(position);
            holder.txtName.Text = item.Name ;
            holder.txtPrice.Text = item.Price.ToString("N0") + " ریال";
            holder.txtBackUp.Text = item.BckUpPrice.ToString("N0") + " ریال";
            holder.chkItem.Click -= HolderChkItemClick;
            holder.chkItem.Click += HolderChkItemClick;
            SetFonts(view);
            return view;
        }
        private void HolderChkItemClick(object sender, EventArgs e)
        {
            var chk = sender as CheckBox;
            var pos = ((MyWrapper<int>)chk.Tag).value;
            prd = _list[pos];
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

            OrderBussines.RaiseEvent( a);
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