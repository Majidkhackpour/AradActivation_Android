using Android.App;
using Android.Views;
using Android.Widget;
using Services.AndroidViewModels;
using System.Collections.Generic;
using Android.Graphics;
using KargozariHamrah.Utils;

namespace KargozariHamrah.Adapters
{
    public class BuildingListAdapter : BaseAdapter<BuildingListViewModel>
    {
        private Activity _context;
        private List<BuildingListViewModel> _list;

        public BuildingListAdapter(Activity context, List<BuildingListViewModel> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.BuildingItemLayout, null);

            var bu = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblBuildingPrice).Text = bu?.Price;
            view.FindViewById<TextView>(Resource.Id.lblBuildingAddress).Text = bu?.Address;
            view.FindViewById<TextView>(Resource.Id.lblTabaqe).Text = bu?.TabaqeCount;
            view.FindViewById<TextView>(Resource.Id.lblSaleSakht).Text = bu?.SaleSakht;
            view.FindViewById<TextView>(Resource.Id.lblRoomCount).Text = bu?.RoomCount;
            view.FindViewById<TextView>(Resource.Id.lblMetrazh).Text = bu?.Metrazh;
            if (string.IsNullOrEmpty(bu?.ImageName))
                view.FindViewById<ImageView>(Resource.Id.imgBuilding).SetImageResource(Resource.Drawable.ImageNotAvalable);
            else
                view.FindViewById<ImageView>(Resource.Id.imgBuilding).SetImageBitmap(BuildingUtilities.GetImageFromUrl(bu?.ImageName));
            SetFonts(view);

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override BuildingListViewModel this[int position] => _list[position];
        private void SetFonts(View view)
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