using Android.App;
using Android.OS;

namespace AradActivation
{
    [Activity(Label = "پرداخت وجه", Theme = "@style/MyTheme")]
    public class PardakhtMainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PardakhtMainLayout);
        }
    }
}