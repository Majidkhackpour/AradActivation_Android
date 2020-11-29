using Android.App;
using Android.OS;

namespace AradActivation
{
    [Activity(Label = "مدیریت قراردادها", Theme = "@style/MyTheme")]
    public class OrderMainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.OrderMainLayout);
        }
    }
}