using Android.App;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Bumptech.Glide;
using Services.AndroidViewModels;
using System.Collections.Generic;

namespace KargozariHamrah.Adapters
{
    public class ShowBuildingListSellAdapter : RecyclerView.Adapter
    {
        public List<BuildingListViewModel> mHolder;
        private Activity _context;
        public ShowBuildingListSellAdapter(List<BuildingListViewModel> _holder, Activity co)
        {
            mHolder = _holder;
            _context = co;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as BuildingListSellHolder;
            vh.SellPrice.Text = mHolder[position].SellPrice;
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

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.BuildingSellItemLayout, parent, false);
            var vh = new BuildingListSellHolder(itemView, _context);
            return vh;
        }

        public override int ItemCount => mHolder?.Count ?? 0;
    }
    public class BuildingListSellHolder : RecyclerView.ViewHolder
    {
        public TextView SellPrice { get; private set; }
        public TextView SellTitle { get; private set; }
        public TextView Date { get; private set; }
        public TextView Type { get; private set; }
        public TextView Address { get; private set; }
        public TextView Tabaqe { get; private set; }
        public TextView SaleSakht { get; private set; }
        public TextView RoomCount { get; private set; }
        public TextView Metrazh { get; private set; }
        public ImageView Image { get; private set; }

        public BuildingListSellHolder(View itemView, Activity _context) : base(itemView)
        {
            SellPrice = itemView.FindViewById<TextView>(Resource.Id.lblBuildingSell_SellPrice);
            SellTitle = itemView.FindViewById<TextView>(Resource.Id.lblBuildingSell_SellTitle);
            Address = itemView.FindViewById<TextView>(Resource.Id.lblBuildingSell_Address);
            Date = itemView.FindViewById<TextView>(Resource.Id.lblBuildingSell_Date);
            Type = itemView.FindViewById<TextView>(Resource.Id.lblBuildingSell_Type);
            Tabaqe = itemView.FindViewById<TextView>(Resource.Id.lblBuildingSell_TabaqeCount);
            SaleSakht = itemView.FindViewById<TextView>(Resource.Id.lblBuildingSell_SaleSakht);
            RoomCount = itemView.FindViewById<TextView>(Resource.Id.lblBuildingSell_RoomCount);
            Metrazh = itemView.FindViewById<TextView>(Resource.Id.lblBuildingSell_Metrazh);
            Image = itemView.FindViewById<ImageView>(Resource.Id.imgBuildingSell);
            SetFonts(itemView, _context);
        }
        private void SetFonts(View view, Activity _context)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");

            view.FindViewById<TextView>(Resource.Id.lblBuildingSell_SellPrice).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingSell_SellTitle).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingSell_Address).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingSell_Date).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingSell_Type).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingSell_TabaqeCount).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingSell_SaleSakht).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingSell_RoomCount).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblBuildingSell_Metrazh).Typeface = fontYekan;
        }
    }
}