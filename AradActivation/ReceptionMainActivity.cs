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
    [Activity(Label = "دریافت وجه", Theme = "@style/MyTheme")]
    public class ReceptionMainActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private Spinner cmbSandouq;
        private Spinner cmbBank;
        private TextView lblReceptionMainDateSh;
        private TextView ReceptionMainDateShText;
        private AutoCompleteTextView txtReceptionMainCustomerName;
        private TextView ReceptionMainNaqdText;
        private EditText txtReceptionMainNaqdPrice;
        private TextView ReceptionMainBankText;
        private EditText txtReceptionMainBankPrice;
        private EditText txtReceptionMainBankFishNo;
        private TextView ReceptionMainCheckText;
        private EditText txtReceptionMainCheckPrice;
        private AutoCompleteTextView txtReceptionMainBankName;
        private EditText txtReceptionMainYear;
        private EditText txtReceptionMainMounth;
        private EditText txtReceptionMainDay;
        private TextView ReceptionMainDateShText_;
        private ReceptionBussines cls;
        private Guid customerGuid = Guid.Empty;
        private EditText txtReceptionMainChechNo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ReceptionMainLayout);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.ReceptionMainToolbar);
            SetSupportActionBar(myToolbar);
            FindElements();
            SetCmbData();
            SetFonts();

            var names = CustomerBussines.GetAll().Select(q => q.Name).ToList();
            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, names);
            txtReceptionMainCustomerName.Adapter = adapter;


            var names_ = ReceptionBussines.GetAll().Select(q => q.BankName).ToList();
            ArrayAdapter adapter_ = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, names_);
            txtReceptionMainBankName.Adapter = adapter_;


            cls = new ReceptionBussines();

            txtReceptionMainYear.Text = Calendar.GetYearOfDateSh(cls.DateSh).ToString();
            txtReceptionMainMounth.Text = Calendar.GetMonthOfDateSh(cls.DateSh).ToString();
            txtReceptionMainDay.Text = Calendar.GetDayOfDateSh(cls.DateSh).ToString();
            lblReceptionMainDateSh.Text = cls.DateSh;

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
                cmbSandouq = FindViewById<Spinner>(Resource.Id.cmbReceptionMainNaqd);
                cmbBank = FindViewById<Spinner>(Resource.Id.cmbReceptionMainBank);


                lblReceptionMainDateSh = FindViewById<TextView>(Resource.Id.lblReceptionMainDateSh);
                ReceptionMainDateShText = FindViewById<TextView>(Resource.Id.ReceptionMainDateShText);
                txtReceptionMainCustomerName = FindViewById<AutoCompleteTextView>(Resource.Id.txtReceptionMainCustomerName);
                ReceptionMainNaqdText = FindViewById<TextView>(Resource.Id.ReceptionMainNaqdText);
                txtReceptionMainNaqdPrice = FindViewById<EditText>(Resource.Id.txtReceptionMainNaqdPrice);
                ReceptionMainBankText = FindViewById<TextView>(Resource.Id.ReceptionMainBankText);
                txtReceptionMainBankPrice = FindViewById<EditText>(Resource.Id.txtReceptionMainBankPrice);
                txtReceptionMainBankFishNo = FindViewById<EditText>(Resource.Id.txtReceptionMainBankFishNo);
                ReceptionMainCheckText = FindViewById<TextView>(Resource.Id.ReceptionMainCheckText);
                txtReceptionMainCheckPrice = FindViewById<EditText>(Resource.Id.txtReceptionMainCheckPrice);
                txtReceptionMainBankName = FindViewById<AutoCompleteTextView>(Resource.Id.txtReceptionMainBankName);
                txtReceptionMainYear = FindViewById<EditText>(Resource.Id.txtReceptionMainYear);
                txtReceptionMainMounth = FindViewById<EditText>(Resource.Id.txtReceptionMainMounth);
                txtReceptionMainDay = FindViewById<EditText>(Resource.Id.txtReceptionMainDay);
                ReceptionMainDateShText_ = FindViewById<TextView>(Resource.Id.ReceptionMainDateShText_);
                txtReceptionMainChechNo = FindViewById<EditText>(Resource.Id.txtReceptionMainChechNo);
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

            lblReceptionMainDateSh.Typeface = fontYekan;
            ReceptionMainDateShText.Typeface = fontYekan;
            txtReceptionMainCustomerName.Typeface = fontYekan;
            ReceptionMainNaqdText.Typeface = fontTitr;
            txtReceptionMainNaqdPrice.Typeface = fontYekan;
            ReceptionMainBankText.Typeface = fontTitr;
            txtReceptionMainBankPrice.Typeface = fontYekan;
            txtReceptionMainBankFishNo.Typeface = fontYekan;
            ReceptionMainCheckText.Typeface = fontTitr;
            txtReceptionMainCheckPrice.Typeface = fontYekan;
            txtReceptionMainBankName.Typeface = fontYekan;
            txtReceptionMainYear.Typeface = fontYekan;
            txtReceptionMainMounth.Typeface = fontYekan;
            txtReceptionMainDay.Typeface = fontYekan;
            ReceptionMainDateShText_.Typeface = fontYekan;
            txtReceptionMainChechNo.Typeface = fontYekan;
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
            cls.NaqdPrice = txtReceptionMainNaqdPrice.Text.ParseToDecimal();
            cls.BankName = txtReceptionMainBankName.Text;
            cls.BankPrice = txtReceptionMainBankPrice.Text.ParseToDecimal();
            cls.Check = txtReceptionMainCheckPrice.Text.ParseToDecimal();
            cls.CheckNo = txtReceptionMainChechNo.Text;
            cls.CreateDate = DateTime.Now;
            cls.Description = "";
            cls.FishNo = txtReceptionMainBankFishNo.Text;
            cls.Receptor = customerGuid;
            cls.SarResid = txtReceptionMainYear.Text + "/" + txtReceptionMainMounth.Text + "/" +
                           txtReceptionMainDay.Text;

           
            ReceptionBussines.Save(cls);
            Finish();
            return base.OnOptionsItemSelected(item);
        }
        private ReturnedSaveFuncInfo CheckValidation()
        {
            var res = new ReturnedSaveFuncInfo();
            try
            {
                if (string.IsNullOrEmpty(txtReceptionMainCustomerName.Text))
                {
                    res.AddReturnedValue(ReturnedState.Error, "مشتری انتخاب شده معتبر نمی باشد");
                    return res;
                }
                var cust = CustomerBussines.Get(txtReceptionMainCustomerName.Text);
                if (cust == null || cust.Guid == Guid.Empty)
                {
                    res.AddReturnedValue(ReturnedState.Error, "مشتری انتخاب شده معتبر نمی باشد");
                    return res;
                }

                customerGuid = cust.Guid;

                if (txtReceptionMainNaqdPrice.Text.ParseToDecimal() <= 0 &&
                    txtReceptionMainBankPrice.Text.ParseToDecimal() <= 0 &&
                    txtReceptionMainCheckPrice.Text.ParseToDecimal() <= 0)
                {
                    res.AddReturnedValue(ReturnedState.Error, "حتما باید یکی از قیلدهای مبلغ را وارد نمایید");
                    return res;
                }

                if (txtReceptionMainDay.Text.ParseToInt() > 0 && txtReceptionMainDay.Text.ParseToInt() > 31)
                {
                    res.AddReturnedValue(ReturnedState.Error, "تاریخ سررسید چک با فرمت ناصحیح پرشده است");
                    return res;
                }
                if (txtReceptionMainMounth.Text.ParseToInt() > 0 && txtReceptionMainMounth.Text.ParseToInt() > 12)
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