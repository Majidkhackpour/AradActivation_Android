using Android.App;
using Android.OS;

namespace AradActivation
{
    [Activity(Label = "دریافت وجه", Theme = "@style/MyTheme")]
    public class ReceptionMainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ReceptionMainLayout);
        }
    }
}