using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.View;
using Android.Widget;
using AradActivation.Utilities;
using DepartmentDal.Classes;
using Services;

namespace AradActivation
{
    [Activity(Label = "نمایش لیست کاربران", Theme = "@style/MyTheme")]
    public class UserBlockActivity : Activity
    {
        private ListView lstCustomers;
        private List<UserBussines> list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShowUsers);
            lstCustomers = FindViewById<ListView>(Resource.Id.UserListView);
            BindList();
            lstCustomers.ItemClick += LstCustomers_ItemClick;
        }
        private void LstCustomers_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e.Position < 0 && list.Count <= 0) return;
            var cus = lstCustomers.GetItemAtPosition(e.Position).Cast<UserBussines>();
            if (!cus.IsBlock)
            {
                AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(
                    new ContextThemeWrapper(this, Android.Resource.Style.WidgetMaterialLightButtonBarAlertDialog));
                alertDialogBuilder.SetTitle("مسدودسازی کاربر");
                alertDialogBuilder.SetMessage($"آیا از مسدود کردن {cus?.Name} اطمینان دارید؟");
                alertDialogBuilder.SetCancelable(false);


                alertDialogBuilder
                    .SetPositiveButton("بله", (sent, args) =>
                    {
                        cus.IsBlock = true;
                        UserBussines.Save(cus);
                        BindList();
                    })
                    .SetNegativeButton("خیر", (sent, args) => { })
                    ;
                alertDialogBuilder.Show();
            }
            else
            {
                AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(
                    new ContextThemeWrapper(this, Android.Resource.Style.WidgetMaterialLightButtonBarAlertDialog));
                alertDialogBuilder.SetTitle("آزادسازی کاربر");
                alertDialogBuilder.SetMessage($"آیا از آزاد کردن {cus?.Name} اطمینان دارید؟");
                alertDialogBuilder.SetCancelable(false);


                alertDialogBuilder
                    .SetPositiveButton("بله", (sent, args) =>
                    {
                        cus.IsBlock = false;
                        UserBussines.Save(cus);
                        BindList();
                    })
                    .SetNegativeButton("خیر", (sent, args) => { })
                    ;
                alertDialogBuilder.Show();
            }
        }
        private void BindList()
        {
            try
            {
                list = UserBussines.GetAll();
                lstCustomers.Adapter = new UserAdapter(this, list.OrderBy(q => q.Name).ToList());
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}