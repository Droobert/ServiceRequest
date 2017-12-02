using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using DoYourJob;

namespace ServiceRequest
{
    [Activity(Label = "AddStaffActivity")]
    public class AddStaffActivity : Activity
    {
        Button createStaffButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddStaff);

            createStaffButton = FindViewById<Button>(Resource.Id.CreateStaffButton);

            createStaffButton.Click += (sender, e) =>
            {
                createStaff();
            };
        }

        public void createStaff()
        {
            Staff h = new Staff(FindViewById<EditText>(Resource.Id.StaffNameEditText).Text, null);

            var mainActivity = new Intent(this, typeof(MainActivity));
            mainActivity.PutExtra("currentUser", JsonConvert.SerializeObject(h));
            StartActivity(mainActivity);
        }
    }
}