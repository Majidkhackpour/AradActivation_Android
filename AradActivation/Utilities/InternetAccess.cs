using Android.App;
using Android.Content;
using Android.Net;

namespace AradActivation.Utilities
{
    public class InternetAccess
    {
        public static bool CheckNetworkConnection()
        {
            var connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            return activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting;
        }
    }
}