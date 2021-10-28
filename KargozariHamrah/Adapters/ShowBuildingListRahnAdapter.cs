using Android.App;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Bumptech.Glide;
using Services.AndroidViewModels;
using System;
using System.Collections.Generic;

namespace KargozariHamrah.Adapters
{
    public class ShowBuildingListRahnAdapter : RecyclerView.Adapter
    {
        public List<BuildingListViewModel> mHolder;
        private BuildingListViewModel _current;
        private Activity _context;
        public ShowBuildingListRahnAdapter(List<BuildingListViewModel> _holder, Activity co)
        {
            mHolder = _holder;
            _context = co;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (!(holder is BuildingListRahnHolder vh)) return;
            _current = position == 0 ? mHolder[position] : mHolder[position - 1];
            vh.MoreImage.Click += MoreImageOnClick;
            vh.RahnPrice.Text = mHolder[position].RahnPrice;
            vh.EjarePrice.Text = mHolder[position].EjarePrice;
            vh.Address.Text = mHolder[position].Address;
            vh.Date.Text = mHolder[position].Date;
            vh.Type.Text = mHolder[position].Type;
            vh.Tabaqe.Text = mHolder[position].TabaqeCount;
            vh.SaleSakht.Text = mHolder[position].SaleSakht;
            vh.RoomCount.Text = mHolder[position].RoomCount;
            vh.Metrazh.Text = mHolder[position].Metrazh;
            if (string.IsNullOrEmpty(mHolder[position].ImageName))
                vh.Image.SetImageResource(Resource.Drawable.Arad_NotAwailable);
            else
                Glide.With(_context).Load(mHolder[position].ImageName).CenterCrop().Into(vh.Image);
        }
        private void MoreImageOnClick(object sender, EventArgs e)
        {
            if (_current == null) return;
            //Toast.MakeText(_context, _current.Address, ToastLength.Long).Show();
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(_context);
            alertDialogBuilder.SetMessage("Are you sure,You wanted to make decision");

            AlertDialog alertDialog = alertDialogBuilder.Create();
            alertDialog.Show();
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.BuildingRahnItemLayout, parent, false);
            var vh = new BuildingListRahnHolder(itemView, _context);
            itemView.Click += ItemViewOnClick;
            return vh;
        }
        private void ItemViewOnClick(object sender, EventArgs e)
        {
            Toast.MakeText(_context, _current.Address, ToastLength.Long).Show();
        }
        public override int ItemCount => mHolder?.Count ?? 0;
    }
    public class BuildingListRahnHolder : RecyclerView.ViewHolder
    {
        public TextView RahnPrice { get; private set; }
        public TextView RahnTitle { get; private set; }
        public TextView EjarePrice { get; private set; }
        public TextView EjareTitle { get; private set; }
        public TextView Date { get; private set; }
        public TextView Type { get; private set; }
        public TextView Address { get; private set; }
        public TextView Tabaqe { get; private set; }
        public TextView SaleSakht { get; private set; }
        public TextView RoomCount { get; private set; }
        public TextView Metrazh { get; private set; }
        public ImageView Image { get; private set; }
        public ImageView MoreImage { get; private set; }

        public BuildingListRahnHolder(View itemView, Activity _context) : base(itemView)
        {
            RahnPrice = itemView.FindViewById<TextView>(Resource.Id.lblBuildingRahn_RahnPrice);
            RahnTitle = itemView.FindViewById<TextView>(Resource.Id.lblBuildingRahn_RahnTitle);
            EjarePrice = itemView.FindViewById<TextView>(Resource.Id.lblBuildingRahn_EjarePrice);
            EjareTitle = itemView.FindViewById<TextView>(Resource.Id.lblBuildingRahn_EjareTitle);
            Address = itemView.FindViewById<TextView>(Resource.Id.lblBuildingRahn_Address);
            Date = itemView.FindViewById<TextView>(Resource.Id.lblBuildingRahn_Date);
            Type = itemView.FindViewById<TextView>(Resource.Id.lblBuildingRahn_Type);
            Tabaqe = itemView.FindViewById<TextView>(Resource.Id.lblBuildingRahn_TabaqeCount);
            SaleSakht = itemView.FindViewById<TextView>(Resource.Id.lblBuildingRahn_SaleSakht);
            RoomCount = itemView.FindViewById<TextView>(Resource.Id.lblBuildingRahn_RoomCount);
            Metrazh = itemView.FindViewById<TextView>(Resource.Id.lblBuildingRahn_Metrazh);
            Image = itemView.FindViewById<ImageView>(Resource.Id.imgBuildingRahn);
            MoreImage = itemView.FindViewById<ImageView>(Resource.Id.imgBuildingRahnMore);
            SetFonts(itemView, _context);
        }
        private void SetFonts(View view, Activity _context)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");

            view.FindViewById<TextView>(Resource.Id.lblBuildingRahn_RahnPrice).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingRahn_RahnTitle).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingRahn_EjarePrice).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingRahn_EjareTitle).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingRahn_Address).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingRahn_Date).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingRahn_Type).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingRahn_TabaqeCount).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingRahn_SaleSakht).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingRahn_RoomCount).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingRahn_Metrazh).Typeface = fontYekan;
        }
    }
}