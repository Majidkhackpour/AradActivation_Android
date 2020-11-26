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
    [Activity(Label = "مدیریت مشتریان", Theme = "@style/MyTheme")]
    public class CustomerMainActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private EditText txtName;
        private EditText txtCompany;
        private EditText txtNatCode;
        private EditText txtZip;
        private EditText txtTell1;
        private EditText txtTell2;
        private EditText txtEmail;
        private EditText txtAddress;
        private CustomerBussines cus;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CustomerMainLayout);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.mainCustToolbar);
            SetSupportActionBar(myToolbar);

            var guid = Intent.GetStringExtra("CusGuid");
            var cusGuid = Guid.Empty;
            if (!string.IsNullOrEmpty(guid)) cusGuid = Guid.Parse(guid);

            cus = CustomerBussines.Get(cusGuid);


            txtName = FindViewById<EditText>(Resource.Id.txtName);
            txtCompany = FindViewById<EditText>(Resource.Id.txtCompany);
            txtNatCode = FindViewById<EditText>(Resource.Id.txtNatCode);
            txtZip = FindViewById<EditText>(Resource.Id.txtZipCode);
            txtTell1 = FindViewById<EditText>(Resource.Id.txtTell1);
            txtTell2 = FindViewById<EditText>(Resource.Id.txtTell2);
            txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            txtAddress = FindViewById<EditText>(Resource.Id.txtAddress);


            txtName.Text = cus?.Name;
            txtCompany.Text = cus?.CompanyName;
            txtNatCode.Text = cus?.NationalCode;
            txtZip.Text = cus?.PostalCode;
            txtTell1.Text = cus?.Tell1;
            txtTell2.Text = cus?.Tell2;
            txtEmail.Text = cus?.Email;
            txtAddress.Text = cus?.Address;
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

            if (cus == null)
            {
                cus = new CustomerBussines()
                {
                    Guid = Guid.NewGuid(),
                    Account = 0,
                    HardSerial = "",
                    AppSerial = "",
                    Description = "",
                    ExpireDate = DateTime.Now.AddYears(1),
                    Modified = DateTime.Now,
                    Password = "",
                    SiteUrl = "",
                    Status = true,
                    Tell3 = "",
                    Tell4 = "",
                    UserGuid = Guid.Empty,
                    CreateDate = DateTime.Now,
                    UserName = ""
                };
            }

            cus.Tell1 = txtTell1.Text;
            cus.Tell2 = txtTell2.Text;
            cus.Name = txtName.Text;
            cus.CompanyName = txtCompany.Text;
            cus.Address = txtAddress.Text;
            cus.Email = txtEmail.Text;
            cus.NationalCode = txtNatCode.Text;
            cus.PostalCode = txtZip.Text;
            CustomerBussines.Save(cus);
            Finish();
            return base.OnOptionsItemSelected(item);
        }

        private ReturnedSaveFuncInfo CheckValidation()
        {
            var res = new ReturnedSaveFuncInfo();
            try
            {
                txtName.Error = null;
                txtTell1.Error = null;
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    var msg = "لطفا نام و نام خانوادگی مشتری را وارد نمایید";
                    txtName.Error = msg;
                    res.AddReturnedValue(ReturnedState.Error, msg);
                }

                if (res.HasError) return res;

                if (string.IsNullOrEmpty(txtTell1.Text))
                {
                    var msg = "لطفا شماره مشتری را وارد نمایید";
                    txtTell1.Error = msg;
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