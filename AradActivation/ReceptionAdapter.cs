using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;

namespace AradActivation
{
    public class ReceptionAdapter : BaseAdapter<ReceptionBussines>
    {
        private Activity _context;
        private List<ReceptionBussines> _list;

        public ReceptionAdapter(Activity context, List<ReceptionBussines> list)
        {
            _context = context;
            _list = list;
        }

        public override long GetItemId(int position) => 0;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.ReceptionItem, null);

            var cust = _list[position];

            view.FindViewById<TextView>(Resource.Id.lblReceptionCustName).Text = cust?.ReceptorName;
            view.FindViewById<TextView>(Resource.Id.lblReceptionDateSh).Text = cust?.DateSh;
            view.FindViewById<TextView>(Resource.Id.lblReceptionNaqd).Text = cust?.NaqdPrice.ToString("N0") + " ریال";
            view.FindViewById<TextView>(Resource.Id.lblReceptionBank).Text = cust?.BankPrice.ToString("N0") + " ریال";
            view.FindViewById<TextView>(Resource.Id.lblReceptionCheck).Text = cust?.Check.ToString("N0") + " ریال";

            SetFonts(view);

            return view;
        }
        public override int Count => _list?.Count ?? 0;
        public override ReceptionBussines this[int position] => _list[position];
        private void SetFonts(View view)
        {
            var fontYekan = Typeface.CreateFromAsset(_context.Assets, "B Yekan.TTF");
            var fontTitr = Typeface.CreateFromAsset(_context.Assets, "B TITR BOLD.TTF");

            view.FindViewById<TextView>(Resource.Id.lblReceptionBank).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblReceptionCheck).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblReceptionNaqd).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblReceptionDateSh).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.lblReceptionCustName).Typeface = fontTitr;



            view.FindViewById<TextView>(Resource.Id.ReceptionDateText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.ReceptionNaqdText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.ReceptionBankText).Typeface = fontYekan;
            view.FindViewById<TextView>(Resource.Id.ReceptionCheckText).Typeface = fontYekan;
        }
    }
}