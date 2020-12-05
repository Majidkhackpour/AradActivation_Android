using System;
using System.Linq;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;
using Services;

namespace AradActivation
{
    [Activity(Label = "پرداخت وجه", Theme = "@style/MyTheme")]
    public class PardakhtMainActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private Spinner cmbSandouq;
        private Spinner cmbBank;
        private TextView lblPardakhtMainDateSh;
        private TextView PardakhtMainDateShText;
        private AutoCompleteTextView txtPardakhtMainCustomerName;
        private TextView PardakhtMainNaqdText;
        private EditText txtPardakhtMainNaqdPrice;
        private TextView PardakhtMainBankText;
        private EditText txtPardakhtMainBankPrice;
        private EditText txtPardakhtMainBankFishNo;
        private TextView PardakhtMainCheckText;
        private EditText txtPardakhtMainCheckPrice;
        private AutoCompleteTextView txtPardakhtMainBankName;
        private EditText txtPardakhtMainYear;
        private EditText txtPardakhtMainMounth;
        private EditText txtPardakhtMainDay;
        private TextView PardakhtMainDateShText_;
        private PardakhtBussines cls;
        private Guid customerGuid = Guid.Empty;
        private EditText txtPardakhtMainChechNo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PardakhtMainLayout);

            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.PardakhtMainToolbar);
            SetSupportActionBar(myToolbar);
            FindElements();
            SetCmbData();
            SetFonts();

            var names = CustomerBussines.GetAll().Select(q => q.Name).ToList();
            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, names);
            txtPardakhtMainCustomerName.Adapter = adapter;


            var names_ = PardakhtBussines.GetAll().Select(q => q.BankName).ToList();
            ArrayAdapter adapter_ = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, names_);
            txtPardakhtMainBankName.Adapter = adapter_;


            cls = new PardakhtBussines();

            txtPardakhtMainYear.Text = Calendar.GetYearOfDateSh(cls.DateSh).ToString();
            txtPardakhtMainMounth.Text = Calendar.GetMonthOfDateSh(cls.DateSh).ToString();
            txtPardakhtMainDay.Text = Calendar.GetDayOfDateSh(cls.DateSh).ToString();
            lblPardakhtMainDateSh.Text = cls.DateSh;

            cmbSandouq.ItemSelected += CmbSandouq_ItemSelected;
            cmbBank.ItemSelected += CmbBank_ItemSelected;
        }
        private void CmbBank_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var s = sender as Spinner;
            var a = s.GetItemAtPosition(e.Position);
            var safe = SafeBoxBussines.Get(a.ToString());
            if (safe == null) return;
            cls.BankSafeBoxGuid = safe.Guid;
        }
        private void CmbSandouq_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var s = sender as Spinner;
            var a = s.GetItemAtPosition(e.Position);
            var safe = SafeBoxBussines.Get(a.ToString());
            if (safe == null) return;
            cls.NaqdSafeBoxGuid = safe.Guid;
        }
        private void FindElements()
        {
            try
            {
                cmbSandouq = FindViewById<Spinner>(Resource.Id.cmbPardakhtMainNaqd);
                cmbBank = FindViewById<Spinner>(Resource.Id.cmbPardakhtMainBank);


                lblPardakhtMainDateSh = FindViewById<TextView>(Resource.Id.lblPardakhtMainDateSh);
                PardakhtMainDateShText = FindViewById<TextView>(Resource.Id.PardakhtMainDateShText);
                txtPardakhtMainCustomerName = FindViewById<AutoCompleteTextView>(Resource.Id.txtPardakhtMainCustomerName);
                PardakhtMainNaqdText = FindViewById<TextView>(Resource.Id.PardakhtMainNaqdText);
                txtPardakhtMainNaqdPrice = FindViewById<EditText>(Resource.Id.txtPardakhtMainNaqdPrice);
                PardakhtMainBankText = FindViewById<TextView>(Resource.Id.PardakhtMainBankText);
                txtPardakhtMainBankPrice = FindViewById<EditText>(Resource.Id.txtPardakhtMainBankPrice);
                txtPardakhtMainBankFishNo = FindViewById<EditText>(Resource.Id.txtPardakhtMainBankFishNo);
                PardakhtMainCheckText = FindViewById<TextView>(Resource.Id.PardakhtMainCheckText);
                txtPardakhtMainCheckPrice = FindViewById<EditText>(Resource.Id.txtPardakhtMainCheckPrice);
                txtPardakhtMainBankName = FindViewById<AutoCompleteTextView>(Resource.Id.txtPardakhtMainBankName);
                txtPardakhtMainYear = FindViewById<EditText>(Resource.Id.txtPardakhtMainYear);
                txtPardakhtMainMounth = FindViewById<EditText>(Resource.Id.txtPardakhtMainMounth);
                txtPardakhtMainDay = FindViewById<EditText>(Resource.Id.txtPardakhtMainDay);
                PardakhtMainDateShText_ = FindViewById<TextView>(Resource.Id._PardakhtMainDateShText);
                txtPardakhtMainChechNo = FindViewById<EditText>(Resource.Id.txtPardakhtMainChechNo);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        private void SetCmbData()
        {
            try
            {
                var listSandouq = SafeBoxBussines.GetAllSandouq();
                var listBank = SafeBoxBussines.GetAllBank();

                var ad = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, listSandouq.Select(q => q.Name).ToList());
                ad.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                var ad2 = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, listBank.Select(q => q.Name).ToList());
                ad2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                cmbSandouq.Adapter = ad;
                cmbBank.Adapter = ad2;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        private void SetFonts()
        {
            var fontYekan = Typeface.CreateFromAsset(Assets, "B Yekan.TTF");
            var fontTitr = Typeface.CreateFromAsset(Assets, "B TITR BOLD.TTF");

            lblPardakhtMainDateSh.Typeface = fontYekan;
            PardakhtMainDateShText.Typeface = fontYekan;
            txtPardakhtMainCustomerName.Typeface = fontYekan;
            PardakhtMainNaqdText.Typeface = fontTitr;
            txtPardakhtMainNaqdPrice.Typeface = fontYekan;
            PardakhtMainBankText.Typeface = fontTitr;
            txtPardakhtMainBankPrice.Typeface = fontYekan;
            txtPardakhtMainBankFishNo.Typeface = fontYekan;
            PardakhtMainCheckText.Typeface = fontTitr;
            txtPardakhtMainCheckPrice.Typeface = fontYekan;
            txtPardakhtMainBankName.Typeface = fontYekan;
            txtPardakhtMainYear.Typeface = fontYekan;
            txtPardakhtMainMounth.Typeface = fontYekan;
            txtPardakhtMainDay.Typeface = fontYekan;
            PardakhtMainDateShText_.Typeface = fontYekan;
            txtPardakhtMainChechNo.Typeface = fontYekan;
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.addMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var res = new ReturnedSaveFuncInfo();
            res.AddReturnedValue(CheckValidation());
            if (res.HasError)
            {
                Toast.MakeText(this, res.ErrorMessage, ToastLength.Short).Show();
                return base.OnOptionsItemSelected(item);
            }


            cls.Guid = Guid.NewGuid();
            cls.Modified = DateTime.Now;
            cls.Status = true;
            cls.UserGuid = CurrentUser.User.Guid;
            cls.NaqdPrice = txtPardakhtMainNaqdPrice.Text.ParseToDecimal();
            cls.BankName = txtPardakhtMainBankName.Text;
            cls.BankPrice = txtPardakhtMainBankPrice.Text.ParseToDecimal();
            cls.Check = txtPardakhtMainCheckPrice.Text.ParseToDecimal();
            cls.CheckNo = txtPardakhtMainChechNo.Text;
            cls.CreateDate = DateTime.Now;
            cls.Description = "";
            cls.FishNo = txtPardakhtMainBankFishNo.Text;
            cls.Payer = customerGuid;
            cls.SarResid = txtPardakhtMainYear.Text + "/" + txtPardakhtMainMounth.Text + "/" +
                           txtPardakhtMainDay.Text;


            PardakhtBussines.Save(cls);
            Finish();
            return base.OnOptionsItemSelected(item);
        }
        private ReturnedSaveFuncInfo CheckValidation()
        {
            var res = new ReturnedSaveFuncInfo();
            try
            {
                if (string.IsNullOrEmpty(txtPardakhtMainCustomerName.Text))
                {
                    res.AddReturnedValue(ReturnedState.Error, "مشتری انتخاب شده معتبر نمی باشد");
                    return res;
                }
                var cust = CustomerBussines.Get(txtPardakhtMainCustomerName.Text);
                if (cust == null || cust.Guid == Guid.Empty)
                {
                    res.AddReturnedValue(ReturnedState.Error, "مشتری انتخاب شده معتبر نمی باشد");
                    return res;
                }

                customerGuid = cust.Guid;

                if (txtPardakhtMainNaqdPrice.Text.ParseToDecimal() <= 0 &&
                    txtPardakhtMainBankPrice.Text.ParseToDecimal() <= 0 &&
                    txtPardakhtMainCheckPrice.Text.ParseToDecimal() <= 0)
                {
                    res.AddReturnedValue(ReturnedState.Error, "حتما باید یکی از قیلدهای مبلغ را وارد نمایید");
                    return res;
                }

                if (txtPardakhtMainDay.Text.ParseToInt() > 0 && txtPardakhtMainDay.Text.ParseToInt() > 31)
                {
                    res.AddReturnedValue(ReturnedState.Error, "تاریخ سررسید چک با فرمت ناصحیح پرشده است");
                    return res;
                }
                if (txtPardakhtMainMounth.Text.ParseToInt() > 0 && txtPardakhtMainMounth.Text.ParseToInt() > 12)
                {
                    res.AddReturnedValue(ReturnedState.Error, "تاریخ سررسید چک با فرمت ناصحیح پرشده است");
                    return res;
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                res.AddReturnedValue(ex);
            }

            return res;
        }
    }
}