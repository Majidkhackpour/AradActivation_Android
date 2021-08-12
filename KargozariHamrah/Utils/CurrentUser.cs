using Android.App;
using Android.OS;
using Android.Provider;
using Services;
using System;

namespace KargozariHamrah.Utils
{
    public static class CurrentUser
    {
        private static string _id = "";
        public static string Imei
        {
            get
            {
                try
                {
                    if (!string.IsNullOrEmpty(_id)) return _id;
                    _id = Build.Serial;
                    if (!string.IsNullOrWhiteSpace(_id) && _id != Build.Unknown && _id != "0") return _id;
                    var context = Application.Context;
                    _id = Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AndroidId);
                }
                catch (Exception ex)
                {
                    Android.Util.Log.Warn("DeviceInfo", "Unable to get id: " + ex);
                    WebErrorLog.ErrorInstence.StartErrorLog(ex);
                }

                return _id;
            }
        }
    }
}