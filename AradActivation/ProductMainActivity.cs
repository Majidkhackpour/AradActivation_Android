using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using DepartmentDal.Classes;
using Services;

namespace AradActivation
{
    [Activity(Label = "مدیریت محصولات", Theme = "@style/MyTheme")]
    public class ProductMainActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private EditText txtName;
        private EditText txtCode;
        private EditText txtPrice;
        private EditText txtBackUp;
        private ProductBussines prd;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ProductMainLayout);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.mainPrdToolbar);
            SetSupportActionBar(myToolbar);

            var guid = Intent.GetStringExtra("PrdGuid");
            var prdGuid = Guid.Empty;
            if (!string.IsNullOrEmpty(guid)) prdGuid = Guid.Parse(guid);

            prd = ProductBussines.Get(prdGuid);


            txtName = FindViewById<EditText>(Resource.Id.txtPrdName);
            txtCode = FindViewById<EditText>(Resource.Id.txtPrdCode);
            txtPrice = FindViewById<EditText>(Resource.Id.txtPrdPrice);
            txtBackUp = FindViewById<EditText>(Resource.Id.txtPrdBackUp);


            txtName.Text = prd?.Name;
            txtCode.Text = prd?.Code;
            txtPrice.Text = prd?.Price.ToString("N0");
            txtBackUp.Text = prd?.BckUpPrice.ToString("N0");

            if (prd == null || prd.Guid == Guid.Empty) txtCode.Text = ProductBussines.NextCode();
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

            if (prd == null)
            {
                prd = new ProductBussines()
                {
                    Guid = Guid.NewGuid(),
                    Modified = DateTime.Now,
                    Status = true
                };
            }

            prd.Name = txtName.Text;
            prd.Code = txtCode.Text;
            prd.Price = txtPrice.Text.ParseToDecimal();
            prd.BckUpPrice = txtBackUp.Text.ParseToDecimal();
            ProductBussines.Save(prd);
            Finish();
            return base.OnOptionsItemSelected(item);
        }

        private ReturnedSaveFuncInfo CheckValidation()
        {
            var res = new ReturnedSaveFuncInfo();
            try
            {
                txtName.Error = null;
                txtPrice.Error = null;
                txtCode.Error = null;
                txtBackUp.Error = null;
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    var msg = "لطفا عنوان محصول را وارد نمایید";
                    txtName.Error = msg;
                    res.AddReturnedValue(ReturnedState.Error, msg);
                }

                if (res.HasError) return res;

                if (string.IsNullOrEmpty(txtCode.Text))
                {
                    var msg = "لطفا کد محصول را وارد نمایید";
                    txtCode.Error = msg;
                    res.AddReturnedValue(ReturnedState.Error, msg);
                }
                if (res.HasError) return res;
                if (string.IsNullOrEmpty(txtPrice.Text))
                {
                    var msg = "لطفا قیمت محصول را وارد نمایید";
                    txtPrice.Error = msg;
                    res.AddReturnedValue(ReturnedState.Error, msg);
                }

                if (res.HasError) return res;

                if (string.IsNullOrEmpty(txtBackUp.Text))
                {
                    var msg = "لطفا هزینه پشتیبانی محصول را وارد نمایید";
                    txtBackUp.Error = msg;
                    res.AddReturnedValue(ReturnedState.Error, msg);
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