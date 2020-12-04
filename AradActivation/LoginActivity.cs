using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using AradActivation.Utilities;
using DepartmentDal.Classes;
using Services;

namespace AradActivation
{
    [Activity(Label = "Arad", Theme = "@style/MyTheme", MainLauncher = true, Icon = "@drawable/Arad_Icon", NoHistory = true)]
    public class LoginActivity : AppCompatActivity
    {
        private TextView lblLoginArad;
        private TextView lblLogin;
        private AutoCompleteTextView txtUserName;
        private EditText txtPassword;
        private Button btnLogin;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.LoginLayout);
            
            FindElements();
            SetFonts();
            if (InternetAccess.CheckNetworkConnection())
            {
                var names = UserBussines.GetAll().Select(q => q.UserName).ToList();
                ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, names);
                txtUserName.Adapter = adapter;
            }

            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            var res = new ReturnedSaveFuncInfo();
            res.AddReturnedValue(CheckValidation());
            if (res.HasError)
            {
                Toast.MakeText(this, res.ErrorMessage, ToastLength.Short).Show();
                return;
            }

            var user = UserBussines.Get(txtUserName.Text.Trim());
            if (user == null)
            {
                Toast.MakeText(this, $"کاربر با نام کاربری {txtUserName.Text} یافت نشد", ToastLength.Long).Show();
                txtUserName.SelectAll();
                return;
            }

            var ue = new UTF8Encoding();
            var bytes = ue.GetBytes(txtPassword.Text.Trim());
            var md5 = new MD5CryptoServiceProvider();
            var hashBytes = md5.ComputeHash(bytes);
            var password = System.Text.RegularExpressions.Regex.Replace(BitConverter.ToString(hashBytes), "-", "")
                .ToLower();
            if (password != user.Password)
            {
                Toast.MakeText(this, "رمز عبور اشتباه است", ToastLength.Long).Show();
                txtPassword.SelectAll();
                return;
            }

            if (user.IsBlock)
            {
                Toast.MakeText(this, "دسترسی شما به برنامه، از طریق پنل مدیریت محدود شده است", ToastLength.Long).Show();
                txtPassword.SelectAll();
                return;
            }

            CurrentUser.User = user;
            CurrentUser.LastVorrod = DateTime.Now;

            StartActivity(typeof(MainActivity));

        }
        private ReturnedSaveFuncInfo CheckValidation()
        {
            var res = new ReturnedSaveFuncInfo();
            try
            {
                txtUserName.Error = null;
                txtPassword.Error = null;
                if (!InternetAccess.CheckNetworkConnection())
                {
                    res.AddReturnedValue(ReturnedState.Error,
                        "لطفا دسترسی خود به اینترنت را بررسی کرده و مجددا تلاش نمایید");
                    return res;
                }
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    var msg = "لطفا نام کاربری را وارد نمایید";
                    txtUserName.Error = msg;
                    res.AddReturnedValue(ReturnedState.Error, msg);
                }

                if (res.HasError) return res;

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    var msg = "لطفا کلمه عبور را وارد نمایید";
                    txtPassword.Error = msg;
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
        private void FindElements()
        {
            lblLoginArad = FindViewById<TextView>(Resource.Id.lblLoginArad);
            lblLogin = FindViewById<TextView>(Resource.Id.lblLogin);
            txtUserName = FindViewById<AutoCompleteTextView>(Resource.Id.txtUserName);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
        }
        private void SetFonts()
        {
            var fontYekan = Typeface.CreateFromAsset(Assets, "B Yekan.TTF");
            var fontTitr = Typeface.CreateFromAsset(Assets, "B TITR BOLD.TTF");

            lblLogin.Typeface = fontYekan;
            lblLoginArad.Typeface = fontTitr;
            txtUserName.Typeface = fontYekan;
            txtPassword.Typeface = fontYekan;
            btnLogin.Typeface = fontYekan;
        }
    }
}