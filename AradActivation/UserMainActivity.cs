using System;
using System.Security.Cryptography;
using System.Text;
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
    [Activity(Label = "مدیریت کاربران", Theme = "@style/MyTheme")]
    public class UserMainActivity : AppCompatActivity
    {
        private Android.Support.V7.Widget.Toolbar myToolbar;
        private EditText txtName;
        private EditText txtUserName;
        private EditText txtPass;
        private EditText txtMobile;
        private EditText txtEmail;
        private RadioButton rbtnManager;
        private RadioButton rbtnOperator;
        private RadioButton rbtnVisitor;
        private UserBussines cus;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UserMainLayout);
            myToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.UserMainToolbar);
            SetSupportActionBar(myToolbar);

            var guid = Intent.GetStringExtra("UserGuid");
            var userGuid = Guid.Empty;
            if (!string.IsNullOrEmpty(guid)) 
                userGuid = Guid.Parse(guid);

            cus = UserBussines.Get(userGuid);


            txtName = FindViewById<EditText>(Resource.Id.txtUserMainName);
            txtUserName = FindViewById<EditText>(Resource.Id.txtUserMain_UserName);
            txtPass = FindViewById<EditText>(Resource.Id.txtUserMainPass);
            txtMobile = FindViewById<EditText>(Resource.Id.txtUserMainMobile);
            txtEmail = FindViewById<EditText>(Resource.Id.txtUserMainEmail);
            rbtnManager = FindViewById<RadioButton>(Resource.Id.rbtnManager);
            rbtnOperator = FindViewById<RadioButton>(Resource.Id.rbtnOperator);
            rbtnVisitor = FindViewById<RadioButton>(Resource.Id.rbtnVisitor);

            SetFonts();

            txtName.Text = cus?.Name;
            txtUserName.Text = cus?.UserName;
            txtMobile.Text = cus?.Mobile;
            txtEmail.Text = cus?.Email;
            if (cus?.Type == EnUserType.Manager)
                rbtnManager.Checked = true;
            else if (cus?.Type == EnUserType.Operator)
                rbtnOperator.Checked = true;
            else if (cus?.Type == EnUserType.Visitor)
                rbtnVisitor.Checked = true;
        }
        private void SetFonts()
        {
            var fontYekan = Typeface.CreateFromAsset(Assets, "B Yekan.TTF");

            txtName.Typeface = fontYekan;
            txtUserName.Typeface = fontYekan;
            txtPass.Typeface = fontYekan;
            txtMobile.Typeface = fontYekan;
            txtEmail.Typeface = fontYekan;
            rbtnVisitor.Typeface = fontYekan;
            rbtnManager.Typeface = fontYekan;
            rbtnOperator.Typeface = fontYekan;
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
                cus = new UserBussines()
                {
                    Guid = Guid.NewGuid(),
                    Modified = DateTime.Now,
                    Status = true,
                };
            }

            cus.Name = txtName.Text;
            cus.UserName = txtUserName.Text;
            cus.Mobile = txtMobile.Text;
            cus.Email = txtEmail.Text;
            var ue = new UTF8Encoding();
            var bytes = ue.GetBytes(txtPass.Text.Trim());
            var md5 = new MD5CryptoServiceProvider();
            var hashBytes = md5.ComputeHash(bytes);
            var password = System.Text.RegularExpressions.Regex.Replace(BitConverter.ToString(hashBytes), "-", "")
                .ToLower();
            cus.Password = password;
            if (rbtnManager.Checked)
                cus.Type = EnUserType.Manager;
            else if (rbtnOperator.Checked)
                cus.Type = EnUserType.Operator;
            else if (rbtnVisitor.Checked)
                cus.Type = EnUserType.Visitor;

            UserBussines.Save(cus);
            Finish();
            return base.OnOptionsItemSelected(item);
        }
        private ReturnedSaveFuncInfo CheckValidation()
        {
            var res = new ReturnedSaveFuncInfo();
            try
            {
                txtName.Error = null;
                txtUserName.Error = null;
                txtPass.Error = null;
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    var msg = "لطفا نام و نام خانوادگی مشتری را وارد نمایید";
                    txtName.Error = msg;
                    res.AddReturnedValue(ReturnedState.Error, msg);
                }

                if (res.HasError) return res;

                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    var msg = "لطفا نام کاربری را وارد نمایید";
                    txtUserName.Error = msg;
                    res.AddReturnedValue(ReturnedState.Error, msg);
                }
                if (res.HasError) return res;

                if (string.IsNullOrEmpty(txtPass.Text))
                {
                    var msg = "لطفا رمز را وارد نمایید";
                    txtPass.Error = msg;
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