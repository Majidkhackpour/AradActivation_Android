using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using KargozariHamrah.Utils;
using Services.AndroidViewModels;

namespace KargozariHamrah.Adapters
{
    public class ShowBuildingListAdapter : RecyclerView.Adapter
    {
        public List<BuildingListViewModel> mHolder;
        private Activity _context;
        public ShowBuildingListAdapter(List<BuildingListViewModel> _holder, Activity co)
        {
            mHolder = _holder;
            _context = co;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as BuildingListHolder;
            vh.Price.Text = mHolder[position].Price;
            vh.Address.Text = mHolder[position].Address;
            vh.Tabaqe.Text = mHolder[position].TabaqeCount;
            vh.SaleSakht.Text = mHolder[position].SaleSakht;
            vh.RoomCount.Text = mHolder[position].RoomCount;
            vh.Metrazh.Text = mHolder[position].Metrazh;
            if (string.IsNullOrEmpty(mHolder[position].ImageName))
                vh.Image.SetImageResource(Resource.Drawable.Arad_NotAwailable);
            else
            {
               // GlideApp
               //     .with(myFragment)
               //     .load(url)
               //     .centerCrop()
               //     .placeholder(R.drawable.loading_spinner)
               //     .into(myImageView);
                vh.Image.SetImageBitmap(BuildingUtilities.GetImageFromUrl(mHolder[position].ImageName));
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.BuildingItemLayout, parent, false);
            var vh = new BuildingListHolder(itemView, _context);
            return vh;
        }

        public override int ItemCount => mHolder?.Count ?? 0;
    }
    public class BuildingListHolder : RecyclerView.ViewHolder
    {
        public TextView Price { get; private set; }
        public TextView Address { get; private set; }
        public TextView Tabaqe { get; private set; }
        public TextView SaleSakht { get; private set; }
        public TextView RoomCount { get; private set; }
        public TextView Metrazh { get; private set; }
        public ImageView Image { get; private set; }

        public BuildingListHolder(View itemView, Activity _context) : base(itemView)
        {
            Price = itemView.FindViewById<TextView>(Resource.Id.lblBuildingPrice);
            Address = itemView.FindViewById<TextView>(Resource.Id.lblBuildingAddress);
            Tabaqe = itemView.FindViewById<TextView>(Resource.Id.lblTabaqe);
            SaleSakht = itemView.FindViewById<TextView>(Resource.Id.lblSaleSakht);
            RoomCount = itemView.FindViewById<TextView>(Resource.Id.lblRoomCount);
            Metrazh = itemView.FindViewById<TextView>(Resource.Id.lblMetrazh);
            Image = itemView.FindViewById<ImageView>(Resource.Id.imgBuilding);
            SetFonts(itemView, _context);
        }
        private void SetFonts(View view, Activity _context)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");
            var fontTitr = Typeface.CreateFromAsset(_context.Assets, "B TITR BOLD.TTF");

            view.FindViewById<TextView>(Resource.Id.lblBuildingPrice).Typeface = fontTitr;
            view.FindViewById<TextView>(Resource.Id.lblBuildingAddress).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblTabaqe).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblSaleSakht).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblRoomCount).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblMetrazh).Typeface = fontYekan;
        }
    }
}