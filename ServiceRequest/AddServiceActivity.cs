using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    [Activity(Label = "AddServiceActivity")]
    public class AddServiceActivity : Activity
    {
        //TextView dateDisplay;
        Button dateButton;
        Button submitButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddService);
            // Create your application here

            dateButton = FindViewById<Button>(Resource.Id.selectDateButton);

            dateButton.Click += (sender, e) =>
            {
                pickDate(sender, e);
            };

            submitButton = FindViewById<Button>(Resource.Id.submitButton);

            submitButton.Click += (sender, e) =>
            {
                createService();
            };

            //_dateSelectButton = FindViewById<Button>(Resource.Id.dateSelectButton);
            //_dateDisplay.Click += DateSelect_OnClick;
        }

        void pickDate(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime date)
            {
                dateButton.Text = date.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        public void createService()
        {
            Service s = new Service(FindViewById<EditText>(Resource.Id.serviceEditText).Text, 
                                FindViewById<Button>(Resource.Id.selectDateButton).Text,
                                FindViewById<EditText>(Resource.Id.locationEditText).Text,
                                FindViewById<EditText>(Resource.Id.descriptionEditText).Text);

            var mainActivity = new Intent(this, typeof(MainActivity));
            mainActivity.PutExtra("NewService", JsonConvert.SerializeObject(s));
            StartActivity(mainActivity);
        }
    }
}