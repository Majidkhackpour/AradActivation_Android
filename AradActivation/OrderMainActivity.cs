using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Activity(Label = "مدیریت قراردادها", Theme = "@style/MyTheme")]
    public class OrderMainActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private ListView lstProducts;
        private List<ProductBussines> list;
        private TextView lblOrderCode;
        private TextView orderCodeText;
        private TextView lblDateSh;
        private TextView odrerMainDateShText;
        private AutoCompleteTextView txtOrderMainCustomerName;
        private TextView txtOrderMainLearningCount;
        private TextView lblOrderMainSum;
        private TextView odrerMainSumText;
        private TextView txtOrderMainDiscount;
        private TextView lblOrderMainTotal;
        private TextView odrerMainTotalText;
        private OrderBussines order;
        private Guid customerGuid = Guid.Empty;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.OrderMainLayout);
            FindElements();
            SetSupportActionBar(myToolbar);
            BindList();
            SetFonts();

            order = new OrderBussines();
            lblOrderCode.Text = OrderBussines.NextCode();
            lblDateSh.Text = order?.DateSh;
            txtOrderMainLearningCount.Text = "1";
            txtOrderMainDiscount.Text = order?.DetList?.Sum(q => q.Discount).ToString("N0");
            SetLables();

            var names = CustomerBussines.GetAll().Select(q => q.Name).ToList();
            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, names);
            txtOrderMainCustomerName.Adapter = adapter;

            OrderBussines.OnDetailsChanged += OrderBussines_OnDetailsChanged;
            txtOrderMainDiscount.TextChanged += TxtOrderMainDiscount_TextChanged;
        }
        private void TxtOrderMainDiscount_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOrderMainDiscount.Text)) return;
                SetLables();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        private void SetFonts()
        {
            var fontYekan = Typeface.CreateFromAsset(Assets, "B Yekan.TTF");


            lblOrderCode.Typeface = fontYekan;
            orderCodeText.Typeface = fontYekan;
            lblDateSh.Typeface = fontYekan;
            odrerMainDateShText.Typeface = fontYekan;
            txtOrderMainCustomerName.Typeface = fontYekan;
            txtOrderMainLearningCount.Typeface = fontYekan;
            lblOrderMainSum.Typeface = fontYekan;
            odrerMainSumText.Typeface = fontYekan;
            txtOrderMainDiscount.Typeface = fontYekan;
            lblOrderMainTotal.Typeface = fontYekan;
            odrerMainTotalText.Typeface = fontYekan;
        }
        private async Task OrderBussines_OnDetailsChanged(OrderDetailBussines e)
        {
            try
            {
                if (order == null) return;
                if (order.DetList == null) order.DetList = new List<OrderDetailBussines>();
                var list = order.DetList.Select(q => q.PrdGuid).ToList();
                if (list != null && list.Count > 0 && list.Contains(e.PrdGuid))
                {
                    var index = order.DetList.FindIndex(q => q.PrdGuid == e.PrdGuid);
                    order.DetList.RemoveAt(index);
                }
                else order.DetList.Add(e);

                SetLables();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        private void FindElements()
        {
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.mainOrderToolbar);
            lstProducts = FindViewById<ListView>(Resource.Id.OrderDetListView);
            lblOrderCode = FindViewById<TextView>(Resource.Id.lblOrderCode);
            orderCodeText = FindViewById<TextView>(Resource.Id.odrerCodeText);
            lblDateSh = FindViewById<TextView>(Resource.Id.lblOrderMainDateSh);
            odrerMainDateShText = FindViewById<TextView>(Resource.Id.odrerMainDateShText);
            txtOrderMainCustomerName = FindViewById<AutoCompleteTextView>(Resource.Id.txtOrderMainCustomerName);
            txtOrderMainLearningCount = FindViewById<TextView>(Resource.Id.txtOrderMainLearningCount);
            lblOrderMainSum = FindViewById<TextView>(Resource.Id.lblOrderMainSum);
            odrerMainSumText = FindViewById<TextView>(Resource.Id.odrerMainSumText);
            txtOrderMainDiscount = FindViewById<TextView>(Resource.Id.txtOrderMainDiscount);
            lblOrderMainTotal = FindViewById<TextView>(Resource.Id.lblOrderMainTotal);
            odrerMainTotalText = FindViewById<TextView>(Resource.Id.odrerMainTotalText);
        }
        private void BindList()
        {
            try
            {
                list = ProductBussines.GetAll();
                lstProducts.Adapter = new OrderDetAdapter(this, list.OrderByDescending(q => q.Price).ToList());
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        private void SetLables()
        {
            try
            {
                lblOrderMainSum.Text = order?.DetList?.Sum(q => q.Price).ToString("N0");

                lblOrderMainTotal.Text =
                    (lblOrderMainSum.Text.ParseToDecimal() - txtOrderMainDiscount.Text.ParseToDecimal()).ToString("N0") + " ریال";
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.addMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            try
            {
                var res = new ReturnedSaveFuncInfo();
                res.AddReturnedValue(CheckValidation());
                if (res.HasError)
                {
                    Toast.MakeText(this, res.ErrorMessage, ToastLength.Short).Show();
                    return base.OnOptionsItemSelected(item);
                }


                order.ContractCode = lblOrderCode.Text;
                order.CustomerGuid = customerGuid;
                order.Guid = Guid.NewGuid();
                order.Date = DateTime.Now;
                order.Discount = txtOrderMainDiscount.Text.ParseToDecimal();
                order.LearningCount = txtOrderMainLearningCount.Text.ParseToInt();
                order.Modified = DateTime.Now;
                order.Status = true;
                order.Sum = lblOrderMainSum.Text.ParseToDecimal();
                order.Total = (order?.DetList?.Sum(q => q.Total) ?? 0) - order.Discount;
                order.UserGuid = CurrentUser.User.Guid;



                OrderBussines.Save(order);
                Finish();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
            return base.OnOptionsItemSelected(item);
        }
        private ReturnedSaveFuncInfo CheckValidation()
        {
            var res = new ReturnedSaveFuncInfo();
            try
            {
                if (string.IsNullOrEmpty(txtOrderMainCustomerName.Text))
                {
                    res.AddReturnedValue(ReturnedState.Error, "مشتری انتخاب شده معتبر نمی باشد");
                    return res;
                }
                var cust = CustomerBussines.Get(txtOrderMainCustomerName.Text);
                if (cust == null || cust.Guid == Guid.Empty)
                {
                    res.AddReturnedValue(ReturnedState.Error, "مشتری انتخاب شده معتبر نمی باشد");
                    return res;
                }

                customerGuid = cust.Guid;

                if (order?.DetList == null || order.DetList.Count <= 0)
                {
                    res.AddReturnedValue(ReturnedState.Error, "لیست اقلام نمی تواند خالی باشد");
                    return res;
                }

                if (txtOrderMainDiscount.Text.ParseToDecimal() > order?.DetList?.Sum(q => q.Total))
                {
                    res.AddReturnedValue(ReturnedState.Error, "تخفیف از جمع کل فاکتور نمی تواند بیشتر باشد");
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