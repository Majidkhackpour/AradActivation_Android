using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    public class PardakhtAdapter : BaseAdapter<PardakhtBussines>
    {
        private Activity _context;
        private List<PardakhtBussines> _list;

        public PardakhtAdapter(Activity context, List<PardakhtBussines> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.PardakhtItem, null);

            var cust = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblPardakhtCustName).Text = cust?.PayerName;
            view.FindViewById<TextView>(Resource.Id.lblPardakhtDateSh).Text = cust?.DateSh;
            view.FindViewById<TextView>(Resource.Id.lblPardakhtNaqd).Text = cust?.NaqdPrice.ToString("N0") + " ریال";
            view.FindViewById<TextView>(Resource.Id.lblPardakhtBank).Text = cust?.BankPrice.ToString("N0") + " ریال";
            view.FindViewById<TextView>(Resource.Id.lblPardakhtCheck).Text = cust?.Check.ToString("N0") + " ریال";

            SetFonts(view);

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override PardakhtBussines this[int position] => _list[position];
        private void SetFonts(View view)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");
            var fontTitr = Typeface.CreateFromAsset(_context.Assets, "B TITR BOLD.TTF");

            view.FindViewById<TextView>(Resource.Id.lblPardakhtBank).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblPardakhtCheck).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblPardakhtNaqd).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblPardakhtDateSh).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblPardakhtCustName).Typeface = fontTitr;



            view.FindViewById<TextView>(Resource.Id.PardakhtDateText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.PardakhtNaqdText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.PardakhtBankText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.PardakhtCheckText).Typeface = fontYekan;
        }
    }
}